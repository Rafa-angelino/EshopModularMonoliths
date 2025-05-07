namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto Product)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess); //resultado
    public class UpdateProductHandler(CatalogDbContext dbContext)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            //update Product Entity from commando object
            //save to database
            //return the result

            var product = await dbContext.Products
                .FindAsync([command.Product.Id], cancellationToken) 
                ?? throw new Exception($"Produto não encontrado com o id {command.Product.Id}");

            UpdateProductWithNewValues(product, command.Product);

            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);    

            return new UpdateProductResult(true);
        }

        private static void UpdateProductWithNewValues(Product product, ProductDto productDto)
        {
            product.Update(
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price
                );
        }
    }
}
