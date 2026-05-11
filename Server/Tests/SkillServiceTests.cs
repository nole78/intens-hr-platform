using Server.Services.SkillService;
using Server.Domain.Models;
using Server.Domain.DTOs;
using Server.Persistance.Repositories.Skills;
using Moq;

namespace Tests
{
    [TestFixture]
    public class SkillServiceTests
    {
        private Mock<ISkillRepository> _skillRepositoryMock;
        private SkillService _skillService;

        public SkillServiceTests()
        {
            _skillRepositoryMock = new Mock<ISkillRepository>();
            _skillService = new SkillService(_skillRepositoryMock.Object);
        }

        [SetUp]
        public void SetUp()
        {
            _skillRepositoryMock = new Mock<ISkillRepository>();
            _skillService = new SkillService(_skillRepositoryMock.Object);
        }

        [Test]
        [TestCase("C# programming")]
        [TestCase("Java programming")]
        [TestCase("English")]
        public void AddSkillAsync_ShouldAddSkill_WhenSkillDoesNotExist(string skillName)
        {
            // Arrange
            var expectedSkill = new Skill { Id = 1, Name = skillName };

            _skillRepositoryMock.Setup(repo => repo.GetByNameAsync(skillName))
                .ReturnsAsync((Skill?)null);

            _skillRepositoryMock.Setup(repo => repo.AddSkillAsync(It.IsAny<Skill>()))
                .ReturnsAsync(expectedSkill);

            // Act
            var result = _skillService.AddSkillAsync(new CreateSkillDto { Name = skillName }).Result;

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value?.Id, Is.EqualTo(expectedSkill.Id));
            Assert.That(result.Value?.Name, Is.EqualTo(expectedSkill.Name));
        }

        [Test]
        [TestCase("C# programming")]
        [TestCase("Java programming")]
        [TestCase("English")]
        public void AddSkillAsync_ShouldReturnError_WhenSkillAlreadyExists(string skillName)
        {
            // Arrange
            _skillRepositoryMock.Setup(repo => repo.GetByNameAsync(skillName))
                .ReturnsAsync(new Skill { Id = 1, Name = skillName });
            // Act
            var result = _skillService.AddSkillAsync(new CreateSkillDto { Name = skillName }).Result;

            // Assert
            Assert.That(result.IsSuccess, Is.Not.True);
            Assert.That(result.Value, Is.Null);
            Assert.That(result.Error, Is.EqualTo("Skill with the same name already exists"));
        }


        [TearDown]
        public void TearDown()
        {
            _skillRepositoryMock.Reset();
            _skillService = new SkillService(_skillRepositoryMock.Object);
        }
    }
}