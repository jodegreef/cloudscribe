﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:					2014-06-22
// Last Modified:			2015-10-16
// 

using Microsoft.Framework.OptionsModel;
using Microsoft.Dnx.Runtime;
using System;
using System.IO;

namespace cloudscribe.DbHelpers.Sqlite
{
    public class SqliteConnectionstringResolver
    {
        public SqliteConnectionstringResolver(
            IApplicationEnvironment appEnv,
            IOptions<SqliteConnectionOptions> configuration)
        {
            if (configuration == null) { throw new ArgumentNullException(nameof(configuration)); }
            if (appEnv == null) { throw new ArgumentNullException(nameof(appEnv)); }

            appBasePath = appEnv.ApplicationBasePath;
            options = configuration.Value;

        }

        private SqliteConnectionOptions options;
        private string appBasePath;

        public string Resolve()
        {
            if(options.ConnectionString.Length > 0) { return options.ConnectionString; }

            string pathToDbFile = appBasePath + "/config/sqlitedb/".Replace("/", Path.DirectorySeparatorChar.ToString()) + options.DbFileName;
            string connectionString = "data source=" + pathToDbFile + ";version=3;";
            return connectionString;
        }
    }
}
