using BuildingBlocks.DataAccessAbstraction.Queries;
using EGHeals.Application.Dtos.Users;

namespace EGHeals.Application.Features.Users.Queries.GetSubUsersByOwnership
{
    //public record GetSubUsersByOwnershipQuery(PaginationRequest PaginationRequest) : IQuery<EGResponse<PaginatedResult<SubUserDto>>>;
    public record GetSubUsersByOwnershipQuery(QueryOptions<SystemUser> QueryOptions) : IQuery<EGResponse<PaginatedResult<SubUserDto>>>;

}
