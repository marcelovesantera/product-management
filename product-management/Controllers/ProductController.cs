using Microsoft.AspNetCore.Mvc;
using product_management.BLL;
using product_management.DTO.Models;

namespace product_management.API.Controllers
{
    /// <summary>
    /// Controller para operações relacionadas a produtos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductBLL _productBLL;

        public ProductController(ProductBLL productBLL)
        {
            _productBLL = productBLL;
        }

        /// <summary>
        /// Retorna todos os produtos.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productBLL.GetProducts();

                if(products == null)
                    return NotFound("Erro ao buscar todos os produtos.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        /// <summary>
        /// Retorna um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>O produto correspondente ao ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productBLL.GetProductById(id);

                if (product == null)
                    return NotFound("Produto não encontrado.");

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Insere um novo produto.
        /// </summary>
        /// <param name="product">Dados do produto a ser inserido.</param>
        /// <returns>ID do produto inserido.</returns>
        [HttpPost]
        public async Task<IActionResult> InsertProduct(ProductInsertDTO product)
        {
            try
            {
                var productId = await _productBLL.InsertProduct(product);

                if(productId < 0)
                    return BadRequest("Erro ao salvar produto.");

                return Ok(productId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um produto existente pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="product">Novos dados do produto.</param>
        /// <returns>Indica se a operação de atualização foi bem-sucedida.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDTO product)
        {
            try
            {
                var success = await _productBLL.UpdateProduct(id, product);

                if (!success)
                    return BadRequest("Erro ao atualizar produto.");

                return Ok(success);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto a ser excluído.</param>
        /// <returns>Mensagem indicando o resultado da operação.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var success = await _productBLL.DeleteProduct(id);

                if (!success)
                    return NotFound("Produto não encontrado.");

                return Ok("Produto excluído com sucesso.");                    
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
