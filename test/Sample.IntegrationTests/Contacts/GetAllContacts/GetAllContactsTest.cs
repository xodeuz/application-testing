using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Sample.Domain.Contacts;
using Sample.IntegrationTests.Configuration;
using Sample.IntegrationTests.Contacts.Seeds;
using Sample.IntegrationTests.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Sample.IntegrationTests.Contacts
{
    public class GetAllContactsTest : IDisposable
    {
        private readonly WebApplicationFactory<Program> testHost;

        public GetAllContactsTest()
        {
            testHost = new WebApiApplicationFactory<ContactDatabaseSeed>();
        }

        public void Dispose()
        {
            testHost.Dispose();
        }

        [Fact]
        public async Task When_Calling_GetContacts_Then_Response_Should_Not_Be_Empty()
        {
            // Arrange
            using var client = testHost.CreateClient();

            // Act
            var (Contract, StatusCode) = await client.GetContractAsync<IEnumerable<ContactDto>>("contact");

            // Assert
            StatusCode.Should().Be(HttpStatusCode.OK);
            Contract.Should().NotBeEmpty();
        }

    }
}
