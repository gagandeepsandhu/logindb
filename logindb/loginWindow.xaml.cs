using System;
using System.Collections.Generic;
using System.Data;
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

namespace logindb
{
    /// <summary>
    /// Interaction logic for loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        public loginWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_OnClick(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon =
                new SqlConnection(
                    @"Data Source=LAPTOP-0LKHNMC8\SQLEXPRESS;Initial Catalog=login;Integrated Security=True");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM tblUser WHERE Username = @Username AND Password =@Password";
                SqlCommand SqlCmd = new SqlCommand(query, sqlCon);
                SqlCmd.CommandType = CommandType.Text;
                SqlCmd.Parameters.AddWithValue("@Username", txtusername.Text);
                SqlCmd.Parameters.AddWithValue("Password", txtpassword.Password);
                int count = Convert.ToInt32(SqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is Incorrect");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}