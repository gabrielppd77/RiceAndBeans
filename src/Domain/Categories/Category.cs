using Domain.Common.Entities;
using Domain.Common.Positions;
using Domain.Companies;

namespace Domain.Categories;

public class Category : Entity, IPositionable
{
    public Guid CompanyId { get; private set; }
    public string Name { get; private set; }
    public int Position { get; private set; }
    public Company? Company { get; private set; }

    protected Category()
    {
    }

    public Category(Guid companyId, string name, int position)
    {
        CompanyId = companyId;
        Name = name;
        ChangePosition(position);
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