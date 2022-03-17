using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Sample.Domain.Contacts;

namespace Sample.Domain.NUnit.NSubstitute.UnitTests
{
    [TestFixture]
    public class ContactTest
    {
        private IFixture _fixture;

        /// <summary>
        ///     Runs only once per fixture
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }


        [TestCase("@test.com")]
        [TestCase("test")]
        public void When_Creating_With_Incorrect_EmailAddress_Then_InvalidOperationException_Should_Be_Raised(
            string emailAddress)
        {
            // Arrange
            string name = _fixture.Create<string>();
            string phonenumber = "0734100100";

            // Act
            Action act = () => Contact.New(name, emailAddress, phonenumber);

            // Act & Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [TestCase("@test.com")]
        [TestCase("test")]
        public void When_Creating_With_Incorrect_PhoneNumber_Then_InvalidOperationException_Should_Be_Raised(
            string phoneNumber)
        {
            // Arrange
            string name = _fixture.Create<string>();
            string email = "test@test.com";

            // Act
            Action act = () => Contact.New(name, email, phoneNumber);

            // Act & Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
