using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.Core
{
    public class OALoginSettingInfo
    {
        public virtual int Id { set; get; }

        public virtual int UserId { set; get; }

        public virtual string OAUserName { set; get; }

        public virtual string OAUserPassword { set; get; }
    }
}
