using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //1. Halka
    public class AlmanyaRezervasyon : ToplantiSalonRezervasyon
    {
        protected override void ara(object sender, AramaKriteri kriter)
        {
            if (kriter.Ulke == "Almanya")
            {
                Console.WriteLine("Almanya için uygun salonlar aranıyor");
            }
            else
            {
                //birsonrakiSorumlu boş değilse
                BirSonrakiSorumlu?.UygunSalonlariAra(kriter);
            }
        }
    }
    //2. Halka
    public class BelcikaRezervasyon : ToplantiSalonRezervasyon
    {
        protected override void ara(object sender, AramaKriteri kriter)
        {
            if (kriter.Ulke == "Belçika")
            {
                Console.WriteLine("Belçika için uygun salonlar aranıyor");
            }
            else
            {
                BirSonrakiSorumlu?.UygunSalonlariAra(kriter);
            }
        }
    }
    //3. Halka
    public class TurkiyeRezervasyon : ToplantiSalonRezervasyon
    {
        protected override void ara(object sender, AramaKriteri kriter)
        {
            if (kriter.Ulke == "Türkiye")
            {
                Console.WriteLine("Türkiye için uygun salonlar aranıyor");
            }
        }
    }
}
