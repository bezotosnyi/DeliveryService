namespace DeliveryService.UI
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using DeliveryService.BLL.Contract;
    using DeliveryService.Domain;

    public partial class DeliveryForm : Form
    {
        private readonly IDeliveryManager deliveryManager;

        public DeliveryForm()
        {
            this.InitializeComponent();
        }

        public DeliveryForm(IDeliveryManager deliveryManager)
        {
            this.deliveryManager = deliveryManager;
            this.InitializeComponent();
        }

        private async Task FillCustomerTableAsync()
        {
            this.dataGridView1.DataSource =
                (await this.deliveryManager.CustomerManager.GetAllCustomersAsync()).ToDataTable();
        }

        private async Task FillCourierTableAsync()
        {
            this.dataGridView2.DataSource =
                (await this.deliveryManager.CourierManager.GetAllCouriersAsync()).ToDataTable();
        }

        private async Task FillTypeOfCargoTableAsync()
        {
            this.dataGridView4.DataSource =
                (await this.deliveryManager.TypeOfCargoManager.GetAllTypeOfCargoesAsync()).ToDataTable();
        }

        private async Task FillTransportTableAsync()
        {
            this.dataGridView3.DataSource =
                (await this.deliveryManager.TransportManager.GetAllTransportsAsync()).ToDataTable();
        }

        private async Task FillDeliveryTableAsync()
        {
            this.dataGridView5.DataSource =
                (await this.deliveryManager.GetAllDeliveriesDtoAsync()).ToDataTable();
        }

        private void DeliveryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Show();
        }

        private async void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                try
                {
                    var rowIndex = this.dataGridView1.CurrentRow.Index;
                    var customerId = long.Parse(this.dataGridView1[0, rowIndex].Value.ToString());
                    var customer = await this.deliveryManager.CustomerManager.GetCustomerByIdAsync(customerId);
                    this.textBox4.Text = customer.ToString();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private async void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView2.CurrentRow != null)
            {
                try
                {
                    var rowIndex = this.dataGridView2.CurrentRow.Index;
                    var courierId = long.Parse(this.dataGridView2[0, rowIndex].Value.ToString());
                    var courier = await this.deliveryManager.CourierManager.GetCourierDtoByIdAsync(courierId);
                    this.textBox1.Text = courier.ToString();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private async void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView4.CurrentRow != null)
            {
                try
                {
                    var rowIndex = this.dataGridView4.CurrentRow.Index;
                    var typeOfCargoId = long.Parse(this.dataGridView4[0, rowIndex].Value.ToString());
                    var typeOfCargo = await this.deliveryManager.TypeOfCargoManager.GetTypeOfCargoByIdAsync(typeOfCargoId);
                    this.textBox7.Text = typeOfCargo.ToString();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private async void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView3.CurrentRow != null)
            {
                try
                {
                    var rowIndex = this.dataGridView3.CurrentRow.Index;
                    var transportId = long.Parse(this.dataGridView3[0, rowIndex].Value.ToString());
                    var transport = await this.deliveryManager.TransportManager.GetTransportByIdAsync(transportId);
                    this.textBox2.Text = transport.ToString();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var rowIndex = this.dataGridView1.CurrentRow.Index;
                var customerId = long.Parse(this.dataGridView1[0, rowIndex].Value.ToString());
                rowIndex = this.dataGridView2.CurrentRow.Index;
                var courierId = long.Parse(this.dataGridView2[0, rowIndex].Value.ToString());
                rowIndex = this.dataGridView4.CurrentRow.Index;
                var typeOfCargoId = long.Parse(this.dataGridView4[0, rowIndex].Value.ToString());
                rowIndex = this.dataGridView3.CurrentRow.Index;
                var transportId = long.Parse(this.dataGridView3[0, rowIndex].Value.ToString());

                var departure = new Departure
                                    {
                                        CourierId = courierId,
                                        DateOfDeparture = this.dateTimePicker1.Value,
                                        TransportId = transportId,
                                        PaymentForMileage = decimal.Parse(this.textBox3.Text)
                                    };

                var delivery = new Delivery
                                   {
                                       CustomerId = customerId,
                                       Departure = departure,
                                       DateOfDelivery = this.dateTimePicker2.Value,
                                       Mileage = int.Parse(this.textBox5.Text),
                                       CargeName = this.textBox6.Text,
                                       TypeOfCargoId = typeOfCargoId,
                                       CostOfCargo = decimal.Parse(this.textBox8.Text)
                                   };
                
                await this.deliveryManager.AddDeliveryAsync(delivery);
                await this.FillDeliveryTableAsync();
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
                if (this.dataGridView5.CurrentRow == null)
                    throw new Exception("Выберите доставку для редактирования.");

                var rowIndex = this.dataGridView1.CurrentRow.Index;
                var customerId = long.Parse(this.dataGridView1[0, rowIndex].Value.ToString());
                rowIndex = this.dataGridView2.CurrentRow.Index;
                var courierId = long.Parse(this.dataGridView2[0, rowIndex].Value.ToString());
                rowIndex = this.dataGridView4.CurrentRow.Index;
                var typeOfCargoId = long.Parse(this.dataGridView4[0, rowIndex].Value.ToString());
                rowIndex = this.dataGridView3.CurrentRow.Index;
                var transportId = long.Parse(this.dataGridView3[0, rowIndex].Value.ToString());
                rowIndex = this.dataGridView5.CurrentRow.Index;
                var deliveryId = long.Parse(this.dataGridView5[0, rowIndex].Value.ToString());


                var delivery = await this.deliveryManager.GetDeliveryByIdAsync(deliveryId);
                var departure = await this.deliveryManager.DepartureManager.GetDepartureByIdAsync(delivery.DepartureId);

                departure.CourierId = courierId;
                departure.DateOfDeparture = this.dateTimePicker1.Value;
                departure.TransportId = transportId;
                departure.PaymentForMileage = decimal.Parse(this.textBox3.Text);

                delivery.Departure = departure;
                delivery.CustomerId = customerId;
                delivery.DateOfDelivery = this.dateTimePicker2.Value;
                delivery.Mileage = int.Parse(this.textBox5.Text);
                delivery.CargeName = this.textBox6.Text;
                delivery.TypeOfCargoId = typeOfCargoId;
                delivery.CostOfCargo = decimal.Parse(this.textBox8.Text);

                await this.deliveryManager.UpdateDeliveryAsync(delivery);
                await this.FillDeliveryTableAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView5.CurrentRow == null)
                    throw new Exception("Выберите доставку для удаления.");

                var rowIndex = this.dataGridView5.CurrentRow.Index;
                var deliveryId = long.Parse(this.dataGridView5[0, rowIndex].Value.ToString());
                var delivery = await this.deliveryManager.GetDeliveryByIdAsync(deliveryId);

                await this.deliveryManager.DeleteDeliveryAsync(delivery);
                await this.FillDeliveryTableAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.StackTrace, @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridView5.CurrentRow != null)
            {
                try
                {
                    var rowIndex = this.dataGridView5.CurrentRow.Index;
                    var deliveryId = long.Parse(this.dataGridView5[0, rowIndex].Value.ToString());
                    var delivery = await this.deliveryManager.GetDeliveryDtoByIdAsync(deliveryId);
                    this.textBox4.Text = delivery.Customer;
                    this.textBox1.Text = delivery.Courier;
                    this.textBox7.Text = delivery.TypeOfCargo;
                    this.textBox2.Text = delivery.Transport;
                    this.dateTimePicker2.Value = delivery.DateOfDelivery;
                    this.textBox5.Text = delivery.Mileage.ToString();
                    this.textBox6.Text = delivery.CargeName;
                    this.textBox8.Text = delivery.CostOfCargo.ToString();
                    this.dateTimePicker1.Value = delivery.DateOfDeparture;
                    this.textBox3.Text = delivery.PaymentForMileage.ToString();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private async void DeliveryForm_Load(object sender, EventArgs e)
        {
            await this.FillCustomerTableAsync();
            await this.FillCourierTableAsync();
            await this.FillTypeOfCargoTableAsync();
            await this.FillTransportTableAsync();
            await this.FillDeliveryTableAsync();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var customerForm = new CustomerForm(ManagerFactoryResolver.ManagerFactory.CreateCustomerManager()) { Owner = this };
            customerForm.Show();
            this.Hide();
        }

        private void курьерыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var customerForm = new CourierForm(ManagerFactoryResolver.ManagerFactory.CreateCourierManager()) { Owner = this };
            customerForm.Show();
            this.Hide();
        }

        private void типыГрузовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cargoForm = new CargoForm(ManagerFactoryResolver.ManagerFactory.CreateTypeOfCargoManager()) { Owner = this };
            cargoForm.Show();
            this.Hide();
        }

        private void транспортToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var transportForm = new TransportForm(ManagerFactoryResolver.ManagerFactory.CreateTransportManager()) { Owner = this };
            transportForm.Show();
            this.Hide();
        }
    }
}
