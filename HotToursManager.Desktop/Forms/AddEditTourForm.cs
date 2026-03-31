using HotToursManager.Models;
using HotToursManager.Services.Contracts;

namespace HotToursManager.Desktop
{
    public partial class AddEditTourForm : Form
    {
        private ITourService tourService;
        private Tour currentTour;
        private ErrorProvider errorProvider;
        private int? editingId;

        private TextBox txtDestination;
        private DateTimePicker dtpDeparture;
        private TextBox txtNights;
        private TextBox txtCostPerPerson;
        private TextBox txtNumberOfPeople;
        private CheckBox chkHasWiFi;
        private TextBox txtSurcharges;
        private Label lblTotalCost;
        private Label lblPricePerNight;
        private Button btnSave;
        private Button btnCancel;

        public AddEditTourForm(ITourService service, int? id)
        {
            tourService = service;
            editingId = id;
            currentTour = new Tour();
            errorProvider = new ErrorProvider();

            InitializeComponent();

            if (id.HasValue)
            {
                this.Text = "Редактирование тура";
                Tour existingTour = tourService.GetTourById(id.Value);
                if (existingTour != null)
                {
                    currentTour = existingTour;
                    LoadTourToForm();
                }
            }
            else
            {
                this.Text = "Добавление тура";
            }
        }
        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(450, 550);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lblDestination = new Label() { Text = "Направление:", Location = new System.Drawing.Point(20, 20), Width = 150 };
            Label lblDate = new Label() { Text = "Дата вылета:", Location = new System.Drawing.Point(20, 60), Width = 150 };
            Label lblNights = new Label() { Text = "Кол-во ночей (1-99):", Location = new System.Drawing.Point(20, 100), Width = 150 };
            Label lblCostPerPerson = new Label() { Text = "Стоимость за чел. (руб):", Location = new System.Drawing.Point(20, 140), Width = 150 };
            Label lblNumberOfPeople = new Label() { Text = "Кол-во отдыхающих (1-5):", Location = new System.Drawing.Point(20, 180), Width = 150 };
            Label lblHasWiFi = new Label() { Text = "Наличие Wi-Fi:", Location = new System.Drawing.Point(20, 220), Width = 150 };
            Label lblSurcharges = new Label() { Text = "Доплата (руб):", Location = new System.Drawing.Point(20, 260), Width = 150 };

