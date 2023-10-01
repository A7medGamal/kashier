
using Microsoft.AspNetCore.Mvc;
using kashier.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using kashier.DAL.Entities;
using Newtonsoft.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using kashier.BL.Interface;

namespace kashier.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ITicketRep ticket;
        public PaymentController(ITicketRep _Ticket)
        {
            this.ticket = _Ticket;
        }
        public IActionResult Index()
        {

            // TempData["mydata"] = JsonConvert.DeserializeObject<List<TicketsVm>>(TempData["mydata"].ToString());
            // TicketsVm tick = JsonConvert.DeserializeObject<List<TicketsVm>>(TempData["mtdata"].ToString());
            //if (TempData.TryGetValue("myData", out var data))
            //{

            //}
            string amount="0";
           
            if (TempData.TryGetValue("myData", out var serializedObject))
            {
                var myObject = JsonConvert.DeserializeObject<TicketsVm>(serializedObject.ToString());

                amount = myObject.TotalAmount.ToString();
                
               
            }
            // Create an instance of Config and Order, and populate them with your data.
            var config = new Config
            {
                // Populate your config data here.
                Mode = "test",
                Live = new EnvironmentConfig
                {
                    BaseUrl = "https://checkout.kashier.io",
                    ApiKey = "",
                    Mid = ""
                },
                Test = new EnvironmentConfig
                {
                    BaseUrl = "https://checkout.kashier.io",
                    ApiKey = "your api key",
                    Mid = "Your merchant id"
                }
            };

            var order = new Order
            {
                // Populate your order data here.
                Amount =amount,
                Currency = "EGP",
                Mode = config.Mode,
                Secret = config.Test.ApiKey,
                Mid = config.Test.Mid,
                BaseUrl = config.Test.BaseUrl,
                MerchantOrderId = DateTime.Now.Ticks.ToString(),
                AllowedMethods = "card,wallet"
            };

            // Generate the order hash.
            var hash = GenerateKashierOrderHash(order);

            // Pass the order data and hash to the view.
            //var model = new Order
            //{
            //    Amount = order.Amount,
            //    Currency = order.Currency,
            //    MerchantOrderId = order.MerchantOrderId,
            //    Mid = order.Mid,
            //    Secret = order.Secret,
            //    BaseUrl = order.BaseUrl,
            //    Mode = order.Mode,
            //    AllowedMethods = order.AllowedMethods,
            //    Hash = hash
            //};
            var viewModel = new OrderViewModel
            {
                Order = order,
                Hash = hash
            };
       
            
            
            return View("~/Views/Tickets/Index.cshtml", viewModel);
            //return View("test");
        }
   
        private string GenerateKashierOrderHash(Order order)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(order.Secret)))
            {
                var path = $"/?payment={order.Mid}.{order.MerchantOrderId}.{order.Amount}.{order.Currency}";
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(path));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public IActionResult CallBack(string paymentStatus, string transactionId)
        {
            int id;
            if (TempData.TryGetValue("UpdatData", out var serializedObject))
            {
                var myObject = JsonConvert.DeserializeObject<TicketsVm>(serializedObject.ToString());

                id = myObject.Id;
                if (paymentStatus == "SUCCESS")
                {
                    //var data = ticket.GetByID(id);
                    myObject.TransactionId = transactionId;
                    myObject.IsPaid = true;
                    ticket.Edit(myObject);
                }

            }

          



            return Redirect("~/Index.html");
        }
    }
}
