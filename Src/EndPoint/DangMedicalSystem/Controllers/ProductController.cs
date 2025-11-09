using Application.Product.Commands.Create;
using Application.Product.Commands.Edit;
using Application.Product.Commands.Remove;
using Common.AspNetCore;
using Facade.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Query.Product.DTOs;
using Query.Product.DTOs.FilterDto;

namespace DangMedicalSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiController
    {
        private readonly IProductFacade _facade;

        public ProductController(IProductFacade facade)
        {
            _facade = facade;
        }

        [HttpPost("CreateProduct")]
        public async Task<ApiResult> CreateProduct([FromForm] CreateProductCommand command)
        {
            return CommandResult(await _facade.CreateProduct(command));
        }

        [HttpPatch("EditProduct")]
        public async Task<ApiResult> EditProduct([FromForm] EditProductCommand command)
        {
            return CommandResult(await _facade.EditProduct(command));
        }

        [HttpDelete("RemoveProduct")]
        public async Task<ApiResult> RemoveProduct(Guid productId)
        {
            return CommandResult(await _facade.RemoveProduct(new RemoveProductCommand
            {
                productId = productId,
            }));
        }

        //[Authorize]
        [HttpGet("GetProductByFilter")]
        public async Task<ApiResult<ProductFilterResult>> GetProductByFilter([FromQuery] ProductFilterParam param)
        {
            return QueryResult(await _facade.GetByFilter(param));
        }

        [HttpGet("GetProductBySlug")]
        public async Task<ApiResult<ProductDto?>> GetProductBySlug(string slug)
        {
            return QueryResult(await _facade.GetBySlug(slug));
        }
       
        [HttpGet("GetProductById")]
        public async Task<ApiResult<ProductDto?>> GetProductById(Guid productId)
        {
            return QueryResult(await _facade.GetById(productId));
        }
    }
}
