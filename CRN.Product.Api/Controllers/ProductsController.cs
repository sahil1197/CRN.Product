using Asp.Versioning;
using CRN.Product.Application.DTOs.Product;
using CRN.Product.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRN.Product.Api.Controllers
{
    /// <summary>
    /// Provides endpoints to manage Product resources.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">Product service.</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all available products.
        /// </summary>
        /// <returns>Returns the list of products.</returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <returns>Returns the requested product.</returns>
        [AllowAnonymous]
        [HttpGet("{id:int}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="dto">Product information.</param>
        /// <returns>Returns the newly created product.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var product = await _productService.CreateProductAsync(dto);

            return CreatedAtRoute(
                "GetProductById",
                new { version = "1", id = product.Id },
                product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateProductDto dto)
        {
            var product = await _productService.UpdateProductAsync(id, dto);

            return Ok(product);
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);

            return Ok(new
            {
                Success = true,
                Message = $"Product with Id {id} has been deleted successfully."
            });
        }
    }
}