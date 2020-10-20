using List.Models;
using List.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginTest
{
    public class Tests
    {
        AppDbContext _dbContext;
        List<RoomsListModel> rooms = new List<RoomsListModel>();
        IQueryable<RoomsListModel> roomsdata;
        Mock<DbSet<RoomsListModel>> mockset;
        Mock<AppDbContext> mockcontext;

        [SetUp]
        public void Setup()
        {
            rooms = new List<RoomsListModel>
            {
                new RoomsListModel{ Id=1,Name="Prince",City="Hisar",Location="Kacchi basti",SpaceCount=1,Rent=1000,Description="",ContactNo="0000000000" },
                new RoomsListModel{ Id=1,Name="Varun",City="Panipat",Location="Ganda nala",SpaceCount=1,Rent=1000,Description="",ContactNo="4444444444" }
            };

            roomsdata = rooms.AsQueryable();

            mockset = new Mock<DbSet<RoomsListModel>>();
            mockset.As<IQueryable<RoomsListModel>>().Setup(m => m.Provider).Returns(roomsdata.Provider);
            mockset.As<IQueryable<RoomsListModel>>().Setup(m => m.Expression).Returns(roomsdata.Expression);
            mockset.As<IQueryable<RoomsListModel>>().Setup(m => m.ElementType).Returns(roomsdata.ElementType);
            mockset.As<IQueryable<RoomsListModel>>().Setup(m => m.GetEnumerator()).Returns(roomsdata.GetEnumerator());

            var mocksetcontext = new DbContextOptions<AppDbContext>();
            mockcontext = new Mock<AppDbContext>(mocksetcontext);
            mockcontext.Setup(x => x.RoomList).Returns(mockset.Object);

            // var mocksetcontext = new Mock<AppDbContext>();
            //mocksetcontext.Setup(c => c.RoomsListModel).Returns(mockset.Object);
            // _dbcontext = mocksetcontext.Object;
        }

        [Test]
        public async void Test1()
        {
            //Assert.Pass();
            /*AttendeeRepository attendeerecord = new AttendeeRepository(_dbcontext);
            AttendeeRecordsController attenderecordobj = new AttendeeRecordsController(attendeerecord);
            var result = attenderecordobj.GetAllAttendees();
            var response = result as OkObjectResult;*/

            var roomRepo = new RoomRepository(mockcontext.Object);
            var roomList = await roomRepo.GetRoomList();
            var expectedList = mockcontext.Object.RoomList.ToList();
            Assert.AreEqual(7, roomList.Count);
        }

        
    }
}