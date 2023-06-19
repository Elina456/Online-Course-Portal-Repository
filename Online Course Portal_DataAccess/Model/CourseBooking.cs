using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess.Model
{
    public class CourseBooking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int Id { get; set; }
       

        //[Column(TypeName = "varchar(50)")]
        //public string StudentName { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [Column(TypeName = "varchar(100)")]
        [ValidateNever]
        public Course Course { get; set; }
    }
}
