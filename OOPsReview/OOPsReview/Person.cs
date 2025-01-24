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
        private String _FirstName;
        private String _LastName;
        #endregion

        #region Properties
        public String FirstName
        {
            get { return _FirstName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) 
                {
                    throw new ArgumentException($"{value} is invalid. First Name is Required; can not be null, empty, or whitespace.");
                }
                _FirstName = value;
            }
        }
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public ResidentAddress Address { get; set; }
        public List<Employment> EmploymentPositions { get; set; }

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

        #endregion
    }
}
