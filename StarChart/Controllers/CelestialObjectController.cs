using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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

        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject co)
        {
            _context.CelestialObjects.Add(co);

            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = co.Id }, co);



        }


        [HttpPut("{id}")]
        public IActionResult Update(int id,CelestialObject co)
        {
            var old = _context.CelestialObjects.Find(id);
            if (old == null) return NotFound();
            old.Name = co.Name;
            old.OrbitalPeriod = co.OrbitalPeriod;
            old.OrbitedObjectId = co.OrbitedObjectId;
            _context.CelestialObjects.Update(old);
            _context.SaveChanges();
            return NoContent();

        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id,string name)
        {
            var old = _context.CelestialObjects.Find(id);
            if (old == null) return NotFound();
            old.Name = name;
            _context.CelestialObjects.Update(old);
            _context.SaveChanges();
            return NoContent();


        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objs = _context.CelestialObjects.Where(o => (o.Id == id) || o.OrbitedObjectId == id).ToList();
            if (!objs.Any()) return NotFound();
            _context.CelestialObjects.RemoveRange(objs);
            _context.SaveChanges();
            return NoContent();


        }




    }
}
