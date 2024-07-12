using product_management.DAL;
using product_management.DTO.Models;

namespace product_management.BLL
{
    public class ProductBLL
    {
        private readonly ProductDAL _productDAL;

        public ProductBLL(ProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            return await _productDAL.GetProducts();
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            return await _productDAL.GetProductById(id);
        }

        public async Task<int> InsertProduct(ProductInsertDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Nome do produto é obrigatório.");

            if (product.Price <= 0)
                throw new ArgumentException("Preço do produto deve ser maior que zero.");

            return await _productDAL.InsertProduct(product);
        }

        public async Task<bool> UpdateProduct(int id, ProductUpdateDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Nome do produto é obrigatório.");

            if (product.Price <= 0)
                throw new ArgumentException("Preço do produto deve ser maior que zero.");

            return await _productDAL.UpdateProduct(id, product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productDAL.DeleteProduct(id);
        }
    }
}
