using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatitle}-{cateId}",
                defaults: new { controller = "Product", action = "CategoryDetail", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
                name: "Add item to cart",
                url: "add-to-cart",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "cart",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
                name: "Payment",
                url: "payment",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
                name: "Order Complete",
                url: "complete",
                defaults: new { controller = "Cart", action = "Complete", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
               name: "Contact",
               url: "contact",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
               name: "Register",
               url: "register",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
               name: "News",
               url: "news",
               defaults: new { controller = "Content", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop2.Controllers" }
           );

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
               name: "Login",
               url: "login",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
               name: "Search",
               url: "search",
               defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop2.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop2.Controllers" }
            );
        }
    }
}
