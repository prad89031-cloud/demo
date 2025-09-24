﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Procurement.PurchaseOrder
{
    public interface IPurchaseOrderRepository
    {
        Task<object> AddAsync(PurchaseOrder obj);
        Task<object> GetAllAsync(Int32 requesterid, Int32 BranchId, Int32 SupplierId, int orgid, int poid);
        Task<object> GetByIdAsync(int poid, int branchid, int orgid);
        Task<object> UpdateAsync(PurchaseOrder obj);
        Task<object> GetPurchaseRequositionList(int supplierid, int branchid, int orgid, int currencyid);
        Task<object> GetPurchaseRequisitionItemsList( int branchid, int orgid , int prid);

        Task<object> GetPORequstorAutoComplete(int branchid, int orgid, string requestorname);
        Task<object> GetPOSupplierAutoComplete(int branchid, int orgid, string suppliername);

        Task<object> GetByPONoSeqAsync(int branchid, int orgid);

        Task<object> GetPOnoAutoComplete(int branchid, int orgid, string pono);

        Task<object> GetPurchaseorderPrint(int opt, int poid, int branchid, int orgid);

        Task<object> GetSupplierCurrencyList(int supplierid, int branchid, int orgid);
    }
}
