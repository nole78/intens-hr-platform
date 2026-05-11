using Server.Services.SkillService;
using Server.Domain.Models;
using Server.Domain.DTOs;
using Server.Persistance.Repositories.Skills;
using Moq;
using Server.Common;

namespace Tests.Services
{
    [TestFixture]
    public class SkillServiceTests
    {
        private Mock<ISkillRepository> _skillRepositoryMock;
        private SkillService _skillService;

        [SetUp]
        public void SetUp()
        {
            _skillRepositoryMock = new Mock<ISkillRepository>();
            _skillService = new SkillService(_skillRepositoryMock.Object);
        }

        [Test]
        [TestCase("C# programming")]
        public async Task AddSkillAsync_ShouldAddSkill_WhenSkillDoesNotExist(string skillName)
        {
            // Arrange
            var expectedSkill = new Skill { Id = 1, Name = skillName };

            _skillRepositoryMock.Setup(repo => repo.GetByNameAsync(skillName))
                .ReturnsAsync((Skill?)null);

            _skillRepositoryMock.Setup(repo => repo.AddSkillAsync(It.IsAny<Skill>()))
                .ReturnsAsync(expectedSkill);

            // Act
            var result = await _skillService.AddSkillAsync(new CreateSkillDto { Name = skillName });

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value!.Id, Is.EqualTo(expectedSkill.Id));
            Assert.That(result.Value.Name, Is.EqualTo(expectedSkill.Name));
        }

        [Test]
        [TestCase("Java programming")]
        public async Task AddSkillAsync_ShouldReturnError_WhenSkillAlreadyExists(string skillName)
        {
            // Arrange
            _skillRepositoryMock.Setup(repo => repo.GetByNameAsync(skillName))
                .ReturnsAsync(new Skill { Id = 1, Name = skillName });
            // Act
            var result = await _skillService.AddSkillAsync(new CreateSkillDto { Name = skillName });

            // Assert
            Assert.That(result.IsSuccess, Is.Not.True);
            Assert.That(result.Error, Is.EqualTo("Skill with the same name already exists"));
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.Conflict));
        }
    }
}