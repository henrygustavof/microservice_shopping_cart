using System.Collections.Generic;
using System.Linq;
using Product.Api.Common.Application;
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
            bool status = _unitOfWork.BeginTransaction();

            var entity = _productRepository.Get(id);

            _unitOfWork.Commit(status);
            return _productCreateAssembler.fromEntity(entity);
        }

        public List<ProductOutputDto> GetAll()
        {
            bool status = _unitOfWork.BeginTransaction();
            var entities = _productCreateAssembler.fromEntityList(_productRepository.GetAll().ToList());

            _unitOfWork.Commit(status);

            return entities;
        }

        public void Create(ProductCreateDto productCreateDto)
        {
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
    }
}
