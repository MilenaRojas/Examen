using Lab3MVC.Models.Domain;
using System.Data.SqlClient;

namespace Lab3MVC.Models.Data
{
    public class StudentDAO
    {
        private readonly IConfiguration _configuration;
        string connectionString;

        public StudentDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public int Insert(Student student)
        {
            int resultToReturn = 0;//it will save 1 or 0 depending on the result of insertion
            Exception? exception = new Exception();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertStudent", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@Password", student.Password);
                    command.Parameters.AddWithValue("@MajorId", student.Major.Id);

                    resultToReturn = command.ExecuteNonQuery();
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                exception = ex;
                throw exception;
            }


            return resultToReturn;

        }

        public List<Student> Get()
        {

            List<Student> students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand command = new SqlCommand("GetAllStudents", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {

                    students.Add(new Student
                    {
                        Id = Convert.ToInt32(sqlDataReader["Id"]),
                        Name = sqlDataReader["Name"].ToString(),
                        Email = sqlDataReader["Email"].ToString(),
                        Major = new Major(0, null, sqlDataReader["MajorName"].ToString())

                    });

                }

                connection.Close();

                return students;

            }
        }

        public Student Get(string email)
        {
            Student student = new Student();
            Exception? exception = new Exception();
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    connection.Open();
                    SqlCommand command = new SqlCommand("GetStudent", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);

                    SqlDataReader sqlDataReader = command.ExecuteReader();


                    if (sqlDataReader.Read())
                    {
                        student.Id = Convert.ToInt32(sqlDataReader.GetInt32(0));
                        student.Name = sqlDataReader.GetString(1);
                        student.Email = sqlDataReader.GetString(2);
                        student.Major = new Major(Convert.ToInt32(sqlDataReader.GetInt32(4)), null, null);
                    }

                    connection.Close();


                    return student;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                throw exception;
            }
        }

        public List<Student> SelectStudent(string name)
        {
            List<Student> students = new List<Student>();
            Exception? exception = new Exception();
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SelectStudentByName", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", name);

                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        students.Add(new Student
                        {
                            Id = Convert.ToInt32(sqlDataReader["Id"]),
                            Name = sqlDataReader["Name"].ToString(),
                            Email = sqlDataReader["Email"].ToString(),
                            Major = new Major(0, null, sqlDataReader["MajorName"].ToString())

                        });

                    }

                    connection.Close();


                    return students;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                throw exception;
            }
        }


        public int Update(Student student)
        {
            int resultToReturn = 0;//it will save 1 or 0 depending on the result of insertion
            Exception? exception = new Exception();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    connection.Open();
                    SqlCommand command = new SqlCommand("UpdateStudent", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Email", student.Email);
                    command.Parameters.AddWithValue("@Password", student.Password);
                    command.Parameters.AddWithValue("@MajorId", student.Major.Id);

                    resultToReturn = command.ExecuteNonQuery();
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                exception = ex;
                throw exception;
            }


            return resultToReturn;

        }
    }
}
