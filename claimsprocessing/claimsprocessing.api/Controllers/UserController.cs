using AutoMapper;
using claimsprocessing.api.DTO;
using claimsprocessing.api.Models;
using claimsprocessing.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace claimsprocessing.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
            tbl_user? tbl_user = await _userService.GetUserByIdAsync(id);

            if (tbl_user == null)
            {
                return NotFound();
            }

            return Ok(tbl_user);
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserById(int id, UserUpdateDTO userUpdateDTO)
        {
            if (id != userUpdateDTO.user_id)
            {
                return BadRequest();
            }

            tbl_user? user = _mapper.Map<tbl_user>(userUpdateDTO);

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
        public async Task<ActionResult<tbl_user>> CreateUser(UserCreateDTO userCreateDTO)
        {
            if (userCreateDTO == null)
            {
                return BadRequest();
            }

            tbl_user? user = _mapper.Map<tbl_user>(userCreateDTO);

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