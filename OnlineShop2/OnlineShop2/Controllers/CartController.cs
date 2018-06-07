using Model.Dao;
using Model.EF;
using OnlineShop2.Common;
using OnlineShop2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop2.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var listItem = new List<CartItem>();
            if (cart != null)
            {
                listItem = (List<CartItem>)cart;
            }
            return View(listItem);
        }

        public ActionResult AddItem(long productId, int quantity)
        {
            var cart = Session[CommonConstants.CartSession];
            var listItem = new List<CartItem>();
            var product = new ProductDao().Detail(productId);
            if (cart != null)
            {
                listItem = (List<CartItem>)cart;
                if (listItem.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in listItem)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    listItem.Add(item);
                }
                Session[CommonConstants.CartSession] = listItem;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                listItem.Add(item);
                Session[CommonConstants.CartSession] = listItem;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            foreach(var item in sessionCart)
            {
                var cartItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if(cartItem != null)
                {
                    item.Quantity = cartItem.Quantity;
                }
            }
            return Json(new {
                success = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                success = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            return Json(new
            {
                success = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CommonConstants.CartSession];
            var listItem = new List<CartItem>();
            if (cart != null)
            {
                listItem = (List<CartItem>)cart;
            }
            return View(listItem);
        }

        [HttpPost]
        public ActionResult Payment(string name, string phone, string address, string email)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var order = new Order();
                    order.CreatedDate = DateTime.Now;
                    order.ShipName = name;
                    order.ShipMobile = phone;
                    order.ShipAddress = address;
                    order.ShipEmail = email;
                    long orderId = new OrderDao().Insert(order);
                    var cart = (List<CartItem>)Session[CommonConstants.CartSession];
                    foreach (var item in cart)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.ProductID = item.Product.ID;
                        orderDetail.OrderID = orderId;
                        orderDetail.Price = item.Product.Price;
                        orderDetail.Quantity = item.Quantity;
                        new OrderDetailDao().Insert(orderDetail);
                    }
                    return Redirect("/complete");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return View("Payment");
        }

        public ActionResult Complete()
        {
            var cart = Session[CommonConstants.CartSession];
            var listItem = new List<CartItem>();
            if (cart != null)
            {
                listItem = (List<CartItem>)cart;
            }
            return View(listItem);
        }
    }
}