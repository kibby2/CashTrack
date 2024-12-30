using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashTrack.DataModel.Model
{
    public class Tag
    {
        public Guid TagID { get; set; }
        public string TagName { get; set; }

        public Tag(string tagName)
        {
            TagID = Guid.NewGuid();
            TagName = tagName;
        }
    }
}
