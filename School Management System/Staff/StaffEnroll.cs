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
using System.IO;
using System.Text.RegularExpressions;

namespace School_Management_System.Staff
{
    public partial class StaffEnroll : TempleteWindowsForm
    {
        public StaffEnroll()
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

        private void StaffEnroll_Load(object sender, EventArgs e)
        {
            privious_experience.SelectedIndex = 0;
            enrollment_date.Text = DateTime.Now.Date.ToString();

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
                staff_photo.Text = row["picture"].ToString();
                staff_aadhar_no.Text = row["Aadhar_no"].ToString();
                staff_gender.Text = row["gender"].ToString();
                staff_birthplace.Text = row["birth_place"].ToString();
                staff_dob.Text = row["dob"].ToString();
                staff_nationality.Text = row["nationality"].ToString();
                staff_age.Text = row["age"].ToString();
                qualification.Text = row["qualification"].ToString();
                qualification_summery.Text = row["summery_of_qual"].ToString();
                enrollment_year.Text = row["enroll_year"].ToString();
                job_status.Text = row["job_status"].ToString();
                enrollment_date.Text = row["enroll_date"].ToString();
                contract_termination.Text = row["contract_termination"].ToString();
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
            else
            {
                GetId("Staff_Details", staff_id, "Staff_id");
            }
        }

        private DataTable GetStudentInfoById()
        {
            DataTable dtStaff = new DataTable();

            string cmdString = "SELECT * FROM Staff_Details WHERE Staff_id=@Id";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Id", this.Id);

                SqlDataReader reader = cmd.ExecuteReader();

                dtStaff.Load(reader);
                con.Close();
            }
            return dtStaff;
        }

