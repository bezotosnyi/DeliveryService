namespace DeliveryService.UI
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    using DeliveryService.BLL.Contract;

    public partial class SelectionForm : Form
    {
        private readonly ISelectionManager selectionManager;

        public SelectionForm()
        {
            this.InitializeComponent();
        }

        public SelectionForm(ISelectionManager selectionManager)
        {
            this.selectionManager = selectionManager;
            this.InitializeComponent();
            this.FillCourierTable();
        }

        private async void FillCourierTable()
        {
            this.dataGridView1.DataSource =
                (await this.selectionManager.DeliveryManager.CourierManager.GetAllCouriersAsync()).ToDataTable();
        }
        private void SelectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.dataGridView1.CurrentRow == null)
                    throw new Exception("Выберите курьера.");

                var courierId = long.Parse(this.dataGridView1[0, this.dataGridView1.CurrentRow.Index].Value.ToString());
                var courier = await this.selectionManager.DeliveryManager.CourierManager.GetCourierByIdAsync(courierId);

                var result = (await this.selectionManager.GetAllDeliveriesByCourierAsync(courier)).ToList();

                if (!result.Any())
                    throw new Exception("У этого курьера еще нет доставок.");

                this.dataGridView2.DataSource = result.ToDataTable();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
