using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using School_Management_System.Model;
using School_Management_System.Reports;

namespace School_Management_System
{
    public partial class Report : TempleteWindowsForm
    {
        string str;
        DataSet dss;
        public Report(string st,DataSet ds)
        {
            str = st;
            dss=ds;
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            if (str == "Student")
            {
                StudentReport sr = new StudentReport();
                sr.SetDataSource(dss);
                crystalReportViewer1.ReportSource = sr;
            }
            else if (str == "Staff")
            {
                StaffReport sr2 = new StaffReport();
                sr2.SetDataSource(dss);
                crystalReportViewer1.ReportSource = sr2;
            }
            else if (str == "User")
            {
                UserReport sr2 = new UserReport();
                sr2.SetDataSource(dss);
                crystalReportViewer1.ReportSource = sr2;
            }
            else if (str == "Class")
            {
                ClassReport cr = new ClassReport();
                cr.SetDataSource(dss);
                crystalReportViewer1.ReportSource = cr;
            }
        }
    }
}
