﻿using BackEnd.Procurement.GoodsReceiptNote;
using BackEnd.Procurement.PurchaseOrder;
using BackEnd.Procurement.PurchaseRequitision;
using Core.Abstractions;
using Core.Models;
using Core.Procurement.GoodsReceiptNote;
using Core.Procurement.PurchaseOrder;
using Core.Procurement.PurchaseRequisition;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GoodsReceiptNoteRepository : IGoodsReceiptNoteRepository
    {
        private readonly IDbConnection _connection;
        string IPAddress = "";
        public GoodsReceiptNoteRepository(IUnitOfWorkDB2 unitOfWork)
        {
            _connection = unitOfWork.Connection;
        }
        public async Task<object> GetByGRNNoSeqAsync(int branchid, int orgid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 1);
                param.Add("@grnid", 0);
                param.Add("@supplierid", 0);
                param.Add("@poid", 0);
                param.Add("@branchid", branchid);
                param.Add("@orgid", orgid);               
                param.Add("@suppliername", null, DbType.String);
                param.Add("@grnno", null, DbType.String);


                var data = await _connection.QueryFirstOrDefaultAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                return new ResponseModel()
                {
                    Data = data,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong",
                    Status = false
                };
            }


        }
        public async Task<object> GetPOSupplierListAsync(int branchid, int orgid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 2);
                param.Add("@grnid", 0);
                param.Add("@branchid", branchid);
                param.Add("@orgid", orgid);
                param.Add("@supplierid", 0);
                param.Add("@suppliername", null, DbType.String);
                param.Add("@poid", 0);
                param.Add("@grnno", null, DbType.String);

                var data = await _connection.QueryAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                return new ResponseModel()
                {
                    Data = data,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong",
                    Status = false
                };
            }


        }
        public async Task<object> GetPOListAsync(int supplierid, int branchid, int orgid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 3);
                param.Add("@grnid", 0);
                param.Add("@branchid", branchid);
                param.Add("@orgid", orgid);
                param.Add("@supplierid", supplierid);
                param.Add("@suppliername", null, DbType.String);
                param.Add("@poid", 0);
                param.Add("@grnno", null, DbType.String);

                var data = await _connection.QueryAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                return new ResponseModel()
                {
                    Data = data,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong",
                    Status = false
                };
            }


        }
        public async Task<object> GetPoItemList(int poId, int orgId, int branchId,Int32 grnid)
        {

            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 5);
                param.Add("@grnid", grnid);
                param.Add("@branchid", branchId);
                param.Add("@orgid", orgId);
                param.Add("@supplierid", 0);
                param.Add("@suppliername", null, DbType.String);
                param.Add("@poid", poId);
                param.Add("@grnno", null, DbType.String);
    


                var data = await _connection.QueryAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                return new ResponseModel()
                {
                    Data = data,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong",
                    Status = false
                };
            }


        }
        public async Task<object> GetAllGRNAsync(int supplierid, int grnid, int branchid, int orgid)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@opt", 4);
                param.Add("@grnid", grnid);
                param.Add("@branchid", branchid);
                param.Add("@orgid", orgid);
                param.Add("@supplierid", supplierid);
                param.Add("@suppliername", null, DbType.String);
                param.Add("@poid", 0);
                param.Add("@grnno", null, DbType.String);

                var data = await _connection.QueryAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                return new ResponseModel()
                {
                    Data = data,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong",
                    Status = false
                };
            }


        }
        public async Task<object> GetGoodsReceiptNoteByIdAsync(int grnid, int branchid, int orgid)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("@opt", 6);
                param.Add("@grnid", grnid);
                param.Add("@branchid", branchid);
                param.Add("@orgid", orgid);
                param.Add("@supplierid", 0);
                param.Add("@suppliername", null, DbType.String);
                param.Add("@poid", 0);
                param.Add("@grnno", null, DbType.String);

                //var data = await _connection.QueryAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                var List = await _connection.QueryMultipleAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);
                dynamic Modellist = new ExpandoObject();
                int I = 0;
                while (!List.IsConsumed)
                {
                    dynamic nl = List.Read();

                    if (I == 0)
                    {
                        int count = nl.Count;
                        if (count == 0)
                        {
                            Modellist.Header = new object();
                        }
                        else
                        {
                            Modellist.Header = nl[0];
                        }
                    }
                    else if (I == 1)
                    {
                        Modellist.Details = nl;
                    }


                    I++;
                }
                return new ResponseModel()
                {
                    Data = Modellist,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong",
                    Status = false
                };
            }

        }

        public async Task<object> AddAsync(GoodsReceiptNote Obj)
        {

            try
            {
                int IsValidated = 0;
                string Message = "";
                Int32 Result = 0;
                SharedRepository SR = new SharedRepository(_connection);

                //var response = await SR.GetSeqNumber(0, Obj.Header.grnno, 4, Obj.Header.branchid, Obj.Header.orgid);
                //if (response.Status == true)
                //{
                //    if (response.Data.result == 1)
                //    {
                //        IsValidated = 1;
                //        Message = " - The current Goods Receipt Note number " + Obj.Header.grnno + " is taken for another order so the new order number (" + response.Data.text + ") has been generated for this order";
                //        Obj.Header.grnno = response.Data.text;
                //    }
                //}


                var param = new DynamicParameters();
                param.Add("@opt", 1);
                param.Add("@grnid", 0);
                param.Add("@branchid", Obj.Header.branchid);
                param.Add("@orgid", Obj.Header.orgid);
                param.Add("@supplierid", 0);
                param.Add("@suppliername", null, DbType.String);
                param.Add("@poid", 0);
                param.Add("@grnno", null, DbType.String);

                var data = await _connection.QueryFirstOrDefaultAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                if (data != null && data.grnno != null)
                {
                    Obj.Header.grnno = data.grnno;
                }


                const string headerSql = @"
                      INSERT INTO `tbl_grn_header`(`grnno`,`grndate`,`grnvalue`,`supplierid`,
                        `isactive`,`createddt`,`createdby`,`createdip`,`branchid`,`orgid`,`IsSubmitted`)VALUES
                    (@grnno,@grndate,@grnvalue,@supplierid,
                         1,NOW(),@userid,'',@branchid, @orgid, @isSubmitted);";


                await _connection.ExecuteAsync(headerSql, Obj.Header);

                // Get newly inserted POID
                var grnid = await _connection.QuerySingleAsync<int>("SELECT LAST_INSERT_ID();");


                var pridToPodidMap = new Dictionary<int, int>();
                var detailSql = "";
                foreach (var detail in Obj.Details)
                {
                    detail.grnid = grnid;

                    detailSql = @"                        
                    INSERT INTO `tbl_grn_detail`(`grnid`,`itemid`,`uomid`,`dono`,`dodate`,`poqty`,`alreadyrecqty`,`balanceqty`,
                    `grnQty`,`containerno`,`costperqty`,`amount`,`porid`,`isactive`,`createdby`,`createddt`,`createdip`,`branchid`,`orgid`,`poid`)
                    VALUES(@grnid,@itemid,@uomid,@dono,@dodate,@poqty,@alreadyrecqty,@balanceqty,@grnqty,@containerno,@costperqty,@amount,
                    @porid,1,@userid,Now(),'',@branchid,@orgid,@poid);";


                    var podid = await _connection.ExecuteAsync(detailSql, new
                    {
                        grnid = grnid,
                        itemid = detail.itemid,
                        uomid = detail.uomid,
                        dono = detail.dono,
                        dodate = detail.dodate,
                        poqty = detail.poqty,
                        alreadyrecqty = detail.alreadyrecqty,
                        balanceqty = detail.balanceqty,
                        grnQty = detail.grnqty,
                        containerno = detail.containerno,
                        costperqty = detail.costperqty,
                        amount = detail.amount,
                        porid = detail.porid,
                        userid = Obj.Header.userid,
                        branchid = Obj.Header.branchid,
                        orgid = Obj.Header.orgid,
                        poid = detail.poid
                    });


                }



                int BranchId = Obj.Header.branchid;
                var updateSeq = "UPDATE master_documentnumber SET Doc_Number = Doc_Number + 1 WHERE Doc_Type = 4 AND Unit = @branchid;";
                Result = await _connection.ExecuteAsync(updateSeq, new { BranchId });
                Result = 1;

                if (Result == 0)
                {
                    return new ResponseModel()
                    {
                        Data = null,
                        Message = "Save failed",
                        Status = false
                    };
                }
                else
                {
                    if (Obj.Header.isSubmitted == false)
                    {
                        return new ResponseModel()
                        {
                            Data = null,
                            Message = "Saved Successfully" + Message,
                            Status = true
                        };
                    }
                    else
                    {
                        return new ResponseModel()
                        {
                            Data = null,
                            Message = "Posted Successfully" + Message,
                            Status = true
                        };
                    }
                }
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong" + Ex.Message,
                    Status = false
                };
            }
        }

        public async Task<object> UpdateAsync(GoodsReceiptNote Obj)
        {
            try
            {
                // 1. Update Header
                const string headerSql = @"
                                     UPDATE `tbl_grn_header` 
                                     SET 
                                      grnno = @grnno,
                                      grndate = @grndate,
                                      grnvalue = @grnvalue,
                                      supplierid = @supplierid,                                                                                                           
                                      isactive = @isactive,
                                      modifieddt = NOW(),
                                      modifiedby = @userid,
                                      modifiedip = @modifiedip,
                                      branchid = @branchid,
                                      orgid = @orgid,
                                      IsSubmitted = @isSubmitted
                                      WHERE grnid = @grnid;";

                await _connection.ExecuteAsync(headerSql, Obj.Header);



                var UpdateSoquot = "update tbl_grn_detail set isactive=0 where grnid =" + Obj.Header.grnid;
 

           
                var Result = await _connection.ExecuteAsync(UpdateSoquot);
                //Update Details
                foreach (var detail in Obj.Details)
                {
                    string updateDetailSql = "";

                    detail.grnid = Obj.Header.grnid;

                    if (detail.grndid == 0)
                    {

                        updateDetailSql = @"                        
                    INSERT INTO `tbl_grn_detail`(`grnid`,`itemid`,`uomid`,`dono`,`dodate`,`poqty`,`alreadyrecqty`,`balanceqty`,
                    `grnQty`,`containerno`,`costperqty`,`amount`,`porid`,`isactive`,`createdby`,`createddt`,`createdip`,`branchid`,`orgid`,`poid`)
                    VALUES(@grnid,@itemid,@uomid,@dono,@dodate,@poqty,@alreadyrecqty,@balanceqty,@grnqty,@containerno,@costperqty,@amount,
                    @porid,1,@userid,Now(),'',@branchid,@orgid,@poid);";
                    }
                    else
                    {


                        updateDetailSql = @"
                                       UPDATE `tbl_grn_detail`
                                       SET 
                                       itemid = @itemid,
                                       uomid = @uomid,
                                       dono = @dono,
                                       dodate = @dodate,
                                       poqty = @poqty,
                                       alreadyrecqty = @alreadyrecqty,
                                       balanceqty = @balanceqty,
                                       grnQty = @grnqty,
                                       containerno = @containerno,
                                       costperqty = @costperqty,
                                       amount = @amount,
                                       porid = @porid,
                                       modifiedby = @userid,
                                       modifieddt = NOW(),
                                       modifiedip = '',
                                       branchid = @branchid,
                                       orgid = @orgid,
                                       poid = @poid,
                                       isactive=1 
                                       WHERE grndid = @grndid;";
                    }
                    await _connection.ExecuteAsync(updateDetailSql, new
                    {
                        grndid = detail.grndid,              
                        itemid = detail.itemid,
                        uomid = detail.uomid,
                        dono = detail.dono,
                        dodate = detail.dodate,
                        poqty = detail.poqty,
                        alreadyrecqty = detail.alreadyrecqty,
                        balanceqty = detail.balanceqty,
                        grnQty = detail.grnqty,
                        containerno = detail.containerno,
                        costperqty = detail.costperqty,
                        amount = detail.amount,
                        porid = detail.porid,
                        userid = Obj.Header.userid,
                        branchid = Obj.Header.branchid,
                        orgid = Obj.Header.orgid,
                        poid = detail.poid,
                        grnid=detail.grnid
                    });
                }

                // 4. Return success message
                return new ResponseModel
                {
                    Data = null,
                    Message = Obj.Header.isSubmitted == false
                        ? "Procurement GRN updated successfully"
                        : "Procurement GRN posted successfully",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Data = null,
                    Message = "Something went wrong: " + ex.Message,
                    Status = false
                };
            }
        }

        public async Task<object> GetGrnSupplierAutocomplete(int branchid, int orgid, string suppliername)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("@opt", 7);
                param.Add("@grnid", 0);
                param.Add("@branchid", branchid);
                param.Add("@orgid", orgid);
                param.Add("@supplierid", 0);
                param.Add("@suppliername",suppliername);
                param.Add("@poid", 0);
                param.Add("@grnno", null, DbType.String);

                var data = await _connection.QueryAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                return new ResponseModel()
                {
                    Data = data,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong",
                    Status = false
                };
            }
        }

        public async Task<object> GetGrnNoAutoComplete(int branchid, int orgid, string grnno)
        {
            try
            {
                var param = new DynamicParameters();

                param.Add("@opt", 8);
                param.Add("@grnid", 0);
                param.Add("@branchid", branchid);
                param.Add("@orgid", orgid);
                param.Add("@supplierid", 0);
                param.Add("@suppliername", null,DbType.String);
                param.Add("@poid", 0);
                param.Add("@grnno", grnno);

                var data = await _connection.QueryAsync(GoodsReceiptNoteBackEnd.GoodsReceiptNoteProcedure, param: param, commandType: CommandType.StoredProcedure);

                return new ResponseModel()
                {
                    Data = data,
                    Message = "Success",
                    Status = true
                };
            }
            catch (Exception Ex)
            {

                return new ResponseModel()
                {
                    Data = null,
                    Message = "Something went wrong",
                    Status = false
                };
            }
        }
    }
}
