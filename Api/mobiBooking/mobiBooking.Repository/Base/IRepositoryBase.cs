using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mobiBooking.Repository.Base
{
    public interface IRepositoryBase
    {
        Task Save();
    }
}
