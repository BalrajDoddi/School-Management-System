using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using School_Management_System.Model;
using School_Management_System.Staff;

namespace School_Management_System
{
    public partial class Master : TempleteWindowsForm
    {
        public Master()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void admissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentAdmission sa = new StudentAdmission();
            sa.ShowDialog();
        }

        private void staffToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            StaffEnroll se = new StaffEnroll();
            se.ShowDialog();
        }

        private void addClassToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NewClass cl = new NewClass();
            cl.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.lgForm.Show();
        }

        private void classListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelCall(ClassList.Instance);
        }

        private void studentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelCall(StudentList.Instance);
        }

        private void staffListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelCall(StaffList.Instance);
        }

        private void Master_Load(object sender, EventArgs e)
        {
            panelCall(Home.Instance);
            loggedOnUser.Text = LoggedInUser.UserName;
        }

        public void panelCall(Control contro)
        {
            if (!panel3.Controls.Contains(contro))
            {
                panel3.Controls.Add(contro);
                contro.Dock = DockStyle.Fill;
                contro.BringToFront();
            }
            else
                contro.BringToFront();
        }

        private void homeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panelCall(Home.Instance);
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.lgForm.Show();
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.ShowDialog();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            panelCall(Home.Instance);
        }

        private void StudentButton_Click(object sender, EventArgs e)
        {
            panelCall(StudentList.Instance);
        }

        private void StaffButton_Click(object sender, EventArgs e)
        {
            panelCall(StaffList.Instance);
        }

        private void ClassButton_Click(object sender, EventArgs e)
        {
            panelCall(ClassList.Instance);
        }

        private void UserButton_Click(object sender, EventArgs e)
        {
            panelCall(UserList.Instance);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Time.Text = DateTime.Now.ToLongTimeString();
            this.Date.Text = DateTime.Now.ToLongDateString();
        }

        private void systemUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelCall(UserList.Instance);
        }

        private void changePasswordToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.ShowDialog();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
