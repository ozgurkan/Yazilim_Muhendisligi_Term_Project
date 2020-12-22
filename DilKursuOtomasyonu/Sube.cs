using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DilKursuOtomasyonu
{

    class Sube
    {
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private String sql = null;
        int subekodu;
        string subeadi;
        string adres;
        int telefon;
        public Sube(int subekodu, string subeadi, string adres, int telefon) //Kurucu Metot
        {
            this.subekodu = subekodu;
            this.subeadi = subeadi;
            this.adres = adres;
            this.telefon = telefon;
            conn = new NpgsqlConnection("Server = localhost; Port = 5432;Database=dilkursu; User Id=postgres;Password=the.dog2");
        }

        public int SubeEKle() //Şube Ekleme
        {
            try
            {
                conn.Open();
                string sql = "SELECT * FROM cities ";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //comboBox1.Items.Add(reader[2].ToString());
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                // something went wrong, and you wanna know why
                //MessageBox.Show("Error:" + ex.ToString(), "something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }


            //int secilmisil = comboBox1.SelectedIndex + 1;
            try
            {
                conn.Open();
                string sql = "SELECT * FROM counties where cityid= " + subeadi;// + secilmisil;
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        //comboBox2.Items.Add(reader[2].ToString());
                    }
                }
                //MessageBox.Show(yetki1.ToString());



                conn.Close();

            }
            catch (Exception ex)
            {
                // something went wrong, and you wanna know why
                //MessageBox.Show("Error:" + ex.ToString(), "something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }

            return 1;
            return 1;
        }

        public int SubeGuncelle(int subekodu) //Şube Güncelle
        {
            return 1;

        }

        public int SubeSil(int subekodu) //Şube Sil
        {
         return 1;
        }

        public void SubeListele() //Bakiye Öğren
         {

         }
        }
}

