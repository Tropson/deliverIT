using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DedicatedClient.ServiceReference1;

namespace DedicatedClient
{
    public partial class Form1 : Form
    {
        List<bool> accepts = new List<bool>();
        SenderServiceClient proxy = new SenderServiceClient();
        DataTable table;
        public Form1()
        {
            InitializeComponent();
            InitializeTable();
        }
        private void InitializeTable()
        {
            table = new DataTable();
            table.Columns.Add("CPR", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("CV", typeof(string));
            table.Columns.Add("ID picture", typeof(string));
            table.Columns.Add("Yellow card picture", typeof(string));
            table.Columns.Add("Accept", typeof(bool));
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.InitializeTable();
            this.Bind_DataGridView_Using_DataTable_Load(sender, e);
        }
        private void Bind_DataGridView_Using_DataTable_Load(object sender, EventArgs e)
        {
            accepts.Clear();
            var applications = proxy.GetAllApplications();
            foreach (var i in applications)
            {
                bool accept = false;
                table.Rows.Add(i.Cpr, i.FirstName + " " + i.LastName, i.PhoneNumber, i.Email, i.Address + ", " + i.City + ", " + i.ZipCode, i.CVPath, i.IDPicturePath, i.YellowCardPath,accept);
            }
            dataGridView1.DataSource = table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Bind_DataGridView_Using_DataTable_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var datas = dataGridView1.DataSource;
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
