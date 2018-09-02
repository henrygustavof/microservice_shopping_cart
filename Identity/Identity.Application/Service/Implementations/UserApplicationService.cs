namespace Identity.Application.Service.Implementations
{
    using AutoMapper;
    using Identity.Application.Service.Interfaces;
    using Identity.Domain.Entity;
    using Identity.Domain.Repository;
    using System.Collections.Generic;
    using System.Linq;

    public class UserApplicationService: IUserApplicationService

    {
        private readonly IUnitOfWork _unitOfWork;

        public UserApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Add(Dto.Input.UserDto entity)
        {
            var newEntity = Mapper.Map<User>(entity);

            _unitOfWork.Users.Add(newEntity);

            _unitOfWork.Complete();

            return newEntity.Id;
        }

        public Dto.Output.UserDto Get(int id)
        {
            return Mapper.Map<Dto.Output.UserDto>(_unitOfWork.Users.Get(id));
        }

        public Dto.Output.PaginationDto GetAll(int page, int pageSize, string sortBy, string sortDirection)
        {
            var entities = _unitOfWork.Users.GetAll(page, pageSize, sortBy, sortDirection).ToList();

            var pagedRecord = new Dto.Output.PaginationDto
            {
                Content = Mapper.Map<List<Dto.Output.UserDto>>(entities),
                TotalRecords = _unitOfWork.Users.CountGetAll(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return pagedRecord;
        }

        public int Delete(int id)
        {
            var entity = _unitOfWork.Users.Get(id);

            _unitOfWork.Users.Delete(entity);
            _unitOfWork.Complete();

            return 1;
        }

        public int Update(int id, Dto.Input.UserDto entity)
        {
            entity.Id = id;
            var oldEntity = _unitOfWork.Users.Get(id);

            Mapper.Map(entity, oldEntity);

            _unitOfWork.Users.Update(oldEntity);
            _unitOfWork.Complete();

            return oldEntity?.Id ?? 0;
        }
    }
}
