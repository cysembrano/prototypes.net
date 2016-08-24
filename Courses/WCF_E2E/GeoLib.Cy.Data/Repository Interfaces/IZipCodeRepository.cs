using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GeoLib.Cy.Core;
using System.Data.Entity;

namespace GeoLib.Cy.Data
{
    public interface IZipCodeRepository : IDataRepository<ZipCode>
    {
        ZipCode GetByZip(string zip);
        IEnumerable<ZipCode> GetByState(string state);
        IEnumerable<ZipCode> GetZipsForRange(ZipCode zip, int range);
    }
}
