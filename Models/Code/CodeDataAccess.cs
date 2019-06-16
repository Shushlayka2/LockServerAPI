using LockServerAPI.Models.BaseDataAccesses;
using LockServerAPI.ViewModels;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LockServerAPI.Models.Code
{
    public class CodeDataAccess : BaseDataAccess, ICodeDataAccess
    {
        protected IConfiguration Configuration { get; }

        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="configuration">Configuration</param>
        /// <param name="database">Database context</param>
        public CodeDataAccess(IConfiguration configuration, DatabaseContext database)
            : base(database)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Get all existing codes
        /// </summary>
        /// <returns>Codes collection</returns>
        public List<Code> GetCodes()
        {
            List<Code> result = null;
            using (var conn = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var query = @"select * from referencedata.decrypted_codes";
                conn.Open();
                try
                {
                    var pgcom = new NpgsqlCommand(query, conn);
                    pgcom.CommandType = CommandType.Text;
                    var pgreader = pgcom.ExecuteReader();
                    if (pgreader.HasRows)
                    {
                        var dt = new DataTable();
                        dt.Load(pgreader);
                        result = (from DataRow dr in dt.Rows
                                  select new Code()
                                  {
                                      Id = Convert.ToInt32(dr["id"]),
                                      CodeVal = dr["code"].ToString(),
                                      LockId = dr["lock_id"].ToString(),
                                      Config = dr["config"].ToString()
                                  }).ToList();
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Generate new code
        /// </summary>
        /// <param name="lockId">Lock identifier</param>
        /// <param name="config">Connection configuration</param>
        public void GenerateCode(string lockId, string config)
        {
            var code = new Code();
            code.GenerateSecretCode();
            code.LockId = lockId;
            code.Config = config;
            Database.Codes.Add(code);
            Database.SaveChanges();
        }

        public void EditCode(CodeViewModel model)
        {
            var oldCode = (from elem in GetCodes()
                           where elem.Id == model.Id
                           select elem).FirstOrDefault();
            var newCode = oldCode;
            newCode.LockId = model.LockId ?? newCode.LockId;
            newCode.Config = model.Config ?? newCode.Config;
            using (var conn = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var query = @"select from edit_code(:id, :code, :lock_id, :config)";
                conn.Open();
                try
                {
                    var pgcom = new NpgsqlCommand(query, conn);
                    pgcom.CommandType = CommandType.Text;
                    pgcom.Parameters.AddWithValue("id", newCode.Id);
                    pgcom.Parameters.AddWithValue("code", newCode.CodeVal);
                    pgcom.Parameters.AddWithValue("lock_id", newCode.LockId);
                    pgcom.Parameters.AddWithValue("config", newCode.Config);
                    var pgreader = pgcom.ExecuteReader();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Delete the code
        /// </summary>
        /// <param name="code">Code</param>
        public void RemoveCode(Code code)
        {
            using (var conn = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection")))
            {
                var query = @"select from remove_code(:code, :lock_id)";
                conn.Open();
                try
                {
                    var pgcom = new NpgsqlCommand(query, conn);
                    pgcom.CommandType = CommandType.Text;
                    pgcom.Parameters.AddWithValue("code", code.CodeVal);
                    pgcom.Parameters.AddWithValue("lock_id", code.LockId);
                    var pgreader = pgcom.ExecuteReader();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Find code in database
        /// </summary>
        /// <param name="code">Code</param>
        /// <returns>User id</returns>
        public (string lockId, string config) FindCode(string code)
        {
            (string lockId, string config) tuple = (null, null);
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
                        tuple = (pgreader.GetString(0), pgreader.GetString(1));
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return tuple;
        }
    }
}
