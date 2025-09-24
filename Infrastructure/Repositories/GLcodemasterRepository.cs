using BackEnd.AccountCategories.GLcodemasterProc;
using Core.Abstractions;
using Core.AccountCategories.GLcodemaster;
using Core.Models;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GLCodeMasterRepository : IGLCodeMasterRepository
    {
        private readonly IDbConnection _connection;

        public GLCodeMasterRepository(IUnitOfWorkDB3 financedb)
        {
            _connection = financedb.Connection;
        }

        // GET ALL
        public async Task<object> GetAllAsync()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("p_Action", "GET");
                param.Add("p_Id", 0);                  
                param.Add("p_GLcode", null);
                param.Add("p_CategoryName", null);
                param.Add("p_CategoryId", null);
                param.Add("p_AccountTypeId", null);
                param.Add("p_Description", null);
                param.Add("p_CreatedBy", null);
                param.Add("p_CreatedDate", null);
                param.Add("p_CreatedIP", null);
                param.Add("p_LastModifiedBy", null);
                param.Add("p_LastModifiedDate", null);
                param.Add("p_LastModifiedIP", null);
                param.Add("p_IsActive", null);
                param.Add("p_OrgId", null);
                param.Add("p_BranchId", null);

                var result = await _connection.QueryAsync<GLCodeMastermodels>(
                    GLcodemasterProc.GLCodeMaster,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = result.ToList(),
                    Status = true,
                    Message = "GL Code Masters retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error retrieving GL Code Masters: " + ex.Message
                };
            }
        }


        // CREATE
        public async Task<object> CreateAsync(object glCodeMasterObj)
        {
            if (glCodeMasterObj is not GLCodeMastermodels entity)
                throw new ArgumentException("Invalid type");

            try
            {
                entity.CreatedDate = DateTime.Now;
                entity.LastModifiedDate = DateTime.Now;

                var param = Build(entity, "POST");

                var rowsAffected = await _connection.ExecuteAsync(
                    GLcodemasterProc.GLCodeMaster,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = rowsAffected > 0 ? entity : null,
                    Status = rowsAffected > 0,
                    Message = rowsAffected > 0 ? "GL Code Master created successfully" : "Failed to create GL Code Master"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error creating GL Code Master: " + ex.Message
                };
            }
        }

        // UPDATE
        public async Task<object> UpdateAsync(object glCodeMasterObj)
        {
            if (glCodeMasterObj is not GLCodeMastermodels entity)
                throw new ArgumentException("Invalid type");

            try
            {
                entity.LastModifiedDate = DateTime.Now;

                var param = Build(entity, "UPDATE");

                var rowsAffected = await _connection.ExecuteAsync(
                    GLcodemasterProc.GLCodeMaster,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = rowsAffected > 0 ? entity : null,
                    Status = rowsAffected > 0,
                    Message = rowsAffected > 0 ? "GL Code Master updated successfully" : $"Update failed. No record found for Id {entity.Id}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error updating GL Code Master: " + ex.Message
                };
            }
        }

        // DELETE
        public async Task<object> DeleteAsync(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("p_Action", "DELETE");
                param.Add("p_Id", id);
                param.Add("p_GLcode", null);
                param.Add("p_CategoryName", null);
                param.Add("p_CategoryId", null);
                param.Add("p_AccountTypeId", null);
                param.Add("p_Description", null);
                param.Add("p_CreatedBy", null);
                param.Add("p_CreatedDate", null);
                param.Add("p_CreatedIP", null);
                param.Add("p_LastModifiedBy", null);
                param.Add("p_LastModifiedDate", null);
                param.Add("p_LastModifiedIP", null);
                param.Add("p_IsActive", null);
                param.Add("p_OrgId", null);
                param.Add("p_BranchId", null);

                var rowsAffected = await _connection.ExecuteAsync(
                    GLcodemasterProc.GLCodeMaster,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = rowsAffected,
                    Status = rowsAffected > 0,
                    Message = rowsAffected > 0 ? "GL Code Master deleted successfully" : $"No record found for Id {id}"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = 0,
                    Status = false,
                    Message = "Error deleting GL Code Master: " + ex.Message
                };
            }
        }


        // GENERATE GL SEQUENCE ID
        public async Task<object> GenerateGLSequenceIdAsync(int categoryId, int inputId)
        {
            var param = new DynamicParameters();
            param.Add("p_CategoryId", categoryId);
            param.Add("p_InputId", inputId);

            var sequence = await _connection.QuerySingleOrDefaultAsync<string>(
                GLcodemasterProc.GenerateGLSequenceId,
                param,
                commandType: CommandType.StoredProcedure
            );

            return new ResponseModel
            {
                Data = sequence,
                Status = true,
                Message = "GL sequence generated successfully"
            };
        }


        // GET ALL ACCOUNT TYPE DETAILS
        // GET ALL ACCOUNT TYPE DETAILS
        public async Task<object> GetAllAccountTypeDetailsAsync()
        {
            try
            {
                var result = await _connection.QueryAsync<object>(
                    GLcodemasterProc.GetAllDetails,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = result.ToList(),
                    Status = true,
                    Message = "Account Type Details retrieved successfully",
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error retrieving Account Type Details: " + ex.Message,
                    StatusCode = 500
                };
            }
        }


        // GET ACCOUNT TYPE DETAILS BY GL ID
        public async Task<object> GetAccountTypeDetailsByIdAsync(int glId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("p_GLId", glId);

                var result = await _connection.QueryFirstOrDefaultAsync<object>(
                    GLcodemasterProc.GenerateGLSequence,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                return new ResponseModel
                {
                    Data = result,
                    Status = true,
                    Message = "Account Type Details retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error retrieving Account Type Details: " + ex.Message
                };
            }
        }

        private DynamicParameters Build(GLCodeMastermodels entity, string action)
        {
            var param = new DynamicParameters();
            param.Add("p_Action", action);
            param.Add("p_Id", entity.Id);
            param.Add("p_GLcode", entity.GLcode);
            param.Add("p_CategoryName", entity.CategoryName);
            param.Add("p_CategoryId", entity.CategoryId);
            param.Add("p_Description", entity.Description);
            param.Add("p_CreatedBy", entity.CreatedBy);
            param.Add("p_CreatedDate", entity.CreatedDate);
            param.Add("p_CreatedIP", entity.CreatedIP);
            param.Add("p_LastModifiedBy", entity.LastModifiedBy);
            param.Add("p_LastModifiedDate", entity.LastModifiedDate);
            param.Add("p_LastModifiedIP", entity.LastModifiedIP);
            param.Add("p_IsActive", entity.IsActive);
            param.Add("p_OrgId", entity.OrgId);
            param.Add("p_BranchId", entity.BranchId);
            param.Add("p_AccountTypeId", entity.AccountTypeId);

            return param;
        }
    }
}
