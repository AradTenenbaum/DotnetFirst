using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroContext _context;

        public SuperHeroController(SuperHeroContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if(hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero req)
        {
            var dbHero = await _context.SuperHeros.FindAsync(req.Id);
            if(dbHero == null)
                return BadRequest("Hero not found.");
            
            dbHero.FirstName = req.FirstName;
            dbHero.LastName = req.LastName;
            dbHero.Name = req.Name;
            dbHero.Place = req.Place;

            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await _context.SuperHeros.FindAsync(id);
            if(dbHero == null)
                return BadRequest("Hero not found.");
            
            _context.SuperHeros.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}
