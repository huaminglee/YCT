using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDoc2.YCT.Website.Models
{
    public class Select2Model<T>
    {
        public string text;
        public List<T> children;
    }
}