using HotToursManager.Models;
using HotToursManager.Services.Contracts;

namespace HotToursManager.Desktop
{
    /// <summary>
    /// Главная форма приложения для управления горячими турами
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ITourService service;
        private DataGridView dataGridView1;
        private Label lblStats;
        private decimal maxTotalCost = 1;
        /// <summary>
        /// Создаёт главную форму с указанным сервисом
        /// </summary>
        public MainForm(ITourService service)
        {
            InitializeComponent();
            this.service = service;
            dataGridView1.Dock = DockStyle.Fill;
            lblStats = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 48,
                Padding = new Padding(10),
                Font = new System.Drawing.Font("Segoe UI", 9F),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = System.Drawing.Color.LightGray,
                ForeColor = System.Drawing.Color.Black,
                AutoSize = false
            };
            this.Controls.Add(lblStats);

            SetupGrid();
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.ReadOnly = true;
            RefreshGrid();
            UpdateStats();
        }
        private void RefreshGrid()
        {
            // Устанавливаем DataSource → это запустит AutoGenerateColumns
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = service.GetAllTours();

            // Обновляем максимум для пропорциональной заливки
            var tours = service.GetAllTours();
            maxTotalCost = tours.Any() ? tours.Max(t => t.TotalCost) : 1;

            SetupGrid();
        }
        private void SetupGrid()
        {
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
        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Применяем только к колонке "TotalCost"
            // Защита от некорректных индексов
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
            // Пропорция: сколько процентов от max
            float ratio = Math.Min(1f, (float)totalCost / (float)maxTotalCost);

            // Внутренний прямоугольник с отступами
            int leftPad = 4;
            int topPad = 2;
            int rightPad = 2;
            int bottomPad = 2;

            int availableWidth = e.CellBounds.Width - leftPad - rightPad;
            int fillWidth = (int)(availableWidth * ratio);
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
            // Определяем цвет по диапазонам
            Color barColor;
            if (totalCost <= 500_000)
            {
                barColor = Color.FromArgb(100, 200, 150);   // мягкий зелёный
            }
            else if (totalCost <= 1_000_000)
            {
                barColor = Color.FromArgb(80, 180, 240);    // небесный синий
            }
            else if (totalCost <= 1_500_000)
            {
                barColor = Color.FromArgb(255, 180, 80);    // тёплый оранжевый
            }
            else if (totalCost <= 2_000_000)
            {
                barColor = Color.FromArgb(150, 80, 200);    // фиолетовый
            }
            else
            {
                barColor = Color.FromArgb(244, 67, 54);    // красный (люкс)
            }
            // Рисуем фон ячейки (оставляем текст)
            e.PaintBackground(e.CellBounds, true);

            using var brush = new SolidBrush(barColor);
            e.Graphics.FillRectangle(brush, fillRect);

            // Рисуем текст поверх
            e.PaintContent(e.CellBounds);

            e.Handled = true;
        }
        private void UpdateStats()
        {
            var stats = service.GetStatistics();
            lblStats.Text = $"Всего туров: {stats.TotalTours}, Общая сумма: {stats.TotalCost:N2} ₽, " +
                            $"Туры с доплатами: {stats.ToursWithSurcharges}, Сумма доплат: {stats.TotalSurcharges:N2} ₽";
        }
    }
}
