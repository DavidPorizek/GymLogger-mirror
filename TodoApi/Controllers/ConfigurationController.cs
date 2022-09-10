using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymLogger.Shared.Models;
using GymLogger.Context;

namespace GymLogger.Controllers;

[Route("api/Configurations")]
[ApiController]
public class ConfigurationController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public ConfigurationController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/Configurations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Configuration>>> GetConfigurations()
    {
        if (_context.Configurations == null)
        {
            return NotFound();
        }

        return await _context.Configurations
            .ToListAsync();
    }

    // GET: api/Configurations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Configuration>> GetConfiguration(long id)
    {
        if (_context.Configurations == null)
        {
            return NotFound();
        }
        var Configuration = await _context.Configurations
            .SingleOrDefaultAsync(x => x.Id == id);

        if (Configuration == null)
        {
            return NotFound();
        }

        return Configuration;
    }

    // PUT: api/Configurations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConfiguration(long id, Configuration Configuration)
    {
        if (id != Configuration.Id)
        {
            return BadRequest();
        }

        var updateConfiguration = await _context.Configurations.FindAsync(id);
        if (updateConfiguration == null)
        {
            return NotFound();
        }

        updateConfiguration.IncrementAutomatically = Configuration.IncrementAutomatically;
        updateConfiguration.DeloadAutomatically = Configuration.DeloadAutomatically;
        updateConfiguration.IncrementWholeWorkout = Configuration.IncrementWholeWorkout;
        updateConfiguration.DeloadWholeWorkout = Configuration.DeloadWholeWorkout;
        updateConfiguration.IncrementByPercentage = Configuration.IncrementByPercentage;
        updateConfiguration.DeloadByPercentage = Configuration.DeloadByPercentage;
        updateConfiguration.DeloadRatio = Configuration.DeloadRatio;
        updateConfiguration.IncrementRatio = Configuration.IncrementRatio;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ConfigurationExists(id))
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

    // POST: api/Configurations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Configuration>> CreateConfiguration(Configuration Configuration)
    {
        if (_context.Configurations == null)
        {
            return Problem("Entity set 'GymLoggerContext.Configurations'  is null.");
        }

        var createdConfiguration = new Configuration
        {
            IncrementAutomatically = Configuration.IncrementAutomatically,
            DeloadAutomatically = Configuration.DeloadAutomatically,
            IncrementWholeWorkout = Configuration.IncrementWholeWorkout,
            DeloadWholeWorkout = Configuration.DeloadWholeWorkout,
            IncrementByPercentage = Configuration.IncrementByPercentage,
            DeloadByPercentage = Configuration.DeloadByPercentage,
            DeloadRatio = Configuration.DeloadRatio,
            IncrementRatio = Configuration.IncrementRatio,
        };

        _context.Configurations.Add(createdConfiguration);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConfiguration), new { id = createdConfiguration.Id }, createdConfiguration);
    }

    // DELETE: api/Configurations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.Configurations == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.Configurations.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.Configurations.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ConfigurationExists(long id)
    {
        return (_context.Configurations?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
