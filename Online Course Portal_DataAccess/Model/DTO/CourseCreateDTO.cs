using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess.Model.DTO
{
    public class CourseCreateDTO
    {
        
        [Required(ErrorMessage = "Course Name is required")]
        [MaxLength(50, ErrorMessage = "Course Name cannot exceed 50 characters.")]
        
        public string courseName { get; set; }

        [Required(ErrorMessage = "Course Description is required.")]
       
        public string courseDescription { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        [Display(Name = "Start Date")]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        [Display(Name = "End Date")]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Available seats is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Available seats must be a positive number.")]
        [Display(Name = "Available Seats")]
        [Column(TypeName = "int")]
        public int AvailableSeats { get; set; }
    }
}
