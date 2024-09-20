using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab
{
    /// <summary>
    /// Interaction logic for user.xaml
    /// </summary>
    public partial class user : Window
    {

        public db Connection;
        int userID = -1;
        Client client;
        AppData.Operator operatorData;
        public user(db connection, AppData.Operator @operator)
        {
            operatorData = @operator;
            userID = -1;
            InitializeComponent();
            Connection = connection;
        }

        public user(int id,Client a, db connection, AppData.Operator @operator)
        {
            operatorData = (@operator);
            Connection = connection;
            InitializeComponent();
            deleteButton.Visibility = Visibility.Visible;
            client = a;
            userID = id;
            name.Text = a.Name;
            Surname.Text = a.Surname;
            Patronymic.Text = a.Patronymic;
            dateofbirth.Text = a.dateofbirth;
            passport.Text = a.passport;
            number.Text = a.number;
            email.Text = a.email;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            client = new Client(name.Text, Surname.Text, Patronymic.Text, dateofbirth.Text, passport.Text, number.Text, email.Text);
            if (name.Text != "" &&
                 Surname.Text != "" &&
                 Patronymic.Text != "" &&
                 dateofbirth.Text != "" &&
                 passport.Text != "" &&
                 number.Text != "" &&
                 email.Text != "")
            {
                if (userID > -1)
                {
               //     Connection.UpdateClient(userID, client);
                    Connection.connection.Close();
                    operatorData.refresh();
                    this.Close();
                }
            //    else if (Connection.InsertClient(client))
                {
                    Connection.connection.Close();
                    operatorData.refresh();
                    this.Close();
                }
            }
            else {
                MessageBox.Show("Для сохранения необходимо заполнить все поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите удалить этого клиента?", "Удалить клиента?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
            //    Connection.DeleteClient(userID);
                Connection.connection.Close();
                operatorData.refresh();
                this.Close();
            }
        }
    }
}
