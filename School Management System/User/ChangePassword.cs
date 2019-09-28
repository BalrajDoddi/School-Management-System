using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using School_Management_System.Model;
using System.Data.SqlClient;
using System.Configuration;

namespace School_Management_System
{
    public partial class ChangePassword : TempleteWindowsForm
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            CurrentUser.Text = LoggedInUser.UserName;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (IsValidate())
            {
                try
                {
                    UpdatePassword();
                    MessageBox.Show("Password is Updated Successfully.", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdatePassword()
        {
            string cmdString = "UPDATE Users SET [Password] = @Password WHERE  UserName = @UserName";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Password", NewPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@UserName", LoggedInUser.UserName);

                cmd.ExecuteNonQuery();
                con.Close();
                this.Close();
            }
        }

        private bool IsValidate()
        {
            if (NewPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("New Password is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NewPassword.Focus();
                return false;
            }

            if (ConfirmPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("confirm Password is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConfirmPassword.Focus();
                return false;
            }

            if (NewPassword.Text.Trim() != ConfirmPassword.Text.Trim())
            {
                MessageBox.Show("Confirm Password doesn't match.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConfirmPassword.Focus();
                return false;
            }
            return true;
        }
    }
}
