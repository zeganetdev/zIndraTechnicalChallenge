namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Commands.GetProduct
{
    public class GetProductResultCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
    }
}
