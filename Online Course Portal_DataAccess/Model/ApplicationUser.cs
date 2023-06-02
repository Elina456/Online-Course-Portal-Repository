using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Online_Course_Portal_DataAccess.Model
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }
       
        public string? City { get; set; }
        public string? State { get; set; }



        public string Role { get; set; }
    }
}
