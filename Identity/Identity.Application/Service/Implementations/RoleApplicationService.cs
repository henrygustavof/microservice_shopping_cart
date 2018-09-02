namespace Identity.Application.Service.Implementations
{
    using AutoMapper;
    using Identity.Application.Service.Interfaces;
    using Identity.Domain.Entity;
    using Identity.Domain.Repository;
    using System.Collections.Generic;
    using System.Linq;

    public class RoleApplicationService : IRoleApplicationService

    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Add(Dto.Input.RoleDto entity)
        {
            var newEntity = Mapper.Map<Role>(entity);

            _unitOfWork.Roles.Add(newEntity);

            _unitOfWork.Complete();

            return newEntity.Id;
        }

        public Dto.Output.RoleDto Get(int id)
        {
            return Mapper.Map<Dto.Output.RoleDto>(_unitOfWork.Roles.Get(id));
        }

        public Dto.Output.PaginationDto GetAll(int page, int pageSize, string sortBy, string sortDirection)
        {
            var entities = _unitOfWork.Roles.GetAll(page, pageSize, sortBy, sortDirection).ToList();

            var pagedRecord = new Dto.Output.PaginationDto
            {
                Content = Mapper.Map<List<Dto.Output.RoleDto>>(entities),
                TotalRecords = _unitOfWork.Roles.CountGetAll(),
                CurrentPage = page,
                PageSize = pageSize
            };

            return pagedRecord;
        }

        public int Delete(int id)
        {
            var entity = _unitOfWork.Roles.Get(id);

            _unitOfWork.Roles.Delete(entity);
            _unitOfWork.Complete();

            return 1;
        }

        public int Update(int id, Dto.Input.RoleDto entity)
        {
            var newEntity = _unitOfWork.Roles.Get(id);

            Mapper.Map(entity, newEntity);

            _unitOfWork.Roles.Update(newEntity);
            _unitOfWork.Complete();

            return newEntity?.Id ?? 0;
        }
    }
}
