using Application.Interface;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Queries.Students
{
    public class GetStudentListQuery() : IRequest<List<Student>>
    {

    }

    public class GetStudentListHandler : IRequestHandler<GetStudentListQuery, List<Student>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetStudentListHandler> _logger;

        public GetStudentListHandler(ILogger<GetStudentListHandler> logger,
                                     IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Student>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var students =  await _unitOfWork.StudentRepository.GetAllAsync();
            return students.ToList();
        }
    }
}
