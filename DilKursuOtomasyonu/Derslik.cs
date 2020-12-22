using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilKursuOtomasyonu
{
    class Derslik
    {
        int derslikKodu;
        string derslikAdi;
        int kapasite;
        int doluzaman;
        int boszaman;
        Sube sube;

        public Derslik(int derslikKodu, string derslikAdi, int kapasite, int doluzaman, int boszaman, Sube sube)
        {
            this.derslikKodu = derslikKodu;
            this.derslikAdi = derslikAdi;
            this.kapasite = kapasite;
            this.doluzaman = doluzaman;
            this.boszaman = boszaman;
            this.sube = sube;
        }

        public void DerslikEkle()
        {

        }
    }

}
