using VotoSeguro.Domain.Enum;

namespace VotoSeguro.Domain
{
    public class Poll : TenantBaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required PollStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual IList<PollOption>? PollOptions { get; set; }
        public virtual IList<PollVote>? PollVotes { get; set; }
    }
}
