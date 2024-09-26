using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Faculty: Base
    {
        [Key]
        public Guid FacultyId { get; set; }
    }
}
