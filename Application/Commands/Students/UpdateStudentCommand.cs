
using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Students
{
    public class UpdateStudentCommand : IRequest<Student>
    {
        public Student Student { get; set; }
    }

    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Student>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateStudentHandler> _logger;
        public UpdateStudentHandler(ILogger<UpdateStudentHandler> logger,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Student> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var updatedStudent = await _unitOfWork.StudentRepository.Update(request.Student);
            await _unitOfWork.completeAsync();
            return updatedStudent ? request.Student : new Student();
        }
    }


}
