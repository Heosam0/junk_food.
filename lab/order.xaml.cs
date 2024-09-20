using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;

namespace lab.AppData
{
    /// <summary>
    /// Interaction logic for order.xaml
    /// </summary>
    public partial class order : Window
    {
        public db Connection;
        AppData.Operator operatorData;
        public order(db connection, AppData.Operator @operator)
        {
            InitializeComponent();
            DataTable dataTable = connection.Code("select * from service");
            service_combox.Items.Add(dataTable.Rows[0][1]);
            service_combox.Items.Add(dataTable.Rows[1][1]);
            service_combox.Items.Add(dataTable.Rows[2][1]);
            service_combox.Items.Add(dataTable.Rows[3][1]);
            service_combox.Items.Add(dataTable.Rows[4][1]);
            dataTable = connection.Code("select * from clients");
            for(int i = 0; i < dataTable.Rows.Count; i++)
                client_combox.Items.Add($"{dataTable.Rows[i][1]} {dataTable.Rows[i][2]} {dataTable.Rows[i][3]}");
            operatorData = @operator;
            Connection = connection;
        }
        public order(int client_id, int service_id, db connection, AppData.Operator @operator)
        {

            operatorData = @operator;
         
            InitializeComponent();
            Connection = connection;
        }

      
        private void asd_Click(object sender, RoutedEventArgs e)
        {
            if(service_combox.SelectedItem != null && client_combox.SelectedItem != null) { 
              
                    Connection.Code($"insert into orders values ((SELECT max(id)+1 from orders), CURRENT_DATE , {service_combox.SelectedIndex}, 'WIP', to_date('{datepicker.Text}', 'DD.MM.YYYY'), {client_combox.SelectedIndex}, 1, 0 )");
                    Connection.connection.Close();
                    operatorData.refresh();
                    this.Close();
            }
            else
            {
                MessageBox.Show("Для сохранения необходимо заполнить все поля!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        private void asd_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
