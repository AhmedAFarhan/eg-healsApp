using BuildingBlocks.DataAccessAbstraction.Queries;
using EGHeals.Application.Contracts.Users;
using EGHeals.Application.Dtos.Users;
using EGHeals.Application.Extensions.Users;

namespace EGHeals.Application.Features.Users.Queries.GetSubUsersByOwnership
{
    public class GetSubUsersByOwnershipQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetSubUsersByOwnershipQuery, EGResponse<PaginatedResult<SubUserDto>>>
    {
        public async Task<EGResponse<PaginatedResult<SubUserDto>>> Handle(GetSubUsersByOwnershipQuery query, CancellationToken cancellationToken)
        {
            var userRepo = unitOfWork.GetCustomRepository<IUserRepository>();

            var users = await userRepo.GetSubUsersAsync(options: query.QueryOptions, cancellationToken:cancellationToken);

            var totalCount = await userRepo.GetSubUsersCountAsync(filters: query.QueryOptions.QueryFilters, cancellationToken: cancellationToken);

            return new EGResponse<PaginatedResult<SubUserDto>>
            {
                Success = true,
                Data = new PaginatedResult<SubUserDto>(query.QueryOptions.PageIndex, query.QueryOptions.PageSize, totalCount, users.ToSubUsersDtos())
            };
        }
    }
}
