﻿using CRUD_ASPNetCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_ASPNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly StudentDbContext context;

        public StudentAPIController(StudentDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var std = await context.Students.FindAsync(id);
            
            if (std == null) 
            {
                return NotFound();
            }
            else
            {
                return std;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id,Student std)
        {
            if (id != std.Id) 
            {
                return BadRequest();
            }
            context.Entry(std).State= EntityState.Modified; //to modify
            await context.SaveChangesAsync();  
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var std = await context.Students.FindAsync(id);

            if (std == null)
            {
                return NotFound();
            }
            else
            {
                context.Students.Remove(std);
                await context.SaveChangesAsync();
                return Ok(std); 
            }
        }
         
    }
}
