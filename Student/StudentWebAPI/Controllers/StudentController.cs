using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IStudentRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRepositroy;
using StudentVM;

namespace StudentWebAPI.Controllers
{
        [Route("api/[controller]/[action]")]
        [ApiController]
    public class StudentController : ControllerBase
    {
        #region privateObject
        private readonly IStudentRepo repo;
        public StudentController(IStudentRepo _repo)
        {
            repo = _repo;
        }
        #endregion

        #region StudentList
        [HttpGet]
        public async Task<IEnumerable<StudentDetailsVM>> StudentList()
        {
            return await repo?.StudentList();
        }

        #endregion
       
        #region StudentList
        [HttpGet]
        public async Task<StudentDetailsVM> StudentById(int id)
        {
            return await repo?.StudentById(id);
        }
        #endregion

        #region StudentSave
        [HttpPost]
        public async Task<ResponseVM> StudentSave([FromBody] StudentDetailsVM model)
        {
            return await repo?.SaveStudent(model);
        } 
        #endregion


    }
}