using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Register
{
    /// <summary>
    /// Lógica de interacción para Rcontraseña.xaml
    /// </summary>
    public partial class Rcontraseña : Window
    {
        private const string connectionString = "Data Source=(localdb)\\senati;Initial Catalog=Personas;Integrated Security=True";
        public Rcontraseña()
        {
            InitializeComponent();
            Loaded += Rcontraseña_Loaded;
        }
        private void Rcontraseña_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM usuarios";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridUsuarios.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }

        private void dataGridUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView rowView = dataGridUsuarios.SelectedItem as DataRowView;
                if (rowView != null)
                {
                    DataRow row = rowView.Row;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE usuarios SET nombre = @Nombre, email = @Email, password = @Password WHERE id = @Id";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", row["nombre"]);
                        command.Parameters.AddWithValue("@Email", row["email"]);
                        command.Parameters.AddWithValue("@Password", row["password"]);
                        command.Parameters.AddWithValue("@Id", row["id"]);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar datos: " + ex.Message);
            }


        }
    }
}
