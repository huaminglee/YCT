using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using edoc2 = EDoc2;

namespace EDoc2.YCT.Core
{
    public class MailSender
    {
        public static void Send(string recipientsAddress, string subject, string emailAddressCc, string emailAddressBcc, bool isBodyHtml, string mailContent)
        {
            int result = ApiManager.Api.MailManagement.SendSysMailMessage(recipientsAddress, subject, emailAddressCc, emailAddressBcc, isBodyHtml, mailContent);
            if (result != 0)
            {
                throw new Exception("邮件发送失败, 错误编号：" + result);
            }
        }
    }
}
