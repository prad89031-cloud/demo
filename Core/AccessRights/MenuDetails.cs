using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AccessRights
{
    public class MenuDetails
    {
    }
    public class MenuScreen
    {
        public int MenuOrder { get; set; }

        public int ModuleId { get; set; }
        public int ScreenId { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
        public string ScreenName { get; set; }
        public List<MenuModule> Module { get; set; } = new();
    }

    public class MenuModule
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Icon { get; set; }
        public int? ParentModuleId { get; set; }
        public int MenuOrder { get; set; }
        public List<MenuScreen> Screen { get; set; } = new();
    }

    public class MenuResponse
    {
        public string HomePage { get; set; } 
        public List<MenuModule> Menus { get; set; } = new();
    }

    public class RoleDropdown
    {
        public string Id { get; set; }    // to store the role Id (GUID)
        public string Name { get; set; }  // to store the role Name
    }

    public class DepartmentDropdown
    {
        public int DepartmentId { get; set; }             
        public string DepartmentCode { get; set; }  
    }


    public class ModuleScreens
    {
        public string ModuleName { get; set; } = string.Empty;
        public string Screens { get; set; } = string.Empty; 
    }

    public class AccessRightsSaveRequest
    {
        public AccessRightsHeader Header { get; set; }

        public List<AccessRightsDetail> Details { get; set; }
    }


    public class  AccessRightsHeader

    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public int Hod { get; set; }
        public int BranchId { get; set; }
        public int OrgId { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIP { get; set; } = string.Empty;


        public int? ModifiedBy { get; set; } 
        public string ModifiedIP { get; set; }

        public DateTime EffectiveFrom { get; set; }


    }


    public class AccessRightsDetail
    {
        public int HeaderId { get; set; }
        public string Module { get; set; }
        public string Screen { get; set; }
        public bool View { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Post { get; set; }
        public bool Save { get; set; }
        public bool Print { get; set; }
        public bool ViewRate { get; set; }
        public bool SendMail { get; set; }
        public bool ViewDetails { get; set; }
        public int Records { get; set; }
        public bool IsActive { get; set; } = true;
    }


}
