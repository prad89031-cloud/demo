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

}
