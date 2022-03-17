using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using NSubstitute;
using Sample.Domain.Contacts;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sample.Domain.XUnit.UnitTests
{
    public class ContactServiceTest
    {
        private IFixture _fixture;
        private IContactRepository _repository;

        /// <summary>
        ///     Constructor to setup some dependencies
        /// </summary>
        public ContactServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());
            _repository = _fixture.Freeze<IContactRepository>();

            _repository.GetAsync(Arg.Any<CancellationToken>())
                .Returns(_fixture.Build<Contact>().CreateMany(100));

            _repository.GetAsync(Arg.Any<CancellationToken>())
                .Returns(_fixture.Build<Contact>().CreateMany(100));
        }

        [Fact]
        public async Task When_CreateAsync_Is_Called_Successfully_Then_UnitOfWork_Should_Be_Commited()
        {
            // Arrange
            var sut = _fixture.Create<ContactService>();
            var ct = new CancellationToken(false);

            // Act
            var contact = await sut.CreateAsync("Derp", "derp@test.com", "07012341234", ct);

            // Assert
            await _repository.Received(1).UnitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task When_GetById_Is_Called_With_Invalid_Id_Then_Exception_Should_Be_Raised()
        {
            // Arrange
            var sut = _fixture.Create<ContactService>();
            var ct = new CancellationToken(false);

            // Act
            var act = () => sut.GetByIdAsync(Guid.Empty, ct);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task When_GetById_Is_Called_Entity_With_Expected_Id_Should_Be_Returned()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _repository.GetByIdAsync(Arg.Is(id), Arg.Any<CancellationToken>())
                      .Returns(_fixture.Build<Contact>().With(prop => prop.Id, id).Create());

            var sut = _fixture.Create<ContactService>();
            var ct = new CancellationToken(false);

            // Act
            var result = await sut.GetByIdAsync(id, ct);

            // Assert
            result.Id.Should().Be(id);
        }
    }
}
