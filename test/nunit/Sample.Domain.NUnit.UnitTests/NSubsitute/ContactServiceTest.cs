using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Sample.Domain.Contacts;
#nullable disable

namespace Sample.Domain.NUnit.NSubstitute.UnitTests
{
    [TestFixture]
    public class ContactServiceTest
    {
        private IFixture _fixture;
        private IContactRepository _repository;
        private readonly CancellationToken ct = default;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoNSubstituteCustomization());
            _repository = Substitute.For<IContactRepository>();

            _repository.GetAsync(Arg.Any<CancellationToken>())
                .Returns(_fixture.Build<Contact>().CreateMany(100));

            _repository.GetAsync(Arg.Any<CancellationToken>())
                .Returns(_fixture.Build<Contact>().CreateMany(100));
        }

        [Test]
        public async Task When_CreateAsync_Is_Called_Successfully_Then_UnitOfWork_Should_Be_Commited()
        {
            // Arrange
            var sut = new ContactService(_repository);

            // Act
            _ = await sut.CreateAsync("Derp", "derp@test.com", "07012341234", ct);

            // Assert
            await _repository.Received(1).UnitOfWork.SaveChangesAsync();
        }

        [Test]
        public async Task When_GetById_Is_Called_With_Invalid_Id_Then_Exception_Should_Be_Raised()
        {
            // Arrange
            var sut = new ContactService(_repository);

            // Act
            var act = () => sut.GetByIdAsync(Guid.Empty, ct);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public async Task When_GetById_Is_Called_Entity_With_Expected_Id_Should_Be_Returned()
        {
            // Arrange
            Guid id = _fixture.Create<Guid>();
            var expectedEntity = _fixture.Build<Contact>().With(prop => prop.Id, id).Create();
            _repository.GetByIdAsync(Arg.Is(id), Arg.Any<CancellationToken>()).Returns(expectedEntity);

            var sut = new ContactService(_repository);

            // Act
            var result = await sut.GetByIdAsync(id, ct);

            // Assert
            result.Id.Should().Be(id);
        }
    }
}
