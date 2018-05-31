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
            //Previne atualizar página e perguntar quantidade de jogadores
            ViewBag.Iniciado = jogo.jogoIniciado;

            return View();
        }

        #region Get

        [HttpGet]
        public JsonResult JogarDado()
        {
            return Json(jogo.JogarDado());
        }

        #endregion

        #region Post

        /// <summary>
        /// Adiciona jogadores a partida, min 2 - máx 4
        /// </summary>
        /// <param name="total">Total de jogadores informado pelo usuário</param>
        [HttpPost]
        public bool TotalJogadores(int total)
        {
            if (total > 1 && total < 5)
            {
                jogo.IniciarJogo(total);

                return true;
            }

            return false;
        }

        //[HttpPost]
        //public JsonResult MoverPeca(int jogador, int dado, int peca)
        //{
        //    if (jogo.jogador[jogador].peca.Count != 0)
        //    {
        //        jogo.jogador[jogador].peca[peca].posicao += dado;

        //        return Json(jogo.jogador[jogador].peca);
        //    }

        //    return Json(0);
        //}

        //[HttpPost]
        //public JsonResult RetirarPeca(int jogador)
        //{
        //    jogo.jogador[jogador].peca.Add(new Peca());

        //    var peca = jogo.jogador[jogador].peca.Count;

        //    //Retirar 1, espera um vetor
        //    return Json(peca-1);
        //}

        #endregion


    }
}