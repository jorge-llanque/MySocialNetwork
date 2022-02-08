using MySocialNetwork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetwork.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public List<Post> Posts { get; set; }
    }
}
