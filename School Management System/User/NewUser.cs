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
    public partial class NewUser : TempleteWindowsForm
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (IsValidate())
            {
                SqlCommand cm = new SqlCommand("Select count(*) from Users where UserName= @Username", con);
                cm.Parameters.AddWithValue("@Username", txtUser.Text);
                var result = cm.ExecuteScalar();
                if (result == null)
                {
                    MessageBox.Show(string.Format("Username {0} already exist", txtUser.Text));
                }
                else
                {
                    try
                    {
                        string cmdString = "insert into Users ( UserId, UserName, Password, StaffId, StaffName ) values ( @UserId, @username, @password, @staffid, @staffname )";

                        using (SqlCommand cmd = new SqlCommand(cmdString, con))
                        {
                            cmd.Parameters.AddWithValue("@UserId", UserId.Text);
                            cmd.Parameters.AddWithValue("@username", txtUser.Text);
                            cmd.Parameters.AddWithValue("@password", txtPass.Text);
                            cmd.Parameters.AddWithValue("@staffid", txtStaffId.Text);
                            cmd.Parameters.AddWithValue("@staffname", txtStaffName.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record is Saved Successfully!!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            con.Close();
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : " + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }


        private bool IsValidate()
        {
            if (txtUser.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Username is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
                return false;
            }

            if (txtPass.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Password is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Focus();
                return false;
            }

            if (txtConformPass.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Confirm Password is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConformPass.Focus();
                return false;
            }

            if (txtPass.Text.Trim() != txtConformPass.Text.Trim())
            {
                MessageBox.Show("Confirm Password doesn't match.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConformPass.Focus();
                return false;
            }

            if (txtStaffId.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Staff Id is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStaffId.Focus();
                return false;
            }

            if (txtStaffName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Staff Name is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStaffName.Focus();
                return false;
            }
            return true;
        }

        private DataTable GetStaffId()
        {
            DataTable dtStaff = new DataTable();

            string cmdString = "select Staff_id, staff_first_name, staff_middle_name, staff_last_name from Staff_Details ";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                dtStaff.Load(reader);

                con.Close();
            }

            return dtStaff;
        }

        private void NewUser_Load(object sender, EventArgs e)
        {
            txtStaffId.DataSource = GetStaffId();
            txtStaffId.DisplayMember = "Staff_id";
            txtStaffId.SelectedIndex = -1;

            GetId("Users", UserId, "UserId");
        }

        private void GetId(String TableName, TextBox total, string GenerateId)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ISNULL(MAX( " + GenerateId + " ),0) + 1 FROM " + TableName, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            total.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void getStaffName()
        {
            DataTable dtStaffName = new DataTable();
            string cmdString = "select staff_first_name, staff_middle_name, staff_last_name from Staff_Details Where Staff_id =  @Id";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                try
                {
                    con.Open();
                    int Id = Convert.ToInt16(txtStaffId.Text);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    dtStaffName.Load(reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("First You have to Select Staff Id","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    con.Close();
                }
            }
            DataRow row = dtStaffName.Rows[0];
            txtStaffName.Text = row["staff_first_name"].ToString();
        }

        private void txtStaffId_Leave(object sender, EventArgs e)
        {
            getStaffName();
        }
    }
}
