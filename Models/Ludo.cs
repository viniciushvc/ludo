using System;
using System.Collections.Generic;

namespace Ludo.Models
{
    public class Jogo
    {
        public List<Jogador> jogador = new List<Jogador>();
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