using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProjectsMeasurements.DBContext
{
    public static class DataBaseFacadeExtensions
    {

        #region Methods

        public static T ExecuteScalar<T>(this DatabaseFacade database, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            for (int i = 0; i < parameters.Length; i++)
            {
                DbParameter Parameter = cmd.CreateParameter();
                Parameter.ParameterName = parameters[i].ParameterName;
                Parameter.Value = parameters[i].Value;
                cmd.Parameters.Add(Parameter);
            }
            if (database.CurrentTransaction != null)
                cmd.Transaction = (SqlTransaction)database.CurrentTransaction.GetDbTransaction();
            return (T)cmd.ExecuteScalar();
        }
        public static async Task<T?> ExecuteScalarAsync<T>(this DatabaseFacade database, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            for (int i = 0; i < parameters.Length; i++)
            {
                DbParameter Parameter = cmd.CreateParameter();
                Parameter.ParameterName = parameters[i].ParameterName;
                Parameter.Value = parameters[i].Value;
                cmd.Parameters.Add(Parameter);
            }
            return await ExecuteScalarAsync<T?>(database, cmd);
        }
        public static async Task<T?> ExecuteScalarAsync<T>(this DatabaseFacade database, DbCommand cmd)
        {
            if (database is null)
                throw new ArgumentNullException(nameof(database));
            if (cmd.Connection?.State != ConnectionState.Open)
                cmd.Connection?.Open();
            if (database.CurrentTransaction != null)
                cmd.Transaction = database.CurrentTransaction.GetDbTransaction();
            return (T?)await cmd.ExecuteScalarAsync();
        }
        public static async Task<T?> ExecuteScalarAsync<T>(this DatabaseFacade database, string commandText, CommandType commandType, string? ParamtersAndValues = null)
        {
            using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
            if (!string.IsNullOrWhiteSpace(ParamtersAndValues))
            {
                string[] strParametersTerms = ParamtersAndValues.Split("|||");
                if (strParametersTerms.Length > 0)
                {
                    for (int i = 0; i < strParametersTerms.Length; i++)
                    {
                        string[] strParameterTerm = strParametersTerms[i].Trim().Split('=');
                        if (strParameterTerm.Length == 2 && !string.IsNullOrWhiteSpace(strParameterTerm[0]) && !string.IsNullOrWhiteSpace(strParameterTerm[1]))
                            cmd.Parameters.Add(new SqlParameter($"@{strParameterTerm[0].Trim()}", strParameterTerm[1].Trim()));
                    }
                }
            }
            return await ExecuteScalarAsync<T?>(database, cmd);
        }

        #region Query Methods

        public static (bool, DataTable?, Exception?) ExecuteQuery(this DatabaseFacade database, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
            (bool, DataSet?, Exception?) dsData = ExecuteQuery(cmd);
            if (dsData.Item1)
            {
                if (dsData.Item2 == null || dsData.Item2.Tables.Count < 1)
                    return (true, null, null);
                else
                    return (true, dsData.Item2.Tables[0], null);
            }
            else
                return (false, null, dsData.Item3);
        }
        public static (bool, DataSet?, Exception?) ExecuteQuery(SqlCommand cmd)
        {
            try
            {
                SqlDataAdapter sqldaSelect = new(cmd);
                DataSet dsResult = new();
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                try
                {
                    sqldaSelect.Fill(dsResult);
                }
                catch (Exception ex)
                {
                    return (false, null, ex);
                }
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                if (dsResult == null)
                    return (true, null, null);
                return (true, dsResult, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex);
            }
        }
        public static async Task<(bool, DataTable?, Exception?)> ExecuteQueryAsync(this DatabaseFacade database, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (cmd.Connection.State != ConnectionState.Open)
                database.OpenConnection();
            cmd.Parameters.AddRange(parameters);
            (bool, DataSet?, Exception?) dsData = await ExecuteQueryAsync(cmd);
            if (!dsData.Item1 || dsData.Item2 == null || dsData.Item2.Tables.Count < 1)
                return (false, null, dsData.Item3);
            return (true, dsData.Item2.Tables[0], null);
        }
        public static Task<(bool, DataSet?, Exception?)> ExecuteQueryAsync(this DatabaseFacade database, DbCommand cmd)
        {
            if (database is null)
                throw new ArgumentNullException(nameof(database));
            return Task.Run((bool, DataSet?, Exception?) () =>
            {
                if (cmd.Connection?.State != ConnectionState.Open)
                    cmd.Connection?.Open();
                SqlDataAdapter sqldaSelect = new((SqlCommand)cmd);
                DataSet dsResult = new();
                if (database.CurrentTransaction != null)
                    cmd.Transaction = database.CurrentTransaction.GetDbTransaction();
                try
                {
                    sqldaSelect.Fill(dsResult);
                }
                catch (Exception ex)
                {
                    return (false, null, ex);
                }
                if (dsResult == null)
                    return (true, null, null);
                return (true, dsResult, null);
            });
        }
        public static Task<(bool, DataSet?, Exception?)> ExecuteQueryAsync(SqlCommand cmd)
        {
            return Task.Run((bool, DataSet?, Exception?) () =>
            {
                return ExecuteQuery(cmd);
            });
        }
        public static async Task<(bool, DataTable?, Exception?)> ExecuteQueryAsync(this DatabaseFacade database, string commandText, CommandType commandType, string? ParamtersAndValues = null)
        {
            try
            {
                using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
                cmd.CommandText = commandText;
                cmd.CommandType = commandType;
                if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                if (!string.IsNullOrWhiteSpace(ParamtersAndValues))
                {
                    string[] strParametersTerms = ParamtersAndValues.Split("|||", System.StringSplitOptions.RemoveEmptyEntries);
                    if (strParametersTerms.Length > 0)
                    {
                        for (int i = 0; i < strParametersTerms.Length; i++)
                        {
                            string[] strParameterTerm = strParametersTerms[i].Trim().Split('=');
                            if (strParameterTerm.Length == 2 && !string.IsNullOrWhiteSpace(strParameterTerm[0]) && !string.IsNullOrWhiteSpace(strParameterTerm[1]))
                                cmd.Parameters.Add(new SqlParameter($"@{strParameterTerm[0].Trim()}", strParameterTerm[1].Trim()));
                        }
                    }
                }
                (bool, DataSet?, Exception?) queryResult = await ExecuteQueryAsync(database, cmd);
                if (!queryResult.Item1 || queryResult.Item2 == null || queryResult.Item2.Tables.Count < 1)
                    return (false, null, queryResult.Item3);
                return (true, queryResult.Item2.Tables[0], null);
            }
            catch (Exception ex)
            {
                return (false, null, ex);
            }
        }
        public static async Task<(bool, DataSet?, Exception?)> ExecuteQueryDataSetAsync(this DatabaseFacade database, string commandText, CommandType commandType, string? ParamtersAndValues = null)
        {
            using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
            if (!string.IsNullOrWhiteSpace(ParamtersAndValues))
            {
                string[] strParametersTerms = ParamtersAndValues.Split("|||", System.StringSplitOptions.RemoveEmptyEntries);
                if (strParametersTerms.Length > 0)
                {
                    for (int i = 0; i < strParametersTerms.Length; i++)
                    {
                        string[] strParameterTerm = strParametersTerms[i].Trim().Split('=');
                        if (strParameterTerm.Length == 2 && !string.IsNullOrWhiteSpace(strParameterTerm[0]) && !string.IsNullOrWhiteSpace(strParameterTerm[1]))
                            cmd.Parameters.Add(new SqlParameter($"@{strParameterTerm[0].Trim()}", strParameterTerm[1].Trim()));
                    }
                }
            }
            return await ExecuteQueryAsync(database, cmd);
        }
        public static async Task<(bool, DataSet?, Exception?)> ExecuteQueryDataSetAsync(this DatabaseFacade database, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            cmd.Parameters.AddRange(parameters);
            return await ExecuteQueryAsync(cmd);
        }

        #endregion

        public static async Task<int> ExecuteNoneQyeryAsync(this DatabaseFacade database, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            using SqlCommand cmd = (SqlCommand)database.GetDbConnection().CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            int iExecutionResult = await cmd.ExecuteNonQueryAsync();
            return iExecutionResult;
        }
        public static async Task<int> ExecuteNoneQyeryAsync(this DatabaseFacade database, DbCommand cmd)
        {
            if (database is null)
                throw new ArgumentNullException(nameof(database));
            if (cmd.Connection?.State != ConnectionState.Open)
                cmd.Connection?.Open();
            if (database.CurrentTransaction != null)
                cmd.Transaction = database.CurrentTransaction.GetDbTransaction();
            return await cmd.ExecuteNonQueryAsync();
        }

        #endregion

    }
}
