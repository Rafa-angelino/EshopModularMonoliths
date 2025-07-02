using Shared.Pagination;

namespace Catalog.Products.Features.GetProduct
{
    public record GetProductResponse(PaginationResult<ProductDto> Products); //resultado
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery(request)); // envia a query  para o mediador

                var response = result.Adapt<GetProductResponse>();

                return Results.Ok(response); // 200 OK
            })
            .WithName("GetProducts")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Busca todos os produtos")
            .WithDescription("Busca todos os produtos cadastrados no sistema.");
        }
    }
}
