using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Sample.Domain.Contacts;

namespace Sample.Domain.NUnit.Moq.UnitTests
{
    public class ContactTest
    {
        [TestCase("@test.com")]
        [TestCase("test")]
        public void When_Creating_With_Incorrect_EmailAddress_Then_InvalidOperationException_Should_Be_Raised(
            string emailAddress)
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            Action act = () => Contact.New("Name", emailAddress, "0734100100");

            // Act & Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [TestCase("@test.com")]
        [TestCase("test")]
        public void When_Creating_With_Incorrect_PhoneNumber_Then_InvalidOperationException_Should_Be_Raised(
            string phoneNumber)
        {
            // Arrange
            var fixture = new Fixture();

            // Act
            Action act = () => Contact.New("Name", "test@test.com", phoneNumber);

            // Act & Assert
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
