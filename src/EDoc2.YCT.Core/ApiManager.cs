using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using EDoc2;

namespace EDoc2.YCT.Core
{
    internal class ApiManager
    {
        public static string ServerIp = "127.0.0.1";

        private static Hashtable _apis = new Hashtable();

        protected static EDoc2Api GetApi(string instanceName)
        {
            EDoc2Api api = (EDoc2Api)_apis[instanceName];
            if (api == null)
            {
                api = new EDoc2Api();
                api.Server = ServerIp;
                api.Port = 6260;
                api.InstanceName = instanceName;
                api.Connect();
                
            }
            return api;
        }

        public static EDoc2Api Api
        {
            get
            {
                return GetApi("default");
            }
        }

        private static string _currentUserToken;
        public static string CurrentUserToken
        {
            get
            {
                return _currentUserToken;
            }
            set
            {
                _currentUserToken = value;
            }
        }
    }


}
