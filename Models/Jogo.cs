using System;
using System.Collections.Generic;

namespace Ludo.Models
{
    public class Jogo : Tabuleiro
    {
        public bool jogoIniciado { get; set; }

        #region Inicia game

        /// <summary>
        /// Cria jogadores e pe�as
        /// </summary>
        /// <param name="totalJogadores">n�mero de jogadores informados</param>
        public void IniciarJogo(int totalJogadores)
        {
            //Inst�ncia lista de pe�as
            for (var i = 0; i < tabuleiro.Length; i++)
                tabuleiro[i] = new ListaPecas();

            this.totalJogadores = totalJogadores;

            this.vezJogador = 0;

            this.jogoIniciado = true;
        }

        public bool PodeRetirarPeca(int dado)
        {
            return dado == 6 || dado == 1 ? true : false;
        }

        #endregion

        /// <summary>
        /// Joga o dado
        /// </summary>
        /// <returns></returns>
        public int JogarDado()
        {
            return new Random().Next(1, 7);
        }

        /// <summary>
        /// Retira pe�a do jogador atual, j� verifica o ID da pe�a
        /// </summary>
        public void RetirarPeca()
        {
            if (PecasJogador() < 4)
            {
                var posicao = 0;

                if (this.vezJogador == 0)
                    posicao = 0;

                if (this.vezJogador == 1)
                    posicao = 14;

                if (this.vezJogador == 2)
                    posicao = 28;

                if (this.vezJogador == 3)
                    posicao = 42;

                tabuleiro[posicao].pecas.Add(new Peca() { jogador = this.vezJogador, numeroPeca = numeroProxPeca() });
            }
        }

        /// <summary>
        /// Movimentar pe�a
        /// </summary>
        /// <param name="peca">Pe�a que o usu�rio selecionou</param>
        /// <param name="casas">Valor recebido do dado</param>
        public void MoverPeca(int peca, int casas)
        {
            var posicao = PosicaoPeca(peca);

            var novaPosicao = 0;

            if (VerificaEntrar(posicao, posicao + casas))
                novaPosicao = PodeEntrar(posicao, posicao + casas);
            else
                novaPosicao = RetornaComeco(posicao + casas);

            //Posi��es que j� chegaram
            if (posicao != 61 && posicao != 67 && posicao != 73 && posicao != 79)
                Movimentar(posicao, novaPosicao);
        }

        /// <summary>
        /// Verifica se a pe�a pode entrar
        /// </summary>
        /// <param name="posicao">posi��o em que o dado estar�</param>
        /// <returns>Nova posi��o da pe�a</returns>
        private int PodeEntrar(int posicaoAtual, int proximaPosicao)
        {
            var posicao = proximaPosicao;

            if (this.vezJogador == 0)
            {
                if (posicao > 61)
                    posicao = 61 - (posicao - 61);
            }

            if (this.vezJogador == 1)
            {
                if (proximaPosicao < 60)
                    posicao = proximaPosicao - posicaoAtual + 62;
                else
                    posicao = posicaoAtual + (proximaPosicao - posicaoAtual);

                if (posicao > 67)
                    posicao = 67 - (posicao - 67);
            }

            else if (this.vezJogador == 2)
            {
                if (proximaPosicao < 60)
                    posicao = proximaPosicao - posicaoAtual + 68;
                else
                    posicao = posicaoAtual + (proximaPosicao - posicaoAtual);

                if (posicao > 73)
                    posicao = 73 - (posicao - 73);
            }

            else if (this.vezJogador == 3)
            {
                if (proximaPosicao < 60)
                    posicao = proximaPosicao - posicaoAtual + 74;
                else
                    posicao = posicaoAtual + (proximaPosicao - posicaoAtual);

                if (posicao > 79)
                    posicao = 79 - (posicao - 79);
            }

            return posicao;
        }

        /// <summary>
        /// Passa a vez do jogador
        /// </summary>
        /// <param name="dado">verificar se jogador pode jogar novamente</param>
        public void ProximoJogador(int dado)
        {
            this.vezJogador++;

            if (this.vezJogador >= this.totalJogadores)
                this.vezJogador = 0;
        }

        #region Fun��es auxiliares

        /// <summary>
        /// Movimentar pe�as de uma casa para outra
        /// </summary>
        /// <param name="posicaoAtual">posi��o onde est� as pe�as</param>
        /// <param name="posicaoNova">posi��o onde as pe�as ir�o</param>
        private void Movimentar(int posicaoAtual, int posicaoNova)
        {
            //caso 80+, n�o achou a pe�a
            if (posicaoAtual < 80)
            {
                var copia = tabuleiro[posicaoAtual];

                if (tabuleiro[posicaoNova].pecas.Count > 0)
                {
                    if (tabuleiro[posicaoNova].pecas[0].jogador == this.vezJogador)
                    {
                        foreach (var peca in tabuleiro[posicaoAtual].pecas)
                        {
                            tabuleiro[posicaoNova].pecas.Add(peca);
                        }
                    }
                    else
                        tabuleiro[posicaoNova] = copia;
                }
                else
                    tabuleiro[posicaoNova] = copia;

                tabuleiro[posicaoAtual] = new ListaPecas();

                ReordenarPecas();
            }
        }

        /// <summary>
        /// Buscar o n�mero da pr�xima pe�a do jogador;
        /// </summary>
        /// <returns>id da proxima pe�a</returns>
        private int numeroProxPeca()
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
        /// Previne erro de localiza��o da pe�a quando o usu�rio perde alguma
        /// </summary>
        private void ReordenarPecas()
        {
            var count = 0;

            foreach (var casa in tabuleiro)
            {
                foreach (var peca in casa.pecas)
                {
                    if (peca.jogador == this.vezJogador)
                    {
                        peca.numeroPeca = count;

                        count++;
                    }
                }
            }
        }

        /// <summary>
        /// Encontra a posi��o da pe�a no tabuleiro
        /// </summary>
        /// <param name="numeroPeca">identifica��o da pe�a</param>
        /// <returns>posi��o da pe�a</returns>
        private int PosicaoPeca(int numeroPeca)
        {
            var count = 0;

            foreach (var casa in tabuleiro)
            {
                foreach (var peca in casa.pecas)
                {
                    if (peca.jogador == this.vezJogador && peca.numeroPeca == numeroPeca)
                        return count;
                }

                count++;
            }

            return count;
        }

        #endregion
    }

    public class ListaPecas
    {
        public List<Peca> pecas = new List<Peca>();
    }
}