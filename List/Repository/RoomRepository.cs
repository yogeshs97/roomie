using List.Models;
using List.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace List.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext dbContext;

        public RoomRepository(AppDbContext context)
        {
            this.dbContext = context;
        }
        public async Task<List<RoomsListModel>> GetRoomList()
        {
            return await dbContext.RoomList.ToListAsync();
        }
        public async Task<int> AddRoom(RoomsListViewModel model)
        {

            var alreadyExist = await dbContext.RoomList.Where(x => x.Name == model.Name && x.ContactNo == model.ContactNo).FirstOrDefaultAsync();
            if (alreadyExist != null)
            {
                return -1;
            }
            else
            {
                RoomsListModel temp = new RoomsListModel()
                {
                    Name = model.Name,
                    City = model.City,
                    Location = model.Location,
                    SpaceCount = model.SpaceCount,
                    Rent = model.Rent,
                    Description = model.Description,
                    ContactNo = model.ContactNo
                };
                dbContext.RoomList.Add(temp);
                var row = dbContext.SaveChanges();
                return row;
            }
        }
    }
}
