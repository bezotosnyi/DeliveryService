namespace DeliveryService.UI
{
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var customerForm = new CustomerForm(ManagerFactoryResolver.ManagerFactory.CreateCustomerManager()) { Owner = this};
            customerForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            var customerForm = new CourierForm(ManagerFactoryResolver.ManagerFactory.CreateCourierManager()) { Owner = this };
            customerForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            var cargoForm = new CargoForm(ManagerFactoryResolver.ManagerFactory.CreateTypeOfCargoManager()) { Owner = this };
            cargoForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            var transportForm = new TransportForm(ManagerFactoryResolver.ManagerFactory.CreateTransportManager()) { Owner = this };
            transportForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            var deliveryForm = new DeliveryForm(ManagerFactoryResolver.ManagerFactory.CreateDeliveryManager()) { Owner = this };
            deliveryForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            var selectionForm = new SelectionForm(ManagerFactoryResolver.ManagerFactory.CreateSelectionManager()) { Owner = this };
            selectionForm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, System.EventArgs e)
        {
            var reportForm = new ReportForm(ManagerFactoryResolver.ManagerFactory.CreateReportManager()) { Owner = this };
            reportForm.Show();
            this.Hide();
        }
    }
}
