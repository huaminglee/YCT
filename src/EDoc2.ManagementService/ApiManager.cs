using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using EDoc2;

namespace EDoc2.ManagementService
{
	public class ApiManager
	{
		private static Hashtable _apis = new Hashtable();
        private string server = "localhost";

        public ApiManager()
        {
            EDoc2Api api = new EDoc2Api();
            api.Server = server;
            api.InstanceName = "default";
            bool connected = api.Connect();
            if (connected)
            {
                Api = api;
            }
            else
            {
                LogManagement.Log.Error("api创建失败");
            }
            int result = api.OrgnizationManagement.Impersonate(2, server, out _currentUserToken);
            if (result != 0)
            {
                LogManagement.Log.Error("创建admin Token 失败, 错误编号：" + result);
            }
        }

        private EDoc2Api api;
		public EDoc2Api Api
		{
            set
            {
                api = value;
            }
			get
            {
                return api;
			}
		}

        private string _currentUserToken;
		public string CurrentUserToken
		{
			get
			{
                return _currentUserToken;
			}
		}
	}
}