            lblTotalCost = new Label() { Text = "Общая стоимость:", Location = new System.Drawing.Point(20, 300), Width = 150, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            lblPricePerNight = new Label() { Text = "Цена за ночь:", Location = new System.Drawing.Point(20, 340), Width = 150, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            txtDestination = new TextBox() { Location = new System.Drawing.Point(180, 17), Width = 230 };
            dtpDeparture = new DateTimePicker() { Location = new System.Drawing.Point(180, 57), Width = 230 };
            txtNights = new TextBox() { Location = new System.Drawing.Point(180, 97), Width = 230 };
            txtCostPerPerson = new TextBox() { Location = new System.Drawing.Point(180, 137), Width = 230 };
            txtNumberOfPeople = new TextBox() { Location = new System.Drawing.Point(180, 177), Width = 230 };
            chkHasWiFi = new CheckBox() { Location = new System.Drawing.Point(180, 217), Width = 230 };
            txtSurcharges = new TextBox() { Location = new System.Drawing.Point(180, 257), Width = 230 };

            btnSave = new Button() { Text = "Сохранить", Location = new System.Drawing.Point(100, 450), Size = new System.Drawing.Size(100, 35) };
            btnCancel = new Button() { Text = "Отмена", Location = new System.Drawing.Point(220, 450), Size = new System.Drawing.Size(100, 35), DialogResult = DialogResult.Cancel };

            this.Controls.AddRange(new Control[] {
                lblDestination, lblDate, lblNights, lblCostPerPerson, lblNumberOfPeople, lblHasWiFi, lblSurcharges, lblTotalCost, lblPricePerNight,
                txtDestination, dtpDeparture, txtNights, txtCostPerPerson, txtNumberOfPeople, chkHasWiFi, txtSurcharges, lblTotalCost, lblPricePerNight,
                btnSave, btnCancel
            });

            btnSave.Click += BtnSave_Click;

            txtNights.TextChanged += (s, e) => ValidateAndUpdate();
            txtCostPerPerson.TextChanged += (s, e) => ValidateAndUpdate();
            txtNumberOfPeople.TextChanged += (s, e) => ValidateAndUpdate();
            txtSurcharges.TextChanged += (s, e) => ValidateAndUpdate();
            chkHasWiFi.CheckedChanged += (s, e) => ValidateAndUpdate();
            dtpDeparture.ValueChanged += (s, e) => UpdateCostDisplays();
        }
        private void LoadTourToForm()
        {
            txtDestination.Text = currentTour.Destination;
            dtpDeparture.Value = currentTour.DepartureDate;
            txtNights.Text = currentTour.Nights.ToString();
            txtCostPerPerson.Text = currentTour.CostPerPerson.ToString();
            txtNumberOfPeople.Text = currentTour.NumberOfPeople.ToString();
            chkHasWiFi.Checked = currentTour.HasWiFi;
            txtSurcharges.Text = currentTour.Surcharges.ToString();
        }
        private void ValidateAndUpdate()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtDestination.Text))
            {
                errorProvider.SetError(txtDestination, "Введите направление");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtDestination, "");
                currentTour.Destination = txtDestination.Text;
            }

            if (!int.TryParse(txtNights.Text, out int nights) || nights < 1 || nights > 99)
            {
                errorProvider.SetError(txtNights, "Введите число от 1 до 99");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtNights, "");
                currentTour.Nights = nights;
            }

            if (!decimal.TryParse(txtCostPerPerson.Text, out decimal costPerPerson) || costPerPerson < 1000 || costPerPerson > 500000)
            {
                errorProvider.SetError(txtCostPerPerson, "Введите число от 1000 до 500000");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtCostPerPerson, "");
                currentTour.CostPerPerson = costPerPerson;
            }

            if (!int.TryParse(txtNumberOfPeople.Text, out int numberOfPeople) || numberOfPeople < 1 || numberOfPeople > 5)
            {
                errorProvider.SetError(txtNumberOfPeople, "Введите число от 1 до 5");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtNumberOfPeople, "");
                currentTour.NumberOfPeople = numberOfPeople;
            }

            if (!decimal.TryParse(txtSurcharges.Text, out decimal surcharges) || surcharges < 0 || surcharges > 100000)
            {
                errorProvider.SetError(txtSurcharges, "Введите число от 0 до 100000");
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtSurcharges, "");
                currentTour.Surcharges = surcharges;
            }
            currentTour.HasWiFi = chkHasWiFi.Checked;
            currentTour.DepartureDate = dtpDeparture.Value;
            UpdateCostDisplays();
        }
        private void UpdateCostDisplays()
        {
            lblTotalCost.Text = $"{currentTour.TotalCost:N2} руб";
            lblPricePerNight.Text = $"{currentTour.PricePerNight:N2} руб";
        }
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtDestination.Text))
            {
                MessageBox.Show("Заполните направление", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDestination.Focus();
                return false;
            }

            if (!int.TryParse(txtNights.Text, out int nights) || nights < 1 || nights > 99)
            {
                MessageBox.Show("Количество ночей должно быть от 1 до 99", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNights.Focus();
                return false;
            }

            if (!decimal.TryParse(txtCostPerPerson.Text, out decimal costPerPerson) || costPerPerson < 1000 || costPerPerson > 500000)
            {
                MessageBox.Show("Стоимость должна быть от 1000 до 500000 рублей", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCostPerPerson.Focus();
                return false;
            }

            if (!int.TryParse(txtNumberOfPeople.Text, out int numberOfPeople) || numberOfPeople < 1 || numberOfPeople > 5)
            {
                MessageBox.Show("Количество отдыхающих должно быть от 1 до 5", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumberOfPeople.Focus();
                return false;
            }

            if (!decimal.TryParse(txtSurcharges.Text, out decimal surcharges) || surcharges < 0 || surcharges > 100000)
            {
                MessageBox.Show("Доплата должна быть от 0 до 100000 рублей", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSurcharges.Focus();
                return false;
            }

            if (dtpDeparture.Value.Date < DateTime.Today.Date && editingId.HasValue == false)
            {
                MessageBox.Show("Дата вылета не может быть в прошлом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDeparture.Focus();
                return false;
            }

            return true;
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }
            currentTour.Destination = txtDestination.Text;
            currentTour.DepartureDate = dtpDeparture.Value;
            currentTour.Nights = int.Parse(txtNights.Text);
            currentTour.CostPerPerson = decimal.Parse(txtCostPerPerson.Text);
            currentTour.NumberOfPeople = int.Parse(txtNumberOfPeople.Text);
            currentTour.HasWiFi = chkHasWiFi.Checked;
            currentTour.Surcharges = decimal.Parse(txtSurcharges.Text);

            if (editingId.HasValue)
            {
                tourService.UpdateTour(currentTour);
            }
            else
            {
                tourService.AddTour(currentTour);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
