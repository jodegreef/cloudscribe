﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:					2014-08-11
// Last Modified:			2015-09-03
// 

using cloudscribe.DbHelpers.MySql;
using Microsoft.Framework.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace cloudscribe.Core.Repositories.MySql
{

    internal class DBUserClaims
    {
        internal DBUserClaims(
            string dbReadConnectionString,
            string dbWriteConnectionString,
            ILoggerFactory loggerFactory)
        {
            logFactory = loggerFactory;
            readConnectionString = dbReadConnectionString;
            writeConnectionString = dbWriteConnectionString;
        }

        private ILoggerFactory logFactory;
        private string readConnectionString;
        private string writeConnectionString;

        public async Task<int> Create(
            int siteId,
            string userId,
            string claimType,
            string claimValue)
        {

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("INSERT INTO mp_UserClaims (");
            sqlCommand.Append("SiteId, ");
            sqlCommand.Append("UserId, ");
            sqlCommand.Append("ClaimType, ");
            sqlCommand.Append("ClaimValue )");

            sqlCommand.Append(" VALUES (");
            sqlCommand.Append("?SiteId, ");
            sqlCommand.Append("?UserId, ");
            sqlCommand.Append("?ClaimType, ");
            sqlCommand.Append("?ClaimValue )");
            sqlCommand.Append(";");

            sqlCommand.Append("SELECT LAST_INSERT_ID();");

            MySqlParameter[] arParams = new MySqlParameter[4];

            arParams[0] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[0].Value = userId;

            arParams[1] = new MySqlParameter("?ClaimType", MySqlDbType.Text);
            arParams[1].Value = claimType;

            arParams[2] = new MySqlParameter("?ClaimValue", MySqlDbType.Text);
            arParams[2].Value = claimValue;

            arParams[3] = new MySqlParameter("?SiteId", MySqlDbType.Int32);
            arParams[3].Value = siteId;

            object result = await AdoHelper.ExecuteScalarAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            int newID = Convert.ToInt32(result);

            return newID;

        }


        public async Task<bool> Delete(int id)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserClaims ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("Id = ?Id ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[1];

            arParams[0] = new MySqlParameter("?Id", MySqlDbType.Int32);
            arParams[0].Value = id;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        public async Task<bool> DeleteByUser(int siteId, string userId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserClaims ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("((?SiteId = -1) OR (SiteId = ?SiteId)) ");
            sqlCommand.Append(" AND ");
            sqlCommand.Append("UserId = ?UserId ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[2];

            arParams[0] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[0].Value = userId;

            arParams[1] = new MySqlParameter("?SiteId", MySqlDbType.Int32);
            arParams[1].Value = siteId;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        public async Task<bool> DeleteByUser(int siteId, string userId, string claimType)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserClaims ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("((?SiteId = -1) OR (SiteId = ?SiteId)) ");
            sqlCommand.Append(" AND ");
            sqlCommand.Append("UserId = ?UserId ");
            sqlCommand.Append("AND ");
            sqlCommand.Append("ClaimType = ?ClaimType ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[3];

            arParams[0] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[0].Value = userId;

            arParams[1] = new MySqlParameter("?ClaimType", MySqlDbType.Text);
            arParams[1].Value = claimType;

            arParams[2] = new MySqlParameter("?SiteId", MySqlDbType.Int32);
            arParams[2].Value = siteId;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);


        }

        public async Task<bool> DeleteByUser(int siteId, string userId, string claimType, string claimValue)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserClaims ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("((?SiteId = -1) OR (SiteId = ?SiteId)) ");
            sqlCommand.Append(" AND ");
            sqlCommand.Append("UserId = ?UserId ");
            sqlCommand.Append("AND ");
            sqlCommand.Append("ClaimType = ?ClaimType ");
            sqlCommand.Append("AND ");
            sqlCommand.Append("ClaimValue = ?ClaimValue ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[4];

            arParams[0] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[0].Value = userId;

            arParams[1] = new MySqlParameter("?ClaimType", MySqlDbType.Text);
            arParams[1].Value = claimType;

            arParams[2] = new MySqlParameter("?ClaimValue", MySqlDbType.Text);
            arParams[2].Value = claimValue;

            arParams[3] = new MySqlParameter("?SiteId", MySqlDbType.Int32);
            arParams[3].Value = siteId;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        public async Task<bool> DeleteBySite(int siteId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_UserClaims ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("SiteId  = ?SiteId ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[1];

            arParams[0] = new MySqlParameter("?SiteId", MySqlDbType.Int32);
            arParams[0].Value = siteId;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > 0);

        }

        public async Task<DbDataReader> GetByUser(int siteId, string userId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  * ");
            sqlCommand.Append("FROM	mp_UserClaims ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("SiteId  = ?SiteId ");
            sqlCommand.Append(" AND ");
            sqlCommand.Append("UserId = ?UserId ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[2];

            arParams[0] = new MySqlParameter("?UserId", MySqlDbType.VarChar, 128);
            arParams[0].Value = userId;

            arParams[1] = new MySqlParameter("?SiteId", MySqlDbType.Int32);
            arParams[1].Value = siteId;

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                sqlCommand.ToString(),
                arParams);

        }

        public async Task<DbDataReader> GetUsersByClaim(int siteId, string claimType, string claimValue)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  u.* ");

            sqlCommand.Append("FROM	mp_Users u ");

            sqlCommand.Append("JOIN mp_UserClaims uc ");
            sqlCommand.Append("ON u.UserID = uc.UserId ");

            sqlCommand.Append("WHERE ");
            sqlCommand.Append("u.SiteID = ?SiteId ");
            sqlCommand.Append("AND ");
            sqlCommand.Append("uc.ClaimType = ?ClaimType ");
            sqlCommand.Append("AND ");
            sqlCommand.Append("uc.ClaimValue = ?ClaimValue ");

            sqlCommand.Append("ORDER BY ");
            sqlCommand.Append("u.Name ");
            sqlCommand.Append(";");

            MySqlParameter[] arParams = new MySqlParameter[3];

            arParams[0] = new MySqlParameter("?SiteId", MySqlDbType.Int32);
            arParams[0].Value = siteId;

            arParams[1] = new MySqlParameter("?ClaimType", MySqlDbType.Text);
            arParams[1].Value = claimType;

            arParams[2] = new MySqlParameter("?ClaimValue", MySqlDbType.Text);
            arParams[2].Value = claimValue;


            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                sqlCommand.ToString(),
                arParams);


        }


     }
}
