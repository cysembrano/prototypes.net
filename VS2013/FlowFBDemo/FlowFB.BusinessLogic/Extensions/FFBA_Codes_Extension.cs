using FlowFB.Data;
using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.BusinessLogic.Extensions
{
    public static class FFBA_Codes_Extension
    {
        public static FAGLCode[] ToFAGLCodes(this IEnumerable<FFBA_GLCodes> entities)
        {
            return entities.Select(g => g.ToFAGLCode()).ToArray();
        }

        public static FATaxCode[] ToFATaxCodes(this IEnumerable<FFBA_TaxCodes> entities)
        {
            return entities.Select(g => g.ToFATaxCode()).ToArray();
        }

        public static FACostCenterCode[] ToFACostCenterCodes(this IEnumerable<FFBA_CostCenter> entities)
        {
            return entities.Select(g => g.ToFACostCenterCode()).ToArray();
        }

        public static FAGLCode ToFAGLCode(this FFBA_GLCodes entity)
        {
            return new FAGLCode()
            {
                Id = entity.GLCodeID,
                Code = entity.GLCode,
                Status = (CodeStatus)Enum.ToObject(typeof(CodeStatus), entity.Status),
                Description = entity.Description,
                ProjectID = entity.FBProjectId

            };
        }

        public static FATaxCode ToFATaxCode(this FFBA_TaxCodes entity)
        {
            return new FATaxCode()
            {
                Id = entity.TaxCodeID,
                Code = entity.TaxCode,
                Status = (CodeStatus)Enum.ToObject(typeof(CodeStatus), entity.Status),
                Description = entity.Description,
                ProjectID = entity.FBProjectId

            };
        }

        public static FACostCenterCode ToFACostCenterCode(this FFBA_CostCenter entity)
        {
            return new FACostCenterCode()
            {
                Id = entity.CostCenterID,
                Code = entity.CostCenterCode,
                Status = (CodeStatus)Enum.ToObject(typeof(CodeStatus), entity.Status),
                Description = entity.Description,
                ProjectID = entity.FBProjectId

            };
        }
    }
}
