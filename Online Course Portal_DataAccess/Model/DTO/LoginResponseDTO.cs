﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess.Model.DTO
{
    public class LoginResponseDTO
    {
        public UserDTO user { get; set; }
        public string Token { get; set; }
        
    }
}
