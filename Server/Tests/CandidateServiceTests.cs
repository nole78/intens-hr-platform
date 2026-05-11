using Server.Services.CandidateService;
using Server.Domain.Models;
using Server.DTOs;
using Moq;
using Server.Persistance.Repositories.Candidates;
using Server.Persistance.Repositories.Skills;
using Server.Persistance.Repositories.CandidateSkills;
using Server.Common;
using Server.Domain.DTOs;

namespace Tests
{
    [TestFixture]
    class CandidateServiceTests
    {
        private Mock<ICandidateRepository> _candidateRepositoryMock;
        private Mock<ISkillRepository> _skillRepositoryMock;
        private Mock<ICandidateSkillRepository> _candidateSkillRepositoryMock;
        private CandidateService _candidateService;

        [SetUp]
        public void SetUp()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _skillRepositoryMock = new Mock<ISkillRepository>();
            _candidateSkillRepositoryMock = new Mock<ICandidateSkillRepository>();
            _candidateService = new CandidateService(_candidateRepositoryMock.Object, _skillRepositoryMock.Object, _candidateSkillRepositoryMock.Object);
        }

        #region GetCandidateAsync Tests

        [Test]
        [TestCase("John Smith")]
        public async Task GetCandidateAsync_ShouldReturnCandidates_WhenCandidatesExist(string candidateName)
        {
            // Arrange
            var expecteedCandidates = new List<Candidate>
            {
                new Candidate
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john@test.com",
                    ContactNumber = "123",
                    DateOfBirth = DateOnly.MaxValue,
                    CandidateSkills = []
                }
            };

            _candidateRepositoryMock.Setup(r => r.SearchAsync(It.IsAny<string?>(), It.IsAny<List<int>?>()))
                .ReturnsAsync(expecteedCandidates);

