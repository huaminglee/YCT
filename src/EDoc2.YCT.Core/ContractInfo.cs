using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDoc2.YCT.Core
{
    public class ContractInfo
    {
        public virtual int Id { set; get; }

        //public string Name { set; get; }

        public virtual int EDoc2FileId { set; get; }

        //public int EDoc2FileName { set; get; }

        //public int EDoc2FolderId { set; get; }

        //public string Code { set; get; }

        //public DateTime SignDate { set; get; }

        //public int ValidDays { set; get; }
        public virtual int? ValidMonth { set; get; }

        public virtual DateTime? ExpireDate { set; get; }

        //public string CompanyName { set; get; }

        //public string Summary { set; get; }

        //public decimal Amount { set; get; }

        //public ContractStatus Status { set; get; }
    }
}
