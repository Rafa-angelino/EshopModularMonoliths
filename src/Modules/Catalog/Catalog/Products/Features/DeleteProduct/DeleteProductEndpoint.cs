namespace Catalog.Products.Features.DeleteProduct
{
    //public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool isSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id)); // envia o comando para o mediador
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response); // 200 OK
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Deleta um produto")
            .WithDescription("Deleta um produto com o ID fornecido na url.");
        }
    }
}
