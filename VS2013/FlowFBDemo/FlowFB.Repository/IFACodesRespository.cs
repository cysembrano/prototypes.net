using FlowFB.Repository.Filters;
using FlowFB.Repository.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.Repository
{
    public interface IFACodesRepository
    {
        IEnumerable<FACode> SearchCode(FACodeFilter filter);

        int? CreateCode(string project, string Code, string Description);

        void EditCode(string project, string Code, string Description, int codeId);

        void DeleteCode(string project, int codeId);
    }
}
