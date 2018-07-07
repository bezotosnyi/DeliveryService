namespace DeliveryService.BLL.Contract
{
    using System.Threading.Tasks;

    public interface IUserManager
    {
        Task<bool> LoginAsync(string login, string password);

        Task RegistrationAsync(string name, string lastName, string login, string password);
    }
}