using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okul
{
    interface IPersonel
    {
        public int KimlikNo { get; set; }
        public Enumlar.Gorevler Gorevi { get; set; }
        
    }
}
