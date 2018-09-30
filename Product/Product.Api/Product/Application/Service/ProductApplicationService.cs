using System;
using System.Collections.Generic;
using System.Linq;
using Product.Api.Common.Application.Notification;
using Product.Api.Common.Infrastructure.Persistence;
using Product.Api.Product.Application.Assembler;
using Product.Api.Product.Application.Dto;
using Product.Api.Product.Domain.Repository;

namespace Product.Api.Product.Application.Service
{

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

            return _productCreateAssembler.fromEntity(entity);
        }

        public List<ProductOutputDto> GetAll()
        {
            var entities = _productCreateAssembler.fromEntityList(_productRepository.GetAll().ToList());

            return entities;
        }

        public void Create(ProductCreateDto productCreateDto)
        {
            Notification notification =  ValidateModel(productCreateDto);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            bool status = _unitOfWork.BeginTransaction();

            try
            { 
                Domain.Entity.Product product = _productCreateAssembler.toEntity(productCreateDto);
                _productRepository.Create(product);
                _unitOfWork.Commit(status);
            }
            catch
            {
                _unitOfWork.Rollback(status);

            }
        }

        private Notification ValidateModel(ProductCreateDto model)
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
