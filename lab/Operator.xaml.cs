using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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
    public partial class Operator : Page
    {
        db Postgres;
        string login;
        string password;
        int sadfdf = 0;
        enum currentTab
        {
            clients,employees,category
        }
        public Operator(string login, string password)
        {
            this.login = login;
            this.password = password;
            InitializeComponent();
            Postgres = new db();
            Postgres.Initialize(login, password);
            refresh();

            if (tabControl.TabIndex == 3) ;
          //      asd.Visibility = Visibility.Hidden;
          //  else asd.Visibility = Visibility.Visible;
            

        }

        public void refresh()
        {
            InitializeComponent();
            Postgres = new db();
            Postgres.Initialize(login, password);
            try
            {
                datagrid1.ItemsSource = Postgres.Code("select * from show_orders").DefaultView;
                datagrid2.ItemsSource = Postgres.Code("select * from show_employees").DefaultView;
                datagrid3.ItemsSource = Postgres.Code("select * from show_ingredients").DefaultView;
                datagrid4.ItemsSource = Postgres.Code("select * from show_menu").DefaultView;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            a();
        }


        async void a()
        {
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
      //     
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

            if (datagrid1.SelectedItem != null)
            {

                DataRowView selectedItem = (DataRowView)datagrid1.SelectedItem;
                if (selectedItem != null)
                {
                    Client a = new Client(selectedItem["first_name"].ToString(),
                        selectedItem["last_name"].ToString(),
                        selectedItem["patronymic"].ToString(),
                        selectedItem["date_of_birth"].ToString(),
                        selectedItem["passport"].ToString(),
                        selectedItem["phone"].ToString(),
                        selectedItem["email"].ToString()
                        );
                    user window = new user(Convert.ToInt32(selectedItem["id"]), a, Postgres, this);
                    window.Show();
                    
                }
            }


            
            
        }

        private void asd_Click(object sender, RoutedEventArgs e)
        {
            user window = new user(Postgres, this);
            window.Show();
        }

       

        private void asdd_Copy_Click(object sender, RoutedEventArgs e)
        {
            order a = new order(Postgres, this);
            a.Show();
        }

        private void asdd_Click(object sender, RoutedEventArgs e)
        {
            order a = new order(Postgres, this);
            a.Show();
        }
    }

   
}
