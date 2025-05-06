namespace Catalog.Products.Features.GetProductByCategory
{
    //public record GetProductByCategoryRequest(string Category); // é o query object que será envidado para o mediator para buscar os produtos e retornar o resultado
    public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products); //resultado
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category)); // envia a query para o mediador
                var response = result.Adapt<GetProductByCategoryResponse>(); //mapeia o resultado para o response
                return Results.Ok(response); //retorna o resultado
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Busca produtos por categoria")
            .WithDescription("Busca produtos cadastrados no sistema pela categoria fornecida na url.");
        }
    }
}
