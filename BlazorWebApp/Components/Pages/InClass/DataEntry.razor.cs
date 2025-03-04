using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OOPsReview;

namespace BlazorWebApp.Components.Pages.InClass
{
    public partial class DataEntry
    {
        private string FeedbackMsg = String.Empty; //same as "";
        private Dictionary<string, string> ErrorMessages = new(); //Shorthand for new Dictionary<string, string>();

        private string EmploymentTitle = String.Empty;
        private double EmploymentYears = 0.0;
        private DateTime EmploymentStart = DateTime.Today;
        private SupervisoryLevel EmploymentLevel = SupervisoryLevel.Entry;

        private Employment EmploymentData;

        //Dependenct Injection

        //Gives us access to the Web host and lets us determine where our application is.
        [Inject]
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        //Allows us to call JS functions
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        //Allows for various URI and Navigation related functions.
        [Inject]
        private NavigationManager NavManager {  get; set; }

        protected override void OnInitialized()
        {

            base.OnInitialized();
        }

        private void Submit()
        {
            FeedbackMsg = String.Empty;
            ErrorMessages.Clear();

            if (String.IsNullOrEmpty(EmploymentTitle))
            {
                ErrorMessages.Add("Title", "Title can not be empty.");
            }

            if (EmploymentYears < 0)
            {
                ErrorMessages.Add("Year", "Years must be positive or 0.");
            }

            if (EmploymentStart >= DateTime.Today.AddDays(1))
            {
                ErrorMessages.Add("Start", "Start date can not be in the future.");
            }

            if (ErrorMessages.Count == 0)
            {
                //FeedbackMsg = $"Success: {EmploymentTitle}, {EmploymentYears}, {EmploymentStart}, {EmploymentLevel}";

                try
                {
                    EmploymentData = new Employment(EmploymentTitle, EmploymentLevel, EmploymentStart, EmploymentYears);

                    //To do File IO we need a path to our file.
                    //But where is our application?

                    //This gets us the Absolute path to our wwwroot folder
                    string AppPathName = WebHostEnvironment.ContentRootPath;

                    //Absolute path to our CSV file
                    //@ allows us to treat / and similar characters as is without having to terminate them
                    string CSVPath = $@"{AppPathName}/Data/Employment.csv";

                    //Store our content
                    string line = $"{EmploymentData.ToString()}\n";

                    //Writes our line to a file
                    System.IO.File.AppendAllText(CSVPath, line);
                }
                catch (ArgumentNullException ex)
                {
                    ErrorMessages.Add($"Missing Data", GetInnerException(ex).Message);
                }
                catch (ArgumentException ex)
                {
                    ErrorMessages.Add($"Bad Data", GetInnerException(ex).Message);
                }
                catch (Exception ex)
                {
                    ErrorMessages.Add($"System Error", GetInnerException(ex).Message);
                }

                FeedbackMsg = "Success!";
            }
        }

        private async void Clear()
        {
            FeedbackMsg = String.Empty;

            object[] message = new object[]
            {
                "Clearing will lose all unsaved data. Are you sure you wish to continue?"
            };

            if (await JSRuntime.InvokeAsync<bool>("confirm", message))
            {
                ErrorMessages.Clear();

                EmploymentTitle = String.Empty;
                EmploymentYears = 0.0;
                EmploymentStart = DateTime.Today;
                EmploymentLevel = SupervisoryLevel.Entry;
            }
        }

        private async void GoToReport()
        {
            string message = "Leaving the page will lose any unsaved data. Are you sure you wish to continue?";

            if (await JSRuntime.InvokeAsync<bool>("confirm", message))
            {
                NavManager.NavigateTo("report");
            }
        }

        /// <summary>
        /// Returns the deepest nested exception.
        /// </summary>
        /// <param name="ex">The exception to check.</param>
        /// <returns></returns>
        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex;
        }
    }
}
