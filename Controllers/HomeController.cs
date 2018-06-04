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
        public JsonResult JogarDado(bool primeiraVez)
        {
            var dado = jogo.JogarDado();

            if (!jogo.JogarNovamente(dado) && !primeiraVez)
                jogo.ProximoJogador(dado);

            return Json(dado);
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

        [HttpPost]
        public JsonResult MoverPeca(int dado, int peca)
        {
            jogo.MoverPeca(peca, dado);

            return Json(jogo.tabuleiro);
        }

        [HttpGet]
        public JsonResult VezJogador()
        {
            var corJogador = new string[] { "Verde", "Amarelo", "Azul", "Vermelho" };

            return Json(corJogador[jogo.vezJogador]);
        }

        [HttpGet]
        public JsonResult PossuiPeca()
        {
            return Json(jogo.PossuiPeca());
        }

        [HttpPost]
        public JsonResult RetirarPeca(int dado)
        {
            if (jogo.PodeRetirarPeca(dado))
                jogo.RetirarPeca();

            return Json(jogo.tabuleiro);
        }


        [HttpGet]
        public JsonResult VerificaGanhou()
        {
            var ganhador = jogo.VerificaGanhou();

            return Json(ganhador);
        }

        #endregion

    }
}