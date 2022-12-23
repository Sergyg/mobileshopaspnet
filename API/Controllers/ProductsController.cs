using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
// using Core.Specifications;
// using API.Dtos;
// using AutoMapper;
// using API.Errors;
using Microsoft.AspNetCore.Http;
// using API.Helpers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
        // BaseApiController
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductsBrands()
        {
            return Ok( await _repo.GetProductBrandsAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductsTypes()
        {
            return Ok( await _repo.GetProductTypesAsync());
        }
    }
}
//         private readonly IGenericRepository<ProductBrand> _productBrandRepo;
//         private readonly IGenericRepository<ProductType> _productTypeRepo;
//         private readonly IGenericRepository<Product> _productsRepo;
//         private readonly IMapper _mapper;
//
//         public ProductsController(IGenericRepository<Product> productsRepo,
//             IGenericRepository<ProductType> productTypeRepo,
//             IGenericRepository<ProductBrand> productBrandRepo,
//             IMapper mapper)
//         {
//             _mapper = mapper;
//             _productsRepo = productsRepo;
//             _productTypeRepo = productTypeRepo;
//             _productBrandRepo = productBrandRepo;
//         }
//
//         [Cached(600)]
//         [HttpGet]
//         public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
//             [FromQuery] ProductSpecParams productParams)
//         {
//             var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
//             var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
//
//             var totalItems = await _productsRepo.CountAsync(countSpec);
//
//             var products = await _productsRepo.ListAsync(spec);
//
//             var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
//
//             return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
//                 productParams.PageSize, totalItems, data));
//         }
//
//         [Cached(600)]
//         [HttpGet("{id}")]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//         public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
//         {
//             var spec = new ProductsWithTypesAndBrandsSpecification(id);
//
//             var product = await _productsRepo.GetEntityWithSpec(spec);
//
//             if (product == null) return NotFound(new ApiResponse(404));
//
//             return _mapper.Map<ProductToReturnDto>(product);
//         }
//
//         [Cached(600)]
//         [HttpGet("brands")]
//         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
//         {
//             return Ok(await _productBrandRepo.ListAllAsync());
//         }
//
//         [Cached(600)]
//         [HttpGet("types")]
//         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
//         {
//             return Ok(await _productTypeRepo.ListAllAsync());
//         }
//     }

