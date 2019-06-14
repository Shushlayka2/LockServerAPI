using LockServerAPI.Models.BaseDataAccesses;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace LockServerAPI.Models.User
{
    public class UserDataAccess : BaseDataAccess, IUserDataAccess
    {
        protected IConfiguration Configuration { get; }

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="database">Database context</param>
        public UserDataAccess(IConfiguration configuration, DatabaseContext database)
            : base(database)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Find user in database
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Is user found/returns>
        public bool FindUser(string username, string password)
        {
            var result = false;
            using (var conn = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var query = @"select * from search_user(:username, :password)";
                conn.Open();
                try
                {
                    var pgcom = new NpgsqlCommand(query, conn);
                    pgcom.CommandType = CommandType.Text;
                    pgcom.Parameters.AddWithValue("username", username);
                    pgcom.Parameters.AddWithValue("password", password);
                    var pgreader = pgcom.ExecuteReader();
                    if (pgreader.HasRows)
                    {
                        pgreader.Read();
                        result = pgreader.GetBoolean(0);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
    }
}
