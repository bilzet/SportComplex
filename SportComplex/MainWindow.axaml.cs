using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace SportComplex;

public partial class MainWindow : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_23;User Id=user_01;password=user01pro";
    private List<Client> _clients;
    private MySqlConnection _connection;
    public MainWindow()
    {
        InitializeComponent();
        string fullTable = "select * from Client";
        ShowTable(fullTable);
    }

    public void ShowTable(string sql)
    {
        _clients = new List<Client>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentClient = new Client()
            {
                Client_ID = reader.GetInt32("Client_id"),
                Name = reader.GetString("Name"),
                Surname = reader.GetString("Surname"),
                Phone_Number = reader.GetInt32("Phone_number")
            };
            _clients.Add(currentClient);
        }

        _connection.Close();
        ClientGrid.ItemsSource = _clients;
    }

    private void DirectionWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        Direction directions = new Direction();
        directions.Show();
    }

    private void SubscribeWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        Subscribe subscribes = new Subscribe();
        subscribes.Show();
    }

    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string searchSql = "select * from Client" + txtSearch.Text + "%';";
        ShowTable(searchSql);
    }

    private void EditClients_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void DeleteClients_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void TxtFilter_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (txtFilter.SelectedIndex == 0)
        {
            var filteredd = _clients.Where(it => it.Client_id.ToString().Contains(txtSearch.Text)).ToList();
            filteredd = filteredd.OrderBy>(clientid => clientid.Client_id).ToList();
            ClientGrid.ItemsSource = filteredd;
        }
        else if (txtFilter.SelectedIndex == 1)
        {
            var filtereddd = _clients.Where(it => it.Name.ToLower().Contains(txtSearch.Text)).ToList();
            filtereddd = filtereddd.OrderBy(nam => nam.Name).ToList();
            ClientGrid.ItemsSource = filtereddd;
        }
        else if (txtFilter.SelectedIndex == 2)
        {
            var filtered = _clients.Where(it => it.Surname.ToLower().Contains(txtSearch.Text)).ToList();
            filtered = filtered.OrderBy(sur => sur.Surname).ToList();
            ClientGrid.ItemsSource = filtered;
        }
        else if (txtFilter.SelectedIndex == 3)
        {
            var filtereddddd = _clients.Where(it => it.Phone_number.ToString().Contains(txtSearch.Text)).ToList();
            filtereddddd = filtereddddd.OrderBy(number => number.Phone_number).ToList();
            ClientGrid.ItemsSource = filtereddddd;
        }
        
    }

}
