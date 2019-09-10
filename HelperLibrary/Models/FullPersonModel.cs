using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Models
{
    public class FullPersonModel : tbl_test1 //datenstruktur der db in cs code erfassen als class
    {
        public int Id { get; set; }
        public tbl_test2 CellPhone { get; set; }
    }
}
