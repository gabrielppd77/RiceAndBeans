using Domain.Common.Entities;
using Domain.Users;

namespace Domain.Companies
{
    public class Company : Entity
    {
        public Guid UserId { get; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string Path { get; private set; }
        public User? User { get; private set; }

        protected Company()
        {
        }

        public Company(User user, string name)
        {
            UserId = user.Id;
            Name = name;
            Path = GeneratePath(name);
            User = user;
        }

        public void UpdateFormFields(string name, string? description, string path)
        {
            Name = name;
            Description = description;
            Path = path;
        }

        private string GeneratePath(string path)
        {
            return path.ToLower().Replace(" ", "-");
        }
    }
}