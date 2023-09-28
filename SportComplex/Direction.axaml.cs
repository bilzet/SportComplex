using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace SportComplex;

public partial class Direction : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_23;User Id=user_01;password=user01pro";
    private List<Direction> _directions;
    private MySqlConnection _connection;
    public Direction()
    {
        InitializeComponent();
    }
    public void ShowTable()
    {
        string sql =
            "select * from Direction";
        _directions = new List<Direction>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentInstructor = new Direction()
            {
                Direction_id = reader.GetInt32("Instructor_id"),
                Name = reader.GetString("Name"),
                Age = reader.GetInt32("Age"),
            };
            _directions.Add(currentInstructor);
        }

        _connection.Close();
        DirectionGrid.ItemsSource = _directions;
    }
}