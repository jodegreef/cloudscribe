﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:				    2007-11-03
// Last Modified:			2015-06-13
// 

using cloudscribe.DbHelpers.pgsql;
using Microsoft.Framework.Logging;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;


namespace cloudscribe.Core.Repositories.pgsql
{

    internal class DBRoles
    {
        internal DBRoles(
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



        public async Task<int> RoleCreate(
            Guid roleGuid,
            Guid siteGuid,
            int siteId,
            string roleName)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[4];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("rolename", NpgsqlTypes.NpgsqlDbType.Text, 50);
            arParams[1].Value = roleName;

            arParams[2] = new NpgsqlParameter("siteguid", NpgsqlTypes.NpgsqlDbType.Char, 36);
            arParams[2].Value = siteGuid.ToString();

            arParams[3] = new NpgsqlParameter("roleguid", NpgsqlTypes.NpgsqlDbType.Char, 36);
            arParams[3].Value = roleGuid.ToString();

            object result = await AdoHelper.ExecuteScalarAsync(
                writeConnectionString,
                    CommandType.StoredProcedure,
                    "mp_roles_insert(:siteid,:rolename,:siteguid,:roleguid)",
                    arParams);

            int newID = Convert.ToInt32(result);

            return newID;

        }

        public async Task<bool> Update(int roleId, string roleName)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[2];

            arParams[0] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = roleId;

            arParams[1] = new NpgsqlParameter("rolename", NpgsqlTypes.NpgsqlDbType.Text, 50);
            arParams[1].Value = roleName;

            object result = await AdoHelper.ExecuteScalarAsync(
                writeConnectionString,
                CommandType.StoredProcedure,
                "mp_roles_update(:roleid,:rolename)",
                arParams);

            int rowsAffected = Convert.ToInt32(result);

            return (rowsAffected > -1);
        }

        public async Task<bool> Delete(int roleId)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[1];

            arParams[0] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = roleId;

            object result = await AdoHelper.ExecuteScalarAsync(
                writeConnectionString,
                CommandType.StoredProcedure,
                "mp_roles_delete(:roleid)",
                arParams);

            int rowsAffected = Convert.ToInt32(result);

