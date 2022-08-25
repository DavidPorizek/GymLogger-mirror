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

[Route("api/Workouts")]
[ApiController]
public class WorkoutController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public WorkoutController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/Workouts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workout>>> GetWorkouts()
    {
        if (_context.Workouts == null)
        {
            return NotFound();
        }

        return await _context.Workouts.ToListAsync();
    }

    // GET: api/Workouts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Workout>> GetWorkout(long id)
    {
        if (_context.Workouts == null)
        {
            return NotFound();
        }
        var Workout = await _context.Workouts.FindAsync(id);

        if (Workout == null)
        {
            return NotFound();
        }

        return Workout;
    }

    // PUT: api/Workouts/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkout(long id, Workout Workout)
    {
        if (id != Workout.Id)
        {
            return BadRequest();
        }

        var updateWorkout = await _context.Workouts.FindAsync(id);
        if (updateWorkout == null)
        {
            return NotFound();
        }

        updateWorkout.Name = Workout.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WorkoutExists(id))
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

    // POST: api/Workouts
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Workout>> CreateWorkout(Workout Workout)
    {
        if (_context.Workouts == null)
        {
            return Problem("Entity set 'GymLoggerContext.Workouts'  is null.");
        }

        var createdWorkout = new Workout
        {
            Name = Workout.Name
        };

        _context.Workouts.Add(createdWorkout);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWorkout), new { id = createdWorkout.Id }, createdWorkout);
    }

    // DELETE: api/Workouts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.Workouts == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.Workouts.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.Workouts.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool WorkoutExists(long id)
    {
        return (_context.Workouts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
