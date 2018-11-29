using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DedicatedClient.ServiceReference1;
using System.Threading;

namespace DedicatedClient
{
    public partial class Login : Form
    {
        SenderServiceClient proxy = new SenderServiceClient();
        Thread th;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = proxy.GetAllUsers().SingleOrDefault(x=>x.Username==textBox1.Text);
            if (user == null)
            {

            }
            else if (user.Password == textBox2.Text && user.AccountType==3)
            {
                Close();
                th = new Thread(openLoggedIn);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }
        private void openLoggedIn()
        {
            Application.Run(new Form1());
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
