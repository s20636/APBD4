using Cwiczenia4.Models;
using Cwiczenia4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Cwiczenia4.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDbService _service;
        public AnimalsController(IDbService dbService)
        {
            _service = dbService;
        }

        [HttpGet]
        public IActionResult GetAnimals(string orderBy)
        {
            var animals = _service.GetAnimals(orderBy);
            return Ok(animals);
        }
        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        { 
            bool result = _service.AddAnimal(animal);
            if (result)
            { 
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{idAnimal}")]
        public IActionResult DeleteAnimal(int idAnimal)
        {
            bool result = _service.DeleteAnimal(idAnimal);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("{idAnimal}")]
        public IActionResult UpdateAnimal(int idAnimal, Animal animal)
        {
            bool result = _service.UpdateAnimal(idAnimal, animal);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        
    }
}
