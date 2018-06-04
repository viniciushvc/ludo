using System;
using System.Collections.Generic;

namespace Ludo.Models
{
    public class Jogo
    {
        public ListaPecas[] tabuleiro = new ListaPecas[80];

        public bool jogoIniciado { get; set; }

        public int vezJogador { get; set; }

        public int totalJogadores { get; set; }

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

                tabuleiro[posicao].pecas.Add(new Peca() { jogador = this.vezJogador, numeroPeca = numeroProxPeca(), jaSaiu = false, chegou = false });
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
        public int PodeEntrar(int posicaoAtual, int proximaPosicao)
        {
            var posicao = proximaPosicao;

            if (this.vezJogador == 0)
            {
                if (posicao > 61)
                {
                    posicao = 61 - (posicao - 61);
                }
            }

            if (this.vezJogador == 1)
            {
                posicao = proximaPosicao - posicaoAtual + 62;

                if (posicao > 67)
                {
                    posicao = 67 - (posicao - 67);
                }
            }

            else if (this.vezJogador == 2)
            {
                posicao = proximaPosicao - posicaoAtual + 68;

                if (posicao > 73)
                {
                    posicao = 73 - (posicao - 73);
                }
            }

            else if (this.vezJogador == 3)
            {
                posicao = proximaPosicao - posicaoAtual + 74;

                if (posicao > 79)
                {
                    posicao = 79 - (posicao - 79);
                }
            }

            return posicao;
        }

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

        /// <summary>
        /// Retorna ao come�o do tabuleiro
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>nova posi��o da pe�a</returns>
        public int RetornaComeco(int pos)
        {
            var posicao = pos;

            if (pos > 55 && pos < 62 && this.vezJogador != 0)
                posicao = pos - 56;

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

        #region Valida��es

        /// <summary>
        /// Verifica se o jogador pode jogar novamente
        /// </summary>
        /// <param name="dado"></param>
        /// <returns>true = pode jogar novamente - false = n�o pode jogar novamente</returns>
        public bool JogarNovamente(int dado)
        {
            return dado == 6 ? true : false;
        }

        /// <summary>
        /// Verifica quantidade de pe�as que o jogador possui
        /// </summary>
        /// <returns>quantidade de pe�as do jogador</returns>
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
        /// Verifica se o jogador possui pe�as no campo
        /// </summary>
        /// <returns>true = tem pe�a - false = n�o tem pe�a</returns>
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
        /// Verifica se jogador chegou com as 4 pe�as no final
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

        #endregion

        #region Fun��es auxiliares

        /// <summary>
        /// Movimentar pe�as de uma casa para outra
        /// </summary>
        /// <param name="posicaoAtual">posi��o onde est� as pe�as</param>
        /// <param name="posicaoNova">posi��o onde as pe�as ir�o</param>
        private void Movimentar(int posicaoAtual, int posicaoNova)
        {
            //mesma posi��o
            if (posicaoAtual == posicaoNova)
                return;

            var copia = tabuleiro[posicaoAtual];

            if (tabuleiro[posicaoNova].pecas.Count > 0)
            {
                if (tabuleiro[posicaoNova].pecas[0].jogador == this.vezJogador)
                {
                    foreach(var peca in tabuleiro[posicaoAtual].pecas)
                    {
                        tabuleiro[posicaoNova].pecas.Add(peca);
                    }
                }
            }
            else
            {
                tabuleiro[posicaoNova] = copia;
            }

            tabuleiro[posicaoAtual] = new ListaPecas();

            //proximas posi��es
            /* if (posicaoNova < posicaoAtual)
                 Array.Copy(tabuleiro, posicaoNova, tabuleiro, posicaoNova + 1, posicaoAtual - posicaoNova);

             //posi��es anteriores
             else
                 Array.Copy(tabuleiro, posicaoAtual + 1, tabuleiro, posicaoAtual, posicaoNova - posicaoAtual);

             //movimentou pe�a
             tabuleiro[posicaoNova] = copia*/
            ;
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

    public class Peca
    {
        public int jogador { get; set; }

        public int numeroPeca { get; set; }

        public bool jaSaiu { get; set; }

        public bool chegou { get; set; }
    }
}