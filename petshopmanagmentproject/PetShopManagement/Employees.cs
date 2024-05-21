using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using static PetShopManagement.Login;

namespace PetShopManagement
{
    public partial class Employees : Form
    {
        private User loggedInUser;
        public Employees()
        {
            InitializeComponent();
            DisplayEmployees();
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
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\OneDrive\Documents\PetShopDbV2.mdf;Integrated Security=True;Connect Timeout=30");
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update EmployeeTable set EmpName=@EN, EmpAdd=@EA, EmpPhone=@EP, EmpPass=@EPa where EmpNum=@EKey", conn);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EPa", EmpPassTb.Text);
                    cmd.Parameters.AddWithValue("@EKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated!");
                    conn.Close();
                    DisplayEmployees();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void DisplayEmployees()
        {
            try
            {
                conn.Open();
                string Query = "Select * from EmployeeTable";
                SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                EmployeesDGV.DataSource = ds.Tables[0];
                conn.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Clear()
        {
            try
            {
                EmpNameTb.Text = "";
                EmpPassTb.Text = "";
                EmpPhoneTb.Text = "";
                EmpAddTb.Text = "";
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" || EmpPassTb.Text == "")
            {
                MessageBox.Show("Missing Information!");
            } else
            {
                try 
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTable (EmpName, EmpAdd, EmpPhone, EmpPass ) values(@EN, @EA, @EP, @EPa)", conn);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EPa", EmpPassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Added!");
                    conn.Close();
                    DisplayEmployees();
                    Clear();
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int key = 0;
        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EmployeesDGV.SelectedRows.Count > 0)
            {
                key = Convert.ToInt32(EmployeesDGV.SelectedRows[0].Cells[0].Value.ToString());
                EmpNameTb.Text = EmployeesDGV.SelectedRows[0].Cells[1].Value.ToString();
                EmpAddTb.Text = EmployeesDGV.SelectedRows[0].Cells[2].Value.ToString();
                EmpPhoneTb.Text = EmployeesDGV.SelectedRows[0].Cells[3].Value.ToString();
                EmpPassTb.Text = EmployeesDGV.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select An Employee!");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from EmployeeTable where EmpNum = @EmpKey", conn);
                    cmd.Parameters.AddWithValue("@EmpKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted!");
                    conn.Close();
                    DisplayEmployees();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
