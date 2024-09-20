using Dapper;
using Npgsql;
using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab
{
    public class db
    {
       
            public NpgsqlConnection connection;
            string login;
            string password;

            public string Initialize(string login, string password)
            {
                this.login = login;
                this.password = password;
                try
                {
                    string connectionString = $"Host=localhost:5432;Username={login};Password={password};Database=jf";
                    connection = new NpgsqlConnection(connectionString);
                    connection.Open();
                    return "Succesful";
                }
                catch (Exception ex)
                {
                    return ex.Message;
            }
            }

            public DataTable Code(string sql)
            {
                try
                {
                    DataTable dt = new DataTable();
                    NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dt.Load(reader);
                        return dt;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return null;
                }
            }

      //  public bool InsertClient(Client client)
      //  {
      //      try
      //      {
      //       
      //
      //          string sql = $"insert into clients " +
      //              $"values ((SELECT max(id)+1 from clients), '{client.Name}', '{client.Surname}', '{client.Patronymic}', to_date('{client.dateofbirth}', 'DD.MM.YYYY'), '{client.passport}', '{client.number}',  '{client.email}', '{{\"login\": \"{client.email.ToLower().Split('@')[0]}\", \"password\": \"{new Random().Next(1000000, 9999999)}\"}}')    ";
      //          NpgsqlCommand command = new NpgsqlCommand(sql, connection);
      //          NpgsqlDataReader reader = command.ExecuteReader();
      //         
      //      }
      //      catch (Exception ex)
      //      {
      //          MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
      //          return false;
      //      }
      //      return true;
      //  }
      //
      //  public void UpdateClient(int id,Client client)
      //  {
      //      try
      //      {
      //          string sql = $"Update clients SET first_name = '{client.Name}', " +
      //              $"last_name = '{client.Surname}', " +
      //              $"patronymic = '{client.Patronymic}', " +
      //              $"date_of_birth = to_date('{client.dateofbirth}', 'DD.MM.YYYY')," +
      //              $"passport = '{client.passport}', " +
      //              $"phone = '{client.number}', " +
      //              $"email = '{client.email}' " +
      //              $"where id = {id} ";
      //          NpgsqlCommand command = new NpgsqlCommand(sql, connection);
      //          NpgsqlDataReader reader = command.ExecuteReader();
      //
      //      }
      //      catch (Exception ex)
      //      {
      //          MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
      //
      //      }
      //  }
      //
      //  public void DeleteClient(int id)
      //  {
      //      try
      //      {
      //          string sql = $"delete from clients " +
      //              $"where id = {id} ";
      //          NpgsqlCommand command = new NpgsqlCommand(sql, connection);
      //          NpgsqlDataReader reader = command.ExecuteReader();
      //      }
      //      catch (Exception ex)
      //      {
      //          MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
      //
      //      }
      //  }
      //
        //    public ObservableCollection<Client> GetClientFullNames()
        //    {
        //        try
        //        {
        //            ObservableCollection<Client> ClientsList = new ObservableCollection<Clients>();
        //            string sql = $"select * from clients";
        //            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        //            NpgsqlDataReader reader = command.ExecuteReader();
        //            while(reader.Read())
        //            {
        //
        //                ClientsList.Add(new Client
        //                {
        //                    first_name = reader.GetInt32(0),
        //                    Name = reader.GetString(1),
        //                    Email = reader.GetString(2)
        //                });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            return null;
        //        }
        //    }
        //


    }
    }



