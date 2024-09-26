using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Students
{
    public class GetStudentQuery : IRequest<Student>
    {
        public Guid Id { get; set; }
    }

    public class GetStudentHandler : IRequestHandler<GetStudentQuery, Student>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetStudentHandler> _logger;
        public GetStudentHandler(ILogger<GetStudentHandler> logger,
                                 IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Student> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.StudentRepository.GetAsync(request.Id);
        }
    }
}
