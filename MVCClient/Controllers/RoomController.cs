using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using MVCClient.Models.ViewModel;
using Newtonsoft.Json;

namespace MVCClient.Controllers
{
    public class RoomController : Controller
    {
        AccountController ac = new AccountController();
        [HttpGet]
        public async Task<IActionResult> RoomList()
        {
            if (HttpContext.Session.GetString("LoggedIn")==null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<RoomsListModel> roomslist= new List<RoomsListModel>();

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56990");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("/api/RoomList/GetList");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                roomslist = JsonConvert.DeserializeObject<List<RoomsListModel>>(jsonContent);
                return View(roomslist);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ViewBag.message = "There is no room available.";
                return View();
            }
            else
            {
                ViewBag.message = "No response from server";
                return View();
            }
        }
        
        [HttpGet]
        public IActionResult AddRoom()
        {
            if (HttpContext.Session.GetString("LoggedIn") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomsListViewModel model)
        {
            if (HttpContext.Session.GetString("LoggedIn") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56990"); 
            var jsonString = JsonConvert.SerializeObject(model);
            var message = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/RoomList/AddRoom", message);
            if(response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ModelState.AddModelError("","You cannot add more than one request");
                return View(model);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("RoomList", "Room");
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                ModelState.AddModelError("", "Something went wrong. Your Details have not been saved");
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "No Response from the server");
                return View(model);
            }

        }
    }
}