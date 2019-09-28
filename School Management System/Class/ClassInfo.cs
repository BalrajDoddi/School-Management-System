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
    public partial class ClassInfo : TempleteWindowsForm
    {
        public ClassInfo()
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

        private void ClassInfo_Load(object sender, EventArgs e)
        {
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(ClassId.Text);

            NewClass nc = new NewClass();
            nc.Id = id;
            nc.IsUpdate = true;
            nc.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do You Really Want to Delete this Record ?", "ConFormation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                string cmdString = "DELETE FROM CLass_Details WHERE Class_id=@Id";

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
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Roll_no", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Catagory", typeof(string));
            dt.Columns.Add("DOB", typeof(DateTime));

            string cmdString = "SELECT Roll_no, Full_name as Name, Gender, Catagory, DOB FROM Student_Details WHERE Std = '" + Std.Text + "' AND Div = '" + Div.Text + "' ";

            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                con.Close();
            }
            
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample.xml");

            Report r = new Report("Class", ds);
            r.ShowDialog();
        }
    }
}
