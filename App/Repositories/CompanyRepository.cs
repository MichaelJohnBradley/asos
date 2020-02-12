using App.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace App.Repositories
{
    public class CompanyRepository : ICompanyRepository 
    {

        private readonly string _connectionString;
        public CompanyRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

        }
        public Company GetById(int id)
        {
            Company company = null;           

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "uspGetCompanyById"
                };

                var parameter = new SqlParameter("@CompanyId", SqlDbType.Int) { Value = id };
                command.Parameters.Add(parameter);

                connection.Open();
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    company = new Company
                                      {
                                          Id = int.Parse(reader["CompanyId"].ToString()),
                                          Name = reader["Name"].ToString(),
                                          Classification = (Classification)int.Parse(reader["ClassificationId"].ToString())
                                      };
                }
            }

            return company;
        }
    }
}
