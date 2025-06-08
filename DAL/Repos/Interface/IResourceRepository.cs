using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Repos.Interface
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Task<Resource?> GetByNameAsync(string name);
    }
}
