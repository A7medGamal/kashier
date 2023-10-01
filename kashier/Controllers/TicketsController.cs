
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using kashier.Models;
using kashier.BL.Interface;
using System.Security.Cryptography;
using System.Text;
using kashier.DAL.Entities;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace kashier.Controllers;
public class TicketsController : Controller
{
    private readonly ITicketRep ticket;
    public TicketsController(ITicketRep _Ticket)
    {
        this.ticket = _Ticket;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Index(TicketsVm tick)
    {
        try
        {
            
                tick.TotalAmount = CalculateOrderAmount(tick.NumOfTickets);


                tick.Id = ticket.Add(tick);
          
                TempData["mydata"]= JsonConvert.SerializeObject(tick);
                TempData["UpdatData"] = JsonConvert.SerializeObject(tick);
                return RedirectToAction("Index","Payment"/*,new {tick.TotalAmount}*/);
           




        }
        catch (Exception ex)
        {
            EventLog log = new EventLog();
            log.Source = "Admin Dashboard";
            log.WriteEntry(ex.Message, EventLogEntryType.Error);

            return View(tick);
        }

    }

    [HttpGet]
   
    public decimal CalculateOrderAmount(int numberOfTickets)
    {

        return numberOfTickets * 10;
    }
    
}

