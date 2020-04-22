using StudentVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using IStudentRepository;

namespace StudentRepositroy
{
   public class StudentRepo: IStudentRepo
    {
        #region PrivateObject
        IConfiguration config;
        string connection;
       public StudentRepo(IConfiguration _config)
        {
            config = _config;
            connection = config.GetSection("ConnectionStrings").GetSection("SqlConnection").Value;
        }
        #endregion

        #region StudentList
        public async Task<IEnumerable<StudentDetailsVM>> StudentList()
        {
            List<StudentDetailsVM> _studentLst = new List<StudentDetailsVM>();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "GetAllStudent";
                using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                {
                    while (sqlDataReader.Read())
                    {
                        StudentDetailsVM obj = new StudentDetailsVM()
                        {
                            StudentId = (int)(sqlDataReader["StudentId"] == DBNull.Value ? null : sqlDataReader["StudentId"]),
                            StudentName = (string)(sqlDataReader["StudentName"] == DBNull.Value ? null : sqlDataReader["StudentName"]),
                            Class = (string)(sqlDataReader["Class"] == DBNull.Value ? null : sqlDataReader["Class"]),
                            Divistion = (string)(sqlDataReader["Division"] == DBNull.Value ? null : sqlDataReader["Division"])
                        };
                        _studentLst.Add(obj);
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            return _studentLst;
        }
        #endregion

        #region StudentById
        public async Task<StudentDetailsVM> StudentById(int id)
        {
            StudentDetailsVM obj = new StudentDetailsVM();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "GetStudentById";
                sqlCommand.Parameters.AddWithValue("@StudentId", id);
                using (SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                {
                    while (sqlDataReader.Read())
                    {
                        obj.StudentId = (int)(sqlDataReader["StudentId"] == DBNull.Value ? null : sqlDataReader["StudentId"]);
                        obj.StudentName = (string)(sqlDataReader["StudentName"] == DBNull.Value ? null : sqlDataReader["StudentName"]);
                        obj.Class = (string)(sqlDataReader["Class"] == DBNull.Value ? null : sqlDataReader["Class"]);
                        obj.Divistion = (string)(sqlDataReader["Division"] == DBNull.Value ? null : sqlDataReader["Division"]);
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
            return obj;
        }
        #endregion

        #region SaveStudent
        public async Task<ResponseVM> SaveStudent(StudentDetailsVM model)
        {
            ResponseVM obj = new ResponseVM();
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "SaveStudent";
                    sqlCommand.Parameters.Add("@ResCode", SqlDbType.VarChar, 30);
                    sqlCommand.Parameters["@ResCode"].Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.Add("@ResMessage", SqlDbType.VarChar, 30);
                    sqlCommand.Parameters["@ResMessage"].Direction = ParameterDirection.Output;
                    sqlCommand.Parameters.AddWithValue("@StudentId", model.StudentId);
                    sqlCommand.Parameters.AddWithValue("@StudentName", model.StudentName);
                    sqlCommand.Parameters.AddWithValue("@Class", model.Class);
                    sqlCommand.Parameters.AddWithValue("@Division", model.Divistion);
                    await sqlCommand.ExecuteNonQueryAsync();
                    obj.ResCode = Convert.ToInt32(sqlCommand.Parameters["@ResCode"].Value);
                    obj.ResMessage = (string)sqlCommand.Parameters["@ResMessage"].Value;
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {

                }
            }
            return obj;
        } 
        #endregion
    }
}
