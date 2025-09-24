﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Procurement.InvoiceReceipt.GetAllSupplierIRNList
{
    public class GetAllSupplierIRNListQuery : IRequest<object>
    {
        public int branchid { get; set; }
        public int orgid { get; set; }
    }
}