            // Act
            var result = await _candidateService.GetCandidateAsync(candidateName, null);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value!.Count, Is.EqualTo(expecteedCandidates[0].Id));
            Assert.That(result.Value[0].Name, Is.EqualTo(expecteedCandidates[0].Name));
        }

        [Test]
        [TestCase("John Smith")]
        public async Task GetCandidateAsync_ShouldReturnFailure_WhenRepoReturnsEmpty(string candidateName)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.SearchAsync(It.IsAny<string?>(), It.IsAny<List<int>?>()))
                .ReturnsAsync([]);

            // Act
            var result = await _candidateService.GetCandidateAsync(candidateName, null);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));
        }

        #endregion


        #region AddSkillToCandidateAsync Tests

        [Test]
        [TestCase(1, 1)]
        public async Task AddSkillToCandidateAsync_ShouldReturnSuccess_WhenAllChecksPass(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _skillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _candidateSkillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            _candidateSkillRepositoryMock.Setup(r => r.AddCandidateSkillAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        [TestCase(1, 1)]
        public async Task AddSkillToCandidateAsync_ShouldReturnFailure_WhenCandidateDoesntExist(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));
        }

        [Test]
        [TestCase(1, 1)]
        public async Task AddSkillToCandidateAsync_ShouldReturnFailure_WhenSkillDoesntExist(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _skillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(false);
            // Act
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);
            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));
        }

        [Test]
        [TestCase(1, 1)]
        public async Task AddSkillToCandidateAsync_ShouldReturnFailure_WhenConnectionAlreadyExists(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _skillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _candidateSkillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.Conflict));

        }

        [Test]
        [TestCase(1, 1)]
        public async Task AddSkillToCandidateAsync_ShouldReturnFailure_WhenAddCandidateSkillFails(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _skillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _candidateSkillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            _candidateSkillRepositoryMock.Setup(r => r.AddCandidateSkillAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.Internal));
        }

        #endregion


        #region RemoveSkillToCandidateAsync Tests

        [Test]
        [TestCase(1, 1)]
        public async Task RemoveSkillFromCandidateAsync_ShouldReturnSuccess_WhenAllChecksPass(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _skillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _candidateSkillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            _candidateSkillRepositoryMock.Setup(r => r.DeleteCandidateSkillAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        [TestCase(1, 1)]
        public async Task RemoveSkillFromCandidateAsync_ShouldReturnFailure_WhenCandidateDoesntExist(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));
        }

        [Test]
        [TestCase(1, 1)]
        public async Task RemoveSkillFromCandidateAsync_ShouldReturnFailure_WhenSkillDoesntExist(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _skillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(false);
            // Act
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);
            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));
        }

        [Test]
        [TestCase(1, 1)]
        public async Task RemoveSkillFromCandidateAsync_ShouldReturnFailure_WhenConnectionDoesntExists(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _skillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _candidateSkillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));

        }

        [Test]
        [TestCase(1, 1)]
        public async Task RemoveSkillFromCandidateAsync_ShouldReturnFailure_WhenRemoveCandidateSkillFails(int candidateId, int skillId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _skillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            _candidateSkillRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            _candidateSkillRepositoryMock.Setup(r => r.DeleteCandidateSkillAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.Internal));
        }

        #endregion

        
        #region AddCandidateAsync Tests

        [Test]
        [TestCase("John Smith","jd@gmail.com","3234552")]
        public async Task AddCandidateAsync_ShouldReturnCandidate_WhenCandidateWithSameEmailDoesntExist(string name,string email,string phone)
        {
            // Arrange
            var expectedCandidate = new Candidate
            {
                Id = 1,
                Name = "John Smith",
                Email = "jd@gmail.com",
                ContactNumber = "32342552",
                DateOfBirth = DateOnly.MinValue
            };

            _candidateRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((Candidate?)null);

            _candidateRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(expectedCandidate);

            // Act
            var result = await _candidateService.AddCandidateAsync(new CreateCandidateDto
            {
                Name = name,
                Email = email,
                ContactNumber = phone,
                DateOfBirth = DateOnly.MinValue
            });

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value, Is.Not.Null);
            Assert.That(result.Value!.Id, Is.EqualTo(expectedCandidate.Id));
            Assert.That(result.Value.Name, Is.EqualTo(expectedCandidate.Name));
            Assert.That(result.Value.Email, Is.EqualTo(expectedCandidate.Email));
            Assert.That(result.Value.ContactNumber, Is.EqualTo(expectedCandidate.ContactNumber));
            Assert.That(result.Value.DateOfBirth, Is.EqualTo(expectedCandidate.DateOfBirth));
        }

        [Test]
        [TestCase("John Smith", "jd@gmail.com", "3234552")]
        public async Task AddCandidateAsync_ShouldReturnFailure_WhenCandidateWithSameEmailExists(string name, string email, string phone)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new Candidate
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "jd@gmail.com",
                    ContactNumber = "064567883",
                    DateOfBirth = DateOnly.MaxValue
                });

            // Act
            var result = await _candidateService.AddCandidateAsync(new CreateCandidateDto
            {
                Name = name,
                Email = email,
                ContactNumber = phone,
                DateOfBirth = DateOnly.MinValue
            });

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.Conflict));
        }

        #endregion

        
        #region RemoveCandidateAsync Tests

        [Test]
        [TestCase(1)]
        public async Task RemoveCandidateAsync_ShouldReturnSucces_WhenCandidateExists(int candidateId)
        {
            // Arrange
            var expectedCandidate = new Candidate
            {
                Id = 1,
                Name = "John Smith",
            };

            _candidateRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedCandidate);

            _candidateRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(true);

            // Act
            var result = await _candidateService.RemoveCandidateAsync(candidateId);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        [TestCase(1)]
        public async Task RemoveCandidateAsync_ShouldReturnFailure_WhenCandidateDoesntExist(int candidateId)
        {
            // Arrange
            _candidateRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Candidate?)null);

            // Act
            var result = await _candidateService.RemoveCandidateAsync(candidateId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));

        }

        [Test]
        [TestCase(1)]
        public async Task RemoveCandidateAsync_ShouldReturnFailure_WhenDeleteFails(int candidateId)
        {
            // Arrange
            var expectedCandidate = new Candidate
            {
                Id = 1,
                Name = "John Smith",
            };

            _candidateRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedCandidate);

            _candidateRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(false);

            // Act
            var result = await _candidateService.RemoveCandidateAsync(candidateId);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.Internal));
        }

        #endregion

    }
}
