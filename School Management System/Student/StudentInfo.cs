using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using School_Management_System.Model;
using School_Management_System.Certificates;
using School_Management_System.Student;

namespace School_Management_System
{
    public partial class StudentInfo : TempleteWindowsForm
    {
        public StudentInfo()
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

        private void StudentInfo_Load(object sender, EventArgs e)
        {
            if(this.IsUpdate)
            {
                DataTable dtStudent = GetStudentInfoById();

                DataRow row = dtStudent.Rows[0];

                student_id.Text = row["Student_id"].ToString();
                student_first_name.Text = row["First_name"].ToString();
                student_middle_name.Text = row["Middle_name"].ToString();
                student_last_name.Text = row["Last_name"].ToString();
                student_mother_name.Text = row["Mother_name"].ToString();
                student_guardian_name.Text = row["Name_of_gardian"].ToString();
                student_address.Text = row["Address"].ToString();
                student_city.Text = row["City"].ToString();
                student_state.Text = row["State"].ToString();
                student_pincode.Text = row["Pincode"].ToString();
                student_contact_no.Text = row["contact_no"].ToString();
                student_mob_no.Text = row["Mob_no"].ToString();
                student_email.Text = row["Email"].ToString();
                student_aadhar_no.Text = row["Aadhar_no"].ToString();
                student_gender.Text = row["Gender"].ToString();
                student_birthplace.Text = row["Place_of_birth"].ToString();
                student_DOB.Text = ((DateTime) row["DOB"]).ToString("dd MMMM yyyy");
                student_nationality.Text = row["Nationality"].ToString();
                student_age.Text = row["Age"].ToString();
                name_of_privious_school.Text = row["Name_of_privous_school"].ToString();
                address_of_privious_school.Text = row["Address_of_privous_school"].ToString();
                school_recognised.Text = row["School_Mention_above_was_recognise"].ToString();
                std_in_privious_school.Text = row["STD_in_privous_school"].ToString();
                year_of_passing.Text = row["Year_of_passing"].ToString();
                date_of_addmission.Text = ((DateTime) row["Date_of_admission"]).ToString("dd MMMM yyyy");
                student_std.Text = row["Std"].ToString();
                student_div.Text = row["Div"].ToString();
                student_roll_no.Text = row["Roll_no"].ToString();
                relation_with_student.Text = row["Relationship_with_student"].ToString();
                occuption.Text = row["Occupation"].ToString();
                annual_income.Text = row["Annual_income"].ToString();
                no_of_dependent.Text = row["No_of_dependents_in_family"].ToString();
                phone_home.Text = row["Phone_home"].ToString();
                phone_office.Text = row["Phone_office"].ToString();
                religion.Text = row["Religion"].ToString();
                caste.Text = row["Caste"].ToString();
                mother_tongue.Text = row["Mother_tongue"].ToString();
                category.Text = row["Catagory"].ToString();

                student_photo.Image = (row["Picture"] is DBNull) ? null : LoadPhoto((byte[])row["Picture"]);
            }
        }

        private static Image LoadPhoto(byte[] photo)
        {
            MemoryStream ms=new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private DataTable GetStudentInfoById()
        {
            DataTable dtStudentInfo = new DataTable();


            string cmdString = "Select * from Student_Details where Student_id=@Id";

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

        private void Previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            student_id.Focus();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            name_of_privious_school.Focus();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(student_id.Text);

            StudentAdmission nc = new StudentAdmission();
            nc.Id = id;
            nc.IsUpdate = true;
            nc.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do You Really Want to Delete this Record ?", "ConFormation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                string cmdString = "DELETE FROM Student_Details WHERE Student_id=@Id";

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

        private void LeavingCertificate_Click(object sender, EventArgs e)
        {
            string DateInWords = WrittenNumerics.DateToWritten(Convert.ToDateTime(student_DOB.Text));

            string AdmissionStd = student_std.Text;
            using (LCDetails cer = new LCDetails(string.Format("{0}", student_id.Text), string.Format("{0}", student_first_name.Text + " " + student_middle_name.Text + " " + student_last_name.Text), string.Format("{0}", category.Text), string.Format("{0}", caste.Text), string.Format("{0}", student_nationality.Text), string.Format("{0}", student_birthplace.Text), string.Format("{0}", student_DOB.Text), string.Format("{0}", DateInWords), string.Format("{0}", name_of_privious_school.Text), string.Format("{0}", date_of_addmission.Text), string.Format("{0}", AdmissionStd), string.Format("{0}", student_std.Text)))
            {
                cer.ShowDialog();
            }
        }

        private void BonafideCertificate_Click(object sender, EventArgs e)
        {
            string DateInWords = WrittenNumerics.DateToWritten(Convert.ToDateTime(student_DOB.Text));
            using (BonafideCertificate cer = new BonafideCertificate(string.Format("{0}", student_id.Text), DateTime.Now.ToString("dd MMMM yyyy"), string.Format("{0}", student_first_name.Text + " " + student_middle_name.Text + " " + student_last_name.Text), string.Format("{0}", student_std.Text), string.Format("{0}", student_DOB.Text), string.Format("{0}", DateInWords), string.Format("{0}", student_birthplace.Text), string.Format("{0}", religion.Text), string.Format("{0}", caste.Text), string.Format("{0}", category.Text)))
            {
                cer.ShowDialog();
            }
        }
    }
}
