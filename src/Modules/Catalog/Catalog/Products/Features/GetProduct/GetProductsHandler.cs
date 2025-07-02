using Shared.Pagination;

namespace Catalog.Products.Features.GetProduct
{
    public record GetProductsQuery(PaginationRequest PaginationRequest) // é o query object que será envidado para o mediator para buscar os produtos e retornar o resultado
        : IQuery<GetProductsResult>;

    public record GetProductsResult(PaginationResult<ProductDto> Products); //resultado
    public class GetProductsHandler(CatalogDbContext dbContext)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            //get all products using dbcontext 
            //return the result
            
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            
            var totalCount = await dbContext.Products.LongCountAsync(cancellationToken);

            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            //mapping product entity to productDto using Mapster

            var productDtos = products.Adapt<List<ProductDto>>();
            
            return new GetProductsResult(
                new PaginationResult<ProductDto>(
                    pageIndex, 
                    pageSize, 
                    totalCount, 
                    productDtos) //retorna o resultado paginado
                );  
        }

        
    }
}
