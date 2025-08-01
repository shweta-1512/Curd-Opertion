using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SawggerDemo.Data;
using SawggerDemo.Model;

namespace SawggerDemo.Controller;

[Route("api/[controller]")]
[ApiController]
public class StudentsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        => await _context.Students.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
        => await _context.Students.FindAsync(id) is Student student
            ? student
            : NotFound();

    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student student)
    {
        if (id != student.Id) return BadRequest();
        _context.Entry(student).State = EntityState.Modified;
        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Students.Any(e => e.Id == id))
                return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
