using App.Models;
using demo_02_connection_commands.VM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace demo_02_connection_commands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        DataTable dataTable;

        private Employee selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set {
                selectedEmployee = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        public ObservableCollection<Employee> Employees 
        { 
            get => employees;
            set {
                employees = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            var connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("Select * from Employees", conn))
                {
                    try
                    {
                        conn.Open();
                        using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                        {
                            dataTable = new DataTable();
                            dataTable.Load(dataReader);
                        }

                    }
                    catch
                    {
                        MessageBox.Show("An error occured!");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            foreach (DataColumn column in dataTable.Columns)
            {
                Debug.WriteLine(column.ColumnName);
            }

            Employees.Clear();

            foreach (DataRow row in dataTable.Rows)
            {

                var emp = new Employee();

                emp.EmployeeId = Convert.ToInt32(row["EmployeeId"]);
                emp.FirstName = row["FirstName"].ToString();
                emp.LastName = row["LastName"].ToString();
                emp.HomePhone = row["HomePhone"].ToString();

                Employees.Add(emp);
            }
            
        }

        private void UpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            var connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            string query =
                $"UPDATE Employees " +
                $"SET FirstName = '{SelectedEmployee.FirstName}', " +
                $"LastName = '{SelectedEmployee.LastName}', " +
                $"HomePhone = '{SelectedEmployee.HomePhone}' " +
                $"WHERE EmployeeId = {SelectedEmployee.EmployeeId}";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("An error occured!");
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        private static int? RunNonQueryCommand(string queryString, string connectionString)
        {
            int? result = null;

            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                { 
                    command.Connection.Open();
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An error occured");
                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
