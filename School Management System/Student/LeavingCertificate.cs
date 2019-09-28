using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using School_Management_System.Model;

namespace School_Management_System.Certificates
{
    public partial class LeavingCertificate : TempleteWindowsForm
    {
        String _Id, _FullName, _Category, _Caste, _Nationality, _BirthPlace, _DOB, _DOBInWords, _LastSchool, _AdmissionDate, _AdmissionStd, _Progress, _Conduct, _LeavingDate, _Std, _ReasonForLeaving, _Remark;
        public LeavingCertificate(string Id, string FullName, string Category, string Caste, string Nationality, string BirthPlace, string DOB, string DOBInWords, string LastSchool, string AdmissionDate, string AdmissionStd, string Progress, string Conduct, string LeavingDate, string Std, string ReasonForLeaving, string Remark)
        {
            _Id = Id;
            _FullName = FullName;
            _Category = Category;
            _Caste = Caste;
            _Nationality = Nationality;
            _BirthPlace = BirthPlace;
            _DOB = DOB;
            _DOBInWords = DOBInWords;
            _LastSchool = LastSchool;
            _AdmissionDate = AdmissionDate;
            _AdmissionStd = AdmissionStd;
            _Progress = Progress;
            _Conduct = Conduct;
            _LeavingDate = LeavingDate;
            _Std = Std;
            _ReasonForLeaving = ReasonForLeaving;
            _Remark = Remark;
            InitializeComponent();
        }

        private void Certificate_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[] 
            {
                new Microsoft.Reporting.WinForms.ReportParameter("Id",_Id),
                new Microsoft.Reporting.WinForms.ReportParameter("FullName",_FullName),
                new Microsoft.Reporting.WinForms.ReportParameter("Category",_Category),
                new Microsoft.Reporting.WinForms.ReportParameter("Caste",_Caste),
                new Microsoft.Reporting.WinForms.ReportParameter("Nationality",_Nationality),
                new Microsoft.Reporting.WinForms.ReportParameter("PlaceOfBirth",_BirthPlace),
                new Microsoft.Reporting.WinForms.ReportParameter("DOB",_DOB),
                new Microsoft.Reporting.WinForms.ReportParameter("DOBInWords",_DOBInWords),
                new Microsoft.Reporting.WinForms.ReportParameter("LastSchool",_LastSchool),
                new Microsoft.Reporting.WinForms.ReportParameter("AdmissionDate",_AdmissionDate),
                new Microsoft.Reporting.WinForms.ReportParameter("AdmissionStd",_AdmissionStd),
                new Microsoft.Reporting.WinForms.ReportParameter("Progress",_Progress),
                new Microsoft.Reporting.WinForms.ReportParameter("Conduct",_Conduct),
                new Microsoft.Reporting.WinForms.ReportParameter("LeavingDate",_LeavingDate),
                new Microsoft.Reporting.WinForms.ReportParameter("Std",_Std),
                new Microsoft.Reporting.WinForms.ReportParameter("ReasonForLeaving",_ReasonForLeaving),
                new Microsoft.Reporting.WinForms.ReportParameter("Remark",_Remark)
            };

            this.reportViewer1.LocalReport.SetParameters(para);
            this.reportViewer1.RefreshReport();
        }
    }
}
