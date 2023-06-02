using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess.Model.DTO
{
    public class CourseBookingDTO
    {
        [Required]
        public string StudentName { get; set; }
        [Required]
        public int? CourseId { get; set; }
    }
}
