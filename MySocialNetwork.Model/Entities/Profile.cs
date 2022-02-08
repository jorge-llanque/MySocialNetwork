using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetwork.Model.Entities
{
    public class Profile
    {
        public int ProfileId { get; set; }
        [Required]
        [StringLength(15)]
        public string Gender { get; set; }
        [Required]
        [StringLength(100)]
        public string Photo { get; set; }
        public User User { get; set; }
    }
}
