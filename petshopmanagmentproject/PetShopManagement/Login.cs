using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShopManagement
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\OneDrive\Documents\PetShopDbV2.mdf;Integrated Security=True;Connect Timeout=30");
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM EmployeeTable WHERE EmpName = @Username AND EmpPass= @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Authentication successful, create a User object if needed
                    User loggedInUser = new User
                    {
                        UserId = Convert.ToInt32(reader["EmpNum"]),
                        Username = username,
                        // Add other user-related properties as needed
                    };

                    // Set the logged-in user using the static class
                    UserManager.SetLoggedInUser(loggedInUser);

                    return true;
                }

                // Authentication failed
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public class User
        {
            public int UserId { get; set; }
            public string Username { get; set; }
            // Add other user-related properties as needed
        }
        public static class UserManager
        {
            private static User loggedInUser;

            public static User LoggedInUser
            {
                get { return loggedInUser; }
            }

            public static void SetLoggedInUser(User user)
            {
                loggedInUser = user;
            }
            public static void Logout()
            {
                loggedInUser = null;
            }
        }
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string username = userNameTb.Text;
            string password = passwordTb.Text;
            if (userNameTb.Text == "" || passwordTb.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                if (AuthenticateUser(username, password))
                {
                    MessageBox.Show("Login successful!");
                    // Open your main application form or perform any other necessary actions
                    // Example: MainApplicationForm mainForm = new MainApplicationForm();
                    // mainForm.Show();
                    Home obj = new Home();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.");
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            userNameTb.Text = "";
            passwordTb.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
