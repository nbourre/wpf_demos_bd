using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;

namespace demo_02_connection_commands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dataTable;
        bool dataLoaded = false;
        List<string> fullNames = new List<string>();

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

            foreach (DataColumn column  in dataTable.Columns)
            {
                Debug.WriteLine(column.ColumnName);
            }

            foreach (DataRow row in dataTable.Rows) {
                string fullName = $"{row["LastName"]}, {row["FirstName"]}";

                fullNames.Add(fullName);
            }

            lvEmployee.ItemsSource = fullNames;
            
        }
    }
}
