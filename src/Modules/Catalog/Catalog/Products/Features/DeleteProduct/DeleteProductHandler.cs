namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId)
        : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool isSuccess); //resultado
    public class DeleteProductHandler(CatalogDbContext dbContext)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            //delete Product Entity from commando object
            //save to database
            //return the result

            var product = await dbContext.Products
                .FindAsync([command.ProductId], cancellationToken)
                ?? throw new Exception($"Produto não encontrado com o id {command.ProductId}");

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
