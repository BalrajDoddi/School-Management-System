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
using School_Management_System.Model;

namespace School_Management_System
{
    public partial class NewClass : TempleteWindowsForm
    {
        public NewClass()
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

        private void NewClass_Load(object sender, EventArgs e)
        {
            Std.Focus();
            if (this.IsUpdate)
            {
                DataTable dtClass = GetClassInfoById();
                DataRow row = dtClass.Rows[0];

                ClassId.Text = row["Class_id"].ToString();
                Div.Text = row["div"].ToString();
                Std.Text = row["std"].ToString();
                Strength.Text = row["Student_strength"].ToString();
                Shedule.Text = row["Class_shedule"].ToString();
                Subject1.Text = row["Subject1"].ToString();
                Subject2.Text = row["Subject2"].ToString();
                Subject3.Text = row["Subject3"].ToString();
                Subject4.Text = row["Subject4"].ToString();
                Subject5.Text = row["Subject5"].ToString();
                Subject6.Text = row["Subject6"].ToString();
                Subject7.Text = row["Subject7"].ToString();
                Subject8.Text = row["Subject8"].ToString();
                Subject9.Text = row["Subject9"].ToString();
                Subject10.Text = row["Subject10"].ToString();
                Subject11.Text = row["Subject11"].ToString();
            }
            else
            {
                GetId("Class_Details", ClassId, "Class_id");
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

        private DataTable GetClassInfoById()
        {
            DataTable dtClassInfo = new DataTable();

            string cmdString = "SELECT * FROM Class_Details WHERE Class_id  = @Id";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                cmd.Parameters.AddWithValue("@Id", this.Id);

                SqlDataReader reader = cmd.ExecuteReader();

                dtClassInfo.Load(reader);
                con.Close();
            }
            return dtClassInfo;
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            requiredfield();
            if (required())
            {
                try
                {
                    if (IsUpdate)
                    {
                        string cmdString = @"UPDATE Class_Details SET std = '" + Std.Text + "', div = '" + Div.Text + "', Student_strength = '" + Strength.Text + "', Class_shedule = '" + Shedule.Text + "', Subject1 = '" + Subject1.Text + "', Subject2 = '" + Subject2.Text + "', Subject3 = '" + Subject3.Text + "', Subject4 = '" + Subject4.Text + "', Subject5 = '" + Subject5.Text + "', Subject6 = '" + Subject6.Text + "', Subject7 = '" + Subject7.Text + "', Subject8 = '" + Subject8.Text + "', Subject9 = '" + Subject9.Text + "', Subject10 = '" + Subject10.Text + "', Subject11 = '" + Subject11.Text + "' WHERE Class_id=@Id";

                        using (SqlCommand cmd = new SqlCommand(cmdString, con))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@Id", this.Id);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record is successfully inserted!!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            con.Close();
                            this.Close();
                        }
                    }
                    else
                    {
                        string cmdString = @"insert into Class_Details( Class_id, std, div, Student_strength, Class_shedule, Subject1, Subject2, Subject3, Subject4, Subject5, Subject6, Subject7, Subject8, Subject9, Subject10, Subject11) values( " + Convert.ToInt32(ClassId.Text) + ",'" + Std.Text + "','" + Div.Text + "','" + Strength.Text + "','" + Shedule.Text + "','" + Subject1.Text + "','" + Subject2.Text + "','" + Subject3.Text + "','" + Subject4.Text + "','" + Subject5.Text + "','" + Subject6.Text + "','" + Subject7.Text + "','" + Subject8.Text + "','" + Subject9.Text + "','" + Subject10.Text + "','" + Subject11.Text + "')";

                        using (SqlCommand cmd = new SqlCommand(cmdString, con))
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Record is successfully inserted!!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            con.Close();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool required()
        {
            bool ok = true;

            ok = IsNull(Subject3, ok);
            ok = IsNull(Subject2, ok);
            ok = IsNull(Subject1, ok);
            ok = IsTabIndexZero(Shedule, ok);
            ok = IsNull(Strength, ok);
            ok = IsNull(Div, ok);
            ok = IsTabIndexZero(Std, ok);

            if (ok == false)
            {
                MessageBox.Show("Please Fill the required field..", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ok;
        }

        private bool IsTabIndexZero(ComboBox box, bool ok)
        {
            if (box.SelectedIndex == -1)
            {
                ok = false;
                box.Focus();
                errorProvider1.SetError(box, "Please select a List Item");
            }
            return ok;
        }


        private bool IsNull(TextBox box, bool ok)
        {
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                ok = false;
                box.Focus();
                errorProvider1.SetError(box, "Please fill the required field");
            }
            return ok;
        }

        private void requiredfield()
        {
            errorProvider1.SetError(ClassId, "");
            errorProvider1.SetError(Std, "");
            errorProvider1.SetError(Div, "");
            errorProvider1.SetError(Strength, "");
            errorProvider1.SetError(Shedule, "");
            errorProvider1.SetError(Subject1, "");
            errorProvider1.SetError(Subject2, "");
            errorProvider1.SetError(Subject3, "");
            errorProvider1.SetError(Subject4, "");
            errorProvider1.SetError(Subject5, "");
            errorProvider1.SetError(Subject6, "");
            errorProvider1.SetError(Subject7, "");
            errorProvider1.SetError(Subject8, "");
            errorProvider1.SetError(Subject9, "");
            errorProvider1.SetError(Subject10, "");
            errorProvider1.SetError(Subject11, "");
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
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 8)
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

        private void Div_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (alphabet(e))
            {
                errorProvider1.SetError(Div, "You are pressed wrong key(number or symbol)..");
            }
        }

        private void Strength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (digit(e))
            {
                errorProvider1.SetError(Strength, "You are pressed wrong key(alphabet or symbol)..");
            }
        }
    }
}
