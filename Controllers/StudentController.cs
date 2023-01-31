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
        private readonly StudentRepository _studentRepository;

        public StudentController()
        {
            _studentRepository = new StudentRepository();
        }


        [HttpGet]
        public IEnumerable<StudentID> Get()
        {

            return _studentRepository.GetAll();
          
                      

        }

        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _studentRepository.GetById(id);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.Add(student);
                return Ok("Added Successfully");
            }
            return Ok("Wrong Cradentials!");

        }


        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody]Student student)
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
        public IActionResult Delete(int id)
        {
            var result = _studentRepository.Delete(id);
            return Ok(result);

        }



    }
}
