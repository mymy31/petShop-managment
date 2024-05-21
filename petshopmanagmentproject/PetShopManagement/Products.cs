using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using static PetShopManagement.Login;

namespace PetShopManagement
{
    public partial class Products : Form
    {
        private User loggedInUser;
        public Products()
        {
            InitializeComponent();
            DisplayProducts();
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
        int key = 0;
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\OneDrive\Documents\PetShopDbV2.mdf;Integrated Security=True;Connect Timeout=30");
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            UserManager.Logout();

            // Navigate to the login form or perform any other necessary actions
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }
        private void DisplayProducts()
        {
            try
            {
                conn.Open();
                string Query = "Select * from ProductTable";
                SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                ProductsDGV.DataSource = ds.Tables[0];
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Clear()
        {
            try
            {
                ProdNameTb.Text = "";
                ProductPriceTb.Text = "";
                ProductQuantityTb.Text = "";
                CategoryCB.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ProdNameTb.Text == "" || ProductPriceTb.Text == "" || ProductQuantityTb.Text == "" || CategoryCB.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProductTable (ProdName, ProdCat, ProdQty, ProdPrice) values(@PN, @PC, @PQ, @PP)", conn);
                    cmd.Parameters.AddWithValue("@PN", ProdNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CategoryCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", ProductQuantityTb.Text);
                    cmd.Parameters.AddWithValue("@PP", ProductPriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Added!");
                    conn.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ProdNameTb.Text == "" || ProductPriceTb.Text == "" || ProductQuantityTb.Text == "" || CategoryCB.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update ProductTable set ProdName=@PN, ProdCat=@PC, ProdQty=@PQ, ProdPrice=@PP where ProdId=@PKey", conn);
                    cmd.Parameters.AddWithValue("@PN", ProdNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CategoryCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", ProductQuantityTb.Text);
                    cmd.Parameters.AddWithValue("@PP", ProductPriceTb.Text);
                    cmd.Parameters.AddWithValue("@PKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Updated!");
                    conn.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select A Product!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from ProductTable where ProdId = @PKey", conn);
                    cmd.Parameters.AddWithValue("@PKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted!");
                    conn.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ProductsDGV.SelectedRows.Count > 0)
            {
                key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());
                ProdNameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
                CategoryCB.Text = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
                ProductQuantityTb.Text = ProductsDGV.SelectedRows[0].Cells[3].Value.ToString();
                ProductPriceTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            }
        }
    }
}
