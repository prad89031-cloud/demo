using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AccountCategories.GLcodemaster
{
    public class GetAccountDetails
    {
        // AccountCategory
        public int AccountCategoryId { get; set; }
        public int AccountCategoryCategoryCode { get; set; }
        public string AccountCategoryCategoryName { get; set; }
        public string AccountCategoryDescription { get; set; }

        // AccountType
        public int AccountTypeId { get; set; }
        public int AccountTypeCategoryCode { get; set; }
        public string AccountTypeCategoryName { get; set; }
        public int AccountTypeCategoryId { get; set; }
        public string AccountTypeDescription { get; set; }

        // GLcodemaster (all columns)
        public int Id { get; set; }
        public int CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedIP { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string LastModifiedIP { get; set; }
        public bool? IsActive { get; set; }
        public int? OrgId { get; set; }
        public int? BranchId { get; set; }

    }
}
