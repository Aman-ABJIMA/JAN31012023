using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Student.Model;
using WebApplication_Student.Repository;

namespace WebApplication_Student.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

       
        [HttpGet]
        public async Task<IEnumerable<StudentID>> Get()
        {

            return await _studentRepository.GetAll();
        }

        [HttpGet("{id}")]
   
        public async Task<Student> Get(int id)
        {
            return await _studentRepository.GetById(id);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                 _studentRepository.Add(student);
                return Ok("Added Successfully");
            }
            return Ok("Wrong Cradentials!");

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody]Student student)
        { 
            StudentID sID = new StudentID();
            sID.Id= id;
            sID.RollNo= student.RollNo;
            sID.ContactNo= student.ContactNo;
            sID.Street= student.Street;
            sID.City= student.City;
            sID.Country= student.Country;
            sID.MotherName= student.MotherName;
            sID.FatherName= student.FatherName;
            sID.Name= student.Name;
            sID.PostalCode= student.PostalCode;
            sID.HouseNo= student.HouseNo;
            sID.State= student.State;
            
            if(ModelState.IsValid)
            {
                 _studentRepository.Update(sID);
                return Ok("Updated Successfully");
            }
            return Ok(" Not Exist ");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentRepository.Delete(id);
            return Ok(result);

        }



#region STORED PROCEDURE METHOD

        [HttpPost]
        [Route("InsertSP")]
        public async Task<IActionResult> InsertSP([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentRepository.InsertStoredProcedure(student);
                return Ok("Added Successfully");
            }
            return Ok("Wrong Cradentials!");
        }


        [HttpGet("SP/{id}")]
        

        public async Task<StudentID> GetByIdStoredProcedure(int id)
        {
            return await _studentRepository.GetById_SP(id);
        }



        #endregion


    }
}
