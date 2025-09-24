using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.AccountCategories.AccountCategory
{
    public class AccountCategoryModel
    {
        public int Id { get; set; }
        public int CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIP { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedIP { get; set; }
        public bool IsActive { get; set; }
        public int OrgId { get; set; }
        public int BranchId { get; set; }
    }
}
