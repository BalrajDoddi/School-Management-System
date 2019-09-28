using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using School_Management_System.Model;

namespace School_Management_System
{
    public partial class Login : TempleteWindowsForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValidate())
            {
                try
                {
                    DataTable dtRowFeatched = new DataTable();
                    string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;
                    string cmdString = "select * from Users where UserName = @UserName and Password=@Password";

                    using (SqlConnection con = new SqlConnection(connString))
                    {
                        using (SqlCommand cmd = new SqlCommand(cmdString, con))
                        {
                            con.Open();

                            cmd.Parameters.AddWithValue("@UserName", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                            SqlDataReader reader = cmd.ExecuteReader();
                            dtRowFeatched.Load(reader);
                            con.Close();
                        }
                    }

                    if (dtRowFeatched.Rows.Count > 0)
                    {
                        LoggedInUser.UserName = txtUsername.Text;
                        this.Hide();

                        Master master = new Master();
                        master.ShowDialog();
                    }

                    else
                    {
                        MessageBox.Show("User Name /Password is incorrect. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsValidate()
        {
            if (txtUsername.Text.Trim() == string.Empty)
            {
                MessageBox.Show("User Name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtUsername.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Password is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_VisibleChanged(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }
    }
}
