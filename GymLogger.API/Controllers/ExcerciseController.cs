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

[Route("api/Excercises")]
[ApiController]
public class ExcerciseController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public ExcerciseController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/Excercises
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Excercise>>> GetExcercises()
    {
        if (_context.Excercises == null)
        {
            return NotFound();
        }

        return await _context.Excercises
            .Include(x => x.User)
            .ToListAsync();
    }

    // GET: api/Excercises/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Excercise>> GetExcercise(long id)
    {
        if (_context.Excercises == null)
        {
            return NotFound();
        }

        var Excercise = await _context.Excercises
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (Excercise == null)
        {
            return NotFound();
        }

        return Excercise;
    }

    // PUT: api/Excercises/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExcercise(long id, Excercise Excercise)
    {
        if (id != Excercise.Id)
        {
            return BadRequest();
        }

        var updateExcercise = await _context.Excercises.FindAsync(id);
        if (updateExcercise == null)
        {
            return NotFound();
        }

        updateExcercise.Name = Excercise.Name;
        updateExcercise.BodyPart = Excercise.BodyPart;
        updateExcercise.UserId = Excercise.UserId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ExcerciseExists(id))
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

    // POST: api/Excercises
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Excercise>> CreateExcercise(Excercise Excercise)
    {
        if (_context.Excercises == null)
        {
            return Problem("Entity set 'GymLoggerContext.Excercises'  is null.");
        }

        var createdExcercise = new Excercise
        {
            Name = Excercise.Name,
            BodyPart = Excercise.BodyPart,
            UserId = Excercise.UserId
        };

        _context.Excercises.Add(createdExcercise);
        await _context.SaveChangesAsync();
        
        _context.Entry(createdExcercise).Reference(x => x.User).Load();

        return CreatedAtAction(nameof(GetExcercise), new { id = createdExcercise.Id }, createdExcercise);
    }

    // DELETE: api/Excercises/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.Excercises == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.Excercises.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.Excercises.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ExcerciseExists(long id)
    {
        return (_context.Excercises?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
