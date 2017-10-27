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

    }
  }
