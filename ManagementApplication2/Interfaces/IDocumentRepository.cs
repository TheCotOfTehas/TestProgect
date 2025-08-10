using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Interfaces
{
    internal interface IDocumentRepository
    {
        public IDocument Create { get; set; }
        public IDocument Delete { get; set; }
        public IDocument Edit { get; set; }

    }
}
