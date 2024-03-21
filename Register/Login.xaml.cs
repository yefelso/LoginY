using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Aquí puedes agregar la lógica que deseas ejecutar cuando el texto cambia en el TextBox
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            // Aquí debes implementar la lógica para verificar las credenciales y permitir el acceso
            if (ValidarCredenciales(email, password))
            {
                MessageBox.Show("Inicio de sesión exitoso.");

                // Cerrar la ventana actual de inicio de sesión
                this.Close();

                // Crear una instancia de la ventana Pagina1
                Pagina1 pagina1 = new Pagina1();

                // Mostrar la ventana Pagina1
                pagina1.Show();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas. Inténtalo de nuevo.");
            }
        }

        private bool ValidarCredenciales(string email, string password)
        {
            // Cadena de conexión a tu base de datos
            string connectionString = "Data Source=(localdb)\\senati;Initial Catalog=Personas;Integrated Security=True";

            // Consulta SQL para buscar el email y la contraseña en la base de datos
            string query = "SELECT COUNT(*) FROM usuarios WHERE email = @Email AND password = @Password";

            // Variables para almacenar el resultado de la consulta
            int count;

            // Intentar ejecutar la consulta
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    count = (int)command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar las credenciales: " + ex.Message);
                return false;
            }

            // Si count es mayor que cero, significa que se encontraron coincidencias en la base de datos
            // Por lo tanto, las credenciales son válidas
            return count > 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            // Mostrar la ventana de registro
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Rcontraseña RcontraseñaWindow = new Rcontraseña();
            RcontraseñaWindow.Show();
        }
    }
}
