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
using System.IO;
using School_Management_System.Model;
using School_Management_System.Staff;

namespace School_Management_System
{
    public partial class StaffInfo : TempleteWindowsForm
    {
        public StaffInfo()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);

        private int classId = 0;
        public int Id
        {
            get { return classId; }
            set { classId = value; }
        }

        private bool isUpdate = false;
        public bool IsUpdate
        {
            get { return isUpdate; }
            set { isUpdate = value; }
        }

        private void StaffInfo_Load(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = 0;
            staff_id.Focus();
            
            if (this.IsUpdate)
            {
                DataTable dtStudent = GetStudentInfoById();
                DataRow row = dtStudent.Rows[0];

                staff_id.Text = row["Staff_id"].ToString();
                staff_first_name.Text = row["staff_first_name"].ToString();
                staff_middle_name.Text = row["staff_middle_name"].ToString();
                staff_last_name.Text = row["staff_last_name"].ToString();
                staff_address.Text = row["address"].ToString();
                staff_city.Text = row["city"].ToString();
                staff_state.Text = row["state"].ToString();
                staff_pincode.Text = row["pincode"].ToString();
                staff_mob_no.Text = row["mob_no"].ToString();
                staff_email.Text = row["email"].ToString();
                staff_religion.Text = row["religion"].ToString();
                staff_caste.Text = row["caste"].ToString();
                staff_aadhar_no.Text = row["Aadhar_no"].ToString();
                staff_gender.Text = row["gender"].ToString();
                staff_birthplace.Text = row["birth_place"].ToString();
                staff_dob.Text = ((DateTime)row["dob"]).ToString("dd MMMM yyyy");
                staff_nationality.Text = row["nationality"].ToString();
                staff_age.Text = row["age"].ToString();
                qualification.Text = row["qualification"].ToString();
                qualification_summery.Text = row["summery_of_qual"].ToString();
                enrollment_year.Text = row["enroll_year"].ToString();
                job_status.Text = row["job_status"].ToString();
                enrollment_date.Text = ((DateTime)row["enroll_date"]).ToString("dd MMMM yyyy");
                contract_termination.Text = ((DateTime)row["contract_termination"]).ToString("dd MMMM yyyy");
                privious_experience.Text = row["pre_exp"].ToString();
                experiance_year.Text = row["no_of_yr_exp"].ToString();
                remark.Text = row["remark"].ToString();
                employer_status.Text = row["pre_emp"].ToString();
                subject1.Text = row["Subject1"].ToString();
                subject2.Text = row["Subject2"].ToString();
                subject3.Text = row["Subject3"].ToString();
                subject4.Text = row["Subject4"].ToString();
                staff_photo.Image = LoadPhoto((byte[])row["Picture"]);
            }
        }

        private Image LoadPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private DataTable GetStudentInfoById()
        {
            DataTable dtStaff = new DataTable();

            string cmdString = "SELECT * FROM Staff_Details WHERE Staff_id=@Id";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Id",this.Id);

                SqlDataReader reader = cmd.ExecuteReader();

                dtStaff.Load(reader);
                con.Close();
            }
            return dtStaff;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(staff_id.Text);

            StaffEnroll nc = new StaffEnroll();
            nc.Id = id;
            nc.IsUpdate = true;
            nc.ShowDialog();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            qualification.Focus();
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            staff_id.Focus();
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do You Really Want to Delete this Record ?", "ConFormation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                string cmdString = "DELETE FROM Staff_Details WHERE Staff_id=@Id";

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

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BonafideCertificate cer = new BonafideCertificate(string.Format("{0}", staff_first_name.Text + " " + staff_middle_name.Text+ " " + staff_last_name.Text), string.Format("{0}", enrollment_date.Text), string.Format("{0}", contract_termination.Text)))
            {
                cer.ShowDialog();
            }
        }

    }
}
