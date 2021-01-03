using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            this._context = context;
        }


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            
            var Satellites = _context.CelestialObjects.FirstOrDefault(s => s.Id == id);
            if (Satellites == null) return NotFound();
            return Ok(Satellites);



        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {

            var Satellites = _context.CelestialObjects.FirstOrDefault(s => s.Name == name);
            if (Satellites == null) return NotFound();
            return Ok(Satellites);



        }


        [HttpGet]
        public IActionResult GetAll()
        {

            var Satellites = _context.CelestialObjects;
            return Ok(Satellites);



        }

    }
}
