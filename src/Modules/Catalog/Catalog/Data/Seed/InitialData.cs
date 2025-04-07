namespace Catalog.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(Guid.NewGuid(), "Iphone 16", ["Category 1"], "Description 1", "ImageFile", 500),
            Product.Create(Guid.NewGuid(), "Samsung S25", ["Category 2"], "Description 2", "ImageFile", 400),
            Product.Create(Guid.NewGuid(), "HyperV", ["Category 3"], "Description 3", "ImageFile", 300)
        };
    }
}
