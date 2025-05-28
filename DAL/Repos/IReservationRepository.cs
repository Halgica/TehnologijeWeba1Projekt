using DAL.Models;
using System;
using System.Collections.Generic;

namespace DAL.Repos
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Reservation>> GetByResourceIdAsync(int resourceId);
        Task<IEnumerable<Reservation>> GetBySlotIdAsync(int slotId);
    }
}
