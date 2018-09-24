using System.Collections.Generic;
using Product.Api.Product.Application.Dto;

namespace Product.Api.Product.Application.Service
{
   public interface IProductApplicationService
   {
       ProductOutputDto Get(int id);
       List<ProductOutputDto> GetAll();
       void Create(ProductCreateDto productCreateDto);

   }
}
