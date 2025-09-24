namespace Core.AccountCategories.GLcodemaster
{
    public interface IGLCodeMasterRepository
    {
        // CRUD methods using object
        Task<object> GetAllAsync();
        Task<object> CreateAsync(object glCodeMaster);
        Task<object> UpdateAsync(object glCodeMaster);
        Task<object> DeleteAsync(int id);

        // Sequence generator
        Task<object> GenerateGLSequenceIdAsync(int categoryId, int inputId);

        // AccountTypeDetails
        Task<object> GetAllAccountTypeDetailsAsync();
        Task<object> GetAccountTypeDetailsByIdAsync(int glId);
    }
}
