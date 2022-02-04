using Microsoft.EntityFrameworkCore;
using MySocialNetwork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetwork.Model
{
    public class MySocialNetworkContext : DbContext
    {
        public MySocialNetworkContext(DbContextOptions<MySocialNetworkContext> options) : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
