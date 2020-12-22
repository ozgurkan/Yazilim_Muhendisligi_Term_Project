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
    public partial class frmPersonel : Form
    {
        private int id;
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private String sql = null;
        public frmPersonel(int val)
        {
            InitializeComponent();
            this.id = id;
            conn = new NpgsqlConnection("Server = localhost; Port = 5432;Database=dilkursu; User Id=postgres;Password=123");
        }

        private void frmPersonel_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            panel2.Visible = false;
            panel3.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lütfen tc kimlik no giriniz.");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Lütfen isim giriniz.");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Lütfen soyisim giriniz.");
            }
            else if (maskedTextBox1.Text == "")
            {
                MessageBox.Show("Lütfen ev telefonu giriniz.");
            }
            else if (maskedTextBox2.Text == "")
            {
                MessageBox.Show("Lütfen cep telefonu giriniz.");
            }
            else
            {
                try
                {
                    conn.Open();
                    sql = @"select * from ogrenciler where tcno=" + Convert.ToInt64(textBox1.Text);
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
                        MessageBox.Show("Böyle bir öğrenci sistemde kayıtlıdır.");

                    }
                    else
                    {

                        NpgsqlCommand command2 = new NpgsqlCommand("insert into ogrenciler (tcno,adi,soyadi,evtel,ceptel)values(@tcno,@adi,@soyadi,@evtel,@ceptel)", conn);
                        command2.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox1.Text));
                        command2.Parameters.AddWithValue("@adi", textBox2.Text);
                        command2.Parameters.AddWithValue("@soyadi", textBox3.Text);
                        command2.Parameters.AddWithValue("@evtel", maskedTextBox1.Text);
                        command2.Parameters.AddWithValue("@ceptel", maskedTextBox2.Text);
                        command2.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Öğrenci Sisteme Eklendi.");
                        panel3.Visible = false;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        maskedTextBox1.Text = "";
                        maskedTextBox2.Text = "";

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

        private void button3_Click(object sender, EventArgs e)
        {
            
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
           

            comboBox1.Text="";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            checkBox1.Checked = false;
            panel2.Visible = true;
            panel3.Visible = false;

            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";
            checkBox1.Checked = false;


            try
            {
                conn.Open();
                string sql = "SELECT * FROM ogrenciler ";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0].ToString());
                    }
                }

                conn.Close();

            }
            catch (Exception ex)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show("Error:" + ex.ToString(), "something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }

            //////////////////////////////
            ///

            try
            {
                conn.Open();
                string sql = "select distinct(il) from subeler";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox4.Items.Add(reader[0].ToString());
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show("Error:" + ex.ToString(), "something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }


            //////////////////////



            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();
            comboBox6.Text = "";            
            try
            {
                conn.Open();
                string sql = "select kursid from kurslar where subekodu="+Convert.ToInt32(comboBox3.Text)+" and dilid in (select dilid from diller where diladi = '"+comboBox2.Text+"' )";

                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox6.Items.Add(reader[0].ToString());

                    }
                }
                //MessageBox.Show(yetki1.ToString());
                conn.Close();

            }
            catch (Exception ex)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show("Error:" + ex.ToString(), "something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            int secilmisdil = comboBox2.SelectedIndex + 1;
            try
            {
                conn.Open();
                string sql = "select  distinct(subekodu) from subeler where il='"+comboBox4.Text+"' AND ilce='"+comboBox5.Text+"' AND subekodu in(select subekodu from ogretmenler where tcno in(select tcno from ogretmendiller where dilid in(select dilid from diller where diladi = '"+comboBox2.Text+"')))";

                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader[0].ToString());

                    }
                }
                //MessageBox.Show(yetki1.ToString());
                conn.Close();

            }
            catch (Exception ex)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show("Error:" + ex.ToString(), "something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            comboBox5.Text = "";
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            int secilmisil = comboBox4.SelectedIndex + 1;
            try
            {
                conn.Open();
                string sql = "select distinct(ilce) from subeler where ilce in (select countyname from counties where cityid in (select cityid from cities where cityname = '"+comboBox4.Text+"'))" ;
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox5.Items.Add(reader[0].ToString());
                    }
                }
                //MessageBox.Show(yetki1.ToString());



                conn.Close();

            }
            catch (Exception ex)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show("Error:" + ex.ToString(), "something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            try
            {
                conn.Open();
                string sql = "select distinct(diladi) from diller where dilid in(select dilid from ogretmendiller where tcno in(select tcno from ogretmenler where subekodu in(select subekodu from subeler where il = '" + comboBox4.Text + "' AND ilce = '" + comboBox5.Text + "')))";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader[0].ToString());
                    }
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

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label18.Text = "";

            try
            {
                conn.Open();
                string sql = "select * from kurslar where kursid="+Convert.ToInt32(comboBox6.Text);
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        label13.Text = reader[2].ToString();
                        label14.Text = reader[3].ToString();
                        label15.Text = reader[4].ToString();
                        label16.Text = reader[5].ToString();
                        label17.Text = reader[8].ToString();
                        label18.Text = reader[9].ToString();
                        //comboBox2.Items.Add(reader[0].ToString());
                    }
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

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text=="")
            {
                MessageBox.Show("Lütfen Öğrenci TC NO seçiniz.");
            }
            else if (comboBox2.Text == "")
            {
                MessageBox.Show("Lütfen dil seçiniz.");
            }
            else if (comboBox3.Text == "")
            {
                MessageBox.Show("Lütfen şube seçiniz.");
            }
            else if (comboBox4.Text == "")
            {
                MessageBox.Show("Lütfen il seçiniz.");
            }
            else if (comboBox5.Text == "")
            {
                MessageBox.Show("Lütfen ilçe seçiniz.");
            }
            else if (comboBox6.Text == "")
            {
                MessageBox.Show("Lütfen grup no seçiniz.");
            }
            else if (comboBox7.Text == "")
            {
                MessageBox.Show("Lütfen ödeme seçiniz.");
            }
            else
            {
                int odemeonayı = 0;
                if (comboBox7.SelectedIndex==0)
                {
                    checkBox1.Visible = true;
                    if (checkBox1.Checked==true)
                    {
                        odemeonayı = 1;
                    }
                    else
                    {
                        odemeonayı = 0;
                    }
                    conn.Open();
                    NpgsqlCommand command2 = new NpgsqlCommand("insert into kurskayit (tcno,il,ilce,dil,sube,kursid,odeme,odemeonay)values(@tcno,@il,@ilce,@dil,@sube,@kursid,@odeme,@odemeonay)", conn);
                    command2.Parameters.AddWithValue("@tcno", Convert.ToInt64(comboBox1.Text));
                    command2.Parameters.AddWithValue("@il", comboBox4.Text);
                    command2.Parameters.AddWithValue("@ilce", comboBox5.Text);
                    command2.Parameters.AddWithValue("@dil", comboBox2.Text);
                    command2.Parameters.AddWithValue("@sube", Convert.ToInt32(comboBox3.Text));
                    command2.Parameters.AddWithValue("@kursid", Convert.ToInt32(comboBox6.Text));
                    command2.Parameters.AddWithValue("@odeme", 0);
                    command2.Parameters.AddWithValue("@odemeonay", odemeonayı);
                    command2.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    NpgsqlCommand command3 = new NpgsqlCommand("insert into fatura (tcno,kursid,no,ucret,durum)values(@tcno,@kursid,@no,@ucret,@durum)", conn);
                    command3.Parameters.AddWithValue("@tcno", Convert.ToInt64(comboBox1.Text));
                    command3.Parameters.AddWithValue("@kursid", Convert.ToInt32(comboBox6.Text));
                    command3.Parameters.AddWithValue("@no",1);
                    command3.Parameters.AddWithValue("@ucret",Convert.ToInt32(label17.Text));
                    command3.Parameters.AddWithValue("@durum", odemeonayı);  
                    command3.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Öğrenci derse başarıyla eklendi.");
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    comboBox5.Text = "";
                    comboBox6.Text = "";
                    comboBox7.Text = "";
                    label13.Text = "";
                    label14.Text = "";
                    label15.Text = "";
                    label16.Text = "";
                    label17.Text ="";
                    label18.Text = "";
                    checkBox1.Checked = false;



                }
                else if (comboBox7.SelectedIndex == 1)
                {
                    checkBox1.Visible = false;
                    conn.Open();
                    NpgsqlCommand command2 = new NpgsqlCommand("insert into kurskayit (tcno,il,ilce,dil,sube,kursid,odeme,odemeonay)values(@tcno,@il,@ilce,@dil,@sube,@kursid,@odeme,@odemeonay)", conn);
                    command2.Parameters.AddWithValue("@tcno", Convert.ToInt64(comboBox1.Text));
                    command2.Parameters.AddWithValue("@il", comboBox4.Text);
                    command2.Parameters.AddWithValue("@ilce", comboBox5.Text);
                    command2.Parameters.AddWithValue("@dil", comboBox2.Text);
                    command2.Parameters.AddWithValue("@sube", Convert.ToInt32(comboBox3.Text));
                    command2.Parameters.AddWithValue("@kursid", Convert.ToInt32(comboBox6.Text));
                    command2.Parameters.AddWithValue("@odeme", 1);
                    command2.Parameters.AddWithValue("@odemeonay", 0);
                    command2.ExecuteNonQuery();
                    conn.Close();

                    int i = 0;
                    for (i=0;i<3;i++)
                    {
                        conn.Open();
                        NpgsqlCommand command3 = new NpgsqlCommand("insert into fatura (tcno,kursid,no,ucret,durum)values(@tcno,@kursid,@no,@ucret,@durum)", conn);
                        command3.Parameters.AddWithValue("@tcno", Convert.ToInt64(comboBox1.Text));
                        command3.Parameters.AddWithValue("@kursid", Convert.ToInt32(comboBox6.Text));
                        command3.Parameters.AddWithValue("@no", i+1);
                        command3.Parameters.AddWithValue("@ucret", Convert.ToInt32(label17.Text)/3);
                        command3.Parameters.AddWithValue("@durum", 0);
                        command3.ExecuteNonQuery();
                        conn.Close();
                    }
                    


                    MessageBox.Show("Öğrenci derse başarıyla eklendi.");
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    comboBox5.Text = "";
                    comboBox6.Text = "";
                    comboBox7.Text = "";
                    label13.Text = "";
                    label14.Text = "";
                    label15.Text = "";
                    label16.Text = "";
                    label17.Text = "";
                    label18.Text = "";
                    checkBox1.Checked = false;

                }
                else if (comboBox7.SelectedIndex == 2)
                {
                    checkBox1.Visible = false;
                    conn.Open();
                    NpgsqlCommand command2 = new NpgsqlCommand("insert into kurskayit (tcno,il,ilce,dil,sube,kursid,odeme,odemeonay)values(@tcno,@il,@ilce,@dil,@sube,@kursid,@odeme,@odemeonay)", conn);
                    command2.Parameters.AddWithValue("@tcno", Convert.ToInt64(comboBox1.Text));
                    command2.Parameters.AddWithValue("@il", comboBox4.Text);
                    command2.Parameters.AddWithValue("@ilce", comboBox5.Text);
                    command2.Parameters.AddWithValue("@dil", comboBox2.Text);
                    command2.Parameters.AddWithValue("@sube", Convert.ToInt32(comboBox3.Text));
                    command2.Parameters.AddWithValue("@kursid", Convert.ToInt32(comboBox6.Text));
                    command2.Parameters.AddWithValue("@odeme", 2);
                    command2.Parameters.AddWithValue("@odemeonay", 0);
                    command2.ExecuteNonQuery();
                    conn.Close();

                    int i = 0;
                    for (i = 0; i < 6; i++)
                    {
                        conn.Open();
                        NpgsqlCommand command3 = new NpgsqlCommand("insert into fatura (tcno,kursid,no,ucret,durum)values(@tcno,@kursid,@no,@ucret,@durum)", conn);
                        command3.Parameters.AddWithValue("@tcno", Convert.ToInt64(comboBox1.Text));
                        command3.Parameters.AddWithValue("@kursid", Convert.ToInt32(comboBox6.Text));
                        command3.Parameters.AddWithValue("@no", i + 1);
                        command3.Parameters.AddWithValue("@ucret", Convert.ToInt32(label17.Text) / 6);
                        command3.Parameters.AddWithValue("@durum", 0);
                        command3.ExecuteNonQuery();
                        conn.Close();
                    }



                    MessageBox.Show("Öğrenci derse başarıyla eklendi.");
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    comboBox5.Text = "";
                    comboBox6.Text = "";
                    comboBox7.Text = "";
                    label13.Text = "";
                    label14.Text = "";
                    label15.Text = "";
                    label16.Text = "";
                    label17.Text = "";
                    label18.Text = "";
                    checkBox1.Checked = false;
                    panel2.Visible = false;
                }
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex==0)
            {
                checkBox1.Visible = true;
            }
            else
            {
                checkBox1.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }
    }
}

