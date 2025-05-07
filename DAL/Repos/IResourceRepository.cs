using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Resource? GetByName(string name);
    }
}
