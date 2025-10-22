using Application.Product.Commands.Create;
using Application.Product.Commands.Edit;
using Application.Product.Commands.Remove;
using Application.User.Create;
using Application.User.Edit;
using Application.User.SetImage;
using Common.AspNetCore;
using Facade.Product;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ApiResult> RemoveProduct(RemoveProductCommand command)
        {
            return CommandResult(await _facade.RemoveProduct(command));
        }
    }
}
