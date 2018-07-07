namespace DeliveryService.UI
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    using DeliveryService.BLL.Contract;

    public partial class ReportForm : Form
    {
        private readonly IReportManager reportManager;

        public ReportForm(IReportManager reportManager)
        {
            this.reportManager = reportManager;
            this.InitializeComponent();
        }

        private void ReportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private async void ReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                var report = (await this.reportManager.GetFinanceReportAsync()).ToList();

                if (!report.Any())
                    throw new Exception("Нет данных для отчета.");

                this.dataGridView1.DataSource = report.ToDataTable();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Owner.Show();
                this.Close();
            }
        }
    }
}
