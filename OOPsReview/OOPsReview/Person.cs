using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Person
    {
        #region Attributes
        private string _FirstName;
        private string _LastName;
        #endregion

        #region Properties
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) 
                {
                    throw new ArgumentException($"{value} is invalid. First Name is Required; can not be null, empty, or whitespace.");
                }
                _FirstName = value.Trim();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"{value} is invalid. Last Name is Required; can not be null, empty, or whitespace.");
                }
                _LastName = value.Trim();
            }
        }
        public ResidentAddress ?Address { get; set; }
        public List<Employment> EmploymentPositions { get; set; }
        public string FullName { get { return $"{LastName}, {FirstName}"; } }

        #endregion

        #region Constructors
        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";

            EmploymentPositions = new List<Employment>();
        }

        public Person(string firstName, string lastName, ResidentAddress address, List<Employment> employments)
        {
            FirstName = firstName;
            LastName = lastName;

            if (employments != null)
            {
                EmploymentPositions = employments;
            }
            else
            {
                EmploymentPositions = new List<Employment>();
            }

            Address = address;
        }

        #endregion

        #region Methods

        public void ChangeFullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void AddEmployment(Employment position)
        {
            if(position == null)
            {
                throw new ArgumentNullException("Employment position can not be empty it is required.");
            }
            EmploymentPositions.Add(position);
        }

        #endregion
    }
}
