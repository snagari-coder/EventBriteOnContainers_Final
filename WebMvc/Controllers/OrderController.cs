﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly.CircuitBreaker;
using Stripe;
using WebMvc.Models;
using WebMvc.Models.OrderModels;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _cartSvc;
        private readonly IOrderService _orderSvc;
        private readonly IIdentityService<ApplicationUser> _identitySvc;
        private readonly ILogger<OrderController> _logger;
        private readonly IConfiguration _config;


        public OrderController(IConfiguration config,
            ILogger<OrderController> logger,
            IOrderService orderSvc,
            ICartService cartSvc,
            IIdentityService<ApplicationUser> identitySvc)
        {
            _identitySvc = identitySvc;
            _orderSvc = orderSvc;
            _cartSvc = cartSvc;
            _logger = logger;
            _config = config;
        }


        public async Task<IActionResult> Create()
        {
            var user = _identitySvc.Get(HttpContext.User);
            var cart = await _cartSvc.GetCart(user);
            var order = _cartSvc.MapCartToOrder(cart);
            ViewBag.StripePublishableKey = _config["StripePublicKey"];
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.OrderModels.Order frmOrder)
        {

            if (ModelState.IsValid)
            {
                var user = _identitySvc.Get(HttpContext.User);

                Models.OrderModels.Order order = frmOrder;

                order.UserName = user.Email;
                order.BuyerId = user.Email;

                var options = new RequestOptions
                {
                    ApiKey = _config["StripePrivateKey"]
                };
                var chargeOptions = new ChargeCreateOptions()

                {
                    //required
                    Amount = (int)(order.OrderTotal * 100),
                    Currency = "usd",
                    Source = order.StripeToken,
                    //optional
                    Description = string.Format("Order Payment {0}", order.UserName),
                    ReceiptEmail = order.UserName,

                };

                var chargeService = new ChargeService();



                Charge stripeCharge = null;
                try
                {
                    stripeCharge = chargeService.Create(chargeOptions, options);
                    _logger.LogDebug("Stripe charge object creation" + stripeCharge.StripeResponse.ToString());
                }
                catch (StripeException stripeException)
                {
                    _logger.LogError("Stripe exception " + stripeException.Message);
                    ModelState.AddModelError(string.Empty, stripeException.Message);
                    return View(frmOrder);
                }


                try
                {

                    if (stripeCharge.Id != null)
                    {
                        //_logger.LogDebug("TransferID :" + stripeCharge.Id);
                        order.PaymentAuthCode = stripeCharge.Id;

                        //_logger.LogDebug("User {userName} started order processing", user.UserName);
                        int orderId = await _orderSvc.CreateOrder(order);
                        //_logger.LogDebug("User {userName} finished order processing  of {orderId}.", order.UserName, order.OrderId);

                       // await _cartSvc.ClearCart(user);

                        return RedirectToAction("Complete", new { id = orderId, userName = user.UserName });
                    }

                    else
                    {
                        ViewData["message"] = "Payment cannot be processed, try again";
                        return View(frmOrder);
                    }

                }
                catch (BrokenCircuitException)
                {
                    ModelState.AddModelError("Error", "It was not possible to create a new order, please try later on. (Business Msg Due to Circuit-Breaker)");
                    return View(frmOrder);
                }
            }
            else
            {
                return View(frmOrder);
            }
        }


        public IActionResult Complete(int id, string userName)
        {

            _logger.LogInformation("User {userName} completed checkout on order {orderId}.", userName, id);
            return View(id);

        }

        
        public async Task<IActionResult> Detail(string orderId)
        {
            var user = _identitySvc.Get(HttpContext.User);

            var order = await _orderSvc.GetOrder(orderId);
            return View(order);
        }
        
        [Authorize]
        public async Task<IActionResult> Index()
        {

            var user = _identitySvc.Get(HttpContext.User);
            var vm = await _orderSvc.GetOrdersByUser(user);
            return View(vm);
        }






        //public async Task<IActionResult> Orders()
        //{


        //    var vm = await _orderSvc.GetOrders();
        //    return View(vm);
        //}


        private decimal GetTotal(List<Models.OrderModels.OrderItem> orderItems)
        {
            return orderItems.Select(p => p.UnitPrice * p.Units).Sum();

        }
    }
}