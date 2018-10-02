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
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ProductCreateAssembler _productCreateAssembler;

        public ProductApplicationService(IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            ProductCreateAssembler productCreateAssembler)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _productCreateAssembler = productCreateAssembler;
        }

        public ProductOutputDto Get(int id)
        {
            var entity = _productRepository.Get(id);

            return _productCreateAssembler.FromEntity(entity);
        }

        public List<ProductOutputDto> GetAll()
        {
            var entities = _productCreateAssembler.FromEntityList(_productRepository.GetAll().ToList());

            return entities;
        }

        public void Create(ProductCreateDto model)
        {
            Notification notification =  ValidateModel(model);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            bool status = _unitOfWork.BeginTransaction();

            try
            { 
                Product product = _productCreateAssembler.ToEntity(model);
                _productRepository.Create(product);
                _unitOfWork.Commit(status);
            }
            catch ( Exception ex)
            {
                _unitOfWork.Rollback(status);

                notification.AddError("there was error creating product");
                throw new ArgumentException(notification.ErrorMessage());

            }
        }

        private Notification ValidateModel(ProductCreateDto model)
        {
            Notification notification = new Notification();

            if (model == null || string.IsNullOrEmpty(model.Name) || model.CategoryId == 0)

            {
                notification.AddError("Invalid JSON data in request body");
                return notification;
            }

            return notification;

        }
    }
}
