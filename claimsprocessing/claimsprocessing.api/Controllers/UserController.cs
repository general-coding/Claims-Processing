using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using claimsprocessing.api.Models;

namespace claimsprocessing.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly claims_processingContext _context;

        public UserController(claims_processingContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<tbl_user>>> GetUsers()
        {
            return await _context.tbl_user.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tbl_user>> GetUserById(int id)
        {
            var tbl_user = await _context.tbl_user.FindAsync(id);

            if (tbl_user == null)
            {
                return NotFound();
            }

            return tbl_user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserById(int id, tbl_user tbl_user)
        {
            if (id != tbl_user.user_id)
            {
                return BadRequest();
            }

            _context.Entry(tbl_user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_userExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tbl_user>> CreateUser(tbl_user tbl_user)
        {
            _context.tbl_user.Add(tbl_user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserById", new { id = tbl_user.user_id }, tbl_user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var tbl_user = await _context.tbl_user.FindAsync(id);
            if (tbl_user == null)
            {
                return NotFound();
            }

            _context.tbl_user.Remove(tbl_user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool tbl_userExists(int id)
        {
            return _context.tbl_user.Any(e => e.user_id == id);
        }
    }
}
