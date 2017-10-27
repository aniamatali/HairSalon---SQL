using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using HairSalon;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [HttpGet("/Stylists")]
      public ActionResult StylistPage()
      {
        return View("ViewStylists",Stylist.GetAll());
      }

      [HttpPost("/Stylists")]
      public ActionResult ViewStylists()
      {
        Stylist newStylist = new Stylist (Request.Form["inputStylist"]);
        newStylist.Save();
        return View (Stylist.GetAll());
      }

      [HttpGet("/Stylists/{id}")]
      public ActionResult AddClientToStylist(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(id);
        List<Client> stylistClients = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);
        model.Add("client", stylistClients);
        return View(model);
      }

      [HttpPost("/Stylists/{id}")]
      public ActionResult AddAClient(int id)
      {
        string clientName = Request.Form["inputClient"];
        Client newClient = new Client(clientName,id,(Request.Form["inputHours"]));
        newClient.Save();

        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(Int32.Parse(Request.Form["stylist-id"]));
        List<Client> stylistClients = selectedStylist.GetClients();
        model.Add("client", stylistClients);
        model.Add("stylist", selectedStylist);


        return View("ClientListForStylist", model);
      }

    }
  }
