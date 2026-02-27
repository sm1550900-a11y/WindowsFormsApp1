using System;
using System.Data;
using System.Windows.Forms;
// или using System.Data.SQLite; если у тебя System.Data.SQLite

namespace WindowsFormsApp1   // ← замени на имя своего проекта, если другое
{
    public partial class DriverForm : Form
    {
        // Строка подключения — файл БД в папке bin/Debug/net8.0-windows (или где у тебя проект)
        private readonly string connectionString = "Data Source=autobase.db;Version=3;";

        public DriverForm()
        {
            InitializeComponent();
        }

        // Загрузка формы — сразу показываем рейсы водителя
        private void DriverForm_Load(object sender, EventArgs e)
        {
            RefreshMyTrips();
        }

        // Метод обновления списка рейсов водителя
        private void RefreshMyTrips()
        {
            try
            {
                string sql = @"
                    SELECT Id, Description, IsCompleted
                    FROM Trips
                    WHERE DriverId = @DriverId";

                using (var connection = new AutobaseDB(connectionString))
                {
                    connection.Open();
                    using (var command = new AutobaseDB(sql, connection))
                    {
                        // Здесь ID водителя — для теста поставь 1 или 2
                        // В реальном приложении можно сделать выбор водителя или логин
                        command.Parameters.AddWithValue("@DriverId", 1);

                        using (var adapter = new AutobaseDB(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            listBox1.Items.Clear();  // ← замени на имя своего ListBox, например lstMyTrips

                            foreach (DataRow row in dt.Rows)
                            {
                                int id = Convert.ToInt32(row["Id"]);
                                string desc = row["Description"].ToString();
                                bool completed = Convert.ToBoolean(row["IsCompleted"]);
                                string status = completed ? "выполнен" : "в работе";

                                listBox1.Items.Add($"{id}: {desc} ({status})");
                            }

                            // Если рейсов нет — показываем подсказку
                            if (listBox1.Items.Count == 0)
                            {
                                listBox1.Items.Add("У вас пока нет назначенных рейсов");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки рейсов:\n" + ex.Message);
            }
        }

        // Кнопка "Обновить"
        private void btnRefresh_Click(object sender, EventArgs e)   // ← переименуй кнопку в btnRefresh, если нужно
        {
            RefreshMyTrips();
        }

        // Кнопка "Отметить рейс выполненным"
        private void btnCompleteTrip_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберите рейс из списка!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCarStatusAfter.Text))
            {
                MessageBox.Show("Укажите состояние автомобиля после рейса!");
                return;
            }

            try
            {
                // Получаем ID рейса из выбранной строки
                string selected = listBox1.SelectedItem.ToString();
                int tripId = int.Parse(selected.Split(':')[0].Trim());

                string newStatus = txtCarStatusAfter.Text.Trim();

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // 1. Отмечаем рейс выполненным
                    string updateTrip = @"
                        UPDATE Trips 
                        SET IsCompleted = 1, 
                            CarStatusAfterTrip = @Status 
                        WHERE Id = @TripId";

                    using (var cmdTrip = new SqliteCommand(updateTrip, connection))
                    {
                        cmdTrip.Parameters.AddWithValue("@Status", newStatus);
                        cmdTrip.Parameters.AddWithValue("@TripId", tripId);
                        cmdTrip.ExecuteNonQuery();
                    }

                    // 2. Обновляем статус автомобиля (если он был назначен)
                    string updateCar = @"
                        UPDATE Cars 
                        SET Status = @Status 
                        WHERE Id = (SELECT CarId FROM Trips WHERE Id = @TripId)";

                    using (var cmdCar = new SqliteCommand(updateCar, connection))
                    {
                        cmdCar.Parameters.AddWithValue("@Status", newStatus);
                        cmdCar.Parameters.AddWithValue("@TripId", tripId);
                        cmdCar.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Рейс отмечен как выполнен!\nСостояние авто обновлено.");
                txtCarStatusAfter.Clear();
                RefreshMyTrips();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отметке рейса:\n" + ex.Message);
            }
        }

        // Кнопка "Заявка на ремонт"
        private void btnRequestRepair_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRepairDesc.Text))
            {
                MessageBox.Show("Опишите проблему с автомобилем!");
                return;
            }

            try
            {
                // Для простоты берём авто №1 — в реальности можно добавить выбор авто
                int carId = 1;
                string description = txtRepairDesc.Text.Trim();

                string sql = "INSERT INTO RepairRequests (CarId, Description) VALUES (@CarId, @Desc)";

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqliteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CarId", carId);
                        command.Parameters.AddWithValue("@Desc", description);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Заявка на ремонт успешно отправлена!");
                txtRepairDesc.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка отправки заявки:\n" + ex.Message);
            }
        }
    }
}