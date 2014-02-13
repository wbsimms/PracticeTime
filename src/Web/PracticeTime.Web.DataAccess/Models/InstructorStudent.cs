using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTime.Web.DataAccess.Models
{
    public class InstructorStudent
    {
        public int InstructorStudentId { get; set; }
        public string InstructorId { get; set; }
        public ApplicationUser Instructor { get; set; }
        public ApplicationUser Student { get; set; }
        public string StudentId { get; set; }
    }
}
