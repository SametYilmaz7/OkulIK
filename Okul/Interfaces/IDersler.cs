using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okul
{
    interface IDersler
    {
        public string DersAdi { get; set; }
        public string Aciklama { get; set; }
        public string OgretimGorevlisi { get; set; }

    }
}
