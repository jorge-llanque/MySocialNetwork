using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetwork.Model.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}
