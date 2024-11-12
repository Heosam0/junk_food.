﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace lab.AppData
{
    /// <summary>
    /// Interaction logic for Tables.xaml
    /// </summary>
    /// 

    public enum Status
    {
        В_процессе,
        Готов,
        Выдан
    }



public partial class Operator : Window
    {
        db Postgres;
        string login;
        string password;
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<MenuItem> MenuItems { get; set; }
        public ObservableCollection<MenuIngredient> MenuIngreds { get; set; }
        public NpgsqlConnection connection;
      
        public Operator(string login, string password)
        {
            this.login = login;
            this.password = password;
            InitializeComponent();
            Postgres = new db();
            connection = Postgres.connection;
            Postgres.Initialize(login, password);
            refresh();
            Employees = new ObservableCollection<Employee>();
            Orders = new ObservableCollection<Order>();
            MenuItems = new ObservableCollection<MenuItem>();
            MenuIngreds = new ObservableCollection<MenuIngredient>();

            LoadData();

        }

       



        private void LoadData()
        {
            var employees = Postgres.GetEmployees();
            foreach (var emp in employees)
            {
                Employees.Add(emp);
            }

            var orders = Postgres.GetOrders();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }

            var menuItems = Postgres.GetMenuItems();
            foreach (var menuItem in menuItems)
            {
                MenuItems.Add(menuItem);
            }

            var ingreds = Postgres.GetMenuIngreds();
            foreach (var ingr in ingreds)
            {
                MenuIngreds.Add(ingr);
            }

            // Привязываем коллекции к DataGrid
            datagrid2.ItemsSource = Employees;
            datagrid1.ItemsSource = Orders;
            datagrid4.ItemsSource = MenuItems;
            datagrid3.ItemsSource = MenuIngreds;
        }

        public void refresh()
        {
            InitializeComponent();
            Postgres = new db();
            Postgres.Initialize(login, password);
            
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid != null && e.EditAction == DataGridEditAction.Commit)
            {
                var editedElement = e.EditingElement as FrameworkElement;

                if (editedElement != null)
                {
                    object newValue = null;

                    if (editedElement is TextBox editedTextbox)
                    {
                        newValue = editedTextbox.Text;
                    }
                    else if (editedElement is ComboBox editedComboBox)
                    {
                        Order a = e.Row.Item as Order;
                        // Получаем статус из перечисления
                        Status newStatus = a.Status; // Например, выбранный статус из ComboBox


                        // Теперь сохраняем его в базе данных
                        Postgres.Code($"UPDATE orders SET status = '{a.Status}'::status WHERE id = {a.Id}");
                       

                    }

                    if (newValue != null)
                    {
                        var rowItem = e.Row.Item;
                        var itemType = rowItem.GetType();

                        var idProperty = itemType.GetProperty("Id");
                        if (idProperty != null)
                        {
                            int id = (int)idProperty.GetValue(rowItem);

                            // Получаем исходное имя свойства (название столбца в базе данных)
                            var boundProperty = e.Column.SortMemberPath;

                            if (!string.IsNullOrEmpty(boundProperty))
                            {
                                // Преобразуем значение статуса в строку перед обновлением
                                UpdateDatabase(dataGrid.Name, id, boundProperty, newValue.ToString());
                            }
                        }
                    }
                }
            }
        }






        private void UpdateDatabase(string dataGridName, int id, string columnName, string newValue)
{
    string tableName = null;

    if (dataGridName == "datagrid1")
    {
        tableName = "orders";
    }
    else if (dataGridName == "datagrid2")
    {
        tableName = "employees";
    }
    else if (dataGridName == "datagrid3")
    {
        tableName = "ingredients";
    }
    else if (dataGridName == "datagrid4")
    {
        tableName = "menu";
    }

    if (tableName != null)
    {
        var updatedValues = new Dictionary<string, object> { { columnName, newValue } };

        // Вызов метода обновления в классе db
        bool success = Postgres.UpdateRecord(tableName, id, updatedValues);

        if (success)
        {
            // Обновление интерфейса после изменения данных
            refresh();
        }
    }
    else
    {
        MessageBox.Show("Не удалось определить имя таблицы для обновления.");
    }
}



        private void SetButtonVisibility()
        {
            if (login == "postgres" || login == "manager")
            {
                CreateButton.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
            }
            else if (login == "cashier")
            {
                CreateButton.Visibility = Visibility.Collapsed;
                EditButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }


        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Пример создания новой записи в таблице orders
            var newOrder = new Dictionary<string, object>
    {
        { "employee_id", 1 }, // Здесь можно задать необходимые значения
        { "howcall", "Phone" },
        { "fordelivery", true },
        { "sum", 100.0 },
        { "tablenumber", 5 }
    };

            // Вызов метода для добавления новой записи
            bool success = AddRecord("orders", newOrder);

            if (success)
            {
                MessageBox.Show("Заказ успешно создан.");
                refresh(); // Обновление данных в DataGrid
            }
            else
            {
                MessageBox.Show("Ошибка при создании заказа.");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Предположим, что пользователь выбрал строку в datagrid1 для редактирования
            if (datagrid1.SelectedItem is DataRowView selectedRow)
            {
                int id = Convert.ToInt32(selectedRow["id"]);

                // Получение обновленных значений из формы (можно сделать отдельное окно или использовать InputBox)
                var updatedValues = new Dictionary<string, object>
        {
            { "howcall", "Updated Call" }, // Здесь можно задать необходимые значения
            { "sum", 150.0 }
        };

                // Вызов метода обновления записи
                bool success = Postgres.UpdateRecord("orders", id, updatedValues);

                if (success)
                {
                    MessageBox.Show("Запись успешно обновлена.");
                    refresh(); // Обновление данных в DataGrid
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования.");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Предположим, что пользователь выбрал строку в datagrid1 для удаления
            if (datagrid1.SelectedItem is DataRowView selectedRow)
            {
                int id = Convert.ToInt32(selectedRow["id"]);

                // Вызов метода для удаления записи
                bool success = DeleteRecord("orders", id); // Предполагаем, что метод DeleteRecord реализован в классе db

                if (success)
                {
                    MessageBox.Show("Запись успешно удалена.");
                    refresh(); // Обновление данных в DataGrid
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении записи.");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

        public bool AddRecord(string tableName, Dictionary<string, object> values)
        {
            var columns = string.Join(", ", values.Keys);
            var parameters = string.Join(", ", values.Keys.Select((k, i) => $"@param{i}"));

            var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            try
            {
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    int index = 0;
                    foreach (var value in values.Values)
                    {
                        command.Parameters.AddWithValue($"@param{index}", value);
                        index++;
                    }
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении записи: {ex.Message}");
                return false;
            }
        }
        public bool DeleteRecord(string tableName, int id)
        {
            var sql = $"DELETE FROM {tableName} WHERE id = @id";

            try
            {
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
                return false;
            }
        }

    }
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }

}
