using List.Models;
using List.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace List.Repository
{
    public interface IRoomRepository
    {
        public Task<List<RoomsListModel>> GetRoomList();

        public Task<int> AddRoom(RoomsListViewModel model);
    }
}
