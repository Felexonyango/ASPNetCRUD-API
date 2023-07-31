using BookApp.Context;
using BookApp.Models;

namespace BookApp.Services
{
    public class BufferedFileUploadLocalService
    {
          private ApplicationDbContext _dbContext;
        public BufferedFileUploadLocalService(ApplicationDbContext context)
        {
            _dbContext = context;
        
        }
        public async Task<bool> SaveFileToDatabase(string fileName, byte[] fileContent)
        {
            try
            {
                var fileUpload = new FileUpload
                {
                    FileName = fileName,
                    Content = fileContent,
                    CreatedOn = DateTime.Now
                };

                _dbContext.Add(fileUpload);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions if needed
                return false;
            }
        }
    }
    
    }
