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

namespace School_Management_System
{
    public partial class ClassList : UserControl
    {
        private static ClassList _instance;

        public static ClassList Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClassList();
                return _instance;
            }
        }

        public ClassList()
        {
            InitializeComponent();
        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);
        
        private void ClassList_Load(object sender, EventArgs e)
        {
            ClassId.Focus();
            LoadDataIntoDataGridView();
        }

        private void LoadDataIntoDataGridView()
        {
            DataGrideView.DataSource = GetClassList();
        }

        private DataTable dtClass = new DataTable();

        private DataTable GetClassList()
        {
            dtClass.Rows.Clear();

            using (SqlCommand cmd = new SqlCommand("select Class_id, std, div, Student_strength, Class_shedule, Subject1, Subject2, Subject3, Subject4, Subject5, Subject6, Subject7, Subject8, Subject9, Subject10 from Class_Details", con))
            {
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                dtClass.Load(reader);
                con.Close();
            }
            return dtClass;
        }

        DataTable print;
        
        private void Filter()
        {
            DataView dvUser = dtClass.DefaultView;
            dvUser.RowFilter = string.Format(@"CONVERT(Class_id,System.String) LIKE '%" + ClassId.Text + "%' AND "+
                                               "CONVERT(div,System.String) LIKE '%" + Div.Text + "%' AND " +
                                              "CONVERT(Class_shedule,System.String) LIKE '%" + Shedule.Text + "%'");
            print = (DataTable)DataGrideView.DataSource;
        }

        private void ClassId_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void Std_TextChanged(object sender, EventArgs e)
        {
            DataView dvUser = dtClass.DefaultView;
            dvUser.RowFilter = string.Format("CONVERT(Std,System.String) LIKE '" + Std.Text + "'");
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
            ClassId.Clear();
            Std.Clear();
            Div.Clear();
            Shedule.Clear();
            ClassId.Focus();
        }

        private void addNewClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewClass cl = new NewClass();
            cl.ShowDialog();

            LoadDataIntoDataGridView();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewClass nc = new NewClass();
            nc.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void DataGrideView_DoubleClick(object sender, EventArgs e)
        {
            int rowToUpdate = DataGrideView.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            int id = Convert.ToInt16(DataGrideView.Rows[rowToUpdate].Cells["Class_id"].Value);

            ClassInfo nc = new ClassInfo();
            nc.Id = id;
            nc.IsUpdate = true;
            nc.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master ms = new Master();
            ms.panelCall(Home.Instance);
        }
    }
}
