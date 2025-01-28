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
        #endregion Facts

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

        [Theory]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowExceptionCreatingAnInstanceWithBadLastName(string? testValue)
        {
            //No setup needed.

            //Execution
            Action action = () => new Person("First", testValue, null, null);

            //Assertion
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("   ", "Last")]
        [InlineData(null, "Last")]
        [InlineData("", "Last")]
        [InlineData("First", "   ")]
        [InlineData("First", null)]
        [InlineData("First", "")]
        public void ThrowExceptionCreatingAnInstanceWithBadName(string? first, string? last)
        {
            //No setup needed.

            //Execution
            Action action = () => new Person(first, last, null, null);

            //Assertion
            action.Should().Throw<ArgumentException>();
        }
        #endregion Theories
        #endregion Constructors

        #region Methods
        #region Facts
        #endregion
        #region Theories
        #endregion
        #endregion

        #region Properties
        #region Facts
        [Fact]
        public void DirectlyChangeFirstNameViaProperty()
        {
            string expectedFirstName = "Bob";

            Person sut = new Person("Dave", "Bowie", null, null);

            sut.FirstName = "Bob";

            sut.FirstName.Should().Be(expectedFirstName);
        }

        [Fact]
        public void DirectlyChangeLastNameViaProperty()
        {
            string expectedLastName = "Smith";

            Person sut = new Person("Dave", "Bowie", null, null);

            sut.LastName = "Smith";

            sut.LastName.Should().Be(expectedLastName);
        }

        [Fact]
        public void DirectlyChangeAddressViaProperty()
        {
            ResidentAddress expectedAddress = new ResidentAddress(321, "Ash Lane", "Edmonton", "AB", "E4R5T6");
            Person sut = new Person("Don", "Welch",
                new ResidentAddress(123, "Maple St", "Edmonton", "AB", "Y7U8I9"), null);

            sut.Address = new ResidentAddress(321, "Ash Lane", "Edmonton", "AB", "E4R5T6");

            sut.Address.Should().Be(expectedAddress);
        }

        [Fact]
        public void DirectlyChangeAddressToEmptyValueViaProperty()
        {
            Person sut = new Person("Don", "Welch",
                new ResidentAddress(123, "Maple St", "Edmonton", "AB", "Y7U8I9"), null);

            sut.Address = null;

            sut.Address.Should().BeNull();
        }
        #endregion

        #region Theories

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void ThrowExceptionWhenDirectlyChangingirstNameWithBadData(string testValue)
        {
            Person sut = new Person("Lowan", "Behold", null, null);

            Action action = () => sut.FirstName = testValue;

            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void ThrowExceptionWhenDirectlyChangingLastNameWithBadData(string testValue)
        {
            Person sut = new Person("Lowan", "Behold", null, null);

            Action action = () => sut.LastName = testValue;

            action.Should().Throw<ArgumentException>();
        }
        #endregion
        #endregion
    }
}
