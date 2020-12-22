using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilKursuOtomasyonu
{
    class Dil
    {
        int dilid;
        string diladi;

        public Dil(int dilid,string diladi)
        {
            this.dilid = dilid;
            this.diladi = diladi;
        }

        public void DilEkle(string diladi)
        {
            this.diladi = diladi;
        }
        public void DilSil(string diladi)
        {

        }
        public void DilGuncelle()
        {

        }
        public void DilListele()
        {

        }
    }
}
