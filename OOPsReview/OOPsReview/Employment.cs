namespace OOPsReview
{
    public class Employment
    {
        #region Attributes

        private String _Title;

        //Years of Employment
        private double _Years;

        private SupervisoryLevel _Level;

        #endregion

        #region Properties

        public String Title
        {
            get { return _Title; }

            set
            {
                //IsNullOrEmpty misses "  " whitespace. Need to validate better.
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title is Required.");
                }

                _Title = value.Trim();
            }
        }

        public double Years
        {
            get { return _Years; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Years value {value} is invalid. Must be non-negative.");
                }
                _Years = value;
            }
        }

        /// <summary>
        /// SupervisoryLevel is Read-Only.
        /// </summary>
        public SupervisoryLevel Level
        {
            get { return _Level; }
            private set
            {
                if (!Enum.IsDefined(typeof(SupervisoryLevel), value))
                {
                    throw new ArgumentException($"Supervisory level is invalid: {value}");
                }
                _Level = value;
            }
        }

        public DateTime StartDate { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// The basic constructor for Employment.
        /// Sets SupervisoryLevel to Entry.
        /// Sets Title to Unknown
        /// </summary>
        public Employment()
        {
            Title = "Unknown";
            Level = SupervisoryLevel.Entry;
            StartDate = DateTime.Today;
        }

        public Employment(String title, SupervisoryLevel level, DateTime startDate, double years = 0.0)
        {
            Title = title;
            Level = level;

            if (startDate >= DateTime.Today.AddDays(1))
            {
                throw new ArgumentException($"The start date {startDate} is in the future.");
            }
            StartDate = startDate;

            if (years > 0.0)
            {
                Years = years;
            }
            else
            {
                TimeSpan span = DateTime.Now - StartDate;
                Years = Math.Round((span.Days / 365.25), 1); //365.25 is the number of days in a year accounting for leap years.
            }
        }
        #endregion

        #region Methods
        //access returnDataType methodName (parameterList - Optional)

        /// <summary>
        /// Sets the responsibility level of this instance.
        /// </summary>
        /// <param name="level">SupervisoryLevel (enum) to set.</param>
        public void SetEmploymentResponsibilityLevel(SupervisoryLevel level)
        {
            Level = level;
        }

        /// <summary>
        /// Returns a CSV (comma separated value) string.
        /// Format is Title,Level,StartDate(MMM,dd,yyyy),Years.
        /// </summary>
        /// <returns>The CSV representation of this instance.</returns>
        public override string ToString()
        {
            return $"{Title},{Level},{StartDate.ToString("MMM,dd,yyyy")},{Years}";
        }

        /// <summary>
        /// Validates a supplied start date and updates the Years if it is valid.
        /// </summary>
        /// <param name="startDate">The start date to set. Can not be in the future.</param>
        /// <exception cref="ArgumentException">Thrown if startDate is in the future.</exception>
        public void CorrectStartDate(DateTime startDate)
        {
            if (startDate >= DateTime.Today.AddDays(1))
            {
                throw new ArgumentException($"The start date {startDate} is in the future.");
            }

            StartDate = startDate;
            
            TimeSpan span = DateTime.Now - StartDate;
            Years = Math.Round((span.Days / 365.25), 1); //365.25 is the number of days in a year accounting for leap years.
        }
        #endregion
    }
}
