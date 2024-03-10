namespace VotoSeguro.Domain
{
    public class PollVote : TenantBaseEntity
    {
        public required Guid PollOptionId { get; set; }
        public required virtual PollOption PollOption { get; set; }
        public required Guid PollId { get; set; }
        public required virtual Poll Poll { get; set; }
        public required Guid UserId { get; set; }
        public required virtual User User { get; set; }
    }
}
