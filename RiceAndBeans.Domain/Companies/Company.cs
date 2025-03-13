namespace RiceAndBeans.Domain.Companies
{
    public class Company
    {
        public Company(Guid userId, string name)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Path = GeneratePath(name);
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public string Name { get; private set; }
        public string Path { get; private set; }

        public void SetName(string name)
        {
            Name = name;
            Path = GeneratePath(name);
        }

        private string GeneratePath(string path)
        {
            return path.ToLower().Replace(" ", "-");
        }
    }
}
