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
using System.Text.RegularExpressions;

namespace School_Management_System
{
    public partial class StudentAdmission : TempleteWindowsForm
    {
        public StudentAdmission()
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

        private void StudentAdmission_Load(object sender, EventArgs e)
        {
            date_of_addmission.Text = DateTime.Now.Date.ToString();
            ClassOne.SelectedIndex = 0;
            student_first_name.Focus();

            student_std.DataSource = GetStd();
            student_std.DisplayMember = "std";
            student_std.SelectedIndex = -1;
            
            if (this.IsUpdate)
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
                student_dob.Text = row["DOB"].ToString();
                student_nationality.Text = row["Nationality"].ToString();
                student_age.Text = row["Age"].ToString();
                name_of_privious_school.Text = row["Name_of_privous_school"].ToString();
                address_of_privious_school.Text = row["Address_of_privous_school"].ToString();
                school_recognised.Text = row["School_Mention_above_was_recognise"].ToString();
                std_in_privious_school.Text = row["STD_in_privous_school"].ToString();
                year_of_passing.Text = row["Year_of_passing"].ToString();
                date_of_addmission.Text = row["Date_of_admission"].ToString();
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

                student_photo.Image = LoadPhoto((byte[])row["Picture"]);
            }
            else
            {
                GetId("Student_Details", student_id, "Student_id");
            }
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

        private void GetRollNo(String TableName, TextBox total, string GenerateId)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT ISNULL(MAX( " + GenerateId + " ),0) + 1 FROM " + TableName + " WHERE Std = '" + student_std.Text + "' AND Div = '" + student_div.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            total.Text = dt.Rows[0][0].ToString();
            con.Close();
        }

        private Image LoadPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
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

