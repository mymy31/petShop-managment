using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShopManagement
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        int startProgress = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startProgress = (startProgress + 1);
            ProgressBar.Value = startProgress;
            PercentageLabel.Text = startProgress + "%";
            if (ProgressBar.Value == 100)
            {
                ProgressBar.Value = 0;
                timer1.Stop();
                Login form = new Login();
                form.Show();
                this.Hide();
            }
        }
    }
}
