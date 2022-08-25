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

[Route("api/ExcerciseLogs")]
[ApiController]
public class LogController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public LogController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/ExcerciseLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExcerciseLog>>> GetLogs()
    {
        if (_context.ExcerciseLogs == null)
        {
            return NotFound();
        }

        return await _context.ExcerciseLogs
            .Include(x => x.WorkoutLog)
            .Include(x => x.Excercise)
            .ToListAsync();
    }

    // GET: api/ExcerciseLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ExcerciseLog>> GetLog(long id)
    {
        if (_context.ExcerciseLogs == null)
        {
            return NotFound();
        }
        var ExcerciseLog = await _context.ExcerciseLogs
            .Include(x => x.WorkoutLog)
            .Include(x => x.Excercise)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (ExcerciseLog == null)
        {
            return NotFound();
        }

        return ExcerciseLog;
    }

    // PUT: api/ExcerciseLogs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLog(long id, ExcerciseLog ExcerciseLog)
    {
        if (id != ExcerciseLog.Id)
        {
            return BadRequest();
        }

        var updateLog = await _context.ExcerciseLogs.FindAsync(id);
        if (updateLog == null)
        {
            return NotFound();
        }

        updateLog.Excercise = ExcerciseLog.Excercise;
        updateLog.Repetition = ExcerciseLog.Repetition;
        updateLog.Series = ExcerciseLog.Series;
        updateLog.Completion = ExcerciseLog.Completion;
        updateLog.WorkoutLogId = ExcerciseLog.WorkoutLogId;
        updateLog.ExcerciseId = ExcerciseLog.ExcerciseId;

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

    // POST: api/ExcerciseLogs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ExcerciseLog>> CreateLog(ExcerciseLog ExcerciseLog)
    {
        if (_context.ExcerciseLogs == null)
        {
            return Problem("Entity set 'GymLoggerContext.ExcerciseLogs'  is null.");
        }

        var createdLog = new ExcerciseLog
        {
            Excercise = ExcerciseLog.Excercise,
            Repetition = ExcerciseLog.Repetition,
            Series = ExcerciseLog.Series,
            Completion = ExcerciseLog.Completion,
            WorkoutLogId = ExcerciseLog.WorkoutLogId,
            ExcerciseId = ExcerciseLog.ExcerciseId
        };

        _context.ExcerciseLogs.Add(createdLog);
        await _context.SaveChangesAsync();

        _context.Entry(createdLog).Reference(x => x.WorkoutLog).Load();
        _context.Entry(createdLog).Reference(x => x.Excercise).Load();

        return CreatedAtAction(nameof(GetLog), new { id = createdLog.Id }, createdLog);
    }

    // DELETE: api/ExcerciseLogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.ExcerciseLogs == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.ExcerciseLogs.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.ExcerciseLogs.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LogExists(long id)
    {
        return (_context.ExcerciseLogs?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
