using Application.Interface;
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
    public class DeleteStudentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteStudentHandler> _logger;
        public DeleteStudentHandler(ILogger<DeleteStudentHandler> logger,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var deletedStudent = await _unitOfWork.StudentRepository.Delete(request.Id);
            await _unitOfWork.completeAsync();
            return deletedStudent;
        }
    }
}
