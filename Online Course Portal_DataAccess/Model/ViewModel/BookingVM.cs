using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Online_Course_Portal_DataAccess.Model.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Course_Portal_DataAccess.Model.ViewModel
{
    public class BookingVM
    {
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        

        public IEnumerable<SelectListItem> Courses { get; set; }
       
    }
}
