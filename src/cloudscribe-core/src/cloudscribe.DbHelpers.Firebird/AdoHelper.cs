﻿// Forked From Enterprise Library licensed under Ms-Pl http://www.codeplex.com/entlib
// but implementing a subset of the API from the 2.0 Application Blocks SqlHelper
// using implementation from the newer Ms-Pl version
// Modifications by Joe Audette
// Last Modified 2010-01-28
// 2014-08-26 modified by Joe Audette to use DBProviderFactory so that we can use Glimpse ADO
// for profiling http://blog.simontimms.com/2014/04/21/glimpse-for-raw-ado/
// 2014-08-26 Joe Audette created this version of SqlHelper renamed as AdoHelper and using the more generic
// Db classes via DbProviderFactory, this allows us to do profileing with Glimpse ADO
// 2015-01-07 Joe Audette added async methods

using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
//using log4net;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.IO;


namespace cloudscribe.DbHelpers.Firebird
{
    public static class AdoHelper
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(AdoHelper));

        private static DbProviderFactory GetFactory()
        {
            var factory = DbProviderFactories.GetFactory("FirebirdSql.Data.FirebirdClient");

            return factory;
        }


        private static DbConnection GetConnection(string connectionString)
        {
            var factory = GetFactory();
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;

            return connection;
        }

        public static String GetParamString(Int32 count)
        {
            if (count <= 1) { return count < 1 ? "" : "?"; }
            return "?," + GetParamString(count - 1);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private static void PrepareCommand(
            DbCommand command,
            DbConnection connection,
            DbTransaction transaction,
            CommandType commandType,
            string commandText,
            DbParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        private static void AttachParameters(DbCommand command, DbParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (DbParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        public static bool ExecuteBatchScript(
            string connectionString,
            string pathToScriptFile)
        {
            // http://stackoverflow.com/questions/9259034/the-type-of-the-sql-statement-could-not-be-determinated

            //FbScript script = new FbScript(pathToScriptFile);
            FbScript script;
            using (StreamReader sr = File.OpenText(pathToScriptFile))
            {
                script = new FbScript(sr.ReadToEnd());
            }


            if (script.Parse() > 0)
            {
                using (FbConnection connection = new FbConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        FbBatchExecution batch = new FbBatchExecution(connection, script);
                        batch.Execute(true);


                    }
                    catch (FbException ex)
                    {
                        //log.Error(ex);
                        throw new Exception(pathToScriptFile, ex);
                    }


                }

            }

            return true;

        }

        public static int ExecuteNonQuery(
            string connectionString,
            string commandText,
            params DbParameter[] commandParameters)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, commandText, commandParameters);
        }

        public static int ExecuteNonQuery(
            string connectionString,
            CommandType commandType,
            string commandText,
            params DbParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) { throw new ArgumentNullException("connectionString"); }

            DbProviderFactory factory = GetFactory();

            using (DbConnection connection = GetConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (DbCommand cmd = factory.CreateCommand())
                    {
                        PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        transaction.Commit();
                        return rowsAffected;
                    }
                }
            }
        }

        public static int ExecuteNonQuery(
            DbTransaction transaction,
            CommandType commandType,
            string commandText,
            params DbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            DbProviderFactory factory = GetFactory();
            DbCommand cmd = factory.CreateCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
            int retval = cmd.ExecuteNonQuery();
            return retval;
        }

        public static async Task<int> ExecuteNonQueryAsync(
            string connectionString,
            string commandText,
            params DbParameter[] commandParameters)
        {
            return await ExecuteNonQueryAsync(connectionString, CommandType.Text, commandText, commandParameters);
        }

        public static async Task<int> ExecuteNonQueryAsync(
            string connectionString,
            CommandType commandType,
            string commandText,
            params DbParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) { throw new ArgumentNullException("connectionString"); }

            DbProviderFactory factory = GetFactory();

            using (DbConnection connection = GetConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (DbCommand cmd = factory.CreateCommand())
                    {
                        PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        transaction.Commit();
                        return rowsAffected;
                    }
                }
            }
        }

        public static async Task<int> ExecuteNonQueryAsync(
            DbTransaction transaction,
            CommandType commandType,
            string commandText,
            params DbParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");

            DbProviderFactory factory = GetFactory();
            DbCommand cmd = factory.CreateCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
            int retval = await cmd.ExecuteNonQueryAsync();
            return retval;
        }

        public static DbDataReader ExecuteReader(
            string connectionString,
            string commandText,
            params DbParameter[] commandParameters)
        {
            return ExecuteReader(connectionString, CommandType.Text, commandText, commandParameters);
        }

        public static DbDataReader ExecuteReader(
            string connectionString,
            CommandType commandType,
            string commandText,
            params DbParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            DbConnection connection = null;
            DbProviderFactory factory = GetFactory();
            try
            {
                connection = GetConnection(connectionString);
                connection.Open();

                DbCommand command = factory.CreateCommand();

                PrepareCommand(
                    command,
                    connection,
                    null,
                    commandType,
                    commandText,
                    commandParameters);

                return command.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch
            {
                if ((connection != null) && (connection.State == ConnectionState.Open)) { connection.Close(); }
                throw;
            }
        }

        public static async Task<DbDataReader> ExecuteReaderAsync(
            string connectionString,
            string commandText,
            params DbParameter[] commandParameters)
        {
            return await ExecuteReaderAsync(connectionString, CommandType.Text, commandText, commandParameters);
        }

        public static async Task<DbDataReader> ExecuteReaderAsync(
            string connectionString,
            CommandType commandType,
            string commandText,
            params DbParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            DbConnection connection = null;
            DbProviderFactory factory = GetFactory();
            try
            {
                connection = GetConnection(connectionString);
                connection.Open();

                DbCommand command = factory.CreateCommand();

                PrepareCommand(
                    command,
                    connection,
                    null,
                    commandType,
                    commandText,
                    commandParameters);

                return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);

            }
            catch
            {
                if ((connection != null) && (connection.State == ConnectionState.Open)) { connection.Close(); }
                throw;
            }
        }

        public static object ExecuteScalar(
            string connectionString,
            string commandText,
            params DbParameter[] commandParameters)
        {
            return ExecuteScalar(connectionString, CommandType.Text, commandText, commandParameters);
        }

        public static object ExecuteScalar(
            string connectionString,
            CommandType commandType,
            string commandText,
            params DbParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            DbProviderFactory factory = GetFactory();

            using (DbConnection connection = GetConnection(connectionString))
            {
                connection.Open();
                DbTransaction transaction = null;
                bool useTransaction = (commandText.Contains("EXECUTE") || commandText.Contains("INSERT"));
                if (useTransaction) { transaction = connection.BeginTransaction(); }

                using (DbCommand command = factory.CreateCommand())
                {
                    PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
                    object result = command.ExecuteScalar();

                    if (transaction != null)
                    {
                        transaction.Commit();
                        transaction.Dispose();
                        transaction = null;

                    }

                    return result;

                }
            }
        }

        public static async Task<object> ExecuteScalarAsync(
            string connectionString,
            string commandText,
            params DbParameter[] commandParameters)
        {
            return await ExecuteScalarAsync(connectionString, CommandType.Text, commandText, commandParameters);
        }

        public static async Task<object> ExecuteScalarAsync(
            string connectionString,
            CommandType commandType,
            string commandText,
            params DbParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            DbProviderFactory factory = GetFactory();

            using (DbConnection connection = GetConnection(connectionString))
            {
                connection.Open();
                DbTransaction transaction = null;
                bool useTransaction = (commandText.Contains("EXECUTE") || commandText.Contains("INSERT"));
                if (useTransaction) { transaction = connection.BeginTransaction(); }

                using (DbCommand command = factory.CreateCommand())
                {
                    PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
                    object result = await command.ExecuteScalarAsync();

                    if (transaction != null)
                    {
                        transaction.Commit();
                        transaction.Dispose();
                        transaction = null;

                    }

                    return result;

                }
            }
        }

        //public static DataSet ExecuteDataset(
        //    string connectionString,
        //    string commandText,
        //    params DbParameter[] commandParameters)
        //{
        //    return ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
        //}

        //public static DataSet ExecuteDataset(
        //    string connectionString,
        //    CommandType commandType,
        //    string commandText,
        //    params DbParameter[] commandParameters)
        //{
        //    if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

        //    DbProviderFactory factory = GetFactory();

        //    using (DbConnection connection = GetConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (DbCommand command = factory.CreateCommand())
        //        {
        //            PrepareCommand(command, connection, (DbTransaction)null, commandType, commandText, commandParameters);

        //            using (DbDataAdapter adapter = factory.CreateDataAdapter())
        //            {
        //                adapter.SelectCommand = command;
        //                DataSet dataSet = new DataSet();
        //                adapter.Fill(dataSet);
        //                return dataSet;
        //            }
        //        }
        //    }
        //}

    }

}
