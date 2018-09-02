namespace Identity.Application.Service.Interfaces
{
    using Identity.Application.Dto.Output;

    public interface IBaseApplicationService<TEntityInput, TEntityOutPut>
    {
        TEntityOutPut Get(int id);

        PaginationDto GetAll(int page, int pageSize, string sortBy, string sortDirection);

        int Add(TEntityInput entity);

        int Update(int id, TEntityInput entity);

        int Delete(int id);

    }
}
