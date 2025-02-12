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
                FeedbackMsg = $"Success: {EmploymentTitle}, {EmploymentYears}, {EmploymentStart}, {EmploymentLevel}";
            }
        }

        private void Clear()
        {
            FeedbackMsg = String.Empty;
            ErrorMessages.Clear();

            EmploymentTitle = String.Empty;
            EmploymentYears = 0.0;
            EmploymentStart = DateTime.Today;
            EmploymentLevel = SupervisoryLevel.Entry;
        }
    }
}
