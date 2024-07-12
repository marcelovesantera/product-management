using Dapper;
using product_management.DTO.Models;
using System.Data;

namespace product_management.DAL
{
    public class ProductDAL
    {
        private readonly IDbConnection _dbConnection;

        public ProductDAL(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            string query = "SELECT * FROM Products";

            return await _dbConnection.QueryAsync<ProductDTO>(query);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            string query = "SELECT * FROM Products WHERE Id = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<ProductDTO>(query, new { Id = id });
        }

        public async Task<int> InsertProduct(ProductInsertDTO product)
        {
            string query = @"
                INSERT INTO Products (Name, Price, Description) 
                VALUES (@Name, @Price, @Description);
                SELECT last_insert_rowid();";

            return await _dbConnection.ExecuteScalarAsync<int>(query, product);
        }

        public async Task<bool> UpdateProduct(int id, ProductUpdateDTO product)
        {
            var updateFields = new List<string>();

            if (!string.IsNullOrWhiteSpace(product.Name))
            {
                updateFields.Add($"Name = @Name");
            }

            if (product.Price > 0)
            {
                updateFields.Add($"Price = @Price");
            }

            if (!string.IsNullOrWhiteSpace(product.Description))
            {
                updateFields.Add($"Description = @Description");
            }

            string setClause = string.Join(", ", updateFields);

            string query = $@"
                    UPDATE Products
                    SET {setClause}
                    WHERE Id = {id};";

            var rowsAffected = await _dbConnection.ExecuteAsync(query, product);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            string query = @"
                DELETE FROM Products
                WHERE Id = @Id;";

            var rowsAffected = await _dbConnection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
