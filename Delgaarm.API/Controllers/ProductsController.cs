using Application.Repositories.Interfaces;
using Application.Repository.Interfaces;
using Application.ViewModels.API.Dtos;
using AutoMapper;
using Delgraarm.API.Controllers;
using Infrastracture.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Application.API.Controllers
{
    public class ProductsController : BaseAPIController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;
        private readonly IProductRepasitory _product;
        public ProductsController( IMapper mapper,
                                   IUnitOfWork context,
                                   IProductRepasitory product)
        {
            _mapper = mapper;
            _context = context;
            _product = product;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductlistDto>> GetProductById(int id)
        {
            var products = await _product.GetProductById(id);
            if (products == null) return NotFound();
            var data = _mapper.Map<Product, ProductlistDto>(products);
            return Ok(data);
        }

        [HttpGet]
        public async Task<ActionResult<ProductlistDto>> GetProducts()
        {
            var products = await _context.productUW.GetEntitiesAsync(null,null, "Categories,productColor");
            var data = _mapper
               .Map<IEnumerable<Product>, IEnumerable<ProductlistDto>>(products);

            return Ok(data);
        }

        [HttpGet("products/{categoryId}")]
        public async Task<ActionResult<ProductlistDto>> GetProductsBytCategoryId(int categoryId)
        {
            var products = await _context.productUW.GetEntitiesAsync(x=> x.CategoryId == categoryId, null, "Categories,productColor");
            var data = _mapper
               .Map<IEnumerable<Product>, IEnumerable<ProductlistDto>>(products);

            return Ok(data);
        }

        [HttpGet("productGallery/{id}")]
        public async Task<ActionResult<ProductGalleryDTO>> GetProductGalleries(int id)
        {
            var galleris = await _context.productGalleryUW.GetEntitiesAsync(x => x.ProductId == id);
            var data = _mapper
                .Map<IEnumerable<ProductGallery>, IEnumerable<ProductGalleryDTO>>(galleris);
            return Ok(data);
        }

        [HttpGet("best-saller")]
        public async Task<ActionResult<ProductlistDto>> GetProductBestSaller()
        {
            var products = await _context.productUW.GetEntitiesAsync(x => x.IsBestSeller);
            var data = _mapper
                .Map<IEnumerable<Product>, IEnumerable<ProductlistDto>>(products);
            return Ok(data);
        }

        [HttpGet("prefered")]
        public async Task<ActionResult<ProductlistDto>> GetProductPrefered()
        {
            var products = await _context.productUW.GetEntitiesAsync(x => x.IsPrefered);
            var data = _mapper
                .Map<IEnumerable<Product>, IEnumerable<ProductlistDto>>(products);
            return Ok(data);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ProductlistDto>> FilterProduct([FromQuery] ProductParamForSearch search)
        {
            if (search.CategoryId== 0)
            {
                var products = await _context.productUW.GetEntitiesAsync(x => x.ProductName.Contains(search.productName), null, "Categories,productColor");
                var data = _mapper
               .Map<IEnumerable<Product>, IEnumerable<ProductlistDto>>(products);
                return Ok(data);
            }
            else
            {
                var products = await _context.productUW.GetEntitiesAsync(x => x.ProductName.Contains(search.productName) && x.CategoryId == search.CategoryId, null, "Categories,productColor");
                var data = _mapper
               .Map<IEnumerable<Product>, IEnumerable<ProductlistDto>>(products);
                return Ok(data);
            }
            

        }

    }
}
