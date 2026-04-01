using HotToursManager.Services.Contracts;

namespace HotToursManager.Desktop.Forms
{
    /// <summary>
    /// Главная форма приложения для управления турами.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ITourService service;
        private DataGridView dataGridView1;
        private decimal maxTotalCost = 1; // для пропорциональной заливки

        /// <summary>
        /// Создаёт экземпляр формы.
        /// </summary>
        public MainForm(ITourService service)
        {
            InitializeComponent();
            this.service = service;

            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.Dock = DockStyle.Fill;

            RefreshGrid();
            UpdateStats();
        }
        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = service.GetAllTours();

            // Обновляем максимум для пропорциональной заливки
            var tours = service.GetAllTours();
            maxTotalCost = tours.Any() ? tours.Max(t => t.TotalCost) : 1;

            SetupGrid();
        }

        private void SetupGrid()
        {
            // Настройка заголовков и форматов
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                switch (col.DataPropertyName)
                {
                    case "Id":
                        col.HeaderText = "Id";
                        col.Width = 50;
                        break;
                    case "Destination":
                        col.HeaderText = "Локация";
                        col.Width = 120;
                        break;
                    case "DepartureDate":
                        col.HeaderText = "Дата вылета";
                        col.Width = 120;
                        ((DataGridViewTextBoxColumn)col).DefaultCellStyle.Format = "d";
                        break;
                    case "NumberOfPeople":
                        col.HeaderText = "Количество отдыхающих";
                        col.Width = 120;
                        break;
                    case "HasWiFi":
                        col.HeaderText = "Наличие Wi-Fi";
                        col.Width = 100;
                        break;
                    case "Nights":
                        col.HeaderText = "Кол-во ночей";
                        col.Width = 100;
                        break;
                    case "CostPerPerson":
                        col.HeaderText = "Стоимость за отдыхающего (руб)";
                        col.Width = 150;
                        ((DataGridViewTextBoxColumn)col).DefaultCellStyle.Format = "N2";
                        break;
                    case "Surcharges":
                        col.HeaderText = "Доплаты (руб)";
                        col.Width = 120;
                        ((DataGridViewTextBoxColumn)col).DefaultCellStyle.Format = "N2";
                        break;
                    case "PricePerNight":
                        col.HeaderText = "Цена за ночь";
                        col.Width = 120;
                        ((DataGridViewTextBoxColumn)col).DefaultCellStyle.Format = "N2";
                        break;
                    case "TotalCost":
                        col.HeaderText = "Общая стоимость";
                        col.Width = 120;
                        ((DataGridViewTextBoxColumn)col).DefaultCellStyle.Format = "N2";
                        break;
                }
            }
        }

        private void UpdateStats()
        {
            var stats = service.GetStatistics();
            label1.Text = $"Общее количество туров: {stats.TotalTours} | " +
                          $"Общая сумма за все туры: {stats.TotalCost:N0} ₽ | " +
                          $"Количество туров с доплатами: {stats.ToursWithSurcharges} | " +
                          $"Общая сумма доплат.: {stats.TotalSurcharges:N0} ₽";
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            var form = new AddEditTourForm(service, null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
                UpdateStats();
            }
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                var form = new AddEditTourForm(service, id);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshGrid();
                    UpdateStats();
                }
            }
            else
            {
                MessageBox.Show(
                    "Выберите тур для редактирования",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                var result = MessageBox.Show(
                    "Удалить выбранный тур?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    service.DeleteTour(id);
                    RefreshGrid();
                    UpdateStats();
                }
            }
            else
            {
                MessageBox.Show(
                    "Выберите тур для удаления",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.ColumnIndex >= dataGridView1.Columns.Count)
            {
                return;
            }
            var column = dataGridView1.Columns[e.ColumnIndex];
            if (column.DataPropertyName != "TotalCost")
            {
                return;
            }
            if (e.Value is not decimal totalCost)
            {
                return;
            }
            var ratio = Math.Min(1f, (float)totalCost / (float)maxTotalCost);

            int leftPad = 4;
            int rightPad = 8;
            int topPad = 3;
            int bottomPad = 3;

            var availableWidth = e.CellBounds.Width - leftPad - rightPad;
            var fillWidth = (int)(availableWidth * ratio);
            if (fillWidth < 0)
            {
                fillWidth = 0;
            }
            var fillRect = new Rectangle(
                e.CellBounds.Left + leftPad,
                e.CellBounds.Top + topPad,
                fillWidth,
                e.CellBounds.Height - topPad - bottomPad
            );

            Color barColor;
            if (totalCost <= 500_000)
            {
                barColor = Color.FromArgb(100, 200, 150);   // зелёный
            }
            else if (totalCost <= 1_000_000)
            {
                barColor = Color.FromArgb(80, 180, 240);    // синий
            }
            else if (totalCost <= 1_500_000)
            {
                barColor = Color.FromArgb(255, 180, 80);    // оранжевый
            }
            else if (totalCost <= 2_000_000)
            {
                barColor = Color.FromArgb(150, 80, 200);    // фиолетовый
            }
            else
            {
                barColor = Color.FromArgb(244, 67, 54);    // красный
            }

            e.PaintBackground(e.CellBounds, true);
            using var brush = new SolidBrush(barColor);
            e.Graphics.FillRectangle(brush, fillRect);
            e.PaintContent(e.CellBounds);
            e.Handled = true;
        }
    }
}
