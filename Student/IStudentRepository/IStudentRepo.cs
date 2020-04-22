using StudentVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IStudentRepository
{
    public interface IStudentRepo
    {
        Task<IEnumerable<StudentDetailsVM>> StudentList();
        Task<StudentDetailsVM> StudentById(int id);
        Task<ResponseVM> SaveStudent(StudentDetailsVM model);
    }
}
