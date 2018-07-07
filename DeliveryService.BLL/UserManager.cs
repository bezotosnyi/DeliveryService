namespace DeliveryService.BLL
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using DeliveryService.BLL.Contract;
    using DeliveryService.BLL.Validator;
    using DeliveryService.DAL.Contract;
    using DeliveryService.Domain;

    public class UserManager : IUserManager
    {
        private readonly IRepository<User> userRepository;

        public UserManager(IRepository<User> userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        ~UserManager()
        {
            this.userRepository?.Dispose();
        }

        public Task<bool> LoginAsync(string login, string password)
        {
            return this.userRepository.Entity.AnyAsync(x => x.Login == login && x.Password == password);
        }

        public async Task RegistrationAsync(string name, string lastName, string login, string password)
        {
            var user = new User { Name = name, LastName = lastName, Login = login, Password = password };

            var validate = DataAnnotationsValidator.Validate(user);
            if (!validate.Success) throw new ArgumentException(validate.ErrorMessage);

            if (await this.userRepository.Entity.AnyAsync(
                x => x.Name == user.Name && x.LastName == user.LastName && x.Login == user.Login
                     && x.Password == user.Password))
            {
                throw new ArgumentException("Такой пользователь уже существует.");
            }
            
            this.userRepository.Entity.Add(user); // добавление нового пользователя
            await this.userRepository.SaveChangesAsync();
        }
    }
}