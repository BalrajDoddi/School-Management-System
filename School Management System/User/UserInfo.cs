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
    public partial class UserInfo : TempleteWindowsForm
    {
        public UserInfo()
        {
            InitializeComponent();
        }
        
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);

        private int id = 0;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private bool isUpdate = false;
        public bool IsUpdate
        {
            get { return isUpdate; }
            set { isUpdate = value; }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do You Really Want to Delete this Record ?", "ConFormation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                string cmdString = "DELETE FROM Users WHERE UserId=@Id";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Id", this.Id);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record Deleted Successfully!!");
                    con.Close();
                    this.Close();
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
            if(this.IsUpdate)
            {
                DataTable dtStudent = GetStudentInfoById();

                DataRow row = dtStudent.Rows[0];

                txtUser.Text = row["UserName"].ToString();
                txtStaffId.Text = row["StaffId"].ToString();
                txtStaffName.Text = row["StaffName"].ToString();
            }
        }

        private DataTable GetStudentInfoById()
        {
            DataTable dtStudentInfo = new DataTable();

            string cmdString = "SELECT UserName, StaffId, StaffName FROM  Users WHERE UserId=@Id";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Id", this.Id);

                SqlDataReader reader = cmd.ExecuteReader();

                dtStudentInfo.Load(reader);
                con.Close();
            }
            return dtStudentInfo;
        }
    }
}
