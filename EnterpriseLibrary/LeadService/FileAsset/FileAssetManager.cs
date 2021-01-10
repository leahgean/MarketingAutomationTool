using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Common.Business;

namespace EnterpriseLibrary.LeadService.FileAsset
{
    public class FileAssetManager
    {
        private Guid CreatedBy;
        private Guid AccountID;

        public FileAssetManager(Guid createdby,Guid accountid)
        {
            CreatedBy = createdby;
            AccountID = accountid;

        }

        public static string AssetsDirectory()
        {
            return Common.Business.SysKey.GetKey("AssetsDirectory");
        }


        public static string LeadServiceDirectory()
        {
            return Common.Business.SysKey.GetKey("LeadServiceDirectory");
        }

        public string GetLeadServiceDirectory()
        {
            return Path.Combine(AssetsDirectory(), LeadServiceDirectory());
        }

        public Result CreateUploadFile(string sNewFileName, byte[] binaryData)
        {
            Result result = new Result(false, string.Empty, null);
            string sFilePath = string.Empty;
            string sFullPath = string.Empty;

            try
            {

                sFilePath = GetLeadServiceDirectory();


                if (!Directory.Exists(sFilePath))
                {
                    Directory.CreateDirectory(sFilePath);
                }

                sFullPath = Path.Combine(sFilePath, sNewFileName);
                if (!File.Exists(sFullPath))
                {
                    File.WriteAllBytes(sFullPath, binaryData);
                    result.Success= true;
                }
                else
                {
                    Logger.Logger.WriteLog(CreatedBy.ToString(), "EnterpiseLibrary-LeadService-FileAsset-FileAssetManager", sFullPath, "Error", "File Exists");
                    result.Success = false;
                    result.ErrorMessage = "File Exists";
                }
                
            }
            catch(Exception ex)
            {
                Logger.Logger.WriteLog(CreatedBy.ToString(), "EnterpiseLibrary-LeadService-FileAsset-FileAssetManager", sFullPath, "Error", ex.Message);
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.ExceptionObj = ex;
            }
            return result;
        }
    }
}
