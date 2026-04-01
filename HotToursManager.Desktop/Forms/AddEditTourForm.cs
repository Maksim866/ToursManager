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

            lblTotalCost = new Label
            {
                Text = "Общая стоимость:",
                Location = new System.Drawing.Point(20, 300),
                Width = 150,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            lblPricePerNight = new Label
            {
                Text = "Цена за ночь:",
                Location = new System.Drawing.Point(20, 340),
                Width = 150,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            txtDestination = new TextBox
            {
                Location = new System.Drawing.Point(180, 17),
                Width = 230
            };
            dtpDeparture = new DateTimePicker()
            {
                Location = new System.Drawing.Point(180, 57),
                Width = 230
            };
            txtNights = new TextBox()
            {
                Location = new System.Drawing.Point(180, 97),
                Width = 230
            };
            txtCostPerPerson = new TextBox()
            {
                Location = new System.Drawing.Point(180, 137),
                Width = 230
            };
            txtNumberOfPeople = new TextBox()
            {
                Location = new System.Drawing.Point(180, 177),
                Width = 230
            };
            chkHasWiFi = new CheckBox()
            {
                Location = new System.Drawing.Point(180, 217),
                Width = 230
            };
            txtSurcharges = new TextBox()
            {
                Location = new System.Drawing.Point(180, 257),
                Width = 230
            };

            btnSave = new Button
            {
                Text = "Сохранить",
                Location = new System.Drawing.Point(100, 450),
                Size = new System.Drawing.Size(100, 35)
            };
            btnCancel = new Button
            {
                Text = "Отмена",
                Location = new System.Drawing.Point(220, 450),
                Size = new System.Drawing.Size(100, 35),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[]
            {
                lblDestination, lblDate, lblNights, lblCostPerPerson, lblNumberOfPeople, lblHasWiFi, lblSurcharges,
                lblTotalCost, lblPricePerNight,
                txtDestination, dtpDeparture, txtNights, txtCostPerPerson, txtNumberOfPeople, chkHasWiFi, txtSurcharges,
                lblTotalCost, lblPricePerNight,
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
