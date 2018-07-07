namespace DeliveryService.UI
{
    using System;
    using System.Windows.Forms;

    using DeliveryService.BLL.Contract;
    using DeliveryService.DTO;

    public partial class CargoForm : Form
    {
        private readonly ITypeOfCargoManager typeOfCargoManager;

        public CargoForm()
        {
            this.InitializeComponent();
        }

        public CargoForm(ITypeOfCargoManager typeOfCargoManager)
        {
            this.typeOfCargoManager = typeOfCargoManager;
            this.InitializeComponent();
            this.FillCargoTable();
        }

        private void CargoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private async void FillCargoTable()
        {
            this.dataGridView1.DataSource = (await this.typeOfCargoManager.GetAllTypeOfCargoesAsync()).ToDataTable();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var name = this.textBox1.Text;
                var description = this.textBox2.Text;

                var typeOfCargoDto = new TypeOfCargoDto(name, description);

                await this.typeOfCargoManager.AddTypeOfCargoAsync(typeOfCargoDto);
                this.FillCargoTable();
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

                var name = this.textBox1.Text;
                var description = this.textBox2.Text;

                var typeOfCargoId = long.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var typeOfCargoDto = new TypeOfCargoDto(name, description) { TypeOfCargoId = typeOfCargoId };

                await this.typeOfCargoManager.DeleteTypeOfCargoAsync(typeOfCargoDto);
                this.FillCargoTable();
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
                    throw new Exception("Выберите тип груза для редактирования.");

                var name = this.textBox1.Text;
                var description = this.textBox2.Text;

                var typeOfCargoId = long.Parse(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                var typeOfCargoDto = new TypeOfCargoDto(name, description) { TypeOfCargoId = typeOfCargoId };

                await this.typeOfCargoManager.UpdateTypeOfCargoAsync(typeOfCargoDto);
                this.FillCargoTable();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                try
                {
                    var rowIndex = this.dataGridView1.CurrentRow.Index;
                    this.textBox1.Text = this.dataGridView1[1, rowIndex].Value.ToString();
                    this.textBox2.Text = this.dataGridView1[2, rowIndex].Value.ToString();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
