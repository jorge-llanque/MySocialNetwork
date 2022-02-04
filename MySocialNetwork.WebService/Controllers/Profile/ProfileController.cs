using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySocialNetwork.WebService.Controllers.Profile
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly MySocialNetworkContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/>
        /// </summary>
        /// <param name="context"></param>
        public ProfileController(MySocialNetworkContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Model.Entities.Profile profileCreacion)
        {
            var profile = profileCreacion;
            if(profile == null)
            {
                return BadRequest();
            }

            _context.Add(profile);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Model.Entities.Profile>>> Get()
        {
            return await _context.Profiles.ToListAsync();
        }
    }
}
