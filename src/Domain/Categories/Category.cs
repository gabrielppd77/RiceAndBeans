using Domain.Companies;

namespace Domain.Categories;

public class Category
{
    public Guid Id { get; }
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; }
    public Company? Company { get; private set; }

    protected Category()
    {
    }

    public Category(Guid companyId, string name)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name;
    }
}