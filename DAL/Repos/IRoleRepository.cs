using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Role> GetByName(RoleName name);
    }
}
