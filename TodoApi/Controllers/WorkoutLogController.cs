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

[Route("api/WorkoutLogs")]
[ApiController]
public class WorkoutLogController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public WorkoutLogController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/WorkoutLogs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkoutLog>>> GetWorkoutLogs()
    {
        if (_context.WorkoutLogs == null)
        {
            return NotFound();
        }

        return await _context.WorkoutLogs.ToListAsync();
    }

    // GET: api/WorkoutLogs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkoutLog>> GetWorkoutLog(long id)
    {
        if (_context.WorkoutLogs == null)
        {
            return NotFound();
        }
        var workoutLog = await _context.WorkoutLogs.FindAsync(id);

        if (workoutLog == null)
        {
            return NotFound();
        }

        return workoutLog;
    }

    // PUT: api/WorkoutLogs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkoutLog(long id, WorkoutLog workoutLog)
    {
        if (id != workoutLog.Id)
        {
            return BadRequest();
        }

        var updateWorkoutLog = await _context.WorkoutLogs.FindAsync(id);
        if (updateWorkoutLog == null)
        {
            return NotFound();
        }

        updateWorkoutLog.Duration = workoutLog.Duration;
        updateWorkoutLog.Date = workoutLog.Date;
        updateWorkoutLog.Logs = workoutLog.Logs;
        updateWorkoutLog.Workout = workoutLog.Workout;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkoutLogExists(id))
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

    // POST: api/WorkoutLogs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WorkoutLog>> CreateWorkoutLog(WorkoutLog workoutLog)
    {
        if (_context.WorkoutLogs == null)
        {
            return Problem("Entity set 'GymLoggerContext.WorkoutLogs'  is null.");
        }

        var createdWorkoutLog = new WorkoutLog
        {
            Date = workoutLog.Date,
            Duration = workoutLog.Duration,
            Workout = workoutLog.Workout,
            Logs = workoutLog.Logs
        };

        _context.WorkoutLogs.Add(createdWorkoutLog);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWorkoutLog), new { id = createdWorkoutLog.Id }, createdWorkoutLog);
    }

    // DELETE: api/WorkoutLogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.WorkoutLogs == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.WorkoutLogs.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.WorkoutLogs.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkoutLogExists(long id)
    {
        return (_context.WorkoutLogs?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
