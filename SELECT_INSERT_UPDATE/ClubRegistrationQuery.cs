using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SELECT_INSERT_UPDATE
{
    internal class ClubRegistrationQuery
    {

        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlDataReader;
        public DataTable dataTable;
        public BindingSource bindingSource;
        private string connectionString;
        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        int _Age;

        public ClubRegistrationQuery()
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            C:\USERS\FRANCISCONATHAN\DOCUMENTS\CLUBDB.MDF;Integrated Security=True; Connect Timeout=30";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            string query = @"Select StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";
            sqlAdapter = new SqlDataAdapter(query, sqlConnect);
            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;
            return true;
        }

        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            try
            {
                sqlCommand = new SqlCommand("INSERT INTO ClubMembers VALUES(@ID,@StudentID,@FirstName" +
               ",@MiddleName,@LastName,@Age,@Gender,@Program)", sqlConnect);
                sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
                sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
                sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
                sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
                sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;

                sqlConnect.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnect.Close();
                return true;

            }
            catch (Exception ex)
            {
                if (sqlConnect.State == ConnectionState.Open)
                    sqlConnect.Close();
                MessageBox.Show("Error inserting student: " + ex.Message);
                return false;
            }
        }

        public bool UpdateStudent(long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            using (SqlConnection sqlConnect = new SqlConnection(connectionString))
            {
                string query = "UPDATE ClubMembers SET FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, Age=@Age, Gender=@Gender, Program=@Program WHERE StudentId=@StudentID";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnect))
                {
                    sqlCommand.Parameters.AddWithValue("@StudentID", StudentID);
                    sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
                    sqlCommand.Parameters.AddWithValue("@MiddleName", MiddleName);
                    sqlCommand.Parameters.AddWithValue("@LastName", LastName);
                    sqlCommand.Parameters.AddWithValue("@Age", Age);
                    sqlCommand.Parameters.AddWithValue("@Gender", Gender);
                    sqlCommand.Parameters.AddWithValue("@Program", Program);

                    sqlConnect.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }

}

