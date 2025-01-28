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
        
        [Fact]
        public void ChangeFullName()
        {
            string expectedFirstName = "Remi";
            string expectedLastName = "The Rat";
            string expectedFullName = "The Rat, Remi";

            Person sut = new Person("Mickey", "Mouse", null, null);

            sut.ChangeFullName("Remi", "The Rat");

            sut.FullName.Should().Be(expectedFullName);
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
        }

        [Fact]
        public void AddNewEmploymentItemToCollection()
        {
            //Setup
            ResidentAddress address = new ResidentAddress(123, "Maple St.",
                                "Edmonton", "AB", "T6Y7U8");
            Employment one = new Employment("PG I", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2013/10/04"), 6.5);

            TimeSpan days = DateTime.Today - DateTime.Parse("2020/04/04");
            Employment two = new Employment("PG II", SupervisoryLevel.TeamMember,
                            DateTime.Parse("2020/04/04"), Math.Round(days.Days / 365.2, 1));

            List<Employment> employments = new();

            employments.Add(one);
            employments.Add(two);

            Person sut = new Person("Lowan", "Behold", address, employments);

            //build expected new employment
            Employment three = new Employment("SUP I", SupervisoryLevel.Supervisor,
                          DateTime.Today, 0);

            List<Employment> expectedEmployments = new List<Employment>();
            expectedEmployments.Add(one);
            expectedEmployments.Add(two);
            expectedEmployments.Add(three);

            int expectedEmploymentPositionsCount = 3;

            //execution
            sut.AddEmployment(three);

            //Assertion
            sut.EmploymentPositions.Count.Should().Be(expectedEmploymentPositionsCount);

            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(expectedEmployments);
        }

        [Fact]
        public void ThrowExceptionWhenAddingEmploymentWithNoParameter()
        {
            Person sut = new Person("Jean-Luc", "Picard", null, null);

            Action action = () => sut.AddEmployment(null);

            action.Should().Throw<ArgumentNullException>().WithMessage("*is required*");
        }
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

        [Fact]
        public void ReturnFullNameViaProperty()
        {
            string expectedFullName = "Last, First";

            Person sut = new Person("First", "Last", null, null);

            sut.FullName.Should().Be(expectedFullName);
        }
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
