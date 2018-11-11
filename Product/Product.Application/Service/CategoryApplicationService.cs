namespace Product.Application.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Product.Application.Notification;
    using Product.Infrastructure.Persistence;
    using Assembler;
    using Dto;
    using Domain.Repository;
    using Domain.Entity;
    using Product.Application.Contracts;

    public class CategoryApplicationService : ICategoryApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryCreateAssembler _categoryCreateAssembler;

        public CategoryApplicationService(IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository,
            CategoryCreateAssembler categoryCreateAssembler)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _categoryCreateAssembler = categoryCreateAssembler;
        }

        public CategoryOutputDto Get(int id)
        {
            var entity = _categoryRepository.Get(id);

            return _categoryCreateAssembler.FromEntity(entity);
        }

        public List<CategoryOutputDto> GetAll()
        {
            var entities = _categoryCreateAssembler.FromEntityList(_categoryRepository.GetAll().ToList());

            return entities;
        }

        public void Create(CategoryCreateDto model)
        {
            Notification notification = ValidateModel(model);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            bool status = _unitOfWork.BeginTransaction();

            try
            {
                Category Category = _categoryCreateAssembler.ToEntity(model);
                _categoryRepository.Create(Category);
                _unitOfWork.Commit(status);
            }
            catch
            {
                _unitOfWork.Rollback(status);

            }
        }

        private Notification ValidateModel(CategoryCreateDto model)
        {
            Notification notification = new Notification();

            if (model == null || string.IsNullOrEmpty(model.Name))

            {
                notification.AddError("Invalid JSON data in request body");
                return notification;
            }

            return notification;

        }
    }
}
