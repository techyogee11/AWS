using Amazon.DynamoDBv2.DataModel;
using DynamoStudentMananger.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamoStudentMananger.Controllers
{

    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IDynamoDBContext _dbContext;
        public StudentController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetById(int studentId) 
        {
            var student = await _dbContext.LoadAsync<Student>(studentId);
            if(student == null) return NotFound();
            return Ok(student);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            var student = await _dbContext.ScanAsync<Student>(default).GetRemainingAsync();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResult(Student studentRequest)
        {
            var student = await _dbContext.LoadAsync<Student>(studentRequest.Id);
            if (student != null) return BadRequest($"Student with Id {student.Id} Already Exists");
            await _dbContext.SaveAsync(studentRequest);
            return Ok(studentRequest);
        }

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var student = await _dbContext.LoadAsync<Student>(studentId);
            if(student == null) return NotFound();  
            await _dbContext.DeleteAsync(student);
            return NoContent();
        }
    }
}
