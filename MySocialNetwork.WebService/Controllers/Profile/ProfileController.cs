using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySocialNetwork.Dto;
using MySocialNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySocialNetwork.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly MySocialNetworkContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public ProfileController(MySocialNetworkContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProfileDto profileCreacion)
        {
            var profile = _mapper.Map<MySocialNetwork.Model.Entities.Profile>(profileCreacion);

            _context.Add(profile);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<ProfileDto>(profile);
            return new CreatedAtRouteResult("getProfile", new { id = profile.ProfileId }, dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProfileDto>>> Get()
        {
            var profiles = await _context.Profiles.ToListAsync();
            return _mapper.Map<List<ProfileDto>>(profiles);
        }

        [HttpGet("{id}", Name = "getProfile")]
        public async Task<ActionResult<ProfileDto>> Get([FromRoute] int id)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.ProfileId == id);
            if(profile == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProfileDto>(profile);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] ProfileDto profileDto)
        {
            var profileDB = await _context.Profiles.FirstOrDefaultAsync(x => x.ProfileId == id);
            if(profileDB == null)
            {
                return NotFound();
            }
            profileDB = _mapper.Map(profileDto, profileDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var exists = await _context.Profiles.AnyAsync(x => x.ProfileId == id);
            if (!exists)
            {
                return NotFound();
            }

            _context.Remove(new MySocialNetwork.Model.Entities.Profile() { ProfileId = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
