using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Controllers
{
    [Route("logbook/[controller]")]
    [ApiController]
    public class JourneysController : ControllerBase
    {

        private readonly LogbookContext _context;

        public JourneysController(LogbookContext context)
        {
            _context = context;
        }

        // GET: logbook/Journeys
        [HttpGet]
        public async Task<ActionResult<Logbook>> GetLogbook()
        {

            Logbook logbook = new Logbook();

            logbook.journeys = await _context.Journeys.ToListAsync();



            return logbook;

        }

        // GET: api/Journeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Journey>> GetJourneys(long id)
        {
            var journey = await _context.Journeys.FindAsync(id);

            if (journey == null)
            {
                return NotFound();
            }

            return journey;
        }


        // POST: api/Drives
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Journey>> PostJourneys([FromBody] Journey journey)


        {

            Journey newJourney = new(journey.Start, journey.End, journey.Descripiton, journey.Distance, journey.Driver);



            _context.Journeys.Add(newJourney);
            await _context.SaveChangesAsync();

            return newJourney;

            //return CreatedAtAction("GetDrives", new { id = drive.Id }, drive);

        }


        private bool JourneysExists(long id)
        {
            return _context.Journeys.Any(e => e.Id == id);
        }
    }
}
