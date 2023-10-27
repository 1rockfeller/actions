using System;
using System.Collections;
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
    public class SundayDataController : ControllerBase
    {
        private readonly StatContext _context;
        private readonly IMapper _mapper;

        public SundayDataController(StatContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/SundayData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SundayDataDto>>> GetSundayDatas()
        {
            if (_context.SundayDatas == null)
            {
                return NotFound();
            }
            //var result = await _context.SundayDatas.Where(i=>i.IsDeleted == true).GroupBy(i=>i.SundayDataDate).ToListAsync();
            //var groupByDateQuery =
            //    from data in _context.SundayDatas
            //    group data by data.SundayDataDate into groupDay
            //    //orderby groupDay.Key descending 
            //    select groupDay;

            var results = _context.SundayDatas.GroupBy(p=>p.SundayDataDate).ToList();
                //(key, g) => new { SundayDataDate = key, Cars = g.ToList() });

            List<SundayDataDto> listToReturn = new List<SundayDataDto>();
            foreach (var nameGroup in results)
            {
                var item = new SundayDataDto();
                item.SundayDataDate = nameGroup.Key;
                var total = 0;
                Console.WriteLine($"Key: {nameGroup.Key}");
                foreach (var grp in nameGroup)
                {
                    total += grp.Total;
                }
                item.Total = total;
                listToReturn.Add(item);
            }
            return _mapper.Map<List<SundayDataDto>>(listToReturn);
        }

        // GET: api/SundayData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SundayDataDto>> GetSundayData(int id)
        {
          if (_context.SundayDatas == null)
          {
              return NotFound();
          }
            var sundayData = await _context.SundayDatas.FindAsync(id);

            if (sundayData == null)
            {
                return NotFound();
            }

            return _mapper.Map<SundayDataDto>(sundayData);
        }

        // PUT: api/SundayData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSundayData(int id, SundayDataDto sundayData)
        {
            if (id != sundayData.SundayDataId)
            {
                return BadRequest();
            }
            var data = _mapper.Map<SundayData>(sundayData);
            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SundayDataExists(id))
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

        // POST: api/SundayData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SundayDataDto>> PostSundayData(SundayDataDto sundayData)
        {
            var data = _mapper.Map<SundayData>(sundayData);

            data.LastModifiedDt = DateTime.Now;
            _context.SundayDatas.Add(data);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<SundayDataDto>(data);

            return CreatedAtAction("GetSundayData", new { id = result.SundayDataId }, result);
        }

        // POST: api/SundayData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("SaveMany")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<SundayDataDto>>> SaveMany([FromBody] IList<SundayDataDto> sundayDatas)
        {
            var rangeData = new List<SundayData>();
            foreach (var item in sundayDatas)
            {
                var data = _mapper.Map<SundayData>(item);

                data.LastModifiedDt = DateTime.Now;
                rangeData.Add(data);
            }
            _context.AddRange(rangeData);
            
            await _context.SaveChangesAsync();

            var result = _mapper.Map<List<SundayDataDto>>(rangeData);

            return CreatedAtAction("GetSundayDatas",result);
        }

        // DELETE: api/SundayData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSundayData(int id)
        {
            if (_context.SundayDatas == null)
            {
                return NotFound();
            }
            var sundayData = await _context.SundayDatas.FindAsync(id);
            if (sundayData == null)
            {
                return NotFound();
            }

            _context.SundayDatas.Remove(sundayData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SundayDataExists(int id)
        {
            return (_context.SundayDatas?.Any(e => e.SundayDataId == id)).GetValueOrDefault();
        }
    }
}
