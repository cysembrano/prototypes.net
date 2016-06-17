using FlowFB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowFB.BusinessLogic.Extensions;
using FlowFB.Data;
using FlowFB.Repository.Models;
using FlowFB.Repository.Cache;
using FlowFB.Logging;

namespace FlowFB.BusinessLogic.SQLRepository
{
    public class FACodesRepository : IFACodesRepository
    {
        public IEnumerable<Repository.Models.FACode> SearchCode(Repository.Filters.FACodeFilter filter)
        {
            if (filter == null)
                return null;

            using(FlowFBEntities context = new FlowFBEntities())
            {
                switch (filter.Project)
                {
                    case "CostCenter":
                        return context.FFBA_CostCenter.ToFACostCenterCodes();
                    case "GLCodes":
                        return context.FFBA_GLCodes.ToFAGLCodes();
                    case "TaxCodes":
                        return context.FFBA_TaxCodes.ToFATaxCodes();
                    default: return null;

                }
            }

        }

        public int? CreateCode(string project, string Code, string Description)
        {
            using (FlowFBEntities context = new FlowFBEntities())
            {
                int? returnId = null;
                switch (project)
                {
                    case "CostCenter":
                        var addedcc = context.FFBA_CostCenter.Add(new FFBA_CostCenter()
                        {
                            Status = CodeStatus.New.GetHashCode(),
                            FBProjectId = ProjectCache.ProjectData["CostCenter"],
                            CostCenterCode = Code,
                            Description = Description
                        });
                        context.SaveChanges();
                        returnId = addedcc.CostCenterID;
                        break;
                    case "GLCodes":
                        var addedgl = context.FFBA_GLCodes.Add(new FFBA_GLCodes()
                        {
                            Status = CodeStatus.New.GetHashCode(),
                            FBProjectId = ProjectCache.ProjectData["GLCodes"],
                            GLCode = Code,
                            Description = Description
                        });
                        context.SaveChanges();
                        returnId = addedgl.GLCodeID;
                        break;
                    case "TaxCodes":
                        var addedtx = context.FFBA_TaxCodes.Add(new FFBA_TaxCodes()
                        {
                            Status = CodeStatus.New.GetHashCode(),
                            FBProjectId = ProjectCache.ProjectData["TaxCodes"],
                            TaxCode = Code,
                            Description = Description
                        });
                        context.SaveChanges();
                        returnId = addedtx.TaxCodeID;
                        break;
                    default: Log4NetManager.Instance.Error(this.GetType(), "Code Creation Failed.  No project by the name of " + project);
                        break;

                }
                return returnId;
            }

           
        }

        public void EditCode(string project, string Code, string Description, int codeId)
        {
            using(FlowFBEntities context = new FlowFBEntities())
            {
                switch (project)
                {
                    case "CostCenter":
                        var ccresult = context.FFBA_CostCenter.FirstOrDefault(g => g.CostCenterID == codeId);
                        if (ccresult != null)
                        {
                            ccresult.CostCenterCode = Code;
                            ccresult.Description = Description;
                            ccresult.Status = CodeStatus.Edited.GetHashCode();
                        }
                        break;
                    case "GLCodes":
                        var glresult = context.FFBA_GLCodes.FirstOrDefault(g => g.GLCodeID == codeId);
                        if (glresult != null)
                        {
                            glresult.GLCode = Code;
                            glresult.Description = Description;
                            glresult.Status = CodeStatus.Edited.GetHashCode();
                        }
                        break;
                    case "TaxCodes":
                        var taxresult = context.FFBA_TaxCodes.FirstOrDefault(g => g.TaxCodeID == codeId);
                        if (taxresult != null)
                        {
                            taxresult.TaxCode = Code;
                            taxresult.Description = Description;
                            taxresult.Status = CodeStatus.Edited.GetHashCode();
                        }
                        break;
                    default: Log4NetManager.Instance.Error(this.GetType(), "Code Editing Failed.  No project by the name of " + project);
                        break;

                }
                context.SaveChanges();
            }
        }   

        public void DeleteCode(string project, int codeId)
        {
            using (FlowFBEntities context = new FlowFBEntities())
            {
                switch (project)
                {
                    case "CostCenter":
                        var ccresult = context.FFBA_CostCenter.FirstOrDefault(g => g.CostCenterID == codeId);
                        if (ccresult != null) context.FFBA_CostCenter.Remove(ccresult);
                        break;
                    case "GLCodes":
                        var glresult = context.FFBA_GLCodes.FirstOrDefault(g => g.GLCodeID == codeId);
                        if (glresult != null) context.FFBA_GLCodes.Remove(glresult);
                        break;
                    case "TaxCodes":
                        var taxresult = context.FFBA_TaxCodes.FirstOrDefault(g => g.TaxCodeID == codeId);
                        if (taxresult != null) context.FFBA_TaxCodes.Remove(taxresult);
                        break;
                    default: Log4NetManager.Instance.Error(this.GetType(), "Code Editing Failed.  No project by the name of " + project);
                        break;

                }
                context.SaveChanges();
            }
        }
    }
}
