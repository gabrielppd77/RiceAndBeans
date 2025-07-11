using Domain.Companies;

namespace Domain.Categories;

public class Category
{
    public Guid Id { get; }
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; }
    public int Position { get; private set; }
    public Company? Company { get; private set; }

    protected Category()
    {
    }

    public Category(Guid companyId, string name, int position)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name;
        Position = position;
    }

    public void UpdateFormFields(string name)
    {
        Name = name;
    }

    public void ChangePosition(int newPosition)
    {
        Position = newPosition;
    }
}