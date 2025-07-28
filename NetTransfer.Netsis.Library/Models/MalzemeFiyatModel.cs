﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTransfer.Netsis.Library.Models
{
    public class MalzemeFiyatModel
    {
        public string kod { get; set; }
        public string kod_baslik { get; set; }
        public string aciklama { get; set; }
        public int tarih_aralik_durum { get; set; }
        public DateTime baslangic_tarihi { get; set; }
        public DateTime bitis_tarihi { get; set; }
        public bool durum { get; set; }
        public string urun_kodu { get; set; }
        public string doviz_kodu { get; set; }
        public double liste_fiyati { get; set; }
    }
}
