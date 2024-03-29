﻿using System;
using System.Collections.Generic;

namespace BaseCureAPI.DB.Models
{
    public partial class Lijekovi
    {
        public Lijekovi()
        {
            Receptis = new HashSet<Recepti>();
        }

        public int LijekId { get; set; }
        public string? NazivLijeka { get; set; }
        public bool? ZahtijevaRecept { get; set; }

        public virtual ICollection<Recepti> Receptis { get; set; }
    }
}
