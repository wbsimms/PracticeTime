using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PracticeTime.Web.DataAccess.Copiers;
using PracticeTime.Web.DataAccess.Models;
using System.Data.Entity;


namespace PracticeTime.Web.DataAccess.Repositories
{
    public interface IInstructorStudentRepository
    {
        InstructorStudent Add(InstructorStudent instructorStudent);
        void Delete(InstructorStudent instructorStudent);
        InstructorStudent GetById(int instructorStudentId);
        List<InstructorStudent> GetAll();
        List<InstructorStudent> GetAllForInstructor(string userId);
    }

    public class InstructorStudentRepository : IInstructorStudentRepository
    {
        public InstructorStudent Add(InstructorStudent instructorStudent)
        {
            if (instructorStudent.InstructorStudentId != 0) throw new ApplicationException("InstructorStudent.InstructorStudentId must be zero");
            
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                // check if exists
                if (
                    context.InstructorStudents.Any(
                        x =>
                            x.InstructorId == instructorStudent.InstructorId &&
                            x.StudentId == instructorStudent.StudentId))
                {
                    return null;
                }

                InstructorStudent toSave = new InstructorStudentCopier().Copy(instructorStudent);
                context.InstructorStudents.Add(toSave);
                context.SaveChanges();
                return GetById(toSave.InstructorStudentId);
            }
        }

        public void Delete(InstructorStudent instructorStudent)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                int toLookup = instructorStudent.InstructorStudentId;
                InstructorStudent toDelete;
                if (toLookup == 0)
                {
                    toLookup = GetByStudentIdInstructorId(instructorStudent.StudentId, instructorStudent.InstructorId).InstructorStudentId;
                }
                toDelete = context.InstructorStudents.FirstOrDefault(x => x.InstructorStudentId == toLookup);
                if (toDelete == null)
                    throw new ApplicationException("Unable to find entity to delete");
                context.InstructorStudents.Remove(toDelete);
                context.SaveChanges();
            }
        }

        public InstructorStudent GetByStudentIdInstructorId(string studentId, string instructorId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.InstructorStudents.AsNoTracking().FirstOrDefault(x => x.InstructorId == instructorId && x.StudentId == studentId);
            }
        }

        public InstructorStudent GetById(int instructorStudentId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.InstructorStudents
                    .Include(x => x.Instructor)
                    .Include(x => x.Student)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.InstructorStudentId == instructorStudentId);
            }
        }

        public List<InstructorStudent> GetAll()
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.InstructorStudents
                    .Include(x => x.Instructor)
                    .Include(x => x.Student)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<InstructorStudent> GetAllForInstructor(string userId)
        {
            using (PracticeTimeContext context = new PracticeTimeContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                return context.InstructorStudents.AsNoTracking()
                    .Include(x => x.Instructor)
                    .Include(x => x.Student)
                    .Where(s => s.InstructorId == userId).ToList();
            }
        }
    }

}
