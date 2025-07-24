using Domain.Categories;
using Domain.Common.Entities;
using Domain.Common.Positions;
using Domain.Companies;

namespace Domain.Products;

public class Product : Entity, IPositionable
{
    public Guid CompanyId { get; private set; }
    public Guid? CategoryId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int Position { get; private set; }
    public decimal Price { get; private set; }
    public Company? Company { get; private set; }
    public Category? Category { get; private set; }

    protected Product()
    {
    }

    public Product(Guid companyId, Guid? categoryId, string name, string? description, int position, decimal price)
    {
        CompanyId = companyId;
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Price = price;
        ChangePosition(position);
    }

    public void UpdateFormFields(Guid? categoryId, string name, string? description, decimal price)
    {
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Price = price;
    }

    public void ChangePosition(int newPosition)
    {
        Position = newPosition;
    }
}