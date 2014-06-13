using System;
using System.Collections.Generic;
using System.Text;
using EDoc2.EDoc2InstanceSchema;
using EDoc2.Common.DataAccess;
using EDoc2.Data;
using System.Data.SqlClient;

namespace EDoc2.ManagementService
{
    public abstract class ServiceDataRepairer
    {
        protected EDoc2.Data.DataProviders.DataProviderManager _edoc2DataProviderManager;
        EDoc2InstanceDatabaseConfiguration _dataBaseConfig;
        DataProviderFactoryBuilder _dataProviderFactoryBuilder;
        public ServiceDataRepairer()
        {
            Instances instances = Instances.LoadFromXmlFile(AppDomain.CurrentDomain.BaseDirectory + "\\instances.config");
            if ((instances != null) && (instances.Instance != null))
            {
                Instance instance = instances.Instance[0];
                _dataBaseConfig = new EDoc2InstanceDatabaseConfiguration(instance.InstanceId, instance.InstanceName, instance.DatabaseType, instance.DatabaseServerName, instance.DatabaseName, instance.DatabaseUserName, instance.DatabasePassword);

                _dataProviderFactoryBuilder = new DataProviderFactoryBuilder();
                _dataProviderFactoryBuilder.DatabaseServerName = _dataBaseConfig.databaseServerName;
                _dataProviderFactoryBuilder.DatabaseName = _dataBaseConfig.databaseName;
                _dataProviderFactoryBuilder.DatabaseUserName = _dataBaseConfig.databaseUserName;
                _dataProviderFactoryBuilder.DatabasePassword = _dataBaseConfig.databasePassword;
                _dataProviderFactoryBuilder.FactoryProviderTypeName = DataProviderFactoryNameLookup.GetFactoryNameByDatabaseType(EDoc2DbType.SqlServer);
                _edoc2DataProviderManager = new EDoc2.Data.DataProviders.DataProviderManager(_dataProviderFactoryBuilder);

            }
        }

        protected void ExcuteSql(string sql)
        {
            using (SqlConnection conn = new SqlConnection(this._dataProviderFactoryBuilder.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public abstract void Repair();
    }
}
