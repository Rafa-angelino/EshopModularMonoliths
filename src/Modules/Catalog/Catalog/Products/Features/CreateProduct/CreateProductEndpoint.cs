namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductRequest(ProductDto Product);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command); // envia o comando para o mediador

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{result.Id}", response); // 201 Created
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Cria um novo produto")
            .WithDescription("Cria um novo produto com as informações fornecidas.");
        }
    }
}
