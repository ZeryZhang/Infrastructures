using System;
using System.Data;
using Hk.Infrastructures.Core.Repositories;
using Hk.Infrastructures.Data;
namespace Hk.Infrastructures.Repositories
{
    public abstract class Repository : IRepository
    {
        private string _groupName = string.Empty;

        protected Repository(string groupName)
        {
            _groupName = groupName;
        }

        protected IDbConnection CreateWriteDbConnection()
        {
            return new ConnectionObject().CreateDbConnection(_groupName, DbAccessType.Write);
        }
        protected IDbConnection CreateReadDbConnection()
        {
            return new ConnectionObject().CreateDbConnection(_groupName, DbAccessType.Read);
        }
    }
}
