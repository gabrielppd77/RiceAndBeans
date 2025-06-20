using Domain.Users;

namespace Domain.Companies
{
    public class Company
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string Path { get; private set; }
        public string? UrlImage { get; private set; }
        public User User { get; private set; }

        public Company()
        {
        }

        public Company(User user, string name)
        {
            Id = Guid.NewGuid();
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

        public void UpdateImage(string? urlImage)
        {
            UrlImage = urlImage;
        }

        private string GeneratePath(string path)
        {
            return path.ToLower().Replace(" ", "-");
        }
    }
}