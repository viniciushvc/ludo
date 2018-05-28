using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ludo.Models;

namespace Ludo.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            ViewData["Title"] = "Torresminha";

            return View();

            Session["jogo"] = new Jogo();
        }

        [HttpPost]
        public JsonResult TotalJogador(int total)
        {
            return Json(total);
        }

        [HttpGet]
        public JsonResult JogarDado()
        {

            return Json(new Random().Next(1, 7));
        }

        [HttpPost]
        public JsonResult TotalJogadores()
        {

            jogo.totalJogadores++;

            return Json(jogo.totalJogadores);
        }
    }
}