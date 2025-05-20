namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDto Product)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id); //resultado

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Nome do produto é obrigatório.");
            RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Categoria do produto é obrigatória.");
            RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("Imagem do produto é obrigatória.");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Preço do produto deve ser maior que zero.");
        }
    }
    public class CreateProductCommandHandler(CatalogDbContext dbContext)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create Product Entity from commando object
            //save to database
            //return the result  

            var product = CreateNewProduct(command.Product);

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id); 

        }

        private static Product CreateNewProduct(ProductDto productDto)
        {
            var product = Product.Create(
                Guid.NewGuid(),
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price
                );
            return product;
        }
    }

    
}
