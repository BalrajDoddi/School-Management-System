using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using School_Management_System.Model;

namespace School_Management_System.Staff
{
    public partial class BonafideCertificate : TempleteWindowsForm
    {
        String _FullName, _FromDate, _ToDate;
        public BonafideCertificate(String FullName, String FromDate, String ToDate)
        {
            _FullName = FullName;
            _FromDate = FromDate;
            _ToDate = ToDate;
            InitializeComponent();
        }

        private void ExperianceCertificate_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[] 
            {
                new Microsoft.Reporting.WinForms.ReportParameter("FullName",_FullName),
                new Microsoft.Reporting.WinForms.ReportParameter("FromDate",_FromDate),
                new Microsoft.Reporting.WinForms.ReportParameter("ToDate",_ToDate)
            };
            this.reportViewer1.LocalReport.SetParameters(para);
            this.reportViewer1.RefreshReport();
        }
    }
}
