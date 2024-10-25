using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P1.API.Data;
using P1.API.Model;
using P1.API.Repository.Interface;
using P1.API.Service.Interface; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P1.API.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly ShahP1Context _context;

        public PetRepository(ShahP1Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pet>> GetAllPetsAsync()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public async Task AddPetAsync(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePetAsync(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }
    }
}
