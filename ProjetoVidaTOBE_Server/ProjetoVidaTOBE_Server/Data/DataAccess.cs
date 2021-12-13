using MySql.Data.MySqlClient;
using ProjetoVidaTOBE_Server.Utils.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace ProjetoVidaTOBE_Server.Data
{
    public class DataAccess : IDataAccess
    {
        private MySqlConnection _connection;

        private readonly string _strConn;
        public MySqlConnection Connection { get { return this._connection; } }
        internal DataAccess(string connectionString)
        {
            this._strConn = connectionString;
        }

        private void OpenConnection()
        {
            try
            {
                if (this._connection == null)
                {
                    this._connection = new MySqlConnection(this._strConn);
                }
                if (this._connection.State == ConnectionState.Closed)
                {
                    this._connection.Open();
                }
            }
            catch (Exception ex)
            {
                var message = new List<string>
                {
                    $"connection string {this._strConn}",
                    $"connection_state {this._connection.State}"
                };
                LogUtil.Create(ex, EventLogEntryType.Error);
            }
        }
        private void CloseConnection()
        {
            try
            {
                if (this._connection.State == ConnectionState.Open)
                {
                    this._connection.Open();
                }
            }
            catch (Exception ex)
            {
                var message = new List<string>
                {
                    $"connection string {this._strConn}",
                    $"connection_state {this._connection.State}"
                };
                LogUtil.Create(ex, EventLogEntryType.Error);
            }
        }
        public long GetCount(string sql, params MySqlParameter[] parameters)
        {
            long scalar = 0;

            try
            {
                this.OpenConnection();

                using (var command = new MySqlCommand(sql, this._connection))
                {
                    command.CommandType = CommandType.Text;
                    scalar = Convert.ToInt64(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                var message = new List<string> { string.Format("sql: {0}", sql) };
                LogUtil.Create(ex);
            }

            return scalar;

        }

        public DataTable GetTable(string sql, params MySqlParameter[] parameters)
        {
            var table = new DataTable();
            try
            {
                this.OpenConnection();
                using (var command = new MySqlCommand(sql, this._connection))
                {
                    command.Parameters.AddRange(parameters);
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    if (reader != null)
                    {
                        table.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                var message = new List<string> { $"sql {sql}", "parameters:" };
                message.AddRange(parameters.Select(param => $"{param.ParameterName} = {param.Value}"));
                LogUtil.Create(ex);
            }
            finally
            {
                this.CloseConnection();
            }
            return table;
        }

        public DataRow GetRow(string sql, params MySqlParameter[] parameters)
        {
            var table = this.GetTable(sql, parameters);
            if (table.Rows == null || table.Rows.Count <= 0)
            {
                return null;
            }
            return table.Rows[0];
        }

        public void ExecuteCommand(string sql, params MySqlParameter[] parameters)
        {
            try
            {
                this.OpenConnection();

                using (var command = new MySqlCommand(sql, this._connection))
                {
                    command.Parameters.AddRange(parameters);
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                var message = new List<string> { $"sql {sql}", "parameters:" };
                message.AddRange(parameters.Select(param => $"{param.ParameterName} = {param.Value}"));
                LogUtil.Create(ex);
            }
            finally
            {
                this.CloseConnection();
            }
        }
    }
}
