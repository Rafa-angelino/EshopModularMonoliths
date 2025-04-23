namespace Catalog.Products.Features.GetProductById
{
    public record class GetProductByIdQuery(Guid Id) // é o query object que será envidado para o mediator para buscar os produtos e retornar o resultado
        : IQuery<GetProductByIdResult>;

    public record class GetProductByIdResult(ProductDto Product); //resultado
    public class GetProductByIdHandler(CatalogDbContext dbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            //get product by id using dbcontext
            //return the result
            var product = await dbContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken) ?? throw new Exception($"Produto com  id {query.Id} não encontrado");

            //mapping product entity to productDto using Mapster
            var productDto = product.Adapt<ProductDto>();
            return new GetProductByIdResult(productDto);
        }
    }
    {
    }
}
