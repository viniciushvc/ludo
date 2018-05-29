#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ludo.Models;

#endregion

namespace Ludo.Controllers
{
    public class HomeController : Controller
    {
        public static Jogo jogo = new Jogo();

        public IActionResult Index()
        {
            ViewData["Title"] = "Torresminha";

            ViewBag.Iniciado = jogo.jogador.Count > 0 ? true : false;

            return View();
        }

        [HttpPost]
        public void TotalJogadores(int total)
        {
            for (int i = 0; i < total; i++)
                jogo.jogador.Add(new Jogador());
        }

        [HttpGet]
        public JsonResult JogarDado()
        {
            return Json(new Random().Next(1, 7));
        }

        [HttpPost]
        public JsonResult Jogada(int jogador, int dado, int peca)
        {
            if ((dado == 1 || dado == 6) && jogo.jogador[jogador - 1].peca.Count < 4)
            {
                jogo.jogador[jogador - 1].peca.Add(new Peca());

                return Json(jogo.jogador[jogador - 1].peca);
            }
            else if (jogo.jogador[jogador - 1].peca.Count != 0)
            {
                jogo.jogador[jogador - 1].peca[peca-1].posicao += dado;

                return Json(jogo.jogador[jogador - 1].peca);
            }

            return Json(0);
        }
    }
}