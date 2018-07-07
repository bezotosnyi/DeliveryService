namespace DeliveryService.UI
{
    using System;
    using System.Windows.Forms;

    using DeliveryService.BLL;
    using DeliveryService.BLL.Contract;
    using DeliveryService.DAL;
    using DeliveryService.DAL.Contract;

    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            IRepositoryFactory repositoryFactory = new RepositoryFactory();
            IManagerFactory managerFactory = new ManagerFactory(repositoryFactory);
            new ManagerFactoryResolver(managerFactory);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(ManagerFactoryResolver.ManagerFactory.CreateUserManager()));

            // Application.Run(new MainForm());
        }
    }
}
