namespace Catalog.Products.Features.GetProduct
{
    public record GetProductsQuery() // é o query object que será envidado para o mediator para buscar os produtos e retornar o resultado
        : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<ProductDto> Products); //resultado
    public class GetProductsHandler(CatalogDbContext dbContext)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            //get all products using dbcontext 
            //return the result 

            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)   
                .ToListAsync(cancellationToken);

            //mapping product entity to productDto using Mapster

            var productDtos = products.Adapt<List<ProductDto>>();
            
            return new GetProductsResult(productDtos);  
        }

        
    }
}
