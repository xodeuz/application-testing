using AutoFixture;
using FluentAssertions;
using Sample.Domain.Contacts;
using System;
using Xunit;

namespace Sample.Domain.XUnit.UnitTests
{
    public class ContactTest
    {
        [Theory]
        [InlineData("@test.com")]
        [InlineData("test")]
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

        [Theory]
        [InlineData("@test.com")]
        [InlineData("test")]
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
