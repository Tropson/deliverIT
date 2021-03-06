﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DedicatedClient.ServiceReference1;
using DedicatedClient.ServiceReference2;
using System.ServiceModel;
using DedicatedClient.Models;
using System.Web.Security;
using System.Threading;
using PubSub.Extension;
using System.Security.Cryptography;
using FluentFTP;
using System.Web.UI.WebControls;

namespace DedicatedClient
{
    public partial class Form1 : Form
    {
        
        List<bool> accepts = new List<bool>();
        AdminServiceClient proxy = new AdminServiceClient();
        NotificationServiceClient notifyProxy = null;
        DataTable table;
        Thread th;
        FtpClient client;
        public Form1()
        {
            InstanceContext context = new InstanceContext(new Notify());
            notifyProxy = new NotificationServiceClient(context);
            notifyProxy.CallCallback();
            this.Subscribe<NotificationModel>(x => { if (x.A == 1){ button1.PerformClick(); } } );
            InitializeComponent();
            InitializeTable();
            client = new FtpClient("deliverit.westeurope.cloudapp.azure.com");
            client.Connect();
           
        }
        private void InitializeTable()
        {
            table = new DataTable();
            table.Columns.Add("CPR", typeof(string)).ReadOnly=true;
            table.Columns.Add("Name", typeof(string)).ReadOnly = true;
            table.Columns.Add("Phone", typeof(string)).ReadOnly = true;
            table.Columns.Add("Email", typeof(string)).ReadOnly = true;
            table.Columns.Add("Address", typeof(string)).ReadOnly = true;
            table.Columns.Add("CV", typeof(string)).ReadOnly = true;
            table.Columns.Add("ID picture", typeof(string)).ReadOnly = true;
            table.Columns.Add("Yellow card picture", typeof(string)).ReadOnly = true;
            table.Columns.Add("Accept", typeof(bool));
            table.Columns.Add("Decline", typeof(bool));
        }

        private void InitializeUSersTable()
        {
            table = new DataTable();
            table.Columns.Add("Username", typeof(string));
            table.Columns.Add("Points", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Cpr", typeof(string));
            table.Columns.Add("Email", typeof(string));
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            th = new Thread(ShowLogin);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
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
            var newFetch = proxy.GetAllApplications();
            for (int i = dataGridView1.Rows.Count; i < newFetch.Length; i++)
            {
                var x = newFetch[i];
                table.Rows.Add(x.Cpr,x.FirstName+" "+x.LastName,x.PhoneNumber,x.Email, x.Address + ", " + x.City + ", " + x.ZipCode,x.CVPath,x.IDPicturePath,x.YellowCardPath,false,false);
            }
        }
        private void Bind_DataGridView_Using_DataTable_Load(object sender, EventArgs e)
        {
            accepts.Clear();
            var applications = proxy.GetAllApplications();
            foreach (var i in applications)
            {
                table.Rows.Add(i.Cpr, i.FirstName + " " + i.LastName, i.PhoneNumber, i.Email, i.Address + ", " + i.City + ", " + i.ZipCode, i.CVPath, i.IDPicturePath, i.YellowCardPath, false,false);
            }
            dataGridView1.DataSource = table;
            this.done();
        }
        private void done()
        {
            Debug.Write("done");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Bind_DataGridView_Using_DataTable_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var cpr = Convert.ToString(row.Cells[0].Value);
                if (row.Cells[8].Value.GetType() == typeof(System.DBNull))
                {
                    row.Cells[8].Value = false;
                }
                if (row.Cells[9].Value.GetType() == typeof(System.DBNull))
                {
                    row.Cells[9].Value = false;
                }
                if (Convert.ToBoolean(row.Cells[8].Value) == true)
                {
                    var app = proxy.GetAllApplications().SingleOrDefault(x => x.Cpr == cpr);
                    string generPassword = Membership.GeneratePassword(10, 0);
                    var salt = Encoding.ASCII.GetString(Guid.NewGuid().ToByteArray());
                    SenderModel courier = new SenderModel(app.Cpr, app.FirstName, app.LastName, app.PhoneNumber, app.Email, app.Address, app.ZipCode, app.City) { AccountType = 2, Points = 0 };
                    proxy.AddCourier(new SenderResource { AccountType = courier.AccountType, Address = courier.Address, City = courier.City, ZipCode = courier.ZipCode, Cpr = courier.Cpr, Email = courier.Email, FirstName = courier.FirstName, LastName = courier.LastName, PhoneNumber = courier.PhoneNumber, Points = courier.Points, Username = courier.Email, Password = generPassword, PassSalt=salt});
                    ApplicationResource appToDelete = new ApplicationResource { Cpr = app.Cpr };
                    proxy.DeleteApplication(appToDelete, false);
                }
                else if (Convert.ToBoolean(row.Cells[9].Value) == true)
                {
                    ApplicationResource appToDelete = new ApplicationResource { Cpr = cpr };
                    proxy.DeleteApplication(appToDelete, true);
                }
            }
            this.InitializeTable();
            this.Bind_DataGridView_Using_DataTable_Load(sender, e);
        }
        private void ataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Accept")
            {
                var otherCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1];
                if (otherCell.Value.GetType() != typeof(System.DBNull))
                {
                    if (Convert.ToBoolean(otherCell.Value) == true) otherCell.Value = false;
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Decline")
            {
                var otherCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1];
                if (otherCell.Value.GetType() != typeof(System.DBNull))
                {
                    if (Convert.ToBoolean(otherCell.Value) == true) otherCell.Value = false;
                }
            }
        }
        private void DownloadFile(string filename, string guid)
        {
            string serverPath = $"public_html/Files/{guid}/{filename}";
            var localPath =Path.GetTempPath() + "/" + filename;
            client.DownloadFile(localPath, serverPath);
            Process.Start(localPath);
        }
        private void ShowLogin()
        {
            Application.Run(new Login());
        }

        private void showUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            accepts.Clear();
            InitializeUSersTable();
            var users = proxy.GetAllUsers();
            foreach (var i in users)
            {
                table.Rows.Add(i.Username, i.Points, i.FirstName + " " + i.LastName, i.Cpr, i.Email);
            }
            dataGridView1.DataSource = table;
            this.done();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            accepts.Clear();
            InitializeTable();
            Bind_DataGridView_Using_DataTable_Load(sender, e);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridView1.Columns[e.ColumnIndex].Name.Contains("CV") || dataGridView1.Columns[e.ColumnIndex].Name.Contains("Yellow") || dataGridView1.Columns[e.ColumnIndex].Name.Contains("ID"))
            {
                var filename = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                var guid = proxy.GetAllApplications().SingleOrDefault(x => x.Email == dataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString()).GuidLine;
                DownloadFile(filename, guid);
            }
        }
    }
}
