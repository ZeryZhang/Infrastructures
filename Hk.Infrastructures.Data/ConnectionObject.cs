using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using Hk.Infrastructures.Data.Configs;
using MySql.Data.MySqlClient;

namespace Hk.Infrastructures.Data
{
    public class ConnectionObject
    {
        public IDbConnection CreateDbConnection(string groupName, DbAccessType dbAccessType)
        {
            ConnectionStringItem connectionStringItem = null;
            if (dbAccessType == DbAccessType.Read)
            {
                connectionStringItem = ConnectionManager.GetReadConnectionStringItem(groupName);
            }
            else
            {
                connectionStringItem = ConnectionManager.GetWriteConnectionStringItem(groupName);
            }

            IDbConnection connection = CreateDbConnection(connectionStringItem.ConnectionString, connectionStringItem.SqlDbType);
            return connection;
        }

        public IDbConnection CreateDbConnection(string connectionString, SqlDbType sqlDbType)
        {
            IDbConnection connection = null;
            switch (sqlDbType)
            {
                case SqlDbType.SqlServer:
                    connection = new SqlConnection(connectionString);
                    break;
                case SqlDbType.MySql:
                    connection = new MySqlConnection(connectionString);
                    break;
                default:
                    connection = new SqlConnection(connectionString);
                    break;
            }
            connection.Open();
            return connection;
        }
    }
    internal static class ConnectionManager
    {
        private static ConcurrentDictionary<string, ConnectionStringGroup> _connectionContainer =
            new ConcurrentDictionary<string, ConnectionStringGroup>();

        static ConnectionManager()
        {
            LoadConfigs();
            ConfigFileManager.ConfigChanged += ConfigFileManager_ConfigChanged;
        }

        static void ConfigFileManager_ConfigChanged(object sender, Hk.Infrastructures.Config.ConfigChangedEventArgs e)
        {
            LoadConfigs();
        }
        public static ConnectionStringItem GetReadConnectionStringItem(string groupName)
        {
            ConnectionStringItem result = null;
            if (!string.IsNullOrWhiteSpace(groupName) && _connectionContainer != null && _connectionContainer.Count > 0)
            {
                if (_connectionContainer.ContainsKey(groupName))
                {
                    var group = _connectionContainer[groupName];
                    result = group.ReadConnectionStringItem;
                }
            }
            return result;
        }

        public static ConnectionStringItem GetWriteConnectionStringItem(string groupName)
        {
            ConnectionStringItem result = null;
            if (!string.IsNullOrWhiteSpace(groupName) && _connectionContainer != null && _connectionContainer.Count > 0)
            {
                if (_connectionContainer.ContainsKey(groupName))
                {
                    var group = _connectionContainer[groupName];
                    result = group.WriteConnectionStringItem;
                }
            }
            return result;
        }

        public static ConnectionStringGroup GetConnectionStringGroup(string groupName)
        {
            ConnectionStringGroup result = null;
            if (!string.IsNullOrWhiteSpace(groupName) && _connectionContainer != null && _connectionContainer.Count > 0)
            {
                if (_connectionContainer.ContainsKey(groupName))
                {
                    result = _connectionContainer[groupName];
                }
            }
            return result;
        }
        private static void LoadConfigs()
        {
            var config = Configs.Config.GetConfig();
            if (config != null && config.ConnectionStringGroups != null && config.ConnectionStringGroups.Count > 0)
            {
                foreach (var group in config.ConnectionStringGroups)
                {
                    if (!_connectionContainer.ContainsKey(group.Name))
                    {
                        _connectionContainer.TryAdd(group.Name, group);
                    }
                    else
                    {
                        _connectionContainer[group.Name] = group;
                    }
                }
            }
        }
    }
}
