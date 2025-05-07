using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IEnumerable<Reservation> GetByUserId(int userId);
        IEnumerable<Reservation> GetByResourceId(int resourceId);
    }
}
