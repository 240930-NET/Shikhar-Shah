using System.Collections.Generic;
using System.Threading.Tasks;
using P1.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P1.API.Data;
using P1.API.Repository.Interface;
using P1.API.Service.Interface; 

namespace P1.API.Service.Interface
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetAllPetsAsync();
        Task<Pet> GetPetByIdAsync(int id);
        Task AddPetAsync(Pet pet);
        Task DeletePetAsync(int id);
    }
}