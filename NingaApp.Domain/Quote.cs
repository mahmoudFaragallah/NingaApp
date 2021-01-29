using System;
using System.Collections.Generic;
using System.Text;

namespace NingaApp.Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        
        public int NingaId { get; set; }
        public Ninga Ninga { get; set; }
    }
}
