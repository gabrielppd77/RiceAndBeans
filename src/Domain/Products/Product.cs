using Domain.Categories;
using Domain.Companies;

namespace Domain.Products;

public class Product
{
    public Guid Id { get; }
    public Guid CompanyId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? UrlImage { get; private set; }
    public int Position { get; private set; }
    public decimal Price { get; private set; }
    public Company? Company { get; private set; }
    public Category? Category { get; private set; }

    protected Product()
    {
    }

    public Product(Guid companyId, Guid? categoryId, string name, string? description, int position, decimal price)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Position = position;
        Price = price;
    }

    //public void UpdateFormFields(Guid? categoryId, string name, string? description, decimal price)
    //{
    //    CategoryId = categoryId;
    //    Name = name;
    //    Description = description;
    //    Price = price;
    //}

    //public void UpdateImage(string? urlImage)
    //{
    //    UrlImage = urlImage;
    //}

    //public void ChangePosition(int newPosition)
    //{
    //    Position = newPosition;
    //}
}