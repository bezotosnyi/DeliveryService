namespace DeliveryService.UI
{
    using System;
    using System.Windows.Forms;

    using DeliveryService.BLL.Contract;
    using DeliveryService.DTO;

    public partial class TransportForm : Form
    {
        private readonly ITransportManager transporManager;

        public TransportForm()
        {
            this.InitializeComponent();
        }

        public TransportForm(ITransportManager transporManager)
        {
            this.transporManager = transporManager;
            this.InitializeComponent();
            this.FillTransportTable();
        }

        private async void FillTransportTable()
        {
            this.dataGridView1.DataSource = (await this.transporManager.GetAllTransportsAsync()).ToDataTable();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var number = this.textBox1.Text;
                var model = this.textBox2.Text;
                var date = this.dateTimePicker1.Value;

                var transportDto = new TransportDto(number, model, date);

                await this.transporManager.AddTransportAsync(transportDto);
                this.FillTransportTable();
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
                    throw new Exception("Выберите транспорт для редактирования.");

                var number = this.textBox1.Text;
                var model = this.textBox2.Text;
                var date = this.dateTimePicker1.Value;

                var transportId = long.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var transportDto = new TransportDto(number, model, date) { TransportId = transportId };

                await this.transporManager.UpdateTransportAsync(transportDto);
                this.FillTransportTable();
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
                    throw new Exception("Выберите транспорт для удаления.");

                var number = this.textBox1.Text;
                var model = this.textBox2.Text;
                var date = this.dateTimePicker1.Value;

                var transportId = long.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var transportDto = new TransportDto(number, model, date) { TransportId = transportId };

                await this.transporManager.DeleteTransportAsync(transportDto);
                this.FillTransportTable();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TransportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
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
                    this.dateTimePicker1.Value = DateTime.Parse(this.dataGridView1[3, rowIndex].Value.ToString());
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
