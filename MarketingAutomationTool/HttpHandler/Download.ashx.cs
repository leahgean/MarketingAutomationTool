using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using EnterpriseLibrary.LeadService.FileAsset;
using BusinessLayer;
using DataModels;
using System.Configuration;

namespace MarketingAutomationTool.HttpHandler
{
    /// <summary>
    /// Summary description for Download
    /// </summary>
    public class Download : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            bool  useServerPath = true;
            //add some logic to validate the user if he has access to download file
            if (context.User.Identity.IsAuthenticated)
            {
                string file = context.Request.QueryString["file"];
                string id = context.Request.QueryString["id"];//uploadfile=jobid, export=searchid
                string accountid = context.Request.QueryString["acct"];
                string filetimestamp= context.Request.QueryString["ts"];

                file = GetFile(file, id, accountid, filetimestamp,ref useServerPath);

                if (useServerPath)
                {
                    file = context.Server.MapPath(file);
                }
                if (!string.IsNullOrEmpty(file) && File.Exists(file))
                {
                    context.Response.Clear();
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("content-disposition", "attachment;filename=" + Guid.NewGuid().ToString().Substring(0, 10) + ".xlsx");
                    context.Response.WriteFile(file);
                    context.Response.End();
                }
                else
                {
                    context.Response.ContentType = "text/HTML";
                    context.Response.Write("File cannot be found!");
                }
            }
        }

        private string GetFile(string p_sFile, string p_Id, string p_accountId, string p_filetimestamp, ref bool useServerPath)
        {
            switch (p_sFile.ToLower())
            {
                case "contact_template":
                    return "/Public/Templates/Template.xlsx";
                case "uploadfile":
                    LeadImport objLI = new LeadImport();
                    ContactJob cj = objLI.GetContactJob(int.Parse(p_Id), Guid.Parse(p_accountId));
                    FileAssetManager fsManager = new FileAssetManager(cj.CreatedBy, Guid.Parse(p_accountId));
                    string leadservicedirectory=fsManager.GetLeadServiceDirectory();
                    useServerPath = false;
                    return string.Format("{0}\\{1}", leadservicedirectory, cj.FileName);
                case "exportfile":
                    BusinessLayer.ContactSearchExport objCSE = new BusinessLayer.ContactSearchExport();
                    string sFileName = objCSE.GetExportFile(int.Parse(p_Id),Guid.Parse(p_accountId), p_filetimestamp);

                    string sLeadsExportFolder = ConfigurationManager.AppSettings["LeadsExportFolder"].ToString().Trim();
                    string sLeadsExportFolderPath = string.Format("/{0}/{1}", sLeadsExportFolder,sFileName);

                    return sLeadsExportFolderPath;
                default:
                    break;
            }
            return string.Empty;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}