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

[Route("api/Equipments")]
[ApiController]
public class EquipmentController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public EquipmentController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/Equipments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipments()
    {
        if (_context.Equipments == null)
        {
            return NotFound();
        }

        return await _context.Equipments.ToListAsync();
    }

    // GET: api/Equipments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Equipment>> GetEquipment(long id)
    {
        if (_context.Equipments == null)
        {
            return NotFound();
        }
        var Equipment = await _context.Equipments.FindAsync(id);

        if (Equipment == null)
        {
            return NotFound();
        }

        return Equipment;
    }

    // PUT: api/Equipments/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEquipment(long id, Equipment Equipment)
    {
        if (id != Equipment.Id)
        {
            return BadRequest();
        }

        var updateEquipment = await _context.Equipments.FindAsync(id);
        if (updateEquipment == null)
        {
            return NotFound();
        }

        updateEquipment.Name = Equipment.Name;
        updateEquipment.BarbellMin = Equipment.BarbellMin;
        updateEquipment.BarbellMax = Equipment.BarbellMax;
        updateEquipment.BarbellIncrements = Equipment.BarbellIncrements;
        updateEquipment.DumbbellMin = Equipment.DumbbellMin;
        updateEquipment.DumbbellMax = Equipment.DumbbellMax;
        updateEquipment.DumbbellIncrements = Equipment.DumbbellIncrements;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EquipmentExists(id))
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

    // POST: api/Equipments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Equipment>> CreateEquipment(Equipment Equipment)
    {
        if (_context.Equipments == null)
        {
            return Problem("Entity set 'GymLoggerContext.Equipments'  is null.");
        }

        var createdEquipment = new Equipment
        {
            Name = Equipment.Name,
            BarbellMin = Equipment.BarbellMin,
            BarbellMax = Equipment.BarbellMax,
            BarbellIncrements = Equipment.BarbellIncrements,
            DumbbellMin = Equipment.DumbbellMin,
            DumbbellMax = Equipment.DumbbellMax,
            DumbbellIncrements = Equipment.DumbbellIncrements
        };

        _context.Equipments.Add(createdEquipment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEquipment), new { id = createdEquipment.Id }, createdEquipment);
    }

    // DELETE: api/Equipments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.Equipments == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.Equipments.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.Equipments.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EquipmentExists(long id)
    {
        return (_context.Equipments?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
