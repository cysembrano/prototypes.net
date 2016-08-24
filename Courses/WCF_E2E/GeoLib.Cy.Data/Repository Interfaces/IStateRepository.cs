using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GeoLib.Cy.Core;
using System.Data.Entity;

namespace GeoLib.Cy.Data
{
    public interface IStateRepository : IDataRepository<State>
    {
        State Get(string abbrev);
        IEnumerable<State> Get(bool primaryOnly);
    }
}
