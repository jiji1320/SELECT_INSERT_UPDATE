using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SELECT_INSERT_UPDATE
{
    public partial class FrmClubRegistration : Form
    {
        public FrmClubRegistration()
        {
            InitializeComponent();
        }
        private ClubRegistrationQuery clubRegistrationQuery;
        private System.Windows.Forms.ComboBox cbPrograms;
        private System.Windows.Forms.TextBox txtMiddleInitial;
        int ID, count = 0;
        int Age;
        long StudentId;
        string FirstName;
        string MiddleName;
        string LastName;
        string Gender;
        string Program;




        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridViewMembers.DataSource = clubRegistrationQuery.bindingSource;
        }


        private int RegistrationID()
        {
            count++;
            return count;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
         
            try
            {
                ID = RegistrationID();
                StudentId = long.Parse(txtStudentID.Text);
                FirstName = txtFirstName.Text;
                MiddleName = txtMiddleName.Text;
                LastName = txtLastName.Text;
                Age = int.Parse(txtAge.Text);
                Gender = cbGender.Text;
                Program = cbProgram.Text;

                bool ok = clubRegistrationQuery.RegisterStudent(
                              ID, StudentId, FirstName, MiddleName, LastName,
                              Age, Gender, Program);

                if (ok)
                {
                    MessageBox.Show("Member Registered Successfully");
                    RefreshListOfClubMembers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            string[] listOfProgram = new string[]
    {
            "BS Information Technology",
            "BS Computer Science",
            "BS Information Systems",
            "BS in Accountancy",
            "BS in Hospitality Management",
            "BS in Tourism Management"
    };
            cbProgram.Items.AddRange(listOfProgram);

            string[] listOfGender = new string[] { "Male", "Female" };
            cbGender.Items.AddRange(listOfGender);

            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (btnRefresh.Enabled)
            {
                RefreshListOfClubMembers();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridViewMembers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a record first before updating.");
                return;
            }

            try
            {
                long studentId = long.Parse(
                    dataGridViewMembers.SelectedRows[0].Cells["StudentId"].Value.ToString()
                );

                FrmUpdateMember updateForm = new FrmUpdateMember();
                updateForm.ShowDialog();

                RefreshListOfClubMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting student ID for update: " + ex.Message);
            }

        }
    }
}





