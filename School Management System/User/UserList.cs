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
using School_Management_System.Model;

namespace School_Management_System
{
    public partial class UserList : UserControl
    {
        private static UserList _instance;

        public static UserList Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserList();
                return _instance;
            }
        }

        public UserList()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);


 
        private void User_list_Load(object sender, EventArgs e)
        {
            username.Focus();
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            DataGrideView.DataSource = GetUserList();
        }

        private DataTable dtUser = new DataTable();

        private DataTable GetUserList()
        {
            //DataTable dtUser = new DataTable();
            dtUser.Rows.Clear();

            using (SqlCommand cmd = new SqlCommand("select UserId, UserName, StaffId, StaffName from Users", con))
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                dtUser.Load(reader);
                con.Close();
            }

            return dtUser;
        }

        private void Filter()
        {
            DataView dvUser = dtUser.DefaultView;
            dvUser.RowFilter = string.Format(@"CONVERT(UserName,System.String) LIKE '%" + username.Text + "%' AND " +
                                              "CONVERT(StaffId,System.String) LIKE '%" + staff_id.Text + "%' AND " +
                                              "CONVERT(StaffName,System.String) LIKE '%" + staff_name.Text + "%'");
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void staff_id_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void staff_name_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void resetFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            username.Clear();
            staff_id.Clear();
            staff_name.Clear();
            username.Focus();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewUser nu = new NewUser();
            nu.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void UserDataGridView_DoubleClick(object sender, EventArgs e)
        {
            int rowToUpdate = DataGrideView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int id = Convert.ToInt16(DataGrideView.Rows[rowToUpdate].Cells["UserId"].Value);

            UserInfo ui = new UserInfo();
            ui.Id = id;
            ui.IsUpdate = true;
            ui.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void generateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            
            dt.Columns.Add("UserId", typeof(Int16));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("StaffId", typeof(string));
            dt.Columns.Add("StaffName", typeof(string));

            foreach (DataGridViewRow dgv in DataGrideView.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value);
            }

            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample.xml");

            Report r = new Report("User",ds);
            r.ShowDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel File|*.xls";
            sfd.Title = "Export to Excel";
            sfd.FileName = "UserData";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string cmdString = "Select * from Users";
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