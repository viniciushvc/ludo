using System;
using System.Collections.Generic;

namespace Ludo.Models
{
    public class Tabuleiro
    {
        public ListaPecas[] tabuleiro = new ListaPecas[80];

        public int vezJogador { get; set; }

        public int totalJogadores { get; set; }

        #region Validações

        /// <summary>
        /// Verifica se o jogador pode jogar novamente
        /// </summary>
        /// <param name="dado"></param>
        /// <returns>true = pode jogar novamente - false = não pode jogar novamente</returns>
        public bool JogarNovamente(int dado)
        {
            return dado == 6 ? true : false;
        }

        /// <summary>
        /// Verifica quantidade de peças que o jogador possui
        /// </summary>
        /// <returns>quantidade de peças do jogador</returns>
        public int PecasJogador()
        {
            var count = 0;

            foreach (var casa in tabuleiro)
            {
                foreach (var peca in casa.pecas)
                {
                    if (peca.jogador == this.vezJogador)
                        count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Verifica se o jogador possui peças no campo
        /// </summary>
        /// <returns>true = tem peça - false = não tem peça</returns>
        public bool PossuiPeca()
        {
            var count = 0;

            foreach (var casa in tabuleiro)
            {
                foreach (var peca in casa.pecas)
                {
                    if (peca.jogador == this.vezJogador)
                        count++;
                }
            }

            return count == 0 ? false : true;
        }

        /// <summary>
        /// Verifica se jogador chegou com as 4 peças no final
        /// </summary>
        /// <returns>Jogador vencedor</returns>
        public string VerificaGanhou()
        {
            if (tabuleiro[61].pecas.Count >= 4)
                return "Verde ganhou";

            if (tabuleiro[67].pecas.Count >= 4)
                return "Amarelo ganhou";

            if (tabuleiro[73].pecas.Count >= 4)
                return "Azul ganhou";

            if (tabuleiro[79].pecas.Count >= 4)
                return "Vermelho ganhou";

            return null;
        }

        /// <summary>
        /// Retorna ao começo do tabuleiro
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>nova posição da peça</returns>
        public int RetornaComeco(int pos)
        {
            var posicao = pos;

            if (pos > 55 && pos < 62 && this.vezJogador != 0)
                posicao = pos - 56;

            return posicao;
        }

        /// <summary>
        /// Valida posição do usuário
        /// </summary>
        /// <param name="posicaoAtual">atual</param>
        /// <param name="proximaPosicao">proxima</param>
        /// <returns></returns>
        public bool VerificaEntrar(int posicaoAtual, int proximaPosicao)
        {
            if (this.vezJogador == 0 && proximaPosicao > 55)
                return true;

            else if (this.vezJogador == 1 && proximaPosicao > 13 && posicaoAtual <= 13 || proximaPosicao > 62)
                return true;

            else if (this.vezJogador == 2 && proximaPosicao > 27 && posicaoAtual <= 27 || proximaPosicao > 62)
                return true;

            else if (this.vezJogador == 3 && proximaPosicao > 41 && posicaoAtual <= 41 || proximaPosicao > 62)
                return true;

            return false;
        }

        #endregion
    }
}