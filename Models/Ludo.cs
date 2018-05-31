using System;
using System.Collections.Generic;

namespace Ludo.Models
{
    public class Jogo
    {
        public ListaPecas[] tabuleiro = new ListaPecas[81];

        public bool jogoIniciado { get; set; }

        public int vezJogador { get; set; }

        public int totalJogadores { get; set; }

        /// <summary>
        /// Joga o dado
        /// </summary>
        /// <returns></returns>
        public int JogarDado()
        {
            return new Random().Next(1, 7);
        }

        /// <summary>
        /// Cria jogadores e peças
        /// </summary>
        /// <param name="totalJogadores">número de jogadores informados</param>
        public void IniciarJogo(int totalJogadores)
        {
            //Instância lista de peças
            for (var i = 0; i < 90; i++)
            {
                tabuleiro[i] = new ListaPecas();
            }

            this.totalJogadores = totalJogadores;

            this.vezJogador = 0;

            RetirarPeca();

            MoverPeca(0, 15);

            ProximoJogador(2);

            RetirarPeca();

            MoverPeca(0, 1);
        }

        public bool PodeRetirarPeca(int dado)
        {
            return dado == 6 || dado == 1 ? true : false;
        }

        /// <summary>
        /// Retira peça do jogador atual, já verifica o ID da peça
        /// </summary>
        public void RetirarPeca()
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

        public void MoverPeca(int peca, int casas)
        {
            var posicao = PosicaoPeca(peca);

            Movimentar(posicao, posicao+casas);
        }

        public void ProximoJogador(int dado)
        {
            if (this.vezJogador >= this.totalJogadores)
                this.vezJogador = 0;

            if (!JogarNovamente(dado))
                this.vezJogador++;
        }

        public bool JogarNovamente(int dado)
        {
            return dado == 6 ? true : false;
        }

        #region Validações


        public int VerificaGanhou()
        {
            if (tabuleiro[81].pecas.Count >= 4)
                return tabuleiro[81].pecas[0].jogador;

            return -1;
        }

        #endregion

        #region Funções auxiliares

        /// <summary>
        /// Movimentar peças de uma casa para outra
        /// </summary>
        /// <param name="posicaoAtual">posição onde está as peças</param>
        /// <param name="posicaoNova">posição onde as peças irão</param>
        private void Movimentar(int posicaoAtual, int posicaoNova)
        {
            //mesma posição
            if (posicaoAtual == posicaoNova)
                return;

            var copia = tabuleiro[posicaoAtual];        

            //proximas posições
            if (posicaoNova < posicaoAtual)
                Array.Copy(tabuleiro, posicaoNova, tabuleiro, posicaoNova + 1, posicaoAtual - posicaoNova);

            //posições anteriores
            else
                Array.Copy(tabuleiro, posicaoAtual + 1, tabuleiro, posicaoAtual, posicaoNova - posicaoAtual);

            //movimentou peça
            tabuleiro[posicaoNova] = copia;

            //caso encontre outro jogador na mesma casa
            tabuleiro[posicaoAtual] = new ListaPecas();
        }

        /// <summary>
        /// Buscar o número da próxima peça do jogador;
        /// </summary>
        /// <returns></returns>
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

            return count == 0 ? 0 : count - 1;
        }

        private int PosicaoPeca(int numeroPeca)
        {
            var count = 0;

            foreach (var casa in tabuleiro)
            {
                foreach (var peca in casa.pecas)
                {
                    if (peca.jogador == this.vezJogador && peca.numeroPeca == numeroPeca)
                        return count;

                    count++;
                }

                count++;
            }

            return -1;
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