        private Image LoadPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void GetId(String TableName, TextBox total, string GenerateId)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ISNULL(MAX( " + GenerateId + " ), 0) + 1 FROM " + TableName, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            total.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void add_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select an Image";
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                staff_photo.Image = new Bitmap(open.FileName);
            }
        }

        private void clear_image_Click(object sender, EventArgs e)
        {
            staff_photo.Image = null;
        }

        private void staff_dob_ValueChanged(object sender, EventArgs e)
        {
            staff_dob.CustomFormat = "dd/MM/yyyy";
            
            DateTime currentDate = DateTime.Now;
            TimeSpan diff = currentDate.Subtract(staff_dob.Value.Date);
            DateTime age = DateTime.MinValue + diff;
            int ageInYears = age.Year - 1;
            staff_age.Text = ageInYears.ToString();
        }

        private void enrollment_date_ValueChanged(object sender, EventArgs e)
        {
            enrollment_date.CustomFormat = "dd/MM/yyyy";
//            DateTime EnrollmentDate = enrollment_date.Value;
//            int contactYears = Convert.ToInt32(enrollment_year.Text);
//            contract_termination.Text = EnrollmentDate.AddYears(contactYears).ToString();
        }

        private void enrollment_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime EnrollmentDate = enrollment_date.Value;
            int contactYears = Convert.ToInt32(enrollment_year.Text);
            contract_termination.Text = EnrollmentDate.AddYears(contactYears).ToString();
        }

        private void contract_termination_ValueChanged(object sender, EventArgs e)
        {
            contract_termination.CustomFormat = "dd/MM/yyyy";
        }

        private void staff_dob_KeyDown(object sender, KeyEventArgs e)
        {
            Key(staff_dob, e);
        }

        private void enrollment_date_KeyDown(object sender, KeyEventArgs e)
        {
            Key(enrollment_date, e);
        }

        private void contract_termination_KeyDown(object sender, KeyEventArgs e)
        {
            Key(contract_termination, e);
        }

        private void Key(DateTimePicker date, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                date.CustomFormat = " ";
            }
        }

        private bool IsEmailValid(TextBox box, bool ok, int tabIndex)
        {

            if (!ValidEmailRegex.IsMatch(box.Text))
            {
                ok = false;
                box.Focus();
                tabControl1.SelectedIndex = tabIndex;
                errorProvider1.SetError(box, "Please provide valid email Address(abc@gmail.com)");
            }

            return ok;
        }

        static Regex ValidEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        //Number Validation

        private bool IsNumberValid(TextBox box, int num, bool ok, int tabIndex)
        {
            if (box.TextLength != num)
            {
                ok = false;
                box.Focus();
                tabControl1.SelectedIndex = tabIndex;
                errorProvider1.SetError(box, "Please provide valid number");
            }
            return ok;
        }

        private bool IsTabIndexZero(ComboBox box, bool ok, int tabIndex)
        {
            if (box.SelectedIndex == -1)
            {
                ok = false;
                box.Focus();
                tabControl1.SelectedIndex = tabIndex;
                errorProvider1.SetError(box, "Please select a List Item");
            }

            return ok;
        }


        private bool IsNull(TextBox box, bool ok, int tabIndex)
        {
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                ok = false;
                box.Focus();
                tabControl1.SelectedIndex = tabIndex;
                errorProvider1.SetError(box, "Please fill the required field");
            }

            return ok;
        }

        private bool IsImageNull(PictureBox box, bool ok, int tabIndex)
        {
            if (box.Image == null)
            {
                ok = false;
                box.Focus();
                tabControl1.SelectedIndex = tabIndex;
                errorProvider1.SetError(box, "Please select an Image");
            }

            return ok;
        }

        private bool IsDateNull(DateTimePicker box, bool ok, int tabIndex)
        {
            if (box.Text == " ")
            {
                ok = false;
                box.Focus();
                tabControl1.SelectedIndex = tabIndex;
                errorProvider1.SetError(box, "Please select a Date");
            }

            return ok;
        }

        private void requiredfield()
        {
            errorProvider1.SetError(staff_first_name, "");
            errorProvider1.SetError(staff_middle_name, "");
            errorProvider1.SetError(staff_last_name, "");
            errorProvider1.SetError(staff_address, "");
            errorProvider1.SetError(staff_city, "");
            errorProvider1.SetError(staff_state, "");
            errorProvider1.SetError(staff_pincode, "");
            errorProvider1.SetError(staff_mob_no, "");
            errorProvider1.SetError(staff_email, "");
            errorProvider1.SetError(staff_religion, "");
            errorProvider1.SetError(staff_caste, "");
            errorProvider1.SetError(staff_photo, "");
            errorProvider1.SetError(staff_aadhar_no, "");
            errorProvider1.SetError(staff_gender, "");
            errorProvider1.SetError(staff_birthplace, "");
            errorProvider1.SetError(staff_dob, "");
            errorProvider1.SetError(staff_nationality, "");
            errorProvider1.SetError(staff_age, "");
            errorProvider1.SetError(qualification, "");
            errorProvider1.SetError(qualification_summery, "");
            errorProvider1.SetError(enrollment_year, "");
            errorProvider1.SetError(job_status, "");
            errorProvider1.SetError(enrollment_date, "");
            errorProvider1.SetError(contract_termination, "");
            errorProvider1.SetError(privious_experience, "");
            errorProvider1.SetError(experiance_year, "");
            errorProvider1.SetError(remark, "");
            errorProvider1.SetError(employer_status, "");
            errorProvider1.SetError(subject1, "");
            errorProvider1.SetError(subject2, "");
            errorProvider1.SetError(subject3, "");
            errorProvider1.SetError(subject4, "");
        }



        private bool digit(KeyPressEventArgs e)
        {
            requiredfield();
            bool ok = true;
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                ok = false;
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                System.Media.SystemSounds.Exclamation.Play();
            }
            return ok;
        }

        private bool alphabet(KeyPressEventArgs e)
        {
            requiredfield();
            bool ok = true;
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 32)
            {
                ok = false;
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                System.Media.SystemSounds.Exclamation.Play();
            }
            return ok;
        }

        private void staff_first_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(staff_first_name, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void staff_middle_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(staff_middle_name, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void staff_last_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(staff_last_name, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void staff_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(staff_city, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void staff_birthplace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(staff_birthplace, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void staff_nationality_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(staff_nationality, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void staff_pincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(staff_pincode, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void staff_mob_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(staff_mob_no, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void staff_aadhar_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(staff_aadhar_no, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void enrollment_year_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(enrollment_year, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void experiance_year_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(experiance_year, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void privious_experience_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (privious_experience.SelectedIndex == 0)
            {
                experiance_year.Enabled = false;
                remark.Enabled = false;
                employer_status.Enabled = false;
            }
            else
            {
                experiance_year.Enabled = true;
                remark.Enabled = true;
                employer_status.Enabled = true;
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            requiredfield();
            if (required())
            {
                try
                {
                    if (this.IsUpdate)
                    {
                        UpdateRecord();
                    }
                    else
                    {
                        SaveRecord();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private bool required()
        {
            bool ok = true;

            ok = IsNumberValid(staff_aadhar_no, 12, ok, 1);
            ok = IsEmailValid(staff_email, ok, 1);
            ok = IsNumberValid(staff_mob_no, 10, ok, 1);
            ok = IsNumberValid(staff_pincode, 6, ok, 1);

            ok = IsTabIndexZero(subject4, ok, 1);
            ok = IsTabIndexZero(subject3, ok, 1);
            ok = IsTabIndexZero(subject2, ok, 1);
            ok = IsTabIndexZero(subject1, ok, 1);
            if (privious_experience.SelectedIndex == 1)
            {
                ok = IsNull(employer_status, ok, 1);
                ok = IsNull(remark, ok, 1);
                ok = IsNull(experiance_year, ok, 1);
            }
            ok = IsTabIndexZero(privious_experience, ok, 1);
            ok = IsDateNull(contract_termination, ok, 1);
            ok = IsDateNull(enrollment_date, ok, 1);
            ok = IsTabIndexZero(job_status, ok, 1);
            ok = IsTabIndexZero(enrollment_year, ok, 1);
            ok = IsNull(qualification_summery, ok, 1);
            ok = IsNull(qualification, ok, 1);
            ok = IsNull(staff_age, ok, 0);
            ok = IsNull(staff_nationality, ok, 0);
            ok = IsDateNull(staff_dob, ok, 0);
            ok = IsNull(staff_birthplace, ok, 0);
            ok = IsTabIndexZero(staff_gender, ok, 0);
            ok = IsNull(staff_aadhar_no, ok, 0);
            ok = IsImageNull(staff_photo, ok, 0);
            ok = IsNull(staff_caste, ok, 0);
            ok = IsTabIndexZero(staff_religion, ok, 0);
            ok = IsNull(staff_email, ok, 0);
            ok = IsNull(staff_mob_no, ok, 0);
            ok = IsNull(staff_pincode, ok, 0);
            ok = IsTabIndexZero(staff_state, ok, 0);
            ok = IsNull(staff_city, ok, 0);
            ok = IsNull(staff_address, ok, 0);
            ok = IsNull(staff_last_name, ok, 0);
            ok = IsNull(staff_middle_name, ok, 0);
            ok = IsNull(staff_first_name, ok, 0);

            if (ok == false)
            {
                MessageBox.Show("Please Fill the required field..", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ok;
        }

        private void UpdateRecord()
        {
            string cmdString = @"UPDATE Staff_Details SET staff_first_name = @staff_first_name,
            staff_middle_name = @staff_middle_name, staff_last_name = @staff_last_name,
            staff_full_name = @FullName, address = @staff_address, city = @staff_city, state = @staff_state,
            pincode = @staff_pincode, mob_no = @staff_mob_no, email = @staff_email, religion = @staff_religion,
            caste = @staff_caste, picture = @Photo, Aadhar_no = @staff_aadhar_no, gender = @staff_gender,
            birth_place = @staff_birthplace, dob = @staff_dob, nationality = @staff_nationality,
            age = @staff_age, qualification = @qualification, summery_of_qual = @qualification_summery,
            enroll_year = @enrollment_year, job_status = @job_status, enroll_date = @enrollment_date,
            contract_termination = @contract_termination, pre_exp = @privious_experience,
            no_of_yr_exp = @experiance_year, remark = @remark, pre_emp = @employer_status, Subject1 = @subject1,
            Subject2 = @subject2, Subject3 = @subject3, Subject4 = @subject4 WHERE Staff_id = " + this.Id + "";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@staff_first_name", staff_first_name.Text);
                cmd.Parameters.AddWithValue("@staff_middle_name", staff_middle_name.Text);
                cmd.Parameters.AddWithValue("@staff_last_name", staff_last_name.Text);
                cmd.Parameters.AddWithValue("@FullName", staff_first_name.Text + " " + staff_middle_name.Text + " " + staff_last_name.Text);
                cmd.Parameters.AddWithValue("@staff_address", staff_address.Text);
                cmd.Parameters.AddWithValue("@staff_city", staff_city.Text);
                cmd.Parameters.AddWithValue("@staff_state", staff_state.Text);
                cmd.Parameters.AddWithValue("@staff_pincode", staff_pincode.Text);
                cmd.Parameters.AddWithValue("@staff_mob_no", staff_mob_no.Text);
                cmd.Parameters.AddWithValue("@staff_email", staff_email.Text);
                cmd.Parameters.AddWithValue("@staff_religion", staff_religion.Text);
                cmd.Parameters.AddWithValue("@staff_caste", staff_caste.Text);
                cmd.Parameters.AddWithValue("@Photo", SavePhoto());
                cmd.Parameters.AddWithValue("@staff_aadhar_no", staff_aadhar_no.Text);
                cmd.Parameters.AddWithValue("@staff_gender", staff_gender.Text);
                cmd.Parameters.AddWithValue("@staff_birthplace", staff_birthplace.Text);
                cmd.Parameters.AddWithValue("@staff_dob", staff_dob.Value.Date);
                cmd.Parameters.AddWithValue("@staff_nationality", staff_nationality.Text);
                cmd.Parameters.AddWithValue("@staff_age", staff_age.Text);
                cmd.Parameters.AddWithValue("@qualification", qualification.Text);
                cmd.Parameters.AddWithValue("@qualification_summery", qualification_summery.Text);
                cmd.Parameters.AddWithValue("@enrollment_year", enrollment_year.Text);
                cmd.Parameters.AddWithValue("@job_status", job_status.Text);
                cmd.Parameters.AddWithValue("@enrollment_date", enrollment_date.Value.Date);
                cmd.Parameters.AddWithValue("@contract_termination", contract_termination.Value.Date);
                cmd.Parameters.AddWithValue("@privious_experience", privious_experience.Text);
                cmd.Parameters.AddWithValue("@experiance_year", experiance_year.Text);
                cmd.Parameters.AddWithValue("@remark", remark.Text);
                cmd.Parameters.AddWithValue("@employer_status", employer_status.Text);
                cmd.Parameters.AddWithValue("@subject1", subject1.Text);
                cmd.Parameters.AddWithValue("@subject2", subject2.Text);
                cmd.Parameters.AddWithValue("@subject3", subject3.Text);
                cmd.Parameters.AddWithValue("@subject4", subject4.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record is Updated Successfully!!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                this.Close();
            }
        }

        private void SaveRecord()
        {
            string cmdString = @"insert into Staff_Details( Staff_id, staff_first_name, staff_middle_name, staff_last_name,
            staff_full_name, address, city, state, pincode, mob_no, email, religion, caste, picture, Aadhar_no,
            gender, birth_place, dob, nationality, age, qualification, summery_of_qual, enroll_year, job_status,
            enroll_date, contract_termination, pre_exp, no_of_yr_exp, remark, pre_emp, Subject1, Subject2,
            Subject3, Subject4 ) values( @staff_id, @staff_first_name, @staff_middle_name, @staff_last_name, @FullName,
            @staff_address, @staff_city, @staff_state, @staff_pincode, @staff_mob_no, @staff_email,
            @staff_religion, @staff_caste, @Photo, @staff_aadhar_no, @staff_gender, @staff_birthplace, 
            @staff_dob, @staff_nationality, @staff_age, @qualification, @qualification_summery, @enrollment_year,
            @job_status, @enrollment_date, @contract_termination, @privious_experience, @experiance_year,
            @remark, @employer_status, @subject1, @subject2, @subject3, @subject4)";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@staff_id", Convert.ToInt32(staff_id.Text));
                cmd.Parameters.AddWithValue("@staff_first_name", staff_first_name.Text);
                cmd.Parameters.AddWithValue("@staff_middle_name", staff_middle_name.Text);
                cmd.Parameters.AddWithValue("@staff_last_name", staff_last_name.Text);
                cmd.Parameters.AddWithValue("@FullName", staff_first_name.Text + " " + staff_middle_name.Text + " " + staff_last_name.Text);
                cmd.Parameters.AddWithValue("@staff_address", staff_address.Text);
                cmd.Parameters.AddWithValue("@staff_city", staff_city.Text);
                cmd.Parameters.AddWithValue("@staff_state", staff_state.Text);
                cmd.Parameters.AddWithValue("@staff_pincode", staff_pincode.Text);
                cmd.Parameters.AddWithValue("@staff_mob_no", staff_mob_no.Text);
                cmd.Parameters.AddWithValue("@staff_email", staff_email.Text);
                cmd.Parameters.AddWithValue("@staff_religion", staff_religion.Text);
                cmd.Parameters.AddWithValue("@staff_caste", staff_caste.Text);
                cmd.Parameters.AddWithValue("@Photo", SavePhoto());
                cmd.Parameters.AddWithValue("@staff_aadhar_no", staff_aadhar_no.Text);
                cmd.Parameters.AddWithValue("@staff_gender", staff_gender.Text);
                cmd.Parameters.AddWithValue("@staff_birthplace", staff_birthplace.Text);
                cmd.Parameters.AddWithValue("@staff_dob", staff_dob.Value.Date);
                cmd.Parameters.AddWithValue("@staff_nationality", staff_nationality.Text);
                cmd.Parameters.AddWithValue("@staff_age", staff_age.Text);
                cmd.Parameters.AddWithValue("@qualification", qualification.Text);
                cmd.Parameters.AddWithValue("@qualification_summery", qualification_summery.Text);
                cmd.Parameters.AddWithValue("@enrollment_year", enrollment_year.Text);
                cmd.Parameters.AddWithValue("@job_status", job_status.Text);
                cmd.Parameters.AddWithValue("@enrollment_date", enrollment_date.Value.Date);
                cmd.Parameters.AddWithValue("@contract_termination", contract_termination.Value.Date);
                cmd.Parameters.AddWithValue("@privious_experience", privious_experience.Text);
                cmd.Parameters.AddWithValue("@experiance_year", experiance_year.Text);
                cmd.Parameters.AddWithValue("@remark", remark.Text);
                cmd.Parameters.AddWithValue("@employer_status", employer_status.Text);
                cmd.Parameters.AddWithValue("@subject1", subject1.Text);
                cmd.Parameters.AddWithValue("@subject2", subject2.Text);
                cmd.Parameters.AddWithValue("@subject3", subject3.Text);
                cmd.Parameters.AddWithValue("@subject4", subject4.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record is Saved Successfully!!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                this.Close();
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            staff_photo.Image.Save(ms, staff_photo.Image.RawFormat);
            return ms.GetBuffer();
        }
    }
}
