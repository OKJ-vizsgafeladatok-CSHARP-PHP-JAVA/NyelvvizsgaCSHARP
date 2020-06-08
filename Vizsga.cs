using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyelvvizsgaCSHARP
{
    class Vizsga
    {
        public string nyelv { get; set; }
        public List<int> evek { get; set; }

        public Vizsga(string nyelv, List<int> evek)
        {
            this.nyelv = nyelv;
            this.evek = evek;
        }

        public int sumAllYear()
        {
            int all=0;
            foreach (var item in evek)
            {
                all += item;
            }
            return all;
        }
    }
}
