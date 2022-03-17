using AutoFixture;
using FluentAssertions;
using Moq;
using NSubstitute;
using NUnit.Framework;
using Sample.Domain.Contacts;
using Sample.Domain.SeedWork;
#nullable disable

namespace Sample.Domain.NUnit.Moq.UnitTests
{
    [TestFixture]
    public class ContactServiceTest
    {
        private IFixture _fixture;
        private Mock<IContactRepository> _mockRepository;
        private readonly CancellationToken ct = default;


        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _mockRepository = new Mock<IContactRepository>();

            // Setup that call to GetAsync should return many objects
            _mockRepository.Setup(call => call.GetAsync(Arg.Any<CancellationToken>()))
                .ReturnsAsync(_fixture.CreateMany<Contact>(100));

            _mockRepository.Setup(call => call.GetAsync(Arg.Any<CancellationToken>()))
                .ReturnsAsync(_fixture.CreateMany<Contact>(100));
        }

        [Test]
        public async Task When_CreateAsync_Is_Called_Successfully_Then_UnitOfWork_Should_Be_Commited()
        {
            // Arrange
            var unitOfWork = Mock.Of<IUnitOfWork>();
            _mockRepository.Setup(x => x.UnitOfWork).Returns(unitOfWork);

            var sut = new ContactService(_mockRepository.Object);

            // Act
            var contact = await sut.CreateAsync("Derp", "derp@test.com", "07012341234", ct);

            // Assert
            _mockRepository.Verify(x => x.Add(It.IsAny<Contact>()));
        }

        [Test]
        public async Task When_GetById_Is_Called_With_Invalid_Id_Then_Exception_Should_Be_Raised()
        {
            // Arrange
            var sut = new ContactService(_mockRepository.Object);

            // Act
            var act = () => sut.GetByIdAsync(Guid.Empty, ct);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public async Task When_GetById_Is_Called_Entity_With_Expected_Id_Should_Be_Returned()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _mockRepository.Setup(call => call.GetByIdAsync(It.Is<Guid>(x => x == id), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(_fixture.Build<Contact>().With(prop => prop.Id, id).Create());

            var sut = new ContactService(_mockRepository.Object);

            // Act
            var result = await sut.GetByIdAsync(id, ct);

            // Assert
            result.Id.Should().Be(id);
        }
    }
}
