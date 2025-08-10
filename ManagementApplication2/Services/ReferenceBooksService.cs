using ManagementApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication.Services
{
    internal class ReferenceBooksService : IReferenceBooksService
    {
        public IBaseEntity Create { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IBaseEntity Delete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IBaseEntity SendToArchive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<IBaseEntity> GetAllByName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<IBaseEntity> GetAllByStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
