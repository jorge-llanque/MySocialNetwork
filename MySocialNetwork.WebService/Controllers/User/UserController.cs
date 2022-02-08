using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySocialNetwork.Dto;
using MySocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySocialNetwork.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MySocialNetwork.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MySocialNetworkContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/>
        /// </summary>
        /// <param name="context"></param>
        public UserController(MySocialNetworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userCreacion)
        {
            var user = _mapper.Map<User>(userCreacion);
            _context.Add(user);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<UserDto>(user);
            return new CreatedAtRouteResult("getUser", new { id = user.UserId }, dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            var users = await _context.Users.ToListAsync();
            if(users == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<UserDto>>(users);
        }

        [HttpGet("{id}", Name = "getUser")]
        public async Task<ActionResult<UserDto>> Get([FromRoute] int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if(user == null)
            {
                return NotFound();
            }
            return _mapper.Map<UserDto>(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UserDto userDto)
        {
            var userDB = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if(userDB == null)
            {
                return NotFound();
            }
            userDB = _mapper.Map(userDto, userDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var exists = await _context.Users.AnyAsync(x => x.UserId == id);
            if (!exists)
            {
                return NotFound();
            }
            _context.Remove(new User() { UserId = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
