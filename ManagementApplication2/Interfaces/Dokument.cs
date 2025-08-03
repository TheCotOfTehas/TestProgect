using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Interfaces
{
    public interface IDocument
    {
        public int Id { get; }
        public int Number { get; set; }
        public DateTime DateTime { get; set; }
    }
}
