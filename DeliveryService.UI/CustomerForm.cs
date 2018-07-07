namespace DeliveryService.UI
{
    using System;
    using System.Windows.Forms;

    using DeliveryService.BLL.Contract;
    using DeliveryService.DTO;

    public partial class CustomerForm : Form
    {
        private readonly ICustomerManager customerManager;

        public CustomerForm()
        {
            this.InitializeComponent();
        }

        public CustomerForm(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;
            this.InitializeComponent();
            this.FillCustomerTable();
        }

        private async void FillCustomerTable()
        {
            this.dataGridView1.DataSource = (await this.customerManager.GetAllCustomersAsync()).ToDataTable();
        }

        private void CustomerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var lastName = this.textBox1.Text;
                var name = this.textBox2.Text;
                var patronimyc = this.textBox3.Text;
                var address = this.textBox4.Text;
                var phone1 = this.textBox5.Text;
                var phone2 = this.textBox6.Text;

                var customer = new CustomerDto(lastName, name, patronimyc, address, phone1, phone2);

                await this.customerManager.AddCustomerAsync(customer);
                this.FillCustomerTable();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentRow == null)
                    throw new Exception("Выберите клиента для редактирования.");

                var lastName = this.textBox1.Text;
                var name = this.textBox2.Text;
                var patronimyc = this.textBox3.Text;
                var address = this.textBox4.Text;
                var phone1 = this.textBox5.Text;
                var phone2 = this.textBox6.Text;

                var customerId = long.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var customer = new CustomerDto(lastName, name, patronimyc, address, phone1, phone2) { CustomerId = customerId };

                await this.customerManager.UpdateCustomerAsync(customer);
                this.FillCustomerTable();             
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentRow == null)
                    throw new Exception("Выберите клиента для удаления.");

                var lastName = this.textBox1.Text;
                var name = this.textBox2.Text;
                var patronimyc = this.textBox3.Text;
                var address = this.textBox4.Text;
                var phone1 = this.textBox5.Text;
                var phone2 = this.textBox6.Text;

                var customerId = long.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var customer = new CustomerDto(lastName, name, patronimyc, address, phone1, phone2) { CustomerId = customerId };

                await this.customerManager.DeleteCustomerAsync(customer);
                this.FillCustomerTable();             
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                try
                {
                    var rowIndex = this.dataGridView1.CurrentRow.Index;
                    this.textBox1.Text = this.dataGridView1[1, rowIndex].Value.ToString();
                    this.textBox2.Text = this.dataGridView1[2, rowIndex].Value.ToString();
                    this.textBox3.Text = this.dataGridView1[3, rowIndex].Value.ToString();
                    this.textBox4.Text = this.dataGridView1[4, rowIndex].Value.ToString();
                    this.textBox5.Text = this.dataGridView1[5, rowIndex].Value.ToString();
                    this.textBox6.Text = this.dataGridView1[6, rowIndex].Value.ToString();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}