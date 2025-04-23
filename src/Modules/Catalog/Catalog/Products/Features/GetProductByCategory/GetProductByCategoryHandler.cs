
namespace Catalog.Products.Features.GetProductByCategory
{
    public record class GetProductByCategoryQuery(string Category) // é o query object que será envidado para o mediator para buscar os produtos e retornar o resultado
        : IQuery<GetProductByCategoryResult>;

    public record class GetProductByCategoryResult(IEnumerable<ProductDto> Products); //resultado
    public class GetProductByCategoryHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            //get products by category using dbcontext
            //return the result

            var products = await dbContext.Products
                .AsNoTracking()
                .Where(p => p.Category.Contains(query.Category))
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            //mapping product entity to productDto using Mapster
            var productDtos = products.Adapt<List<ProductDto>>();

            return new GetProductByCategoryResult(productDtos);
        }
    }
}
