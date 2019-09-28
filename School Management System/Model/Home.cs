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

namespace School_Management_System
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }
        private static Home _instance;

        public static Home Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Home();
                return _instance;
            }
        }

        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbx"].ConnectionString);

        private int imageNumber = 1;

        private void LoadNextImage()
        {
            if (imageNumber == 7)
            {
                imageNumber = 1;
            }
            pictureBox1.ImageLocation = string.Format(@"Images\{0}.jpg", imageNumber);
            imageNumber++;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadNextImage();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            GetTotal("Student_Details",TotalStudent);
            GetTotal("Staff_Details",TotalStaff);
            GetTotal("Class_Details",TotalClasses);
            GetTotal("Users",TotalUsers);
        }

        private void GetTotal(String TableName,Label total)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Count(*) as total FROM " + TableName, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            total.Text = dt.Rows[0][0].ToString();
            con.Close();
        }
    }
}
