using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseForm
{
    class DBUtils
    {
        static SqlConnection connection;
        static SqlConnection getConnection()
        {
            String connectionString = "Data Source=POPAL-PC\\SQLEXPRESS;Initial Catalog=school;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        static void closeConnection()
        {
            connection.Close();
        }

        public static DataTable getStudents()
        {
            DataTable dataTable = new DataTable();

            using (SqlCommand command = new SqlCommand("get_students", getConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader dataReader = command.ExecuteReader();
                dataTable.Load(dataReader);
            }

            return dataTable;
        }

        public static void insertStudent(String name, String lastName, Double fee)
        {
            using (SqlCommand command = new SqlCommand("insert_student", getConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@last_name", lastName);
                command.Parameters.AddWithValue("@fee", fee);
                command.ExecuteNonQuery();
                closeConnection();
            }
        }

        public static void updateStudent(int selectedId, string name, string lastName, double fee)
        {
            using (SqlCommand command = new SqlCommand("update_student", getConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", selectedId);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@last_name", lastName);
                command.Parameters.AddWithValue("@fee", fee);
                command.ExecuteNonQuery();
                closeConnection();
            }
        }

        public static void deleteStudent(int selectedId)
        {
            using (SqlCommand command = new SqlCommand("delete_student", getConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", selectedId);
                command.ExecuteNonQuery();
                closeConnection();

            }
        }
        public static DataTable searchStudent(string search)
        {
            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand("search_student", getConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@s", search);
                SqlDataReader dataReader = command.ExecuteReader();
                dataTable.Load(dataReader);
                return dataTable;
            }
        }
    }
}
