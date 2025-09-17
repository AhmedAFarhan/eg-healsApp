using EGHeals.Application.Dtos.Users;

namespace EGHeals.Application.Features.Users.Queries.GetSubUsersByAdminId
{
    public record GetSubUsersByAdminIdQuery(PaginationRequest PaginationRequest) : IQuery<GetSubUsersByAdminIdResult>;
    public record GetSubUsersByAdminIdResult(EGResponse<PaginatedResult<SubUserDto>> response);
    //public record GetSubUsersByAdminIdResult(PaginatedResult<SubUserDto> SubUsers);
}