        private void submit_Click(object sender, EventArgs e)
        {
            requiredfield();
            if (required())
            {
                try
                {
                    if (this.isUpdate)
                    {
                        UpdateRecord();
                    }
                    else
                    {
                        SaveRecord();
                    }
                }
                catch(ApplicationException ex)
                {
                    MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveRecord()
        {

            string cmdString = @"insert into Student_Details ( Student_id, First_name, Middle_name, Last_name, Full_name,
            Mother_name, Name_of_gardian, Address, City, State, Pincode, contact_no, Mob_no, Email, Picture,
            Aadhar_no, Gender, Place_of_birth, DOB, Nationality, Age, ClassOne, Name_of_privous_school,
            Address_of_privous_school, School_Mention_above_was_recognise, STD_in_privous_school, Year_of_passing,
            Date_of_admission, Std, Div, Roll_no, Relationship_with_student, Occupation, Annual_income,
            No_of_dependents_in_family, Phone_home, Phone_office, Religion, Caste, Mother_tongue, Catagory)
            values( @Student_id, @student_first_name, @student_middle_name, @student_last_name, @Full_name,
            @student_mother_name, @student_guardian_name, @student_address, @student_city, @student_state,
            @student_pincode, @student_contact_no, @student_mob_no, @student_email, @Photo, @student_aadhar_no,
            @student_gender, @student_birthplace, @student_dob, @student_nationality, @student_age,
            @ClassOne, @name_of_privious_school, @address_of_privious_school, @school_recognised,
            @std_in_privious_school, @year_of_passing, @date_of_addmission, @student_std, @student_div,
            @student_roll_no, @relation_with_student, @occuption, @annual_income, @no_of_dependent, @phone_home,
            @phone_office, @religion, @caste, @mother_tongue, @category )";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Student_id", Convert.ToInt32(student_id.Text));
                cmd.Parameters.AddWithValue("@student_first_name", student_first_name.Text);
                cmd.Parameters.AddWithValue("@student_middle_name", student_middle_name.Text);
                cmd.Parameters.AddWithValue("@student_last_name", student_last_name.Text);
                cmd.Parameters.AddWithValue("@Full_name", student_first_name.Text + " " + student_middle_name.Text + " " + student_last_name.Text);
                cmd.Parameters.AddWithValue("@student_mother_name", student_mother_name.Text);
                cmd.Parameters.AddWithValue("@student_guardian_name", student_guardian_name.Text);
                cmd.Parameters.AddWithValue("@student_address", student_address.Text);
                cmd.Parameters.AddWithValue("@student_city", student_city.Text);
                cmd.Parameters.AddWithValue("@student_state", student_state.Text);
                cmd.Parameters.AddWithValue("@student_pincode", student_pincode.Text);
                cmd.Parameters.AddWithValue("@student_contact_no", student_contact_no.Text);
                cmd.Parameters.AddWithValue("@student_mob_no", student_mob_no.Text);
                cmd.Parameters.AddWithValue("@student_email", student_email.Text);
                cmd.Parameters.AddWithValue("@Photo", SavePhoto());
                cmd.Parameters.AddWithValue("@student_aadhar_no", student_aadhar_no.Text);
                cmd.Parameters.AddWithValue("@student_gender", student_gender.Text);
                cmd.Parameters.AddWithValue("@student_birthplace", student_birthplace.Text);
                cmd.Parameters.AddWithValue("@student_dob", student_dob.Value.Date);
                cmd.Parameters.AddWithValue("@student_nationality", student_nationality.Text);
                cmd.Parameters.AddWithValue("@student_age", student_age.Text);
                cmd.Parameters.AddWithValue("@ClassOne", ClassOne.Text);
                cmd.Parameters.AddWithValue("@name_of_privious_school", name_of_privious_school.Text);
                cmd.Parameters.AddWithValue("@address_of_privious_school", address_of_privious_school.Text);
                cmd.Parameters.AddWithValue("@school_recognised", school_recognised.Text);
                cmd.Parameters.AddWithValue("@std_in_privious_school", std_in_privious_school.Text);
                cmd.Parameters.AddWithValue("@year_of_passing", year_of_passing.Text);
                cmd.Parameters.AddWithValue("@date_of_addmission", date_of_addmission.Value.Date);
                cmd.Parameters.AddWithValue("@student_std", student_std.Text);
                cmd.Parameters.AddWithValue("@student_div", student_div.Text);
                cmd.Parameters.AddWithValue("@student_roll_no", student_roll_no.Text);
                cmd.Parameters.AddWithValue("@relation_with_student", relation_with_student.Text);
                cmd.Parameters.AddWithValue("@occuption", occuption.Text);
                cmd.Parameters.AddWithValue("@annual_income", annual_income.Text);
                cmd.Parameters.AddWithValue("@no_of_dependent", no_of_dependent.Text);
                cmd.Parameters.AddWithValue("@phone_home", phone_home.Text);
                cmd.Parameters.AddWithValue("@phone_office", phone_office.Text);
                cmd.Parameters.AddWithValue("@religion", religion.Text);
                cmd.Parameters.AddWithValue("@caste", caste.Text);
                cmd.Parameters.AddWithValue("@mother_tongue", mother_tongue.Text);
                cmd.Parameters.AddWithValue("@category", category.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record is saved successfully!!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                this.Close();
            }
        }

        private void UpdateRecord()
        {
            string cmdString = @"UPDATE Student_Details SET Student_id = @student_id,
            First_name = @student_first_name, Middle_name = @student_middle_name,
            Last_name = @student_last_name, Full_name = @Full_name, Mother_name = @student_mother_name,
            Name_of_gardian = @student_guardian_name, Address = @student_address,
            City = @student_city, State = @student_state, Pincode = @student_pincode,
            contact_no = @student_contact_no, Mob_no = @student_mob_no, Email = @student_email,
            Picture = @Photo, Aadhar_no = @student_aadhar_no, Gender = @student_gender,
            Place_of_birth = @student_birthplace, DOB = @student_dob, Nationality = @student_nationality,
            Age = @student_age, ClassOne = @ClassOne, Name_of_privous_school = @name_of_privious_school,
            Address_of_privous_school = @address_of_privious_school,
            School_Mention_above_was_recognise = @school_recognised, STD_in_privous_school = @std_in_privious_school,
            Year_of_passing = @year_of_passing, Date_of_admission = @date_of_addmission, Std = @student_std,
            Div = @student_div, Roll_no = @student_roll_no, Relationship_with_student = @relation_with_student,
            Occupation = @occuption, Annual_income = @annual_income, No_of_dependents_in_family = @no_of_dependent,
            Phone_home = @phone_home, Phone_office = @phone_office, Religion = @religion, Caste = @caste,
            Mother_tongue = @mother_tongue, Catagory = @category where Student_id=@Student_id";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

//                cmd.Parameters.AddWithValue("@student_id", Convert.ToInt32(student_id.Text));
                cmd.Parameters.AddWithValue("@student_first_name",student_first_name.Text);
                cmd.Parameters.AddWithValue("@student_middle_name",student_middle_name.Text);
                cmd.Parameters.AddWithValue("@student_last_name",student_last_name.Text);
                cmd.Parameters.AddWithValue("@Full_name", student_first_name.Text + " " + student_middle_name.Text + " " + student_last_name.Text);
                cmd.Parameters.AddWithValue("@student_mother_name",student_mother_name.Text);
                cmd.Parameters.AddWithValue("@student_guardian_name",student_guardian_name.Text);
                cmd.Parameters.AddWithValue("@student_address",student_address.Text);
                cmd.Parameters.AddWithValue("@student_city",student_city.Text);
                cmd.Parameters.AddWithValue("@student_state",student_state.Text);
                cmd.Parameters.AddWithValue("@student_pincode",student_pincode.Text);
                cmd.Parameters.AddWithValue("@student_contact_no",student_contact_no.Text);
                cmd.Parameters.AddWithValue("@student_mob_no",student_mob_no.Text);
                cmd.Parameters.AddWithValue("@student_email",student_email.Text);
                cmd.Parameters.AddWithValue("@Photo", SavePhoto());
                cmd.Parameters.AddWithValue("@student_aadhar_no",student_aadhar_no.Text);
                cmd.Parameters.AddWithValue("@student_gender",student_gender.Text);
                cmd.Parameters.AddWithValue("@student_birthplace",student_birthplace.Text);
                cmd.Parameters.AddWithValue("@student_dob",student_dob.Value.Date);
                cmd.Parameters.AddWithValue("@student_nationality",student_nationality.Text);
                cmd.Parameters.AddWithValue("@student_age",student_age.Text);
                cmd.Parameters.AddWithValue("@ClassOne",ClassOne.Text);
                cmd.Parameters.AddWithValue("@name_of_privious_school",name_of_privious_school.Text);
                cmd.Parameters.AddWithValue("@address_of_privious_school",address_of_privious_school.Text);
                cmd.Parameters.AddWithValue("@school_recognised",school_recognised.Text);
                cmd.Parameters.AddWithValue("@std_in_privious_school",std_in_privious_school.Text);
                cmd.Parameters.AddWithValue("@year_of_passing",year_of_passing.Text);
                cmd.Parameters.AddWithValue("@date_of_addmission",date_of_addmission.Value.Date);
                cmd.Parameters.AddWithValue("@student_std",student_std.Text);
                cmd.Parameters.AddWithValue("@student_div",student_div.Text);
                cmd.Parameters.AddWithValue("@student_roll_no",student_roll_no.Text);
                cmd.Parameters.AddWithValue("@relation_with_student",relation_with_student.Text);
                cmd.Parameters.AddWithValue("@occuption",occuption.Text);
                cmd.Parameters.AddWithValue("@annual_income",annual_income.Text);
                cmd.Parameters.AddWithValue("@no_of_dependent",no_of_dependent.Text);
                cmd.Parameters.AddWithValue("@phone_home",phone_home.Text);
                cmd.Parameters.AddWithValue("@phone_office",phone_office.Text);
                cmd.Parameters.AddWithValue("@religion",religion.Text);
                cmd.Parameters.AddWithValue("@caste",caste.Text);
                cmd.Parameters.AddWithValue("@mother_tongue",mother_tongue.Text);
                cmd.Parameters.AddWithValue("@category",category.Text);

                cmd.Parameters.AddWithValue("@Student_id", this.Id);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record is Updated successfully!!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                this.Close();
            }
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            student_photo.Image.Save(ms, student_photo.Image.RawFormat);
            return ms.GetBuffer();  
        }

