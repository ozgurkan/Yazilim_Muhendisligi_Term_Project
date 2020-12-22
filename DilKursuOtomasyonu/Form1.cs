using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DilKursuOtomasyonu
{
    public partial class Form1 : Form
    {
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private String sql = null;
        public Form1()
        {
            InitializeComponent();
            conn = new NpgsqlConnection("Server = localhost; Port = 5432;Database=dilkursu; User Id=postgres;Password=123");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from yonetim where kullanıcıadı='" + textBox1.Text+"' and parola='"+textBox2.Text+"'";
                cmd = new NpgsqlCommand(sql, conn);
                object temp = cmd.ExecuteScalar();
                int result;
                if ((temp == null) || (temp == DBNull.Value))
                    result = 0;
                else
                {
                    result = 1;
                }                         
                if (result == 1)
                {
                    MessageBox.Show("Giriş Başarılı");
                    string sql = "SELECT yetki FROM yonetim WHERE kullanıcıadı='" + textBox1.Text + "'";
                    using (NpgsqlCommand command = new NpgsqlCommand(sql, conn))
                    {
                        int val;
                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            val = Int32.Parse(reader[0].ToString());
                            //do whatever you like
                            this.Hide();
                            if (val == 0)
                            {
                                new frmAdmin(val).Show();
                            }
                            else
                            {
                                new frmPersonel(val).Show();
                            }

                        }
                    }
                    //MessageBox.Show(yetki1.ToString());

                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Parola hatalı girilmiştir.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show("Error:" + ex.ToString(), "something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }
    }
}
