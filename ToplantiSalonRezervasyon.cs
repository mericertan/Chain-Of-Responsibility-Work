using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public abstract class ToplantiSalonRezervasyon
    {
        //Zincirin bir üst halkası
        public ToplantiSalonRezervasyon BirSonrakiSorumlu { get; set; }

        //kriteri yakalayıcı
        private EventHandler<AramaKriteri> aramaKriteriHandler;
        //kriter yakalandığında çalışacak metot
        protected abstract void ara(object sender, AramaKriteri kriter);
        public ToplantiSalonRezervasyon()
        {
            //ara metodunu delege'ye aktar:
            aramaKriteriHandler += ara;
        }

        public void UygunSalonlariAra(AramaKriteri kriter)
        {
            aramaKriteriHandler(this, kriter);
        }
    }

}
