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

[Route("api/Combinations")]
[ApiController]
public class CombinationController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public CombinationController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/Combinations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Combination>>> GetCombinations()
    {
        if (_context.Combinations == null)
        {
            return NotFound();
        }

        return await _context.Combinations.ToListAsync();
    }

    // GET: api/Combinations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Combination>> GetCombination(long id)
    {
        if (_context.Combinations == null)
        {
            return NotFound();
        }
        var Combination = await _context.Combinations.FindAsync(id);

        if (Combination == null)
        {
            return NotFound();
        }

        return Combination;
    }

    // PUT: api/Combinations/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCombination(long id, Combination Combination)
    {
        if (id != Combination.Id)
        {
            return BadRequest();
        }

        var updateCombination = await _context.Combinations.FindAsync(id);
        if (updateCombination == null)
        {
            return NotFound();
        }

        updateCombination.Equipment = Combination.Equipment;
        updateCombination.Excercise = Combination.Excercise;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CombinationExists(id))
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

    // POST: api/Combinations
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Combination>> CreateCombination(Combination Combination)
    {
        if (_context.Combinations == null)
        {
            return Problem("Entity set 'GymLoggerContext.Combinations'  is null.");
        }

        var createdCombination = new Combination
        {
            Equipment = Combination.Equipment,
            Excercise = Combination.Excercise
        };

        //var equipment = await _context.Equipments.FindAsync(createdCombination.Equipment);
        //if (equipment == null)
        //{
        //    return NotFound();
        //}

        //createdCombination.Equipment = equipment;

        //var excercise = await _context.Excercises.FindAsync(createdCombination.Excercise);
        //if (excercise == null)
        //{
        //    return NotFound();
        //}

        //createdCombination.Excercise = excercise;

        _context.Combinations.Add(createdCombination);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCombination), new { id = createdCombination.Id }, createdCombination);
    }

    // DELETE: api/Combinations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.Combinations == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.Combinations.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.Combinations.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CombinationExists(long id)
    {
        return (_context.Combinations?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
