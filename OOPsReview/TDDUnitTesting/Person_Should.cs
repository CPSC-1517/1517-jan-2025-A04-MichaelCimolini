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

        [Fact]
        public void CreateAnInstanceWithGreedyConstructor()
        {
            string expectedFirstName = "Josh";
            string expectedLastName = "Sellars";
            int expectedEmploymentPositionsCount = 0;

            Person sut = new Person("Josh", "Sellars", null, null);

            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionsCount);
        }
        #endregion

        #region Theories
        [Theory]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowExceptionCreatingAnInstanceWithBadFirstName(string ?testValue)
        {
            //No setup needed.

            //Execution
            Action action = () => new Person(testValue, "Last", null, null);

            //Assertion
            action.Should().Throw<ArgumentException>();
        }

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
