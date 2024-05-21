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
using static PetShopManagement.Login;

namespace PetShopManagement
{
    public partial class Home : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\OneDrive\Documents\PetShopDbV2.mdf;Integrated Security=True;Connect Timeout=30");
        private User loggedInUser;
        public Home()
        {
            InitializeComponent();
            CountDogs();
            CountCats();
            CountBirds();
            Finance();
            loggedInUser = UserManager.LoggedInUser;
            if (loggedInUser != null)
            {
                // You can use the user details as needed
                int userId = loggedInUser.UserId;
                string username = loggedInUser.Username;

                // Example: Display the username in a label
                label7.Text = username;
            }
            else
            {
                // User is not logged in
                // Handle the case where no user is logged in
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }
        private void CountDogs()
        {
            conn.Open();
            string category = "Dog";
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTable where ProdCat='" + category + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DogsLabel.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void CountCats()
        {
            conn.Open();
            string category = "Cat";
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTable where ProdCat='" + category + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatsLabel.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void CountBirds()
        {
            conn.Open();
            string category = "Bird";
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProductTable where ProdCat='" + category + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BirdsLabel.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void Finance()
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(Price) from BillTable", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FinanceLabel.Text = "Rs. "+dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            UserManager.Logout();

            // Navigate to the login form or perform any other necessary actions
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }
    }
}
