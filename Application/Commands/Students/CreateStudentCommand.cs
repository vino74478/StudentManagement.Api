using Application.DTO.Student;
using Application.Interface;
using Application.Queries.Students;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Students
{
    public class CreateStudentCommand : IRequest<Student>
    {
        public StudentDto Student { get; set; }
    }

    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Student>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateStudentHandler> _logger;
        public CreateStudentHandler(ILogger<CreateStudentHandler> logger,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Student> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentToAdd = new Student
            {
                StudentId = Guid.NewGuid(),
                FirstName = request.Student.FirstName,
                LastName = request.Student.LastName,
                PhoneNumber = request.Student.PhoneNumber,
                Email = request.Student.Email,
                Address = request.Student.Address,
                City = request.Student.City,
                PostalCode = request.Student.PostalCode,
                Country = request.Student.Country,
                SchoolId = request.Student.SchoolId,
                Class = request.Student.Class
            };
            var students = await _unitOfWork.StudentRepository.Add(studentToAdd);
            await _unitOfWork.completeAsync();
            return studentToAdd;
        }
    }
}
