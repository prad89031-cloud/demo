using Application.AccessRights.SaveAccessRights;
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
    public class AccessRightsRepository : IAccessRightsRepository
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
                param.Add("@HeaderId", 0);

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
                param.Add("@HeaderId", 0);

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
                    Data = new MenuResponse { Menus = topLevelMenus, HomePage = HomeScreens },
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

        public async Task<object> GetRolesAsync(int branchId, int orgId)
        {
            var param = new DynamicParameters();
            param.Add("@opt", 3);
            param.Add("@userid", 0);
            param.Add("@branchid", branchId);
            param.Add("@orgid", orgId);
            param.Add("@ScreenId", 0);
            param.Add("@HeaderId", 0);

            var roles = await _connection.QueryAsync<RoleDropdown>(
                AccessRights.AccessRightsProc,
                param,
                commandType: CommandType.StoredProcedure
            );

            return new ResponseModel
            {
                Data = roles.ToList(),
                Status = true,
                Message = "Success"
            };
        }

        public async Task<object> GetDepartmentsAsync(int branchId, int orgId)
        {
            var param = new DynamicParameters();
            param.Add("@opt", 4); // option 4 = departments
            param.Add("@userid", 0);
            param.Add("@branchid", branchId);
            param.Add("@orgid", orgId);
            param.Add("@ScreenId", 0);
            param.Add("@HeaderId", 0);

            var departments = await _connection.QueryAsync<DepartmentDropdown>(
                AccessRights.AccessRightsProc,
                param,
                commandType: CommandType.StoredProcedure
            );

            return new ResponseModel
            {
                Data = departments.ToList(),
                Status = true,
                Message = "Success"
            };
        }

        public async Task<object> GetModuleScreensAsync(int branchId, int orgId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 5);
                param.Add("@userid", 0);
                param.Add("@branchid", branchId);
                param.Add("@orgid", orgId);
                param.Add("@ScreenId", 0);
                param.Add("@HeaderId", 0);

                var result = await _connection.QueryAsync<object>(
                    AccessRights.AccessRightsProc,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = result.ToList(),
                    Status = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = $"Error retrieving module screens: {ex.Message}"
                };
            }
        }

        public async Task<object> SaveAccessRights(AccessRightsSaveRequest request)
        {
            var sqlHeader = @"INSERT INTO master_accessrights_header
    (Role, Department, Hod, BranchId, OrgId, IsActive, CreatedBy, CreatedDate, CreatedIP, ModifiedBy, LastModifiedDate, ModifiedIP, EffectiveFrom)
    VALUES (@Role, @Department, @Hod, @BranchId, @OrgId, @IsActive, @CreatedBy, @CreatedDate, @CreatedIP, @ModifiedBy, @LastModifiedDate, @ModifiedIP, @EffectiveFrom);
    SELECT LAST_INSERT_ID();";

            try
            {
                // Insert header
                var headerId = await _connection.ExecuteScalarAsync<int>(sqlHeader, new
                {
                    Role = request.Header.Role,
                    Department = request.Header.Department,
                    Hod = request.Header.Hod,
                    BranchId = 1,
                    OrgId = 1,
                    IsActive = true,
                    CreatedBy = request.Header.CreatedBy,
                    CreatedDate = DateTime.Now,
                    CreatedIP = request.Header.CreatedIP,
                    ModifiedBy = request.Header.ModifiedBy,
                    LastModifiedDate = (DateTime?)null,
                    ModifiedIP = request.Header.ModifiedIP,
                    EffectiveFrom = request.Header.EffectiveFrom
                });

                var sqlDetail = @"INSERT INTO master_accessrights_details
        (HeaderId, Module, Screen, `View`, `Edit`, `Delete`, `Post`, `Save`, `Print`, ViewRate, SendMail, ViewDetails, Records, IsActive)
        VALUES (@HeaderId, @Module, @Screen, @View, @Edit, @Delete, @Post, @Save, @Print, @ViewRate, @SendMail, @ViewDetails, @Records, @IsActive);";

                // Insert details
                foreach (var detail in request.Details)
                {
                    await _connection.ExecuteAsync(sqlDetail, new
                    {
                        HeaderId = headerId,
                        Module = detail.Module,
                        Screen = detail.Screen,
                        View = detail.View,
                        Edit = detail.Edit,
                        Delete = detail.Delete,
                        Post = detail.Post,
                        Save = detail.Save,
                        Print = detail.Print,
                        ViewRate = detail.ViewRate,
                        SendMail = detail.SendMail,
                        ViewDetails = detail.ViewDetails,
                        Records = detail.Records,
                        IsActive = detail.IsActive
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    Message = "Access rights saved successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = $"Error saving access rights: {ex.Message}",
                    Data = null
                };
            }
        }



        public async Task<object> UpdateAccessRights(AccessRightsSaveRequest request)
        {
            if (request?.Header == null || request.Header.Id <= 0) // Use Id from Header
                return new ResponseModel
                {
                    Status = false,
                    Message = "Invalid Id for update",
                    Data = null
                };

            var sqlUpdateHeader = @"UPDATE master_accessrights_header
    SET Role = @Role,
        Department = @Department,
        Hod = @Hod,
        BranchId = @BranchId,
        OrgId = @OrgId,
        ModifiedBy = @ModifiedBy,
        LastModifiedDate = @LastModifiedDate,
        ModifiedIP = @ModifiedIP,
        EffectiveFrom = @EffectiveFrom
    WHERE Id = @Id;";

            var sqlDeleteDetails = @"DELETE FROM master_accessrights_details WHERE HeaderId = @Id;";

            var sqlInsertDetail = @"INSERT INTO master_accessrights_details
    (HeaderId, Module, Screen, `View`, `Edit`, `Delete`, `Post`, `Save`, `Print`, ViewRate, SendMail, ViewDetails, Records, IsActive)
    VALUES (@HeaderId, @Module, @Screen, @View, @Edit, @Delete, @Post, @Save, @Print, @ViewRate, @SendMail, @ViewDetails, @Records, @IsActive);";

            try
            {
                // Update header
                await _connection.ExecuteAsync(sqlUpdateHeader, new
                {
                    Id = request.Header.Id,
                    Role = request.Header.Role,
                    Department = request.Header.Department,
                    Hod = request.Header.Hod,
                    BranchId = 1,
                    OrgId = 1,
                    ModifiedBy = request.Header.ModifiedBy,
                    LastModifiedDate = DateTime.Now,
                    ModifiedIP = request.Header.ModifiedIP,
                    EffectiveFrom = request.Header.EffectiveFrom
                });

                // Delete old details
                await _connection.ExecuteAsync(sqlDeleteDetails, new { Id = request.Header.Id });

                // Insert updated details
                foreach (var detail in request.Details)
                {
                    await _connection.ExecuteAsync(sqlInsertDetail, new
                    {
                        HeaderId = request.Header.Id,
                        Module = detail.Module,
                        Screen = detail.Screen,
                        View = detail.View,
                        Edit = detail.Edit,
                        Delete = detail.Delete,
                        Post = detail.Post,
                        Save = detail.Save,
                        Print = detail.Print,
                        ViewRate = detail.ViewRate,
                        SendMail = detail.SendMail,
                        ViewDetails = detail.ViewDetails,
                        Records = detail.Records,
                        IsActive = detail.IsActive
                    });
                }

                return new ResponseModel
                {
                    Status = true,
                    Message = "Access rights updated successfully",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = $"Error updating access rights: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<object> GetAccessRightsByBranchOrg(int branchId, int orgId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 6);       // Option 6 = header + details
                param.Add("@userid", 0);
                param.Add("@branchid", branchId);
                param.Add("@orgid", orgId);
                param.Add("@ScreenId", 0);
                param.Add("@HeaderId", 0);

                // Directly get all rows
                var result = await _connection.QueryAsync(
                    AccessRights.AccessRightsProc,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = result,   
                    Status = true,
                    Message = "Access rights retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = $"Error retrieving access rights: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<object> GetAccessRightsDetailById(int headerId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 7);       
                param.Add("@userid", 0);
                param.Add("@branchid", 0);
                param.Add("@orgid", 0);
                param.Add("@ScreenId", 0);
                param.Add("@HeaderId", headerId);

                // Directly get all rows
                var result = await _connection.QueryAsync(
                    AccessRights.AccessRightsProc,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = result,   
                    Status = true,
                    Message = "Access rights details retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = $"Error retrieving access rights details: {ex.Message}",
                    Data = null
                };
            }
        }

    }
}
