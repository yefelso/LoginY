using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Register
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string connectionString = "Data Source=(localdb)\\senati;Initial Catalog=Personas;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = NameR.Text;
            string email = EmailR.Text;
            string password = PasswordBox.Password;

            try
            {
                Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|senati\.pe)$", RegexOptions.IgnoreCase);
                if (!regex.IsMatch(email))
                {
                    MessageBox.Show("Por favor, ingrese un correo electrónico válido con terminaciones permitidas como gmail.com o senati.pe.");
                    return; // No se registra el usuario si el correo electrónico no es válido
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO usuarios (nombre, email, password) VALUES (@Nombre, @Email, @Password)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show("Registro exitoso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }

        }

        private void Loginenter_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
        }
    }
}