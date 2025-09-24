using BackEnd.AccountCategories.AccountCategoryProc;
using Core.Abstractions;
using Core.AccountCategories.AccountCategory;
using Core.Models;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.AccountCategoryRepository
{
    public class AccountCategoryRepository : IAccountCategoryRepository
    {
        private readonly IDbConnection _connection;

        public AccountCategoryRepository(IUnitOfWorkDB3 financedb)
        {
            _connection = financedb.Connection;
        }

        public async Task<object> GetAll()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("p_ActionType", "GETALL");
                param.Add("p_Id", null);
                param.Add("p_CategoryCode", null);
                param.Add("p_CategoryName", null);
                param.Add("p_CategoryId", null);
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

                var categories = (await _connection.QueryAsync<AccountCategoryModel>(
                    AccountCategoryProc.AccountCategoryprocedure,
                    param,
                    commandType: CommandType.StoredProcedure
                )).ToList();

                return new ResponseModel { Data = categories, Status = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Data = null, Status = false, Message = "Error retrieving account categories: " + ex.Message };
            }
        }

        public async Task<object> Insert(AccountCategoryModel category)
        {
            try
            {
                category.CreatedDate = DateTime.Now;
                category.LastModifiedDate = DateTime.Now;

                var param = new DynamicParameters();
                param.Add("p_ActionType", "INSERT");
                param.Add("p_Id", category.Id);
                param.Add("p_CategoryCode", category.CategoryCode);
                param.Add("p_CategoryName", category.CategoryName);
                param.Add("p_CategoryId", category.CategoryId);
                param.Add("p_Description", category.Description);
                param.Add("p_CreatedBy", category.CreatedBy);
                param.Add("p_CreatedDate", category.CreatedDate);
                param.Add("p_CreatedIP", category.CreatedIP);
                param.Add("p_LastModifiedBy", category.LastModifiedBy);
                param.Add("p_LastModifiedDate", category.LastModifiedDate);
                param.Add("p_LastModifiedIP", category.LastModifiedIP);
                param.Add("p_IsActive", category.IsActive);
                param.Add("p_OrgId", category.OrgId);
                param.Add("p_BranchId", category.BranchId);

                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                await _connection.ExecuteAsync(
                    AccountCategoryProc.AccountCategoryprocedure,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                var newIdObj = await _connection.ExecuteScalarAsync<object>("SELECT LAST_INSERT_ID();");
                if (newIdObj != null && long.TryParse(newIdObj.ToString(), out long newIdLong))
                    category.Id = (int)newIdLong;

                return new ResponseModel { Data = category, Status = true, Message = "success" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Data = null, Status = false, Message = "Error creating account category: " + ex.Message };
            }
        }

        public async Task<object> Update(AccountCategoryModel category)
        {
            try
            {
                if (category == null || category.Id <= 0)
                    return new ResponseModel { Data = null, Status = false, Message = "Invalid category data or ID" };

                category.LastModifiedDate = DateTime.Now;

                var sql = @"
                    UPDATE tbl_AccountCategory
                    SET 
                        CategoryCode = @CategoryCode,
                        CategoryName = @CategoryName,
                        CategoryId = @CategoryId,
                        Description = @Description,
                        LastModifiedBy = @LastModifiedBy,
                        LastModifiedDate = @LastModifiedDate,
                        LastModifiedIP = @LastModifiedIP,
                        IsActive = @IsActive,
                        OrgId = @OrgId,
                        BranchId = @BranchId
                    WHERE Id = @Id AND IsActive = 1";

                var rows = await _connection.ExecuteAsync(sql, category);

                if (rows > 0)
                    return new ResponseModel { Data = category, Status = true, Message = "Account category updated successfully" };

                return new ResponseModel { Data = null, Status = false, Message = "Update failed: record not found or inactive" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Data = null, Status = false, Message = "Error updating account category: " + ex.Message };
            }
        }




        public async Task<object> Delete(int id)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                var sql = "DELETE FROM tbl_AccountCategory WHERE Id = @Id";
                var rows = await _connection.ExecuteAsync(sql, new { Id = id });

                if (rows == 0)
                    return new ResponseModel { Data = null, Status = false, Message = "Delete failed: record not found" };

                return new ResponseModel { Data = rows, Status = true, Message = "Account category deleted successfully" };
            }
            catch (Exception ex)
            {
                return new ResponseModel { Data = null, Status = false, Message = "Error deleting account category: " + ex.Message };
            }
        }

    }
}
