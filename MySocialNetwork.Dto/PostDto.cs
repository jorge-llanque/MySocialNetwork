﻿using MySocialNetwork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialNetwork.Dto
{
    public class PostDto
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
