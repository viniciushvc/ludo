using System;
using System.Collections.Generic;

namespace Ludo.Models
{
    public class Jogo
    {
        public List<Jogador> jogador = new List<Jogador>();

        public void NovoJogador()
        {
            jogador.Add(new Jogador());
        }

        public int JogarDado()
        {
            return new Random().Next(1, 7);
        }
    }

    public class Jogador
    {
        public List<Peca> peca = new List<Peca>();
    }

    public class Peca
    {
        public int posicao { get; set; }

        public Peca()
        {
            posicao = 1;
        }
    }
}