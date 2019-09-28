using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;

namespace School_Management_System
{
    public partial class StudentList : UserControl
    {
        public StudentList()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);

        private static StudentList _instance;

        public static StudentList Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StudentList();
                return _instance;
            }
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            StudentId.Focus();
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            DataGrideView.DataSource = GetStudetList();
            DataGrideView.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private DataTable dtStudent = new DataTable();

        private DataTable GetStudetList()
        {
            dtStudent.Rows.Clear();
            string cmdString = "Select Student_id, Full_name as Name, Std, Div, Roll_no, Gender, Catagory, DOB from Student_Details";
            using (SqlCommand cmd = new SqlCommand(cmdString, con))
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                dtStudent.Load(reader);

                con.Close();
            }
            return dtStudent;
        }

        private void DGVStudentList_DoubleClick(object sender, EventArgs e)
        {
            int rowToUpdate = DataGrideView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int id = Convert.ToInt16(DataGrideView.Rows[rowToUpdate].Cells["Student_id"].Value);

            StudentInfo nc = new StudentInfo();
            nc.Id = id;
            nc.IsUpdate = true;
            nc.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void addNewClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentAdmission nc = new StudentAdmission();
            nc.ShowDialog();
        }

        private void Filter()
        {
            DataView dvUser = dtStudent.DefaultView;
            dvUser.RowFilter = string.Format(@"CONVERT(Student_id,System.String) LIKE '%" + StudentId.Text + "%' AND " +
                                              "CONVERT(Name,System.String) LIKE '%" + StudentName.Text + "%' AND " +
                                              "CONVERT(Div,System.String) LIKE '%" + Div.Text + "%' AND " +
                                              "CONVERT(Roll_no,System.String) LIKE '%" + RollNo.Text + "%' AND " +
                                              "CONVERT(Gender,System.String) LIKE '" + Gender.Text + "%' AND " +
                                              "CONVERT(Catagory,System.String) LIKE '%" + Category.Text + "%' AND " +
                                              "CONVERT(DOB,System.String) LIKE '%" + dob.Text + "%'");

        }

        private void ClassId_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void StudentName_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Std_TextChanged(object sender, EventArgs e)
        {
            DataView dvUser = dtStudent.DefaultView;
            dvUser.RowFilter = string.Format("CONVERT(Std,System.String) LIKE '" + Std.Text + "'");
        }

        private void Div_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void RollNo_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Gender_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Category_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void dob_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master master = new Master();
            master.panelCall(Home.Instance);
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentAdmission sa = new StudentAdmission();
            sa.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void resetFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentId.Clear();
            StudentName.Clear();
            Std.Clear();
            Div.Clear();
            RollNo.Clear();
            Gender.Clear();
            Category.Clear();
            dob.Clear();
            StudentId.Focus();
        }

        private void generateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Student_id", typeof(Int16));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Std", typeof(string));
            dt.Columns.Add("Div", typeof(string));
            dt.Columns.Add("Roll_no", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("Catagory", typeof(string));
            dt.Columns.Add("DOB", typeof(DateTime));

            foreach (DataGridViewRow dgv in DataGrideView.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, dgv.Cells[6].Value, dgv.Cells[7].Value);
            }

            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample.xml");

            Report r = new Report("Student", ds);
            r.ShowDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel File|*.xls";
            sfd.Title = "Export to Excel";
            sfd.FileName = "StudentData";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string cmdString = "Select Student_id, First_name, Middle_name, Last_name, Full_name, Mother_name, Name_of_gardian, Address, City, State, Pincode, contact_no, Mob_no, Email, Aadhar_no, Gender, Place_of_birth, DOB, Nationality, Age, ClassOne, Name_of_privous_school, Address_of_privous_school, School_Mention_above_was_recognise, STD_in_privous_school, Year_of_passing, Date_of_admission, Std, Div, Roll_no, Relationship_with_student, Occupation, Annual_income, No_of_dependents_in_family, Phone_home, Phone_office, Religion, Caste, Mother_tongue, Catagory from Student_Details";
                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    DataSet ds = new DataSet("New_DataSet");
                    ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
                    ds.Tables.Add(dt);
                    ExcelLibrary.DataSetHelper.CreateWorkbook(sfd.FileName, ds);
                }
            }
        }
    }
}
