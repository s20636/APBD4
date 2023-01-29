using Cwiczenia4.Models;
using System.Collections.Generic;

namespace Cwiczenia4.Services
{
    public interface IDbService
    {
        public bool AddAnimal(Animal animal);
        public IEnumerable<Animal> GetAnimals(string orderBy);
        public bool UpdateAnimal(int idAnimal, Animal animal);
        public bool DeleteAnimal(int idAnimal);
    }
}
