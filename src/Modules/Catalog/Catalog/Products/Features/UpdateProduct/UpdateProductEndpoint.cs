namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductRequest(ProductDto Product);
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command); // envia o comando para o mediador
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response); // 200 OK
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Atualiza um produto")
            .WithDescription("Atualiza um produto com as informações fornecidas.");

        }
    }
}
