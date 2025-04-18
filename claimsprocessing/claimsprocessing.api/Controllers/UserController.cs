using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using claimsprocessing.api.Models;
using claimsprocessing.api.Services;

namespace claimsprocessing.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<tbl_user>>> GetUsers()
        {
            IEnumerable<tbl_user> users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tbl_user>> GetUserById(int id)
        {
            tbl_user tbl_user = await _userService.GetUserByIdAsync(id);

            if (tbl_user == null)
            {
                return NotFound();
            }

            return Ok(tbl_user);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserById(int id, tbl_user user)
        {
            if (id != user.user_id)
            {
                return BadRequest();
            }

            bool isUpdated = await _userService.UpdateUserByIdAsync(id, user);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tbl_user>> CreateUser(tbl_user? user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            user = await _userService.CreateUserAsync(user);
            return CreatedAtAction("GetUserById", new { id = user?.user_id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            bool isDeleted = await _userService.DeleteUserByIdAsync(id);

            return isDeleted ? NoContent() : NotFound();
        }
    }
}
