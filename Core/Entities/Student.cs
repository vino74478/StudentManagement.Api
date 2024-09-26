using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : Base
    {
        [Key]
        public Guid StudentId { get; set; } = Guid.NewGuid();
        public string Class { get; set; }
    }
}
