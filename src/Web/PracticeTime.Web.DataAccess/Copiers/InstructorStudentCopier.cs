using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Models;

namespace PracticeTime.Web.DataAccess.Copiers
{
    public class InstructorStudentCopier
    {
        public void Merge(InstructorStudent from, InstructorStudent to)
        {
            if (from == null) return;
            to.InstructorId = from.InstructorId;
            to.StudentId = from.StudentId;
        }

        public InstructorStudent Copy(InstructorStudent from)
        {
            InstructorStudent to = new InstructorStudent();
            to.InstructorStudentId = from.InstructorStudentId;
            Merge(from,to);
            return to;
        }
    }
}
