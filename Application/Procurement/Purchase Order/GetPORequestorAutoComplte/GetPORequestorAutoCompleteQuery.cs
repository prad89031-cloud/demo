﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Procurement.Purchase_Order.GetPORequestorAutoComplte
{
    public class GetPORequestorAutoCompleteQuery : IRequest<object>
    {
        public int branchid { get; set; }
        public int orgid { get; set; }
        public string requestorname { get; set; }
    }
}