            return (rowsAffected > -1);

        }

        public async Task<bool> DeleteUserRoles(int userId)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[1];

            arParams[0] = new NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = userId;

            object result = await AdoHelper.ExecuteScalarAsync(
                writeConnectionString,
                CommandType.StoredProcedure,
                "mp_userroles_deleteuserroles(:userid)",
                arParams);

            int rowsAffected = Convert.ToInt32(result);

            return (rowsAffected > -1);
        }

        public async Task<bool> DeleteUserRolesByRole(int roleId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("DELETE FROM mp_userroles ");
            sqlCommand.Append("WHERE ");
            sqlCommand.Append("roleid = :roleid ");
            sqlCommand.Append(";");

            NpgsqlParameter[] arParams = new NpgsqlParameter[1];

            arParams[0] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = roleId;

            int rowsAffected = await AdoHelper.ExecuteNonQueryAsync(
                writeConnectionString,
                CommandType.Text,
                sqlCommand.ToString(),
                arParams);

            return (rowsAffected > -1);
        }

        public async Task<DbDataReader> GetById(int roleId)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[1];

            arParams[0] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = roleId;

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                CommandType.StoredProcedure,
                "mp_roles_selectone(:roleid)",
                arParams);
        }

        public async Task<DbDataReader> GetByName(int siteId, string roleName)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[2];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("rolename", NpgsqlTypes.NpgsqlDbType.Text, 50);
            arParams[1].Value = roleName;

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                CommandType.StoredProcedure,
                "mp_roles_selectbyname(:siteid,:rolename)",
                arParams);
        }

        public async Task<bool> Exists(int siteId, string roleName)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[2];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("rolename", NpgsqlTypes.NpgsqlDbType.Text, 50);
            arParams[1].Value = roleName;

            object result = await AdoHelper.ExecuteScalarAsync(
                readConnectionString,
                CommandType.StoredProcedure,
                "mp_roles_roleexists(:siteid,:rolename)",
                arParams);

            int count = Convert.ToInt32(result);

            return (count > 0);

        }

        public DbDataReader GetSiteRoles(int siteId)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[1];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  ");
            sqlCommand.Append("r.roleid, ");
            sqlCommand.Append("r.siteid, ");
            sqlCommand.Append("r.rolename, ");
            sqlCommand.Append("r.displayname, ");
            sqlCommand.Append("r.siteguid, ");
            sqlCommand.Append("r.roleguid, ");
            sqlCommand.Append("COUNT(ur.userid) As membercount ");

            sqlCommand.Append("FROM	mp_roles r ");

            sqlCommand.Append("LEFT OUTER JOIN mp_userroles ur ");
            sqlCommand.Append("ON ur.roleid = r.roleid ");

            sqlCommand.Append("WHERE ");

            sqlCommand.Append("r.siteid = :siteid ");

            sqlCommand.Append("GROUP BY ");
            sqlCommand.Append("r.roleid, ");
            sqlCommand.Append("r.siteid, ");
            sqlCommand.Append("r.rolename, ");
            sqlCommand.Append("r.displayname, ");
            sqlCommand.Append("r.siteguid, ");
            sqlCommand.Append("r.roleguid ");

            sqlCommand.Append("ORDER BY r.displayname ");
            sqlCommand.Append(";");

            return AdoHelper.ExecuteReader(
                readConnectionString,
                CommandType.Text,
                sqlCommand.ToString(),
                arParams);
        }

        public DbDataReader GetRoleMembers(int roleId)
        {

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT ");
            sqlCommand.Append("mp_userroles.userid, ");
            sqlCommand.Append("mp_users.name, ");
            sqlCommand.Append("mp_users.loginname, ");
            sqlCommand.Append("mp_users.email ");

            sqlCommand.Append("FROM	mp_userroles ");
            sqlCommand.Append("INNER JOIN mp_users ");
            sqlCommand.Append("ON mp_users.userid = mp_userroles.userid ");

            sqlCommand.Append("WHERE mp_userroles.roleid = :roleid ; ");

            NpgsqlParameter[] arParams = new NpgsqlParameter[1];

            arParams[0] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = roleId;

            return AdoHelper.ExecuteReader(
               readConnectionString,
               CommandType.Text,
               sqlCommand.ToString(),
               arParams);

        }

        public async Task<int> GetCountOfUsersNotInRole(int siteId, int roleId, string searchInput)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT ");
            sqlCommand.Append("COUNT(*) ");

            sqlCommand.Append("FROM	mp_users u ");
            sqlCommand.Append("LEFT OUTER JOIN mp_userroles ur ");
            sqlCommand.Append("ON u.userid = ur.userid ");
            sqlCommand.Append("AND ur.roleid = :roleid ");

            sqlCommand.Append("WHERE u.siteid = :siteid  ");
            sqlCommand.Append("AND ur.roleid IS NULL  ");

            if (searchInput.Length > 0)
            {
                sqlCommand.Append(" AND ");
                sqlCommand.Append("(");
                sqlCommand.Append(" (u.name LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.loginname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.email LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.lastname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.firstname LIKE :searchinput) ");
                sqlCommand.Append(")");
            }
            sqlCommand.Append(";");

            NpgsqlParameter[] arParams = new NpgsqlParameter[3];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[1].Value = roleId;

            arParams[2] = new NpgsqlParameter("searchinput", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            arParams[2].Value = "%" + searchInput + "%";

            object result = await AdoHelper.ExecuteScalarAsync(
               readConnectionString,
               CommandType.Text,
               sqlCommand.ToString(),
               arParams);

            return Convert.ToInt32(result);
        }

        public async Task<DbDataReader> GetUsersNotInRole(
            int siteId,
            int roleId,
            string searchInput,
            int pageNumber,
            int pageSize)
        {
            int pageLowerBound = (pageSize * pageNumber) - pageSize;

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT ");
            sqlCommand.Append("u.* ");


            sqlCommand.Append("FROM	mp_users u ");
            sqlCommand.Append("LEFT OUTER JOIN mp_userroles ur ");
            sqlCommand.Append("ON u.userid = ur.userid ");
            sqlCommand.Append("AND ur.roleid = :roleid ");

            sqlCommand.Append("WHERE u.siteid = :siteid  ");
            sqlCommand.Append("AND ur.roleid IS NULL  ");
            if (searchInput.Length > 0)
            {
                sqlCommand.Append(" AND ");
                sqlCommand.Append("(");
                sqlCommand.Append(" (u.name LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.loginname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.email LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.lastname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.firstname LIKE :searchinput) ");
                sqlCommand.Append(")");
            }

            sqlCommand.Append("ORDER BY u.name  ");
            sqlCommand.Append("LIMIT  :pagesize");

            if (pageNumber > 1)
                sqlCommand.Append(" OFFSET :pageoffset ");

            sqlCommand.Append(";");

            NpgsqlParameter[] arParams = new NpgsqlParameter[5];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[1].Value = roleId;

            arParams[2] = new NpgsqlParameter("searchinput", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            arParams[2].Value = "%" + searchInput + "%";

            arParams[3] = new NpgsqlParameter("pagesize", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[3].Value = pageSize;

            arParams[4] = new NpgsqlParameter("pageoffset", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[4].Value = pageLowerBound;

            return await AdoHelper.ExecuteReaderAsync(
               readConnectionString,
               CommandType.Text,
               sqlCommand.ToString(),
               arParams);


        }

        public async Task<int> GetCountOfUsersInRole(int siteId, int roleId, string searchInput)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT ");
            sqlCommand.Append("COUNT(*) ");

            sqlCommand.Append("FROM	mp_users u ");

            sqlCommand.Append("JOIN mp_userroles ur ");
            sqlCommand.Append("ON u.userid = ur.userid ");
            sqlCommand.Append("AND ur.roleid = :roleid ");

            sqlCommand.Append("WHERE u.siteid = :siteid  ");
            if (searchInput.Length > 0)
            {
                sqlCommand.Append(" AND ");
                sqlCommand.Append("(");
                sqlCommand.Append(" (u.name LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.loginname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.email LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.lastname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.firstname LIKE :searchinput) ");
                sqlCommand.Append(")");
            }

            sqlCommand.Append(";");

            NpgsqlParameter[] arParams = new NpgsqlParameter[3];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[1].Value = roleId;

            arParams[2] = new NpgsqlParameter("searchinput", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            arParams[2].Value = "%" + searchInput + "%";

            object result = await AdoHelper.ExecuteScalarAsync(
               readConnectionString,
               CommandType.Text,
               sqlCommand.ToString(),
               arParams);

            return Convert.ToInt32(result);
        }

        public async Task<DbDataReader> GetUsersInRole(
            int siteId,
            int roleId,
            string searchInput,
            int pageNumber,
            int pageSize)
        {
            int pageLowerBound = (pageSize * pageNumber) - pageSize;

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT ");
            sqlCommand.Append("u.* ");


            sqlCommand.Append("FROM	mp_users u ");

            sqlCommand.Append("JOIN mp_userroles ur ");

            sqlCommand.Append("ON u.userid = ur.userid ");
            sqlCommand.Append("AND ur.roleid = :roleid ");

            sqlCommand.Append("WHERE u.siteid = :siteid  ");

            if (searchInput.Length > 0)
            {
                sqlCommand.Append(" AND ");
                sqlCommand.Append("(");
                sqlCommand.Append(" (u.name LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.loginname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.email LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.lastname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (u.firstname LIKE :searchinput) ");
                sqlCommand.Append(")");
            }

            sqlCommand.Append("ORDER BY u.name  ");
            sqlCommand.Append("LIMIT  :pagesize");

            if (pageNumber > 1)
                sqlCommand.Append(" OFFSET :pageoffset ");

            sqlCommand.Append(";");

            NpgsqlParameter[] arParams = new NpgsqlParameter[5];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[1].Value = roleId;

            arParams[2] = new NpgsqlParameter("searchinput", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            arParams[2].Value = "%" + searchInput + "%";

            arParams[3] = new NpgsqlParameter("pagesize", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[3].Value = pageSize;

            arParams[4] = new NpgsqlParameter("pageoffset", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[4].Value = pageLowerBound;

            return await AdoHelper.ExecuteReaderAsync(
               readConnectionString,
               CommandType.Text,
               sqlCommand.ToString(),
               arParams);
        }

        public DbDataReader GetRolesUserIsNotIn(
            int siteId,
            int userId)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[2];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[1].Value = userId;

            return AdoHelper.ExecuteReader(
                readConnectionString,
                CommandType.StoredProcedure,
                "mp_roles_selectrolesuserisnotin(:siteid,:userid)",
                arParams);
        }

        public async Task<bool> AddUser(
            int roleId,
            int userId,
            Guid roleGuid,
            Guid userGuid
            )
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[4];

            arParams[0] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = roleId;

            arParams[1] = new NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[1].Value = userId;

            arParams[2] = new NpgsqlParameter("roleguid", NpgsqlTypes.NpgsqlDbType.Char, 36);
            arParams[2].Value = roleGuid.ToString();

            arParams[3] = new NpgsqlParameter("userguid", NpgsqlTypes.NpgsqlDbType.Char, 36);
            arParams[3].Value = userGuid.ToString();

            object result = await AdoHelper.ExecuteScalarAsync(
                writeConnectionString,
                CommandType.StoredProcedure,
                "mp_userroles_insert(:roleid,:userid,:roleguid,:userguid)",
                arParams);

            int rowsAffected = Convert.ToInt32(result);

            return (rowsAffected > -1);

        }

        public async Task<bool> RemoveUser(int roleId, int userId)
        {
            NpgsqlParameter[] arParams = new NpgsqlParameter[2];

            arParams[0] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = roleId;

            arParams[1] = new NpgsqlParameter("userid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[1].Value = userId;

            object result = await AdoHelper.ExecuteScalarAsync(
                writeConnectionString,
                CommandType.StoredProcedure,
                "mp_userroles_delete(:roleid,:userid)",
                arParams);

            int rowsAffected = Convert.ToInt32(result);

            return (rowsAffected > -1);

        }

        public int GetCountOfSiteRoles(int siteId)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT ");
            sqlCommand.Append("COUNT(*) ");
            sqlCommand.Append("FROM	mp_roles ");
            sqlCommand.Append("WHERE siteid = :siteid  ");
            sqlCommand.Append(";");

            NpgsqlParameter[] arParams = new NpgsqlParameter[1];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            return Convert.ToInt32(AdoHelper.ExecuteScalar(
               readConnectionString,
               CommandType.Text,
               sqlCommand.ToString(),
               arParams));
        }

        public async Task<int> GetCountOfSiteRoles(int siteId, string searchInput)
        {
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT ");
            sqlCommand.Append("COUNT(*) ");
            sqlCommand.Append("FROM	mp_roles ");
            sqlCommand.Append("WHERE siteid = :siteid  ");
            if (searchInput.Length > 0)
            {
                sqlCommand.Append(" AND ");
                sqlCommand.Append("(");
                sqlCommand.Append(" (displayname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (rolename LIKE :searchinput) ");
                sqlCommand.Append(")");
            }

            sqlCommand.Append(";");

            NpgsqlParameter[] arParams = new NpgsqlParameter[2];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("searchinput", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            arParams[1].Value = "%" + searchInput + "%";

            object result = await AdoHelper.ExecuteScalarAsync(
               readConnectionString,
               CommandType.Text,
               sqlCommand.ToString(),
               arParams);

            return Convert.ToInt32(result);
        }

        public async Task<DbDataReader> GetPage(
            int siteId,
            string searchInput,
            int pageNumber,
            int pageSize)
        {
            int pageLowerBound = (pageSize * pageNumber) - pageSize;

            NpgsqlParameter[] arParams = new NpgsqlParameter[4];

            arParams[0] = new NpgsqlParameter("siteid", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[0].Value = siteId;

            arParams[1] = new NpgsqlParameter("searchinput", NpgsqlTypes.NpgsqlDbType.Varchar, 50);
            arParams[1].Value = "%" + searchInput + "%";

            arParams[2] = new NpgsqlParameter("pagesize", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[2].Value = pageSize;

            arParams[3] = new NpgsqlParameter("pageoffset", NpgsqlTypes.NpgsqlDbType.Integer);
            arParams[3].Value = pageLowerBound;

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.Append("SELECT  ");
            sqlCommand.Append("r.roleid, ");
            sqlCommand.Append("r.siteid, ");
            sqlCommand.Append("r.rolename, ");
            sqlCommand.Append("r.displayname, ");
            sqlCommand.Append("r.siteguid, ");
            sqlCommand.Append("r.roleguid, ");
            sqlCommand.Append("COUNT(ur.userid) As membercount ");

            sqlCommand.Append("FROM	mp_roles r ");

            sqlCommand.Append("LEFT OUTER JOIN mp_userroles ur ");
            sqlCommand.Append("ON ur.roleid = r.roleid ");

            sqlCommand.Append("WHERE ");

            sqlCommand.Append("r.siteid = :siteid ");

            if (searchInput.Length > 0)
            {
                sqlCommand.Append(" AND ");
                sqlCommand.Append("(");
                sqlCommand.Append(" (displayname LIKE :searchinput) ");
                sqlCommand.Append(" OR ");
                sqlCommand.Append(" (rolename LIKE :searchinput) ");
            }

            sqlCommand.Append("GROUP BY ");
            sqlCommand.Append("r.roleid, ");
            sqlCommand.Append("r.siteid, ");
            sqlCommand.Append("r.rolename, ");
            sqlCommand.Append("r.displayname, ");
            sqlCommand.Append("r.siteguid, ");
            sqlCommand.Append("r.roleguid ");

            sqlCommand.Append("ORDER BY r.displayname ");
            sqlCommand.Append("LIMIT  :pagesize");

            if (pageNumber > 1)
                sqlCommand.Append(" OFFSET :pageoffset ");

            sqlCommand.Append(";");

            return await AdoHelper.ExecuteReaderAsync(
                readConnectionString,
                CommandType.Text,
                sqlCommand.ToString(),
                arParams);


        }

    }
}
