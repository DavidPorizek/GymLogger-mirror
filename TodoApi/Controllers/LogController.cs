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

[Route("api/Logs")]
[ApiController]
public class LogController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public LogController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/Logs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
    {
        if (_context.Logs == null)
        {
            return NotFound();
        }

        return await _context.Logs.ToListAsync();
    }

    // GET: api/Logs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Log>> GetLog(long id)
    {
        if (_context.Logs == null)
        {
            return NotFound();
        }
        var Log = await _context.Logs.FindAsync(id);

        if (Log == null)
        {
            return NotFound();
        }

        return Log;
    }

    // PUT: api/Logs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLog(long id, Log Log)
    {
        if (id != Log.Id)
        {
            return BadRequest();
        }

        var updateLog = await _context.Logs.FindAsync(id);
        if (updateLog == null)
        {
            return NotFound();
        }

        updateLog.Excercise = Log.Excercise;
        updateLog.Repetition = Log.Repetition;
        updateLog.Series = Log.Series;
        updateLog.Completion = Log.Completion;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LogExists(id))
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

    // POST: api/Logs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Log>> CreateLog(Log Log)
    {
        if (_context.Logs == null)
        {
            return Problem("Entity set 'GymLoggerContext.Logs'  is null.");
        }

        var createdLog = new Log
        {
            Excercise = Log.Excercise,
            Repetition = Log.Repetition,
            Series = Log.Series,
            Completion = Log.Completion
        };

        _context.Logs.Add(createdLog);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLog), new { id = createdLog.Id }, createdLog);
    }

    // DELETE: api/Logs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.Logs == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.Logs.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.Logs.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LogExists(long id)
    {
        return (_context.Logs?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
