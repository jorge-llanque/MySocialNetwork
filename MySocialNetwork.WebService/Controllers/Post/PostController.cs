using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySocialNetwork.Dto;
using MySocialNetwork.Model;
using MySocialNetwork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySocialNetwork.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly MySocialNetworkContext _context;
        private readonly IMapper _mapper;

        public PostController(MySocialNetworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> NewPost(PostDto postCreacion)
        {
            var post = _mapper.Map<Post>(postCreacion);
            _context.Add(post);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<PostDto>(post);
            return new CreatedAtRouteResult("getPost", new { id = post.PostId }, dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> Get()
        {
            var posts = await _context.Posts.ToListAsync();
            return _mapper.Map<List<PostDto>>(posts);
        }

        [HttpGet("{id}", Name = "getPost")]
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
