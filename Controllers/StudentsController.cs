using HelloWorld.Dto.Orders;
using HelloWorld.Dto.Students;
using HelloWorld.Entities;
using HelloWorld.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers;

[ApiController]
[Route("students")]
public class StudentsController : ControllerBase
{
  
  private readonly IDataBase _dataBase;

  public StudentsController(IDataBase dataBase)
  {
    _dataBase = dataBase;
  }
  
  [HttpGet]
  public IActionResult GetStudents()
  {
    var studentDtos = _dataBase.Students.Values
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
      Email = student.Email
    };

    _dataBase.Students.Add(newStudent.Id, newStudent);

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
      Email = student.Email
    };

    _dataBase.Students[id] = newStudent;

    return Ok(id);
  }

  [HttpGet("{id}")]
  public IActionResult GetStudent(Guid id)
  {
    if (_dataBase.Students.TryGetValue(id, out var studentEntity))
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

    if (_dataBase.Students.ContainsKey(id))
    {
      _dataBase.Students.Remove(id);
      return Ok();
    }

    return NotFound($"Student with Id {id} was not found");
  }
}