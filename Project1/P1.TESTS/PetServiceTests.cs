using Moq;
using P1.API.Model;
using P1.API.Repository.Interface;
using P1.API.Service;
using P1.API.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P1.API.Data;

namespace P1.TESTS.Services
{
    public class PetServiceTests
    {
        private readonly Mock<IPetRepository> _mockPetRepository;
        private readonly IPetService _petService;

        public PetServiceTests()
        {
            _mockPetRepository = new Mock<IPetRepository>();
            _petService = new PetService(_mockPetRepository.Object);
        }

        [Fact]
        public async Task GetAllPetsAsync_ShouldReturnAllPets()
        {
            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Buddy", Age = 3, Type = "Dog", Breed = "Labrador", IsAdopted = false },
                new Pet { Id = 2, Name = "Whiskers", Age = 2, Type = "Cat", Breed = "Siamese", IsAdopted = false }
            };
            _mockPetRepository.Setup(repo => repo.GetAllPetsAsync()).ReturnsAsync(pets);
            var result = await _petService.GetAllPetsAsync();

            Assert.Equal(2, result.Count());
            _mockPetRepository.Verify(repo => repo.GetAllPetsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPetByIdAsync_ShouldReturnPet_WhenPetExists()
        {
            var pet = new Pet { Id = 1, Name = "Buddy", Age = 3, Type = "Dog", Breed = "Labrador", IsAdopted = false };
            _mockPetRepository.Setup(repo => repo.GetPetByIdAsync(1)).ReturnsAsync(pet);

            var result = await _petService.GetPetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Buddy", result.Name);
            _mockPetRepository.Verify(repo => repo.GetPetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task AddPetAsync_ShouldCallRepositoryAdd()
        {
            var pet = new Pet { Id = 3, Name = "Rex", Age = 4, Type = "Dog", Breed = "German Shepherd", IsAdopted = false };

            await _petService.AddPetAsync(pet);
            _mockPetRepository.Verify(repo => repo.AddPetAsync(pet), Times.Once);
        }

        [Fact]
        public async Task DeletePetAsync_ShouldCallRepositoryDelete_WhenPetExists()
        {
            var pet = new Pet { Id = 1, Name = "Buddy", Age = 3, Type = "Dog", Breed = "Labrador", IsAdopted = false };
            _mockPetRepository.Setup(repo => repo.GetPetByIdAsync(1)).ReturnsAsync(pet);

            await _petService.DeletePetAsync(1);

            _mockPetRepository.Verify(repo => repo.DeletePetAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetPetByIdAsync_ShouldReturnNull_WhenPetDoesNotExist()
        {
            _mockPetRepository.Setup(repo => repo.GetPetByIdAsync(It.IsAny<int>())).ReturnsAsync((Pet)null);

            var result = await _petService.GetPetByIdAsync(999);

            Assert.Null(result);
            _mockPetRepository.Verify(repo => repo.GetPetByIdAsync(It.IsAny<int>()), Times.Once);
        }       

    }
}
