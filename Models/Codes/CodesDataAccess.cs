﻿using LockServerAPI.Models.BaseDataAccesses;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace LockServerAPI.Models.Codes
{
    public class CodesDataAccess : BaseDataAccess, ICodesDataAccess
    {
        protected IConfiguration Configuration { get; }

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public CodesDataAccess(IConfiguration configuration, DatabaseContext database)
            : base(database)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Find code in database
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>User id</returns>
        public string FindCode(string code)
        {
            string result = null;
            using (var conn = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var query = @"select * from search_code(:code)";
                conn.Open();
                try
                {
                    var pgcom = new NpgsqlCommand(query, conn);
                    pgcom.CommandType = CommandType.Text;
                    pgcom.Parameters.AddWithValue("code", code);
                    var pgreader = pgcom.ExecuteReader();
                    if (pgreader.HasRows)
                    {
                        pgreader.Read();
                        result = pgreader.GetString(0);
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
