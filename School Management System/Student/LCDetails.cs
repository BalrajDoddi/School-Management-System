using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using School_Management_System.Model;
using School_Management_System.Certificates;
using School_Management_System.Student;

namespace School_Management_System.Student
{
    public partial class LCDetails : TempleteWindowsForm
    {
        String _Id, _FullName, _Category, _Caste, _Nationality, _BirthPlace, _DOB, _DOBInWords, _LastSchool, _AdmissionDate, _AdmissionStd, _Std;
        public LCDetails(string Id, string FullName, string Category, string Caste, string Nationality, string BirthPlace, string DOB, string DOBInWords, string LastSchool, string AdmissionDate, string AdmissionStd, string Std)
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
            _Std = Std;

            InitializeComponent();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (IsValidate())
            {
                string _Progress, _Conduct, _LeavingDate, _ReasonForLeaving, _Remark;
                _Progress = Progress.Text;
                _Conduct = Conduct.Text;
                _LeavingDate = DateTime.Now.Date.ToString("MMMM yyyy");
                _ReasonForLeaving = ReasonForLeaving.Text;
                _Remark = Remark.Text;
                using (LeavingCertificate cer = new LeavingCertificate(string.Format("{0}", _Id), string.Format("{0}", _FullName), string.Format("{0}", _Category), string.Format("{0}", _Caste), string.Format("{0}", _Nationality), string.Format("{0}", _BirthPlace), string.Format("{0}", _DOB), string.Format("{0}", _DOBInWords), string.Format("{0}", _LastSchool), string.Format("{0}", _AdmissionDate), string.Format("{0}", _AdmissionStd), string.Format("{0}", _Progress), string.Format("{0}", _Conduct), string.Format("{0}", _LeavingDate), string.Format("{0}", _Std), string.Format("{0}", _ReasonForLeaving), string.Format("{0}", _Remark)))
                {
                    cer.ShowDialog();
                }
                this.Close();
            }
        }

        private bool IsValidate()
        {
            if (Progress.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Progress is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Progress.Focus();
                return false;
            }

            if (Conduct.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Conduct is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Conduct.Focus();
                return false;
            }

            if (ReasonForLeaving.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Reason for Leave is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReasonForLeaving.Focus();
                return false;
            }

            if (Remark.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Remark is Required.", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Remark.Focus();
                return false;
            }
            return true;
        }
    }
}
