using BackEnd.AccessRights;
using Core.Abstractions;
using Core.AccessRights;
using Core.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AccessRightsRepository: IAccessRightsRepository
    {

        private readonly IDbConnection _connection;

        public AccessRightsRepository(IUnitOfWorkDB4 financedb)
        {
            _connection = financedb.Connection;
        }

        public async Task<object> GetApprovalSettings(int userid, int branchId, Int32 orgid, Int32 screenid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 2);
                param.Add("@userid", userid);
                param.Add("@branchid", branchId);
                param.Add("@orgid", orgid);
                param.Add("@ScreenId", screenid);
                
                var list = await _connection.QueryAsync(AccessRights.AccessRightsProc, param, commandType: CommandType.StoredProcedure);

                return new ResponseModel
                {
                    Data = list,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Message = "Error retrieving access rights.",
                    Status = false
                };
            }
        }

        public async Task<object> GetMenusDetails(int userid, int branchId, int orgid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 1);
                param.Add("@userid", userid);
                param.Add("@branchid", branchId);
                param.Add("@orgid", orgid);
                param.Add("@ScreenId", 0);

                var result = await _connection.QueryMultipleAsync(
                    AccessRights.AccessRightsProc,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                var allModules = result.Read<MenuModule>().ToList();
                var allScreens = result.Read<MenuScreen>().ToList();
                var HomeScreens = result.Read<string>().FirstOrDefault();

                // Create lookup for fast access
                var moduleDict = allModules.ToDictionary(m => m.ModuleId);

                // Attach screens to modules
                foreach (var screen in allScreens)
                {
                    if (moduleDict.ContainsKey(screen.ModuleId))
                    {
                        moduleDict[screen.ModuleId].Screen.Add(screen);
                    }
                }

                // Sort screens in each module
                foreach (var mod in moduleDict.Values)
                {
                    mod.Screen = mod.Screen.OrderBy(s => s.MenuOrder).ToList();
                }

                // Build child module hierarchy (only attach if child has screens, and parent has screens)
                foreach (var module in allModules)
                {
                    if (module.ParentModuleId.HasValue &&
                        moduleDict.ContainsKey(module.ParentModuleId.Value) &&
                        module.Screen.Any()) // Only attach child if it has screens
                    {
                        var parent = moduleDict[module.ParentModuleId.Value];
                        if (parent.Screen.Any())
                        {
                            parent.Screen[0].Module.Add(module);
                        }
                    }
                }

                // Order child modules inside each screen
                foreach (var mod in moduleDict.Values)
                {
                    foreach (var screen in mod.Screen)
                    {
                        screen.Module = screen.Module.OrderBy(m => m.MenuOrder).ToList();
                    }
                }

                // Get top-level modules that have at least one screen
                var topLevelMenus = allModules
                    .Where(m => m.ParentModuleId == null && m.Screen.Any())
                    .OrderBy(m => m.MenuOrder)
                    .ToList();

                return new ResponseModel
                {
                    Data = new MenuResponse { Menus = topLevelMenus , HomePage = HomeScreens },
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Message = $"Error retrieving menus: {ex.Message}",
                    Status = false
                };
            }
        }



    }
}
