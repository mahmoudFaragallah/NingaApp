﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NingaApp.Domain
{
    public class Ninga
    {
        public Ninga()
        {
            Quotes = new List<Quote>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Quote> Quotes { get; set; }

        // public int BattleId { get; set; }
        public List<NingaBattle>  NingaBattles { get; set; }
        public SecretIdentity  SecretIdentity { get; set; }
    }
}
