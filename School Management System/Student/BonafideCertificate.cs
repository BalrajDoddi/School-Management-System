using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using School_Management_System.Model;

namespace School_Management_System.Student
{
    public partial class BonafideCertificate : TempleteWindowsForm
    {
        string _Id, _Date, _FullName, _Std, _DOB, _DateInWords, _BirthPlace, _Religion, _Caste, _Category;
        public BonafideCertificate(string Id, string Date, string FullName, string Std, string DOB, string DateInWords, string BirthPlace, string Religion, string Caste, string Category)
        {
            _Id = Id;
            _Date = Date;
            _FullName = FullName;
            _Std = Std;
            _DOB = DOB;
            _DateInWords = DateInWords;
            _BirthPlace = BirthPlace;
            _Religion = Religion;
            _Caste = Caste;
            _Category = Category;
            InitializeComponent();
        }

        private void BonafideCertificate_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("Id",_Id),
                new Microsoft.Reporting.WinForms.ReportParameter("Date",_Date),
                new Microsoft.Reporting.WinForms.ReportParameter("FullName",_FullName),
                new Microsoft.Reporting.WinForms.ReportParameter("Std",_Std),
                new Microsoft.Reporting.WinForms.ReportParameter("DOB",_DOB),
                new Microsoft.Reporting.WinForms.ReportParameter("DateInWords",_DateInWords),
                new Microsoft.Reporting.WinForms.ReportParameter("BirthPlace",_BirthPlace),
                new Microsoft.Reporting.WinForms.ReportParameter("Religion",_Religion),
                new Microsoft.Reporting.WinForms.ReportParameter("Caste",_Caste),
                new Microsoft.Reporting.WinForms.ReportParameter("Category",_Category)
            };
            this.reportViewer1.LocalReport.SetParameters(para);
            this.reportViewer1.RefreshReport();
        }
    }
}
