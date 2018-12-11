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
using System.Security.Cryptography;

namespace DedicatedClient
{
    public partial class Login : Form
    {
        AdminServiceClient proxy = new AdminServiceClient();
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
            else if (user.Password == getHash(textBox2.Text,Encoding.ASCII.GetBytes(user.PassSalt)) && user.AccountType==3)
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
        private string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        private string getHash(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = 1000;
            var hashed = pbkdf2.GetBytes(32);
            Console.WriteLine(ByteArrayToString(hashed));
            return ByteArrayToString(hashed);
        }

    }
}
