using HelloWorld.Data;
using HelloWorld.Data.Entities;
using HelloWorld.Dto.Students;
using HelloWorld.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers;

[ApiController]
[Route("students")]
public class StudentsController : ControllerBase
{
  
  private readonly IDataBase _dataBase;
  private readonly ApplicationDbContext _applicationDbContext;

  public StudentsController(IDataBase dataBase, ApplicationDbContext applicationDbContext)
  {
    _dataBase = dataBase;
    _applicationDbContext = applicationDbContext;
  }
  
  [HttpGet]
  public IActionResult GetStudents()
  {
    var studentDtos = _applicationDbContext.Students
    .Select(student => new StudentDto
    {
      Id = student.Id,
      Name = student.Name,
      Surname = student.Surname,
      Age = student.Age,
      Group = student.Group,
      Email = student.Email
    })
    .ToArray();

    return Ok(studentDtos);
  }

  [HttpPost]
  public IActionResult CreateStudent([FromBody] StudentAddRequest student)
  {
    var newStudent = new StudentEntity
    {
      Id = Guid.NewGuid(),
      Name = student.Name,
      Surname = student.Surname,
      Age = student.Age,
      Group = student.Group,
      Email = student.Email,
      CityId = student.CityId,
      About = ""
    };

    _applicationDbContext.Students.Add(newStudent);
    _applicationDbContext.SaveChanges();

    return Ok(newStudent.Id);
  }

  [HttpPut("{id}")]
  public IActionResult CreateOrUpdateStudent([FromRoute] Guid id, [FromBody] StudentAddRequest student)
  {
    var newStudent = new StudentEntity
    {
      Id = id,
      Name = student.Name,
      Surname = student.Surname,
      Age = student.Age,
      Group = student.Group,
      Email = student.Email,
      CityId = student.CityId,
      About = ""
    };

    _applicationDbContext.Students.Attach(newStudent);
    _applicationDbContext.SaveChanges();

    return Ok(id);
  }

  [HttpGet("{id}")]
  public IActionResult GetStudent(Guid id)
  {
    var studentEntity = _applicationDbContext.Students.Find(id);
    if (studentEntity != null)
    {
      return Ok(new StudentDto
      {
        Id = studentEntity.Id,
        Name = studentEntity.Name,
        Surname = studentEntity.Surname,
        Age = studentEntity.Age,
        Group = studentEntity.Group,
        Email = studentEntity.Email
      });
    }

    return NotFound($"Student with Id {id} was not found");
  }
  
  [HttpDelete("{id}")]
  public IActionResult DeleteStudent(Guid id)
  {

    var studentEntity = _applicationDbContext.Students.Find(id);
    if (studentEntity != null)
    {
      _applicationDbContext.Students.Remove(studentEntity);
      _applicationDbContext.SaveChanges();
      return Ok();
    }

    return NotFound($"Student with Id {id} was not found");
  }
}