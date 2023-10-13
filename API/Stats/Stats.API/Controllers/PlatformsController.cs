using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stats.API.Data;
using Stats.API.Dto;
using Stats.API.Models;

namespace Stats.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly StatContext _context;
        private readonly IMapper _mapper;


        public PlatformsController(StatContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Platforms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformDto>>> GetPlatforms()
        {
          if (_context.Platforms == null)
          {
              return NotFound();
          }
            var result = await _context.Platforms.Where(x=> x.IsDeleted == false).ToListAsync();
            return _mapper.Map<List<PlatformDto>>(result);
        }

        // GET: api/Platforms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Platform>> GetPlatform(int id)
        {
          if (_context.Platforms == null)
          {
              return NotFound();
          }
            var platform = await _context.Platforms.FindAsync(id);

            if (platform == null)
            {
                return NotFound();
            }

            return platform;
        }

        // PUT: api/Platforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatform(int id, Platform platform)
        {
            if (id != platform.PlatformId)
            {
                return BadRequest();
            }

            _context.Entry(platform).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatformExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Platforms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Platform>> PostPlatform(Platform platform)
        {
            if (_context.Platforms == null)
            {
                return Problem("Entity set 'StatContext.Platforms'  is null.");
            }
            if (!PlatformExists(platform.Name))
            {
                _context.Platforms.Add(platform);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPlatform", new { id = platform.PlatformId }, platform);
            }
            else
            {
                return Problem("Platform name already exists ");
            }

        }

        // DELETE: api/Platforms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform(int id)
        {
            if (_context.Platforms == null)
            {
                return NotFound();
            }
            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }
            platform.IsDeleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlatformExists(int id)
        {
            return (_context.Platforms?.Any(e => e.PlatformId == id)).GetValueOrDefault();
        }

        private bool PlatformExists(string name)
        {
            return (_context.Platforms?.Any(e => e.Name.ToUpper() == name.ToUpper())).GetValueOrDefault();
        }

    }
}
