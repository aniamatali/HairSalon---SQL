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

      [HttpPost("/Stylists/{id}/Delete")]
      public ActionResult RemoveEmployee(int id)
      {
        Stylist.DeleteStylist(id);
        Client.DeleteClients(id);
        return View("RemoveThisEmployee");
      }

      [HttpPost("/Stylists/Delete")]
      public ActionResult RemoveAllEmployees()
      {
        Client.DeleteAll();
        Stylist.DeleteAll();
        return View("RemoveAllEmployees");
      }

      [HttpGet("/AppointmentChanger")]
      public ActionResult AlphaList()
      {
        return View(Client.GetAlphaList());
      }

      [HttpPost("/Clients/Delete")]
      public ActionResult CancelAppointments()
      {
        Client.DeleteAll();
        return View("RemoveAllAppointments");
      }

      [HttpGet("/Stylists/{id}/update")]
    public ActionResult ClientUpdate(int id)
    {
      Client thisClient = Client.Find(id);
      return View(thisClient);
    }

    [HttpPost("/Stylists/{id}/update")]
    public ActionResult ClientEdit(int id)
    {
      Client thisClient = Client.Find(id);
      thisClient.UpdateClientName(Request.Form["new-name"]);
      return RedirectToAction("AlphaList");
    }

    }
  }
