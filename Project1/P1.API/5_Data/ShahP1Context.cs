using Microsoft.EntityFrameworkCore;
using P1.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P1.API.Data;
using P1.API.Repository.Interface;
using P1.API.Service.Interface; 

namespace P1.API.Data;

public partial class ShahP1Context : DbContext
{
    public ShahP1Context(){}
    public ShahP1Context(DbContextOptions<ShahP1Context> options) : base(options){}
    public virtual DbSet<Pet> Pets{get; set;}
}