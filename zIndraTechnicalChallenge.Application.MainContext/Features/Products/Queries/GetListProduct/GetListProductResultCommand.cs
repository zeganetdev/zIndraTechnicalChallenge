namespace zIndraTechnicalChallenge.Application.MainContext.Features.Products.Queries.GetListProduct
{
    public class GetListProductResultCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
