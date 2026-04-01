using HotToursManager.Desktop.Constants;
using HotToursManager.Desktop.Helpers;
using HotToursManager.Models;
using HotToursManager.Services.Contracts;

namespace HotToursManager.Desktop.Forms
{
    /// <summary>
    /// Форма для добавления или редактирования тура.
    /// </summary>
    public class AddEditTourForm : Form
    {
        private ITourService tourService;
        private Tour currentTour;
        private ErrorProvider errorProvider;
        private int? editingId;

        private TextBox txtDestination = new TextBox();
        private DateTimePicker dtpDeparture = new DateTimePicker();
        private TextBox txtNights = new TextBox();
        private TextBox txtCostPerPerson = new TextBox();
        private TextBox txtNumberOfPeople = new TextBox();
        private CheckBox chkHasWiFi = new CheckBox();
        private TextBox txtSurcharges = new TextBox();
        private Label lblTotalCost = new Label();
        private Label lblPricePerNight = new Label();
        private Button btnSave = new Button();
        private Button btnCancel = new Button();
        private bool isFormValid = false;

        /// <summary>
        /// Создаёт экземпляр формы.
        /// </summary>
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
                var existingTour = tourService.GetTourById(id.Value);
                if (existingTour != null)
                {
                    currentTour = existingTour;
                    TourFormMapper.LoadTourToForm(
                        currentTour, txtDestination, dtpDeparture, txtNights, txtCostPerPerson,
                        txtNumberOfPeople, chkHasWiFi, txtSurcharges);
                }
                else
                {
                    this.Text = "Добавление тура";
                }
            }
        }
        private void InitializeComponent()
        {
            this.Size = new System.Drawing.Size(450, 550);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            var lblDestination = new Label
            {
                Text = "Направление:",
                Location = new System.Drawing.Point(20, 20),
                Width = 150
            };
            var lblDate = new Label
            {
                Text = "Дата вылета:",
                Location = new System.Drawing.Point(20, 60),
                Width = 150
            };
            var lblNights = new Label
            {
                Text = $"Кол-во ночей ({TourLimits.MinNights}-{TourLimits.MaxNights}):",
                Location = new System.Drawing.Point(20, 100),
                Width = 150
            };
            var lblCostPerPerson = new Label
            {
                Text = "Стоимость за чел. (руб):",
                Location = new System.Drawing.Point(20, 140),
                Width = 150
            };
            var lblNumberOfPeople = new Label
            {
                Text = $"Кол-во отдыхающих ({TourLimits.MinPeople}-{TourLimits.MaxPeople}):",
                Location = new System.Drawing.Point(20, 180),
                Width = 150
            };
            var lblHasWiFi = new Label
            {
                Text = "Наличие Wi-Fi:",
                Location = new System.Drawing.Point(20, 220),
                Width = 150
            };
            var lblSurcharges = new Label
            {
                Text = "Доплата (руб):",
                Location = new System.Drawing.Point(20, 260),
                Width = 150
            };

            lblTotalCost.Text = "Общая стоимость:";
            lblTotalCost.Location = new System.Drawing.Point(20, 300);
            lblTotalCost.Width = 150;
            lblTotalCost.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

            lblPricePerNight.Text = "Цена за ночь:";
            lblPricePerNight.Location = new System.Drawing.Point(20, 340);
            lblPricePerNight.Width = 150;
            lblPricePerNight.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

            txtDestination.Location = new System.Drawing.Point(180, 17);
            txtDestination.Width = 230;

            dtpDeparture.Location = new System.Drawing.Point(180, 57);
            dtpDeparture.Width = 30;

            txtNights.Location = new System.Drawing.Point(180, 97);
            txtNights.Width = 230;

            txtCostPerPerson.Location = new System.Drawing.Point(180, 137);
            txtCostPerPerson.Width = 230;

            txtNumberOfPeople.Location = new System.Drawing.Point(180, 177);
            txtNumberOfPeople.Width = 230;

            chkHasWiFi.Location = new System.Drawing.Point(180, 217);
            chkHasWiFi.Width = 230;

            txtSurcharges.Location = new System.Drawing.Point(180, 257);
            txtSurcharges.Width = 230;

            btnSave.Text = "Сохранить";
            btnSave.Location = new System.Drawing.Point(100, 450);
            btnSave.Size = new System.Drawing.Size(100, 35);

            btnCancel.Text = "Отмена";
            btnCancel.Location = new System.Drawing.Point(220, 450);
            btnCancel.Size = new System.Drawing.Size(100, 35);
            btnCancel.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[]
            {
                lblDestination, lblDate, lblNights, lblCostPerPerson, lblNumberOfPeople, lblHasWiFi, lblSurcharges,
                lblTotalCost, lblPricePerNight,
                txtDestination, dtpDeparture, txtNights, txtCostPerPerson, txtNumberOfPeople, chkHasWiFi, txtSurcharges,
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
        private void ValidateAndUpdate()
        {
            var isValid = true;

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

            if (!int.TryParse(txtNights.Text, out var nights))
            {
                errorProvider.SetError(txtNights, "Введите целое число");
                isValid = false;
            }
            else if (!TourLimits.IsValidNights(nights))
            {
                errorProvider.SetError(txtNights, TourLimits.GetNightsError());
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtNights, "");
                currentTour.Nights = nights;
            }

            if (!decimal.TryParse(txtCostPerPerson.Text, out var costPerPerson))
            {
                errorProvider.SetError(txtCostPerPerson, "Введите число");
                isValid = false;
            }
            else if (!TourLimits.IsValidCostPerPerson(costPerPerson))
            {
                errorProvider.SetError(txtCostPerPerson, TourLimits.GetCostError());
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtCostPerPerson, "");
                currentTour.CostPerPerson = costPerPerson;
            }

            if (!int.TryParse(txtNumberOfPeople.Text, out var numberOfPeople))
            {
                errorProvider.SetError(txtNumberOfPeople, "Введите целое число");
                isValid = false;
            }
            else if (!TourLimits.IsValidPeople(numberOfPeople))
            {
                errorProvider.SetError(txtNumberOfPeople, TourLimits.GetPeopleError());
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtNumberOfPeople, "");
                currentTour.NumberOfPeople = numberOfPeople;
            }

            if (!decimal.TryParse(txtSurcharges.Text, out var surcharges))
            {
                errorProvider.SetError(txtSurcharges, "Введите число");
                isValid = false;
            }
            else if (!TourLimits.IsValidSurcharge(surcharges))
            {
                errorProvider.SetError(txtSurcharges, TourLimits.GetSurchargeError());
                isValid = false;
            }
            else
            {
                errorProvider.SetError(txtSurcharges, "");
                currentTour.Surcharges = surcharges;
            }

            isFormValid = isValid;
            btnSave.Enabled = isFormValid;

            if (isValid)
            {
                currentTour.Destination = txtDestination.Text;
                currentTour.DepartureDate = dtpDeparture.Value;
                currentTour.CostPerPerson = decimal.Parse(txtCostPerPerson.Text);
                currentTour.NumberOfPeople = int.Parse(txtNumberOfPeople.Text);
                currentTour.HasWiFi = chkHasWiFi.Checked;
                currentTour.Surcharges = decimal.Parse(txtSurcharges.Text);
                UpdateCostDisplays();
            }
        }
        private void UpdateCostDisplays()
        {
            lblTotalCost.Text = $"{currentTour.TotalCost:N2} руб";
            lblPricePerNight.Text = $"{currentTour.PricePerNight:N2} руб";
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TourValidator.ValidateForm(
                txtDestination.Text,
                int.Parse(txtNights.Text),
                decimal.Parse(txtCostPerPerson.Text),
                int.Parse(txtNumberOfPeople.Text),
                decimal.Parse(txtSurcharges.Text),
                dtpDeparture.Value,
                editingId,
                out var errorMessage))
            {
                TourFormMapper.SaveFormToTour(
                    currentTour,
                    txtDestination.Text,
                    dtpDeparture.Value,
                    int.Parse(txtNights.Text),
                    decimal.Parse(txtCostPerPerson.Text),
                    int.Parse(txtNumberOfPeople.Text),
                    chkHasWiFi.Checked,
                    decimal.Parse(txtSurcharges.Text));

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
            else
            {
                MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
