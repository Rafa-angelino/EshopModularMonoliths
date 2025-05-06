namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdResponse(ProductDto Product); //resultado
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id)); //envia a query para o mediator
                var response = result.Adapt<GetProductByIdResponse>(); //mapeia o resultado para o response
                return Results.Ok(response); //retorna o resultado
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Busca um produto por ID")
            .WithDescription("Busca um produto cadastrado no sistema pelo ID fornecido na url.");
        }
    }
}
