using OOPsReview;

namespace BlazorWebApp.Components.Pages.InClass
{
    public partial class EmploymentReport
    {
        string FeedbackMessage = String.Empty;
        List<string> ErrorMessages = new List<string>();
        List<Employment> Employments = null;

        protected override void OnInitialized()
        {
            ReadAndParseCSV();

            base.OnInitialized();
        }

        private void ReadAndParseCSV()
        {
            FeedbackMessage = String.Empty;
            ErrorMessages = new List<string>();

            //Relative path
            string FolderName = @"./Data/";
            List<string> FileNames = new List<string>() 
            { "Employment.csv", "BadEmployment.csv", "EmptyEmployment.csv" };

            string FilePath = FolderName + FileNames[0];
            string BadFilePath = FolderName + FileNames[1];
            string EmptyFilePath = FolderName + FileNames[2];

            List<string> Lines = new List<string>();

            try
            {
                if (System.IO.File.Exists(FilePath))
                {
                    Lines = System.IO.File.ReadAllLines(FilePath).ToList();

                    Employments = new List<Employment>();

                    foreach (string line in Lines)
                    {
                        Employments.Add(Employment.Parse(line));
                    }
                }
                else 
                {
                    throw new Exception($"File: {FilePath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages.Add($"System error: {GetInnerException(ex).Message}");
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
