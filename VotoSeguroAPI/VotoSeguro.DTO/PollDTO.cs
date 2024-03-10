using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotoSeguro.Domain.Enum;

namespace VotoSeguro.DTO
{
    public class PollDTO
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
