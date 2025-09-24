using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Core.Abstractions;
using Core.AccountCategories.AccountType;
using Core.Models;
using BackEnd.AccountCategories.AccounttypeProc;

namespace BackEnd.Repositories.AccountTypeRepository
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly IDbConnection _connection;

        public AccountTypeRepository(IUnitOfWorkDB3 financedb)
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

                var data = (await _connection.QueryAsync<AccountTypemodel>(
                    AccountTypeProc.AccountType,
                    param,
                    commandType: CommandType.StoredProcedure
                )).ToList();

                return new ResponseModel
                {
                    Data = data,
                    Status = true,
                    Message = "Account types retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error retrieving account types: " + ex.Message
                };
            }
        }

        // INSERT
        public async Task<object> InsertAsync(object accountTypeObj)
        {
            if (accountTypeObj is not AccountTypemodel accountType)
                throw new ArgumentException("Invalid type");

            try
            {

                accountType.CreatedDate = DateTime.Now;
                accountType.LastModifiedDate = DateTime.Now;

                var param = new DynamicParameters(accountType);
                 param.Add("p_Id", accountType.Id); 
                param.Add("p_CategoryCode", accountType.CategoryCode);
                param.Add("p_CategoryName", accountType.CategoryName);
                param.Add("p_CategoryId", accountType.CategoryId);
                param.Add("p_Description", accountType.Description);
                param.Add("p_CreatedBy", accountType.CreatedBy);
                param.Add("p_CreatedDate", accountType.CreatedDate);
                param.Add("p_CreatedIP", accountType.CreatedIP);
                param.Add("p_LastModifiedBy", accountType.LastModifiedBy);
                param.Add("p_LastModifiedDate", accountType.LastModifiedDate);
                param.Add("p_LastModifiedIP", accountType.LastModifiedIP);
                param.Add("p_IsActive", accountType.IsActive);
                param.Add("p_OrgId", accountType.OrgId);
                param.Add("p_BranchId", accountType.BranchId);
                param.Add("p_Action", "POST");

                // Execute insert
                await _connection.ExecuteAsync(
                    AccountTypeProc.AccountType,
                    param,
                    commandType: CommandType.StoredProcedure
                );

                // Fetch the last inserted record
                var insertedRecord = await _connection.QuerySingleOrDefaultAsync<AccountTypemodel>(
                    "SELECT * FROM tbl_AccountType ORDER BY Id DESC LIMIT 1"
                );

                return new ResponseModel
                {
                    Data = insertedRecord,
                    Status = true,
                    Message = "Account type created successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error inserting account type: " + ex.Message
                };
            }
        }




        // UPDATE
        public async Task<object> UpdateAsync(object accountTypeObj)
        {
            if (accountTypeObj is not AccountTypemodel accountType)
                throw new ArgumentException("Invalid type");

            try
            {
                
                accountType.LastModifiedDate = DateTime.Now;

                var param = new DynamicParameters(accountType);
                param.Add("p_Id", accountType.Id);
                param.Add("p_CategoryCode", accountType.CategoryCode);
                param.Add("p_CategoryName", accountType.CategoryName);
                param.Add("p_CategoryId", accountType.CategoryId);
                param.Add("p_Description", accountType.Description);
                param.Add("p_CreatedBy", accountType.CreatedBy);
                param.Add("p_CreatedDate", accountType.CreatedDate);
                param.Add("p_CreatedIP", accountType.CreatedIP);
                param.Add("p_LastModifiedBy", accountType.LastModifiedBy);
                param.Add("p_LastModifiedDate", accountType.LastModifiedDate);
                param.Add("p_LastModifiedIP", accountType.LastModifiedIP);
                param.Add("p_IsActive", accountType.IsActive);
                param.Add("p_OrgId", accountType.OrgId);
                param.Add("p_BranchId", accountType.BranchId);
                param.Add("p_Action", "UPDATE");

                var rowsAffected = await _connection.ExecuteAsync(
                    AccountTypeProc.AccountType,
                    param,
                    commandType: CommandType.StoredProcedure
                );


                if (rowsAffected > 0)
                {
                    return new ResponseModel
                    {
                        Data = accountType,
                        Status = true,
                        Message = "Account type updated successfully"
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        Data = 0,
                        Status = false,
                        Message = $"No account type found with Id = {accountType.Id}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error updating account type: " + ex.Message
                };
            }
        }


        // DELETE
        public async Task<object> DeleteAsync(int id)
        {
            try
            {
   
                var param = new DynamicParameters();
                param.Add("p_Id", id);
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
                param.Add("p_Action", "DELETE");

        
                var rowsAffected = await _connection.ExecuteAsync(
                    AccountTypeProc.AccountType,
                    param,
                    commandType: CommandType.StoredProcedure
                );

    
                if (rowsAffected > 0)
                {
                    return new ResponseModel
                    {
                        Data = rowsAffected,
                        Status = true,
                        Message = "Account type deleted successfully"
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        Data = 0,
                        Status = false,
                        Message = $"No account type found with Id = {id}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Status = false,
                    Message = "Error deleting account type: " + ex.Message
                };
            }
        }

    }
}
