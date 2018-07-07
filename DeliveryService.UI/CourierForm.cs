namespace DeliveryService.UI
{
    using System;
    using System.Windows.Forms;

    using DeliveryService.BLL.Contract;
    using DeliveryService.DTO;

    public partial class CourierForm : Form
    {
        private readonly ICourierManager courierManager;

        public CourierForm()
        {
            this.InitializeComponent();
        }
        
        public CourierForm(ICourierManager courierManager)
        {
            this.courierManager = courierManager;
            this.InitializeComponent();
            this.FillCourierTable();
        }

        private async void FillCourierTable()
        {
            this.dataGridView1.DataSource = (await this.courierManager.GetAllCouriersAsync()).ToDataTable();
        }

        private void CourierForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private async void button3_Click(object sender, System.EventArgs e)
        {
            try
            {
                var lastName = this.textBox1.Text;
                var name = this.textBox2.Text;
                var patronimyc = this.textBox3.Text;
                var passport = this.textBox7.Text;
                var address = this.textBox4.Text;
                var phone1 = this.textBox5.Text;
                var phone2 = this.textBox6.Text;
                var date = this.dateTimePicker1.Value;

                var courier = new CourierDto(lastName, name, patronimyc, passport, address, phone1, phone2, date);

                await this.courierManager.AddCourierAsync(courier);
                this.FillCourierTable();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentRow == null)
                    throw new Exception("Выберите курьера для редактирования.");

                var lastName = this.textBox1.Text;
                var name = this.textBox2.Text;
                var patronimyc = this.textBox3.Text;
                var passport = this.textBox7.Text;
                var address = this.textBox4.Text;
                var phone1 = this.textBox5.Text;
                var phone2 = this.textBox6.Text;
                var date = this.dateTimePicker1.Value;

                var courierId = long.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var courier = new CourierDto(lastName, name, patronimyc, passport, address, phone1, phone2, date) { CourierId = courierId };

                await this.courierManager.UpdateCourierAsync(courier);
                this.FillCourierTable();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentRow == null)
                    throw new Exception("Выберите курьера для удаления.");

                var lastName = this.textBox1.Text;
                var name = this.textBox2.Text;
                var patronimyc = this.textBox3.Text;
                var passport = this.textBox7.Text;
                var address = this.textBox4.Text;
                var phone1 = this.textBox5.Text;
                var phone2 = this.textBox6.Text;
                var date = this.dateTimePicker1.Value;

                var courierId = long.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var courier = new CourierDto(lastName, name, patronimyc, passport, address, phone1, phone2, date) { CourierId = courierId };

                await this.courierManager.DeleteCourierAsync(courier);
                this.FillCourierTable();
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
                    this.textBox7.Text = this.dataGridView1[4, rowIndex].Value.ToString();
                    this.textBox4.Text = this.dataGridView1[5, rowIndex].Value.ToString();
                    this.textBox5.Text = this.dataGridView1[6, rowIndex].Value.ToString();
                    this.textBox6.Text = this.dataGridView1[7, rowIndex].Value.ToString();
                    this.dateTimePicker1.Value = DateTime.Parse(this.dataGridView1[8, rowIndex].Value.ToString());
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
