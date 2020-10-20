using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using List.Models;
using List.Models.ViewModel;
using List.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace List.Controllers
{
    //[Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class RoomListController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RoomListController));
        //private readonly AppDbContext dbContext;
        private readonly IRoomRepository RoomRepo;
        //private readonly AddRoomRepository AddRoomRepo;
        public RoomListController(IRoomRepository Room)
        {
            this.RoomRepo = Room;
           // this.AddRoomRepo = AddRoom;
            //this.dbContext = context;
        }
        [HttpGet]
        [Route("api/RoomList/GetList")]
        public async Task<IActionResult> GetList()
        {
            _log4net.Info("API initiated");
            var listOfRooms = await RoomRepo.GetRoomList(); //dbContext.RoomList.ToList();
            if (listOfRooms == null)
            {
                ModelState.AddModelError("", "No rooms found");
                return NotFound();
            }

            return Ok(listOfRooms);

        }

        [HttpPost]
        [Route("api/RoomList/AddRoom")]
        public async Task<IActionResult> AddRoom([FromBody]RoomsListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //var alreadyExist = await dbContext.RoomList.Where(x => x.Name == model.Name && x.ContactNo == model.ContactNo).FirstOrDefaultAsync();
            /*if (alreadyExist != null)
            {
                //ModelState.AddModelError("","You cannot add more than one request");
                return Conflict("you cannot add more than one request");
            }
            else
            {
                dbContext.RoomList.Add(model);
                var row = dbContext.SaveChanges();
                if (row>0)
                {
                    return new StatusCodeResult(201);
                }
                else
                {
                    return new StatusCodeResult(500);
                }
            }*/
            var roomAdded = await RoomRepo.AddRoom(model);
            if (roomAdded == -1)
            {
                return Conflict("you cannot add more than one request");
            }
            else if (roomAdded == 1)
            {
                return new StatusCodeResult(201);
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }
    }
}