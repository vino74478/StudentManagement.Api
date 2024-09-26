using Application.Commands.Students;
using Application.DTO.Student;
using Application.Queries.Students;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace SM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetStudentListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetStudentQuery { Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> Post(StudentDto student)
        {
            return Ok(await _mediator.Send(new CreateStudentCommand { Student = student }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Student student)
        {
            return Ok(await _mediator.Send(new UpdateStudentCommand { Student = student }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteStudentCommand { Id = id }));
        }
    }
}
