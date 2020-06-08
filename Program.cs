using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyelvvizsgaCSHARP
{
    class Program
    {
        public static List<Vizsga> sikeres = beolvas("sikeres.csv");
        public static List<Vizsga> sikertelen = beolvas("sikertelen.csv");

        public static List<Vizsga> beolvas(string file)
        {
            List<Vizsga> list = new List<Vizsga>();
            using (StreamReader sr = new StreamReader(new FileStream(file, FileMode.Open), Encoding.UTF8))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var split = sr.ReadLine().Split(';');
                    List<int> evek = new List<int>();
                    for (int i = 1; i < split.Length; i++)
                    {
                        evek.Add(Convert.ToInt32(split[i]));
                    }
                    Vizsga o = new Vizsga(split[0],evek);
                    list.Add(o);
                }
            }
            return list;
        }

        static void Main(string[] args)
        {
            #region 2. feladat
            Console.WriteLine("2. feladat: A legnépszerűbb nyelvek: ");
            Dictionary<string, int> c= new Dictionary<string, int>();
            foreach (var siker in sikeres)
            {
                foreach (var sikert in sikertelen)
                {
                    if (sikert.nyelv.Equals(siker.nyelv))
                    {
                        var sum = siker.sumAllYear() + sikert.sumAllYear();
                        c.Add(sikert.nyelv,sum);
                    }
                }
            }
            var cOrd = c.OrderByDescending(x=>x.Value);
            int szamlalo = 0;
            foreach (var item in cOrd)
            {
                szamlalo++;
                if (szamlalo<4)
                {
                    Console.WriteLine("\t"+item.Key);
                }
            }
            #endregion

            #region 3. feladat
            Console.WriteLine("3. feladat: ");
            var beker = 0;
            do
            {
                Console.Write("\tVizsgálandó év: ");
                beker = Convert.ToInt32(Console.ReadLine());
            } while (beker<2009 || beker>2017);
            #endregion

            #region 4. feladat
            Console.WriteLine("4. feladat:");
            double arany = 0.0;
            double maxArany = 0.0;
            string maxAlany = "";
            List<string> nullasok = new List<string>();
            for (int i = 0; i < sikeres.Count(); i++)
            {
                for (int j = 0; j < sikertelen.Count(); j++)
                {
                    if (sikeres[i].nyelv.Equals(sikertelen[j].nyelv))
                    {
                        if (sikeres[i].evek[beker-2009]+sikertelen[j].evek[beker-2009]==0)
                        {
                            arany = 0.0;
                            nullasok.Add(sikeres[i].nyelv);
                        }
                        arany = sikertelen[j].evek[beker - 2009] / (double)(sikeres[i].evek[beker-2009]+sikertelen[j].evek[beker-2009])*100;
                        if (arany>maxArany)
                        {
                            maxArany = arany;
                            maxAlany = sikeres[i].nyelv;
                        }
                        arany = 0.0;
                    }
                }
            }

            var ar = Math.Round(maxArany, 2);
            Console.WriteLine($"\t{beker}-ben {maxAlany} nyelvből a sikertelen vizsgák aránya: {ar}%");
            #endregion

            #region 5. feladat
            Console.WriteLine("5. feladat: ");
            if (nullasok.Count()==0)
            {
                Console.WriteLine("\tMinden nyelvől volt vizsgázó");
            }
            else
            {
                foreach (var it in nullasok)
                {
                    Console.WriteLine("\t"+it);
                }
            }
            #endregion

            #region 6. feladat
            Console.WriteLine("6. feladat: osszesites.csv");
            using (StreamWriter sw=new StreamWriter(new FileStream("osszesites.csv",FileMode.Create),Encoding.UTF8))
            {
                foreach (var siker in sikeres)
                {
                    arany = 0.0;
                    foreach (var sikert in sikertelen)
                    {
                        if (siker.nyelv.Equals(sikert.nyelv))
                        {
                            arany = siker.sumAllYear() / ((double)sikert.sumAllYear() + siker.sumAllYear());
                            arany = Math.Round((arany*100),2);
                            sw.WriteLine(siker.nyelv+";"+(siker.sumAllYear()+sikert.sumAllYear())+";"+arany+"%");
                        }
                    }
                }
            }
                #endregion
                Console.ReadKey();
        }
    }
}
