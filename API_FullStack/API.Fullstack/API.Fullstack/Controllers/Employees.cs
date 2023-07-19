using API.Fullstack.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Fullstack.Models;

namespace API.Fullstack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Employees : Controller

    {
        private readonly FullstackDbContext _fullstackDbContext;

        public Employees(FullstackDbContext fullstackDbContext)
        {
            this._fullstackDbContext = fullstackDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullstackDbContext.Employees.ToListAsync();
            return Ok(employees);

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeRequest)
        {
            employeRequest.Id = Guid.NewGuid();
            await _fullstackDbContext.Employees.AddAsync(employeRequest);
            await _fullstackDbContext.SaveChangesAsync();

            return Ok(employeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
           var employee= await _fullstackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] Employee employee)
        {
            var foundEmployee= await _fullstackDbContext.Employees.FindAsync(id);

            if (foundEmployee == null)
            {
                return NotFound();  
            }

            foundEmployee.Name = employee.Name;
            foundEmployee.Email = employee.Email;
            foundEmployee.Phone = employee.Phone;
            foundEmployee.Department= employee.Department;
            foundEmployee.Salary= employee.Salary;

           await _fullstackDbContext.SaveChangesAsync();

            return Ok(foundEmployee);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var foundEmployee = await _fullstackDbContext.Employees.FindAsync(id);
            if (foundEmployee == null)
            {
                return NotFound(id);
            }

            _fullstackDbContext.Employees.Remove(foundEmployee);
            await _fullstackDbContext.SaveChangesAsync();

            return Ok(foundEmployee);
        }
    }
}