        private bool required()
        {
            bool ok = true;

            ok = IsNumberValid(phone_office, 10, ok, 1);
            ok = IsNumberValid(phone_home, 10, ok, 1);
            ok = IsNumberValid(student_aadhar_no, 12, ok, 1);
            ok = IsEmailValid(student_email, ok, 1);
            ok = IsNumberValid(student_mob_no, 10, ok, 1);
            ok = IsNumberValid(student_contact_no, 10, ok, 1);
            ok = IsNumberValid(student_pincode, 6, ok, 1);

            ok = IsListIndexZero(category, ok, 1);
            ok = IsNull(mother_tongue, ok, 1);
            ok = IsNull(caste, ok, 1);
            ok = IsListIndexZero(religion, ok, 1);
            ok = IsNull(phone_office, ok, 1);
            ok = IsNull(phone_home, ok, 1);
            ok = IsNull(no_of_dependent, ok, 1);
            ok = IsNull(annual_income, ok, 1);
            ok = IsNull(occuption, ok, 1);
            ok = IsNull(relation_with_student, ok, 1);
            ok = IsNull(student_roll_no, ok, 1);
            ok = IsListIndexZero(student_div, ok, 1);
            ok = IsListIndexZero(student_std, ok, 1);
            ok = IsDateNull(date_of_addmission, ok,1);
            if (ClassOne.SelectedIndex == 1)
            {
                ok = IsNumberValid(year_of_passing, 4, ok, 1);
                ok = IsNull(year_of_passing, ok, 1);
                ok = IsListIndexZero(std_in_privious_school, ok, 1);
                ok = IsListIndexZero(school_recognised, ok, 1);
                ok = IsNull(address_of_privious_school, ok, 1);
                ok = IsNull(name_of_privious_school, ok, 1);
            }
            ok = IsNull(student_age, ok, 0);
            ok = IsNull(student_nationality, ok, 0);
            ok = IsDateNull(student_dob, ok, 0);
            ok = IsNull(student_birthplace, ok, 0);
            ok = IsListIndexZero(student_gender, ok, 0);
            ok = IsNull(student_aadhar_no, ok, 0);
            ok = IsImageNull(student_photo, ok, 0);
            ok = IsNull(student_email, ok, 0);
            ok = IsNull(student_mob_no, ok, 0);
            ok = IsNull(student_contact_no, ok, 0);
            ok = IsNull(student_pincode, ok, 0);
            ok = IsListIndexZero(student_state, ok, 0);
            ok = IsNull(student_city, ok, 0);
            ok = IsNull(student_address, ok, 0);
            ok = IsNull(student_guardian_name, ok, 0);
            ok = IsNull(student_mother_name, ok, 0);
            ok = IsNull(student_last_name, ok, 0);
            ok = IsNull(student_middle_name, ok, 0);
            ok = IsNull(student_first_name, ok, 0);

            if (ok == false)
            {
                MessageBox.Show("Please Fill the required field..", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ok;
        }
        
        //Email Address Validation

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

        //DropDown List required Validation

        private bool IsListIndexZero(ComboBox box, bool ok, int tabIndex)
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

        //Image Required Validation

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

        //Date Required Validation

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

        //Required Field Validation

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

        private void requiredfield()
        {
            errorProvider1.SetError(category, "");
            errorProvider1.SetError(mother_tongue, "");
            errorProvider1.SetError(caste, "");
            errorProvider1.SetError(religion, "");
            errorProvider1.SetError(phone_office, "");
            errorProvider1.SetError(phone_home, "");
            errorProvider1.SetError(no_of_dependent, "");
            errorProvider1.SetError(annual_income, "");
            errorProvider1.SetError(occuption, "");
            errorProvider1.SetError(relation_with_student, "");
            errorProvider1.SetError(student_roll_no, "");
            errorProvider1.SetError(student_div, "");
            errorProvider1.SetError(student_std, "");
            errorProvider1.SetError(date_of_addmission, "");
            errorProvider1.SetError(year_of_passing, "");
            errorProvider1.SetError(std_in_privious_school, "");
            errorProvider1.SetError(school_recognised, "");
            errorProvider1.SetError(address_of_privious_school, "");
            errorProvider1.SetError(name_of_privious_school, "");
            errorProvider1.SetError(student_age, "");
            errorProvider1.SetError(student_nationality, "");
            errorProvider1.SetError(student_dob, "");
            errorProvider1.SetError(student_birthplace, "");
            errorProvider1.SetError(student_gender, "");
            errorProvider1.SetError(student_aadhar_no, "");
            errorProvider1.SetError(student_photo, "");
            errorProvider1.SetError(student_email, "");
            errorProvider1.SetError(student_mob_no, "");
            errorProvider1.SetError(student_contact_no, "");
            errorProvider1.SetError(student_pincode, "");
            errorProvider1.SetError(student_state, "");
            errorProvider1.SetError(student_city, "");
            errorProvider1.SetError(student_address, "");
            errorProvider1.SetError(student_guardian_name, "");
            errorProvider1.SetError(student_mother_name, "");
            errorProvider1.SetError(student_last_name, "");
            errorProvider1.SetError(student_middle_name, "");
            errorProvider1.SetError(student_first_name, "");
        }

        private void student_dob_ValueChanged(object sender, EventArgs e)
        {
            student_dob.CustomFormat = "dd/MM/yyyy";

            DateTime currentDate = DateTime.Now;
            TimeSpan diff = currentDate.Subtract(student_dob.Value.Date);
            DateTime age = DateTime.MinValue + diff;
            int ageInYears = age.Year - 1;
            student_age.Text = ageInYears.ToString();
        }

        private void student_dob_KeyDown(object sender, KeyEventArgs e)
        {
            Key(student_dob, e);
        }

        private void date_of_addmission_ValueChanged(object sender, EventArgs e)
        {
            date_of_addmission.CustomFormat = "dd/MM/yyyy";
        }

        private void date_of_addmission_KeyDown(object sender, KeyEventArgs e)
        {
            Key(date_of_addmission, e);
        }

        private void Key(DateTimePicker date, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                date.CustomFormat = " ";
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            ClassOne.Focus();
        }

        private void previous_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            student_first_name.Focus();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            student_photo.Image = null;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select an Image";
            open.Filter = "Image Files(*.png; *.jpg; *.gif; *.bmp)|*.png; *.jpg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                student_photo.Image = new Bitmap(open.FileName);
            }
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

        private void student_first_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(student_first_name, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void student_middle_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(student_middle_name, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void student_last_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(student_last_name, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void student_mother_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(student_mother_name, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void student_guardian_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(student_guardian_name, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void student_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(student_city, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void student_birthplace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(student_birthplace, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void student_nationality_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(student_nationality, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void relation_with_student_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(relation_with_student, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void occuption_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(occuption, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void mother_tongue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(mother_tongue, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void student_pincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(student_pincode, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void student_contact_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(student_contact_no, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void student_mob_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(student_mob_no, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void student_aadhar_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(student_aadhar_no, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void student_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(student_age, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void year_of_passing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(year_of_passing, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void student_roll_no_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(student_roll_no, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void annual_income_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(annual_income, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void no_of_dependent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(no_of_dependent, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void phone_home_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(phone_home, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void phone_office_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(phone_office, "You are pressed wrong key(alphabet or symbol)..");
            }
        }

        private void ClassOne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClassOne.SelectedIndex == 0)
            {
                year_of_passing.Enabled = false;
                std_in_privious_school.Enabled = false;
                school_recognised.Enabled = false;
                address_of_privious_school.Enabled = false;
                name_of_privious_school.Enabled = false;
            }
            else
            {
                year_of_passing.Enabled = true;
                std_in_privious_school.Enabled = true;
                school_recognised.Enabled = true;
                address_of_privious_school.Enabled = true;
                name_of_privious_school.Enabled = true;
            }
        }

        private void student_std_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRollNo("Student_Details", student_roll_no, "Roll_no");

            student_div.DataSource = GetDiv();
            student_div.DisplayMember = "div";
            student_div.SelectedIndex = -1;

        }

        private void student_div_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRollNo("Student_Details", student_roll_no, "Roll_no");
        }

        private DataTable GetDiv()
        {
            DataTable dtDiv = new DataTable();


            string cmdString = "select Class_id, std, div from Class_Details where std=@std";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@std", student_std.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                dtDiv.Load(reader);
                con.Close();
            }
            return dtDiv;
        }

        private DataTable GetStd()
        {
            DataTable dtStd = new DataTable();

            string cmdString = "select Class_id, std, div from Class_Details WHERE div = 'A'";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                dtStd.Load(reader);
                con.Close();
            }
            return dtStd;
        }
    }
}
