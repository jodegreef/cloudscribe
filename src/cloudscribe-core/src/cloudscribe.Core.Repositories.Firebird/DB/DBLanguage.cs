﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:				    2008-06-22
// Last Modified:			2015-06-12
// 

using cloudscribe.DbHelpers.Firebird;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Framework.Logging;
using System;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace cloudscribe.Core.Repositories.Firebird
{
    internal class DBLanguage
    {
        internal DBLanguage(
            string dbReadConnectionString,
            string dbWriteConnectionString,
            ILoggerFactory loggerFactory)
        {
            logFactory = loggerFactory;
            readConnectionString = dbReadConnectionString;
            writeConnectionString = dbWriteConnectionString;
        }

        private ILoggerFactory logFactory;
        //private ILogger log;
        private string readConnectionString;
        private string writeConnectionString;


        /// <summary>
        /// Inserts a row in the mp_Language table. Returns rows affected count.
        /// </summary>
        /// <param name="guid"> guid </param>
        /// <param name="name"> name </param>
        /// <param name="code"> code </param>
        /// <param name="sort"> sort </param>
        /// <returns>int</returns>
        public async Task<bool> Create(
            Guid guid,
            string name,
            string code,
            int sort)
        {
            #region Bit Conversion


            #endregion

            FbParameter[] arParams = new FbParameter[4];

            arParams[0] = new FbParameter("@Guid", FbDbType.Char, 36);
            arParams[0].Value = guid.ToString();

            arParams[1] = new FbParameter("@Name", FbDbType.VarChar, 255);
            arParams[1].Value = name;

            arParams[2] = new FbParameter("@Code", FbDbType.Char, 2);
            arParams[2].Value = code;

            arParams[3] = new FbParameter("@Sort", FbDbType.Integer);
            arParams[3].Value = sort;

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("INSERT INTO mp_Language (");
            sqlCommand.Append("Guid, ");
            sqlCommand.Append("Name, ");
            sqlCommand.Append("Code, ");
            sqlCommand.Append("\"Sort\" )");

            sqlCommand.Append(" VALUES (");
            sqlCommand.Append("@Guid, ");
            sqlCommand.Append("@Name, ");
            sqlCommand.Append("@Code, ");
            sqlCommand.Append("@Sort )");
            sqlCommand.Append(";");

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return rowsAffected > 0;

        }


        /// <summary>
        /// Updates a row in the mp_Language table. Returns true if row updated.
        /// </summary>
        /// <param name="guid"> guid </param>
        /// <param name="name"> name </param>
        /// <param name="code"> code </param>
        /// <param name="sort"> sort </param>
        /// <returns>bool</returns>
        public async Task<bool> Update(
            Guid guid,
            string name,
            string code,
            int sort)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("UPDATE mp_Language ");
            sqlCommand.Append("SET  ");
            sqlCommand.Append("Name = @Name, ");
            sqlCommand.Append("Code = @Code, ");
            sqlCommand.Append("\"Sort\" = @Sort ");

            sqlCommand.Append("WHERE  ");
            sqlCommand.Append("Guid = @Guid ");
            sqlCommand.Append("OR Guid = UPPER(@Guid) ");
            sqlCommand.Append(";");

            FbParameter[] arParams = new FbParameter[4];

            arParams[0] = new FbParameter("@Guid", FbDbType.Char, 36);
            arParams[0].Value = guid.ToString();

            arParams[1] = new FbParameter("@Name", FbDbType.VarChar, 255);
            arParams[1].Value = name;

            arParams[2] = new FbParameter("@Code", FbDbType.Char, 2);
            arParams[2].Value = code;

            arParams[3] = new FbParameter("@Sort", FbDbType.Integer);
            arParams[3].Value = sort;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > -1);
        }

        /// <summary>
        /// Deletes a row from the mp_Language table. Returns true if row deleted.
        /// </summary>
        /// <param name="guid"> guid </param>
        /// <returns>bool</returns>
        public async Task<bool> Delete(Guid guid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_Language ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("Guid = @Guid ");
            sqlCommand.Append("OR Guid = UPPER(@Guid) ");
            sqlCommand.Append(";");
            FbParameter[] arParams = new FbParameter[1];

            arParams[0] = new FbParameter("@Guid", FbDbType.Char, 36);
            arParams[0].Value = guid.ToString();

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > -1);
        }

        /// <summary>
        /// Gets an IDataReader with one row from the mp_Language table.
        /// </summary>
        /// <param name="guid"> guid </param>
        public async Task<DbDataReader> GetOne(Guid guid)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  * ");
            sqlCommand.Append("FROM	mp_Language ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("Guid = @Guid ");
            sqlCommand.Append("OR Guid = UPPER(@Guid) ");
            sqlCommand.Append(";");

            FbParameter[] arParams = new FbParameter[1];

            arParams[0] = new FbParameter("@Guid", FbDbType.Char, 36);
            arParams[0].Value = guid.ToString();

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                sqlCommand.ToString(),
                arParams);

        }

        /// <summary>
        /// Gets a count of rows in the mp_Language table.
        /// </summary>
        public async Task<int> GetCount()
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  Count(*) ");
            sqlCommand.Append("FROM	mp_Language ");
            sqlCommand.Append(";");

            object result = await AdoHelper.ExecuteScalarAsync(
                readConnectionString,
                sqlCommand.ToString(),
                null);

            return Convert.ToInt32(result);

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the mp_Language table.
        /// </summary>
        public async Task<DbDataReader> GetAll()
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  * ");
            sqlCommand.Append("FROM	mp_Language ");
            sqlCommand.Append("ORDER BY \"Sort\"  ");
            sqlCommand.Append(";");

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                sqlCommand.ToString(),
                null);

        }


        /// <summary>
        /// Gets a page of data from the mp_Language table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public async Task<DbDataReader> GetPage(
            int pageNumber,
            int pageSize)
        {
            int pageLowerBound = (pageSize * pageNumber) - pageSize;
            //totalPages = 1;
            //int totalRows = GetCount();

            //if (pageSize > 0) totalPages = totalRows / pageSize;

            //if (totalRows <= pageSize)
            //{
            //    totalPages = 1;
            //}
            //else
            //{
            //    int remainder;
            //    Math.DivRem(totalRows, pageSize, out remainder);
            //    if (remainder > 0)
            //    {
            //        totalPages += 1;
            //    }
            //}

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT FIRST " + pageSize.ToString(CultureInfo.InvariantCulture) + " ");
            if (pageNumber > 1)
            {
                sqlCommand.Append("	SKIP " + pageLowerBound.ToString(CultureInfo.InvariantCulture) + " ");
            }
            sqlCommand.Append("	* ");
            sqlCommand.Append("FROM	mp_Language  ");
            //sqlCommand.Append("WHERE   ");
            sqlCommand.Append("ORDER BY \"Sort\"  ");
            sqlCommand.Append("	; ");

            //FbParameter[] arParams = new FbParameter[1];

            //arParams[0] = new FbParameter("@CountryGuid", FbDbType.Char, 36);
            //arParams[0].Direction = ParameterDirection.Input;
            //arParams[0].Value = countryGuid.ToString();

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                sqlCommand.ToString(),
                null);

        }
    }
}
