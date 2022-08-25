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

[Route("api/Users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly GymLoggerContext _context;

    public UserController(GymLoggerContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        if (_context.Users == null)
        {
            return NotFound();
        }

        return await _context.Users
            .Include(x => x.Configuration)
            .ToListAsync();
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(long id)
    {
        if (_context.Users == null)
        {
            return NotFound();
        }
        var User = await _context.Users
            .Include(x => x.Configuration)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (User == null)
        {
            return NotFound();
        }

        return User;
    }

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(long id, User User)
    {
        if (id != User.Id)
        {
            return BadRequest();
        }

        var updateUser = await _context.Users.FindAsync(id);
        if (updateUser == null)
        {
            return NotFound();
        }

        updateUser.Name = User.Name;
        updateUser.ConfigurationId = User.ConfigurationId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    // POST: api/Users
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User User)
    {
        if (_context.Users == null)
        {
            return Problem("Entity set 'GymLoggerContext.Users'  is null.");
        }

        var createdUser = new User
        {
            Name = User.Name,
            ConfigurationId = User.ConfigurationId
        };

        _context.Users.Add(createdUser);
        await _context.SaveChangesAsync();

        _context.Entry(createdUser).Reference(x => x.Configuration).Load();

        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        if (_context.Users == null)
        {
            return NotFound();
        }

        var todoItemDTO = await _context.Users.FindAsync(id);
        if (todoItemDTO == null)
        {
            return NotFound();
        }

        _context.Users.Remove(todoItemDTO);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(long id)
    {
        return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
