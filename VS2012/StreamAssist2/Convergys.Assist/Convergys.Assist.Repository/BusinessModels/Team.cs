using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class Team : IEquatable<Team>
    {
        public int TeamId { get; set; }
        public decimal? SiteId { get; set; }
        public string TeamName { get; set; }

        public bool Equals(Team other)
        {
            if (other == null) return false;
            return (this.TeamId == other.TeamId);
        }

        public override int GetHashCode()
        {
            return TeamId;
        }
    }
}
