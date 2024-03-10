using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotoSeguro.Domain.Enum
{
    public enum PollStatus
    {
        Active,
        Closed,
        Scheduled,
        Cancelled,
        Paused
    }
}
