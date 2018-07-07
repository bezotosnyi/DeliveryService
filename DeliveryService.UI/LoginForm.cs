namespace DeliveryService.UI
{
    using System.Windows.Forms;

    using DeliveryService.BLL.Contract;

    public partial class LoginForm : Form
    {
        private readonly IUserManager userManager;

        public LoginForm()
        {
            this.InitializeComponent();
        }

        public LoginForm(IUserManager userManager)
        {
            this.userManager = userManager;
            this.InitializeComponent();
        }

        private async void LoginButton_Click(object sender, System.EventArgs e)
        {
            var login = this.loginTextBox.Text;
            var password = this.passwordTextBox.Text;
            if (await this.userManager.LoginAsync(login, password))
            {
                var mainForm = new MainForm { Owner = this };
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(@"Неправильный данные входа. Повторите сново или зарегистрируйтесь в системе.", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegistrationButton_Click(object sender, System.EventArgs e)
        {
            var registrationForm = new RegistrationForm(ManagerFactoryResolver.ManagerFactory.CreateUserManager()) { Owner = this };
            registrationForm.Show();
            this.Hide();
        }
    }
}
