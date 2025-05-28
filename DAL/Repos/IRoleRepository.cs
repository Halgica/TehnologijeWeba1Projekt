using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<IEnumerable<Role>> GetByNameAsync(RoleName name);
    }
}
