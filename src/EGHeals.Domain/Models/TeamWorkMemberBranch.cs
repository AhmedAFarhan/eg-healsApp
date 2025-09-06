namespace EGHeals.Domain.Models
{
    public class TeamWorkMemberBranch : Entity<TeamWorkMemberBranchId>
    {
        internal TeamWorkMemberBranch(TeamWorkMemberId teamWorkMemberId, BranchId branchId)
        {
            Id = TeamWorkMemberBranchId.Of(Guid.NewGuid());
            TeamWorkMemberId = teamWorkMemberId;
            BranchId = branchId;
        }

        public TeamWorkMemberId TeamWorkMemberId { get; private set; } = default!;
        public BranchId BranchId { get; private set; } = default!;
    }
}
