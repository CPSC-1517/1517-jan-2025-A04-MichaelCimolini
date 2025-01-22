using FluentAssertions;
using OOPsReview;

namespace TDDUnitTesting
{
    public class Person_Should
    {
        #region Constructors
        #region Facts
        [Fact]
        public void CreateAnInstanceWithDefaultConstructor()
        {
            //Setup
            String expectedFirstName = "Unknown";
            String expectedLastName = "Unknown";
            int expectedEmploymentPositionsCount = 0;

            //Execution
            Person sut = new Person();

            //Assertion
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionsCount);
        }
        #endregion

        #region Theories
        #endregion
        #endregion

        #region Methods
        #region Facts
        #endregion
        #region Theories
        #endregion
        #endregion

        #region Parameters
        #region Facts
        #endregion
        #region Theories
        #endregion
        #endregion
    }
}
