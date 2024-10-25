using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P1.API.Data;
using P1.API.Model;
using P1.API.Repository.Interface;
using P1.API.Service.Interface; 
namespace P1.API.Model;


public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Type { get; set; } = "";
    public string Breed { get; set; } = "";
    public bool IsAdopted { get; set; }

    public string IsAdoptedDisplay => IsAdopted ? "Yes" : "No";
}