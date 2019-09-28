using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using School_Management_System.Staff;

namespace School_Management_System
{
    public partial class StaffList : UserControl
    {
        public StaffList()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);
        
        private static StaffList _instane;

        public static StaffList Instance
        {
            get
            {
                if (_instane == null)
                    _instane = new StaffList();
                return _instane;
            }
        }

        
        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaffEnroll se = new StaffEnroll();
            se.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void StaffList_Load(object sender, EventArgs e)
        {
            StaffId.Focus();
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            DataGrideView.DataSource = GetUserList();
            DataGrideView.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private DataTable dtUser = new DataTable();

        private DataTable GetUserList()
        {
            dtUser.Rows.Clear();

            using (SqlCommand cmd = new SqlCommand("select Staff_id, staff_full_name as Name, gender, contract_termination, Subject1, Subject2, Subject3, Subject4 from Staff_Details", con))
                {
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    dtUser.Load(reader);
                    con.Close();
               
            }

            return dtUser;
        }

        private void DataGrideView_DoubleClick(object sender, EventArgs e)
        {
            int rowToUpdate = DataGrideView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int id = Convert.ToInt16(DataGrideView.Rows[rowToUpdate].Cells["Staff_id"].Value);

            StaffInfo ui = new StaffInfo();
            ui.Id = id;
            ui.IsUpdate = true;
            ui.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void Filter()
        {
            DataView dvUser = dtUser.DefaultView;
            dvUser.RowFilter = string.Format(@"CONVERT(Staff_id,System.String) LIKE '%" + StaffId.Text + "%' AND " +
                                              "CONVERT(Name,System.String) LIKE '%" + FullName.Text + "%' AND " +
                                              "CONVERT(gender,System.String) LIKE '" + Gender.Text + "%' AND " +
                                              "(CONVERT(Subject1,System.String) LIKE '" + Subject.Text + "%' OR "+
                                              "CONVERT(Subject2,System.String) LIKE '" + Subject.Text + "%' OR "+
                                              "CONVERT(Subject3,System.String) LIKE '" + Subject.Text + "%' OR "+
                                              "CONVERT(Subject4,System.String) LIKE '" + Subject.Text + "%')");
        }

        private void ClassId_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Std_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Div_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Shedule_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void resetFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaffId.Clear();
            FullName.Clear();
            Gender.Clear();
            Subject.Clear();
            StaffId.Focus();
        }

        private void generateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Staff_id", typeof(Int16));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("gender", typeof(string));
            dt.Columns.Add("contract_termination", typeof(DateTime));
            dt.Columns.Add("Subject1", typeof(string));
            dt.Columns.Add("Subject2", typeof(string));
            dt.Columns.Add("Subject3", typeof(string));
            dt.Columns.Add("Subject4", typeof(string));

            foreach (DataGridViewRow dgv in DataGrideView.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, dgv.Cells[6].Value, dgv.Cells[7].Value);
            }

            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample.xml");

            Report r = new Report("Staff", ds);
            r.ShowDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel File|*.xls";
            sfd.Title = "Export to Excel";
            sfd.FileName = "StaffData";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string cmdString = "Select Staff_id, staff_first_name, staff_middle_name, staff_last_name, staff_full_name, address, city, state, pincode, mob_no, email, religion, caste, Aadhar_no, gender, birth_place, dob, nationality, age, qualification, summery_of_qual, enroll_year, job_status, enroll_date, contract_termination, pre_exp, no_of_yr_exp, remark, pre_emp, Subject1, Subject2, Subject3, Subject4 from Staff_Details";
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
