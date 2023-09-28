using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace SportComplex;

public partial class Subscribe : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_23;User Id=user_01;password=user01pro";
    private List<Subscribe> _subscribes;
    private MySqlConnection _connection;

    public Subscribe()
    {
        InitializeComponent();
    }

    public void ShowTable()
    {
        string sql =
            "select * from Subscribe";
        _subscribes = new List<Subscribe>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentSubscribe = new Subscribe()
            {
                Subscribe_id = reader.GetInt32("Subscribe_id"),
                Client = reader.GetInt32("Client"),
                Direction = reader.GetInt32("Direction"),
                Working_days = reader.GetInt32("Working_days"),
            };
            _subscribes.Add(currentSubscribe);
        }

        _connection.Close();
        SubscribeGrid.ItemsSource = _subscribes;
    }
}