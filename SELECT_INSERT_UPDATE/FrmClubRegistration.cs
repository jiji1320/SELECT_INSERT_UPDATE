using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SELECT_INSERT_UPDATE
{
    public partial class FrmClubRegistration : Form
    {
        public FrmClubRegistration()
        {
            InitializeComponent();
        }
        ClubRegistrationQuery clubRegistrationQuery;
       

            
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridViewMembers.DataSource = clubRegistrationQuery.bindingSource;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {

            ClubRegistrationQuery.RegisterStudent(
                    RegistrationID(),
                    long.Parse(txtStudentId.Text),
                    txtFirstname.Text,
                    txtMiddlename.Text,
                    txtLastname.Text,
                    int.Parse(txtAge.Text),
                    cbGender.Text,
                    cbProgram.Text
                ); 
            RefreshListOfClubMembers();

        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            ClubRegistrationQuery ClubRegistrationQuery = new ClubRegistrationQuery();
        }
    }
}
