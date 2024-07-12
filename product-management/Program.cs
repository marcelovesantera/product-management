using Microsoft.Data.Sqlite;
using product_management.BLL;
using product_management.DAL;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
var connectionString = connectionStringBuilder.ToString();
var connection = new SqliteConnection(connectionString);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowsAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    });
});

connection.Open();

// Cria a tabela de produtos, se não existir
var command = connection.CreateCommand();
command.CommandText = @"
    CREATE TABLE IF NOT EXISTS Products (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Name TEXT NOT NULL,
        Price DECIMAL NOT NULL,
        Description TEXT
    );
";
command.ExecuteNonQuery();

builder.Services.AddSingleton<IDbConnection>(connection);

builder.Services.AddControllers();
builder.Services.AddScoped<ProductDAL>();
builder.Services.AddScoped<ProductBLL>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API de Produtos", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
