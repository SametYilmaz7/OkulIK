using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okul
{
    interface IKisiselBilgiler
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime Dogumtarihi { get; set; }
    }
}
