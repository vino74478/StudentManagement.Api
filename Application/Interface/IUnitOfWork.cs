using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        IFacultyRepository FacultyRepository { get; }
        Task completeAsync();
    }
}
