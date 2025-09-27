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
            //var pageIndex = query.PaginationRequest.PageIndex;
            //var pageSize = query.PaginationRequest.PageSize;
            //var filterQuery = query.PaginationRequest.FilterQuery;
            //var filterValue = query.PaginationRequest.FilterValue;

            var userRepo = unitOfWork.GetRepository<SystemUser, SystemUserId>();

            var x = await userRepo.GetAllAsync(query.QueryOptions, [u => u.UserRoles]);
            var y = await userRepo.GetCountAsync(query.QueryOptions.QueryFilters);

            //var repo = unitOfWork.GetCustomRepository<IUserRepository>();

            //var users = await repo.GetSubUsersByOwnershipAsync(pageIndex: pageIndex,
            //                                                   pageSize: pageSize,
            //                                                   filterQuery: filterQuery,
            //                                                   filterValue: filterValue,
            //                                                   cancellationToken: cancellationToken);

            //var totalCount = await repo.GetCountAsync(filterQuery: filterQuery,
            //                                          filterValue: filterValue,
            //                                          cancellationToken: cancellationToken);

            //return new EGResponse<PaginatedResult<SubUserDto>>
            //{
            //    Success = true,
            //    Data = new PaginatedResult<SubUserDto>(pageIndex, pageSize, 0, users.ToSubUsersDtos())
            //};

            return new EGResponse<PaginatedResult<SubUserDto>>
            {
                Success = true,
                Data = new PaginatedResult<SubUserDto>(query.QueryOptions.PageIndex, query.QueryOptions.PageSize, 0, x.ToSubUsersDtos())
            };
        }
    }
}
