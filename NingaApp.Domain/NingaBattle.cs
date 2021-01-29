using System;
using System.Collections.Generic;
using System.Text;

namespace NingaApp.Domain
{
    public class NingaBattle
    {
        public int BattleId { get; set; }
        public Battle Battle { get; set; }


        public int NingaId { get; set; }
        public Ninga Ninga { get; set; }
    }
}
