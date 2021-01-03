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


        [HttpGet("{id:int}",Name ="GetById")]
        public IActionResult GetById(int id)
        {
            
            var obj = _context.CelestialObjects.Find(id);
            if (obj == null) return NotFound();
            obj.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
            return Ok(obj);



        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {

            var obj = _context.CelestialObjects.Where(e => e.Name==name).ToList();
            if (!obj.Any()) return NotFound();
            foreach(var c in obj)
            {
                c.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == c.Id).ToList();
            }
            return Ok(obj);


        }


        [HttpGet]
        public IActionResult GetAll()
        {

            var obj = _context.CelestialObjects.ToList();

            foreach(var c in obj)
            {
                c.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == c.Id).ToList();
            }
            return Ok(obj);



        }

    }
}
