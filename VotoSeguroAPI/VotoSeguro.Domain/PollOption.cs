using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotoSeguro.Domain
{
    public class PollOption : TenantBaseEntity
    {
        public required string Title { get; set; }
        public required Guid PollId { get; set; }
        public required virtual Poll Poll { get; set; }
        public virtual IList<PollVote>? PollVotes { get; set; }
    }
}
