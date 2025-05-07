using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByEmail(string email);
    }
}
