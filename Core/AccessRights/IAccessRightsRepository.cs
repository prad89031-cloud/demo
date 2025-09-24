using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AccessRights
{
    public interface IAccessRightsRepository
    {
        Task<object> GetMenusDetails(int userid, int branchId, int orgid);
        Task<object> GetApprovalSettings(int userid, int branchId, Int32 orgid, Int32 screenid);
    }
}
