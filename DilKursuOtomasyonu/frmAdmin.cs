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
    public partial class frmAdmin : Form
    {
        private int id;
        int bir = 0, iki = 0, uc = 0, dort = 0, bes = 0, alti = 0, yedi = 0;
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;
        private String sql = null;
        public frmAdmin(int val)
        {
            InitializeComponent();
            this.id = id;
            conn = new NpgsqlConnection("Server = localhost; Port = 5432;Database=dilkursu; User Id=postgres;Password=123");
        }

        private void frmAdmin_Load(object sender, EventArgs e)
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
                        comboBox1.Items.Add(reader[2].ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel1.Visible = true;
            comboBox4.Text = "";
            comboBox5.SelectedIndex = 0;
            comboBox6.Text = "";
            comboBox7.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;

            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;
            checkBox7.Visible = false;
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            var today = DateTime.Today;
            monthCalendar1.SelectionStart = today;
            monthCalendar2.SelectionStart = today;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            int secilmisil = comboBox1.SelectedIndex + 1;
            try
            {
                conn.Open();
                string sql = "SELECT * FROM counties where cityid= "+secilmisil;
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {
                    
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader[2].ToString());
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

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel2.Visible = true;
            comboBox3.Items.Clear();
            comboBox4.Text = "";
            comboBox5.SelectedIndex = 0;
            comboBox6.Text = "";
            comboBox7.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;

            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;
            checkBox7.Visible = false;
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            var today = DateTime.Today;
            monthCalendar1.SelectionStart = today;
            monthCalendar2.SelectionStart = today;
            try
            {
                conn.Open();
                string sql = "SELECT * FROM subeler ";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {
                    
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader[0].ToString());
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

        private void button4_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(comboBox2.Text) || string.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz");
            }
            else
            {
                try
                {
                    conn.Open();
                    sql = @"select * from subeler where subekodu='" + textBox1.Text + "'";
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
                        MessageBox.Show("Böyle bir şube zaten mevcuttur.");                     

                    }
                    else
                    {
                        
                        NpgsqlCommand command2 = new NpgsqlCommand("insert into subeler (subekodu,subeismi,il,ilce,adres,telefon)values(@subekodu,@subeismi,@il,@ilce,@adres,@telefon)", conn);
                        command2.Parameters.AddWithValue("@subekodu", Convert.ToInt32(textBox1.Text));
                        command2.Parameters.AddWithValue("@subeismi", textBox2.Text);
                        command2.Parameters.AddWithValue("@il", comboBox1.Text);
                        command2.Parameters.AddWithValue("@ilce", comboBox2.Text);
                        command2.Parameters.AddWithValue("@adres", textBox3.Text);
                        command2.Parameters.AddWithValue("@telefon", maskedTextBox1.Text);
                        command2.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Şube Eklendi");
                        panel1.Visible = false;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        maskedTextBox1.Text = "";
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                conn.Open();
                string sql = "SELECT subeismi FROM subeler where subekodu= " +Convert.ToInt32(comboBox3.Text);
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {                    
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        label9.Text=reader[0].ToString();
                    }
                }
                conn.Close();
                conn.Open();
                //MessageBox.Show(yetki1.ToString());
                string sql1 = "SELECT * FROM derslikler where subekodu= " + Convert.ToInt32(comboBox3.Text);
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, conn);
                using (command1)
                {
                    NpgsqlDataReader reader1 = command1.ExecuteReader();
                    while (reader1.Read())
                    {
                        listBox1.Items.Add(reader1[0].ToString() + "  (" + reader1[2].ToString() + "  kişi )");
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
            if (string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz");
            }
            else
            {
                try
                {
                    conn.Open();
                    sql = @"select * from derslikler where subekodu=" + Convert.ToInt32(comboBox3.Text) + " and derslikadi='" + textBox4.Text + "'";
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
                        MessageBox.Show("Bu şubede böyle bir derslik zaten mevcuttur.");

                    }
                    else
                    {

                        NpgsqlCommand command2 = new NpgsqlCommand("insert into derslikler (derslikadi,subekodu,kapasite,bir,iki,uc,dort,bes,alti,yedi,sekiz,dokuz,kon,onbir,oniki,onuc,ondort)values(@derslikadi,@subekodu,@kapasite,@bir,@iki,@uc,@dort,@bes,@alti,@yedi,@sekiz,@dokuz,@kon,@onbir,@oniki,@onuc,@ondort)", conn);
                        command2.Parameters.AddWithValue("@derslikadi", textBox4.Text);
                        command2.Parameters.AddWithValue("@subekodu", Convert.ToInt32(comboBox3.Text));
                        command2.Parameters.AddWithValue("@kapasite", Convert.ToInt32(textBox5.Text));
                        command2.Parameters.AddWithValue("@bir", 0);
                        command2.Parameters.AddWithValue("@iki", 0);
                        command2.Parameters.AddWithValue("@uc", 0);
                        command2.Parameters.AddWithValue("@dort", 0);
                        command2.Parameters.AddWithValue("@bes", 0);
                        command2.Parameters.AddWithValue("@alti", 0);
                        command2.Parameters.AddWithValue("@yedi", 0);
                        command2.Parameters.AddWithValue("@sekiz", 0);
                        command2.Parameters.AddWithValue("@dokuz", 0);
                        command2.Parameters.AddWithValue("@kon", 0);
                        command2.Parameters.AddWithValue("@onbir", 0);
                        command2.Parameters.AddWithValue("@oniki", 0);
                        command2.Parameters.AddWithValue("@onuc", 0);
                        command2.Parameters.AddWithValue("@ondort", 0);

                        command2.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Derslik Eklendi");
                        panel1.Visible = false;
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                    conn.Close();
                    listBox1.Items.Clear();
                    conn.Open();
                    //MessageBox.Show(yetki1.ToString());
                    string sql1 = "SELECT * FROM derslikler where subekodu= " + Convert.ToInt32(comboBox3.Text);
                    NpgsqlCommand command1 = new NpgsqlCommand(sql1, conn);
                    using (command1)
                    {
                        NpgsqlDataReader reader1 = command1.ExecuteReader();
                        while (reader1.Read())
                        {
                            listBox1.Items.Add(reader1[0].ToString() + "  (" + reader1[2].ToString() + "  kişi )");
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
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            listBox1.Items.Clear();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox6.Text = "";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            comboBox9.Items.Clear();
            textBox7.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";
            comboBox11.Text = "";
            maskedTextBox2.Text = "";
            maskedTextBox3.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            checkBox15.Checked = false;
            checkBox16.Checked = false;
            checkBox17.Checked = false;
            checkBox18.Checked = false;
            checkBox19.Checked = false;
            checkBox20.Checked = false;
            checkBox21.Checked = false;
            checkBox22.Checked = false;
            checkBox23.Checked = false;
            checkBox24.Checked = false;
            panel4.Visible = true;

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
                        comboBox9.Items.Add(reader[0].ToString());
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

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox10.Items.Clear();
            comboBox10.Text = "";
            comboBox11.Items.Clear();
            comboBox11.Text = "";
            int secilmisil = comboBox4.SelectedIndex + 1;
            try
            {
                conn.Open();
                string sql = "select distinct(ilce) from subeler where ilce in (select countyname from counties where cityid in (select cityid from cities where cityname = '" + comboBox9.Text + "'))";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox10.Items.Add(reader[0].ToString());
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

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox11.Items.Clear();
            comboBox11.Text = "";
            int secilmisil = comboBox4.SelectedIndex + 1;
            try
            {
                conn.Open();
                string sql = "select subekodu from subeler where il='"+comboBox9.Text+"' AND ilce='"+comboBox10.Text+"'";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox11.Items.Add(reader[0].ToString());
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox7.Text=="")
            {
                MessageBox.Show("Lütfen bir TC NO giriniz.");
            }
            else if (textBox8.Text=="")
            {
                MessageBox.Show("Lütfen bir isim giriniz.");
            }
            else if (textBox9.Text == "")
            {
                MessageBox.Show("Lütfen bir soyisim giriniz.");
            }
            else if (maskedTextBox2.Text == "")
            {
                MessageBox.Show("Lütfen bir ev telefonu giriniz.");
            }
            else if (maskedTextBox3.Text == "")
            {
                MessageBox.Show("Lütfen bir cep telefonu giriniz.");
            }
            else if (comboBox9.Text == "")
            {
                MessageBox.Show("Lütfen bir il giriniz.");
            }
            else if (comboBox10.Text == "")
            {
                MessageBox.Show("Lütfen bir ilçe giriniz.");
            }
            else if (comboBox11.Text == "")
            {
                MessageBox.Show("Lütfen bir şube giriniz.");
            }
            else if (checkBox8.Checked==false && checkBox9.Checked == false && checkBox10.Checked == false && checkBox11.Checked == false && checkBox12.Checked == false && checkBox13.Checked == false && checkBox14.Checked == false && checkBox15.Checked == false && checkBox16.Checked == false && checkBox17.Checked == false)
            {
                MessageBox.Show("Lütfen en az bir dil seçiniz.");
            }
            else if (checkBox18.Checked == false && checkBox19.Checked == false && checkBox20.Checked == false && checkBox21.Checked == false && checkBox22.Checked == false && checkBox23.Checked == false && checkBox24.Checked == false)
            {
                MessageBox.Show("Lütfen en az bir gün seçiniz.");
            }
            else
            {
                conn.Open();
                NpgsqlCommand command2 = new NpgsqlCommand("insert into ogretmenler (tcno,ad,soyad,evtel,ceptel,subekodu)values(@tcno,@ad,@soyad,@evtel,@ceptel,@subekodu)", conn);
                command2.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                command2.Parameters.AddWithValue("@ad", textBox8.Text);
                command2.Parameters.AddWithValue("@soyad", textBox9.Text);
                command2.Parameters.AddWithValue("@evtel", maskedTextBox2.Text);
                command2.Parameters.AddWithValue("@ceptel", maskedTextBox3.Text);
                command2.Parameters.AddWithValue("@subekodu", Convert.ToInt32(comboBox11.Text));  
                command2.ExecuteNonQuery();
                conn.Close();

                if (checkBox8.Checked==true)
                {
                    conn.Open();
                    NpgsqlCommand command3 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command3.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command3.Parameters.AddWithValue("@dilid", 1);        
                    command3.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox9.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command4 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command4.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command4.Parameters.AddWithValue("@dilid", 2);
                    command4.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox10.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command5 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command5.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command5.Parameters.AddWithValue("@dilid", 3);
                    command5.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox11.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command6 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command6.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command6.Parameters.AddWithValue("@dilid", 4);
                    command6.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox12.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command7 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command7.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command7.Parameters.AddWithValue("@dilid", 5);
                    command7.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox13.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command8 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command8.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command8.Parameters.AddWithValue("@dilid", 6);
                    command8.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox14.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command9 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command9.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command9.Parameters.AddWithValue("@dilid", 7);
                    command9.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox15.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command10 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command10.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command10.Parameters.AddWithValue("@dilid", 8);
                    command10.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox16.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command11 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command11.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command11.Parameters.AddWithValue("@dilid", 9);
                    command11.ExecuteNonQuery();
                    conn.Close();
                }
                if (checkBox17.Checked == true)
                {
                    conn.Open();
                    NpgsqlCommand command12 = new NpgsqlCommand("insert into ogretmendiller (tcno,dilid)values(@tcno,@dilid)", conn);
                    command12.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                    command12.Parameters.AddWithValue("@dilid", 10);
                    command12.ExecuteNonQuery();
                    conn.Close();
                }

                int bir = 1, iki = 1, uc = 1, dort = 1, bes = 1, alti = 1,yedi=1;
                if (checkBox18.Checked==true)
                {
                    bir = 0;
                }
                if (checkBox19.Checked == true)
                {
                    iki = 0;
                }
                if (checkBox20.Checked == true)
                {
                    uc = 0;
                }
                if (checkBox21.Checked == true)
                {
                    dort = 0;
                }
                if (checkBox22.Checked == true)
                {
                    bes = 0;
                }
                if (checkBox23.Checked == true)
                {
                    alti = 0;
                }
                if (checkBox24.Checked == true)
                {
                    yedi = 0;
                }

                conn.Open();
                NpgsqlCommand command13 = new NpgsqlCommand("insert into ogretmenmusait (tcno,bir,iki,uc,dort,bes,alti,yedi)values(@tcno,@bir,@iki,@uc,@dort,@bes,@alti,@yedi)", conn);
                command13.Parameters.AddWithValue("@tcno", Convert.ToInt64(textBox7.Text));
                command13.Parameters.AddWithValue("@bir", bir);
                command13.Parameters.AddWithValue("@iki", iki);
                command13.Parameters.AddWithValue("@uc", uc);
                command13.Parameters.AddWithValue("@dort", dort);
                command13.Parameters.AddWithValue("@bes", bes);
                command13.Parameters.AddWithValue("@alti", alti);
                command13.Parameters.AddWithValue("@yedi", yedi);
                command13.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Öğretmen başarıyla eklendi");
                panel4.Visible = false;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            panel3.Visible = true;
            comboBox5.SelectedIndex = 0;            
            comboBox4.Items.Clear();
            try
            {
                conn.Open();
                string sql = "SELECT * FROM diller ";
                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {
                   
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox4.Items.Add(reader[1].ToString());
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

        private void button8_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(monthCalendar1.SelectionRange.Start.ToShortDateString());
           
            if (comboBox4.Text=="")
            {
                MessageBox.Show("Lütfen bir dil seçiniz.");
            }
            else if (comboBox6.Text=="")
            {
                MessageBox.Show("Lütfen bir şube seçiniz.");
            }
            else if (comboBox7.Text=="")
            {
                MessageBox.Show("Lütfen bir öğretmen seçiniz.");
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false && checkBox6.Checked == false && checkBox7.Checked == false)
            {
                MessageBox.Show("Lütfen en az bir kurs günü seçiniz !!!");
            }
            else if (comboBox8.Text=="")
            {
                MessageBox.Show("Lütfen uygun olan bir sınıf seçiniz !!!");
            }
            else if (textBox6.Text=="")
            {
                MessageBox.Show("Lütfen uygun bir ücret giriniz !!!");
            }
            else
            {
                int a = 0, b = 0, c = 0, d = 0, f = 0, g = 0, h = 0;
                int aa = 0, bb = 0, cc = 0, dd = 0, ff = 0, gg = 0, hh = 0;
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM derslikler where derslikadi= '" + comboBox8.Text + "' and subekodu=" + Convert.ToInt32(comboBox6.Text);
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    using (command)
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            a = Convert.ToInt32(reader[3].ToString());
                            b = Convert.ToInt32(reader[4].ToString());
                            c = Convert.ToInt32(reader[5].ToString());
                            d = Convert.ToInt32(reader[6].ToString());
                            f = Convert.ToInt32(reader[7].ToString());
                            g = Convert.ToInt32(reader[8].ToString());
                            h = Convert.ToInt32(reader[9].ToString());

                            aa = Convert.ToInt32(reader[10].ToString());
                            bb = Convert.ToInt32(reader[11].ToString());
                            cc = Convert.ToInt32(reader[12].ToString());
                            dd = Convert.ToInt32(reader[13].ToString());
                            ff = Convert.ToInt32(reader[14].ToString());
                            gg = Convert.ToInt32(reader[15].ToString());
                            hh = Convert.ToInt32(reader[16].ToString());

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
                int derslikkontrol = 0;
                if (comboBox5.SelectedIndex==0)
                {
                    if (checkBox1.Checked==true && a==1)
                    {
                        MessageBox.Show("Seçilen derslik Pazartesi sabah doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox2.Checked == true && b == 1)
                    {
                        MessageBox.Show("Seçilen derslik Salı sabah doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox3.Checked == true && c == 1)
                    {
                        MessageBox.Show("Seçilen derslik Çarşamba sabah doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox4.Checked == true && d == 1)
                    {
                        MessageBox.Show("Seçilen derslik Perşembe sabah doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox5.Checked == true && f == 1)
                    {
                        MessageBox.Show("Seçilen derslik Cuma sabah doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox6.Checked == true && g == 1)
                    {
                        MessageBox.Show("Seçilen derslik Cumartesi sabah doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox7.Checked == true && h == 1)
                    {
                        MessageBox.Show("Seçilen derslik Pazar sabah doludur");
                        derslikkontrol = 1;
                    }
                    else
                    {
                        derslikkontrol = 0;
                    }
                    
                }
                else 
                {
                    if (checkBox1.Checked == true && aa == 1)
                    {
                        MessageBox.Show("Seçilen derslik Pazartesi akşam doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox2.Checked == true && bb == 1)
                    {
                        MessageBox.Show("Seçilen derslik Salı akşam doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox3.Checked == true && cc == 1)
                    {
                        MessageBox.Show("Seçilen derslik Çarşamba akşam doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox4.Checked == true && dd== 1)
                    {
                        MessageBox.Show("Seçilen derslik Perşembe akşam doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox5.Checked == true && ff == 1)
                    {
                        MessageBox.Show("Seçilen derslik Cuma akşam doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox6.Checked == true && gg == 1)
                    {
                        MessageBox.Show("Seçilen derslik Cumartesi akşam doludur");
                        derslikkontrol = 1;
                    }
                    else if (checkBox7.Checked == true && hh == 1)
                    {
                        MessageBox.Show("Seçilen derslik Pazar akşam doludur");
                        derslikkontrol = 1;
                    }
                    else
                    {
                        derslikkontrol = 0;
                    }
                    
                }

                if (derslikkontrol==0)
                {
                    string gunler = "";
                    if (checkBox1.Checked == true)
                    {
                        gunler = gunler + " Pazartesi";
                        bir = 1;
                    }
                    if (checkBox2.Checked == true)
                    {
                        gunler = gunler + " Salı";
                        iki = 1;
                    }
                    if (checkBox3.Checked == true)
                    {
                        gunler = gunler + " Çarşamba";
                        uc = 1;
                    }
                    if (checkBox4.Checked == true)
                    {
                        gunler = gunler + " Perşembe";
                        dort = 1;
                    }
                    if (checkBox5.Checked == true)
                    {
                        gunler = gunler + " Cuma";
                        bes = 1;
                    }
                    if (checkBox6.Checked == true)
                    {
                        gunler = gunler + " Cumartesi";
                        alti = 1;
                    }
                    if (checkBox7.Checked == true)
                    {
                        gunler = gunler + " Pazar";
                        yedi = 1;
                    }
                    //MessageBox.Show(gunler);
                    int dilid = comboBox4.SelectedIndex + 1;
                    conn.Open();
                    NpgsqlCommand command2 = new NpgsqlCommand("insert into kurslar (dilid,baslamatarihi,bitistarihi,gunleri,zamanı,subekodu,ogretmentcno,ucret,derslikadi)values(@dilid,@baslamatarihi,@bitistarihi,@gunleri,@zamanı,@subekodu,@ogretmentcno,@ucret,@derslikadi)", conn);
                    command2.Parameters.AddWithValue("@dilid", dilid);
                    command2.Parameters.AddWithValue("@baslamatarihi", Convert.ToDateTime(monthCalendar1.SelectionRange.Start.ToShortDateString()));
                    command2.Parameters.AddWithValue("@bitistarihi", Convert.ToDateTime(monthCalendar2.SelectionRange.Start.ToShortDateString()));
                    command2.Parameters.AddWithValue("@gunleri", gunler);
                    command2.Parameters.AddWithValue("@zamanı", comboBox5.Text);
                    command2.Parameters.AddWithValue("@subekodu", Convert.ToInt32(comboBox6.Text));
                    command2.Parameters.AddWithValue("@ogretmentcno", Convert.ToInt64(comboBox7.Text));
                    command2.Parameters.AddWithValue("@ucret", Convert.ToInt32(textBox6.Text));
                    command2.Parameters.AddWithValue("@derslikadi", comboBox8.Text);
                    command2.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    NpgsqlCommand command3 = new NpgsqlCommand("update ogretmenmusait set bir=@bir,iki=@iki,uc=@uc,dort=@dort,bes=@bes,alti=@alti,yedi=@yedi where tcno=" + Convert.ToInt64(comboBox7.Text), conn);
                    command3.Parameters.AddWithValue("@bir", bir);
                    command3.Parameters.AddWithValue("@iki", iki);
                    command3.Parameters.AddWithValue("@uc", uc);
                    command3.Parameters.AddWithValue("@dort", dort);
                    command3.Parameters.AddWithValue("@bes", bes);
                    command3.Parameters.AddWithValue("@alti", alti);
                    command3.Parameters.AddWithValue("@yedi", yedi);
                    command3.ExecuteNonQuery();
                    conn.Close();

                    if (comboBox5.SelectedIndex == 0)
                    {
                        if (a==1)
                        {
                            bir = 1;
                        }
                        if (b == 1)
                        {
                            iki = 1;
                        }
                        if (c == 1)
                        {
                            uc = 1;
                        }
                        if (d == 1)
                        {
                            dort = 1;
                        }
                        if (f == 1)
                        {
                            bes = 1;
                        }
                        if (g == 1)
                        {
                            alti = 1;
                        }
                        if (h == 1)
                        {
                            yedi = 1;
                        }

                        conn.Open();
                        NpgsqlCommand command4 = new NpgsqlCommand("update derslikler set bir=@bir,iki=@iki,uc=@uc,dort=@dort,bes=@bes,alti=@alti,yedi=@yedi where derslikadi='" + comboBox8.Text + "'and subekodu=" + Convert.ToInt32(comboBox6.Text), conn);
                        command4.Parameters.AddWithValue("@bir", bir);
                        command4.Parameters.AddWithValue("@iki", iki);
                        command4.Parameters.AddWithValue("@uc", uc);
                        command4.Parameters.AddWithValue("@dort", dort);
                        command4.Parameters.AddWithValue("@bes", bes);
                        command4.Parameters.AddWithValue("@alti", alti);
                        command4.Parameters.AddWithValue("@yedi", yedi);
                        command4.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        if (aa == 1)
                        {
                            bir = 1;
                        }
                        if (bb == 1)
                        {
                            iki = 1;
                        }
                        if (cc == 1)
                        {
                            uc = 1;
                        }
                        if (dd == 1)
                        {
                            dort = 1;
                        }
                        if (ff == 1)
                        {
                            bes = 1;
                        }
                        if (gg == 1)
                        {
                            alti = 1;
                        }
                        if (hh == 1)
                        {
                            yedi = 1;
                        }
                        conn.Open();
                        NpgsqlCommand command5 = new NpgsqlCommand("update derslikler set sekiz=@sekiz,dokuz=@dokuz,kon=@kon,onbir=@onbir,oniki=@oniki,onuc=@onuc,ondort=@ondort where derslikadi='" + comboBox8.Text + "'and subekodu=" + Convert.ToInt32(comboBox6.Text), conn);
                        command5.Parameters.AddWithValue("@sekiz", bir);
                        command5.Parameters.AddWithValue("@dokuz", iki);
                        command5.Parameters.AddWithValue("@kon", uc);
                        command5.Parameters.AddWithValue("@onbir", dort);
                        command5.Parameters.AddWithValue("@oniki", bes);
                        command5.Parameters.AddWithValue("@onuc", alti);
                        command5.Parameters.AddWithValue("@ondort", yedi);
                        command5.ExecuteNonQuery();
                        conn.Close();
                    }


                    //UPDATE public.ogretmenmusait
                    // SET bir =?, iki =?, uc =?, dort =?, bes =?, alti =?, yedi =?
                    //WHERE tcno =;
                    MessageBox.Show("Kurs Eklendi");
                    comboBox4.Text = "";
                    comboBox5.SelectedIndex = 0;
                    comboBox6.Text = "";
                    comboBox7.Text = "";
                    textBox6.Text = "";
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    checkBox4.Checked = false;
                    checkBox5.Checked = false;
                    checkBox6.Checked = false;
                    checkBox7.Checked = false;

                    checkBox1.Visible = false;
                    checkBox2.Visible = false;
                    checkBox3.Visible = false;
                    checkBox4.Visible = false;
                    checkBox5.Visible = false;
                    checkBox6.Visible = false;
                    checkBox7.Visible = false;
                    comboBox6.Items.Clear();
                    comboBox7.Items.Clear();
                    var today = DateTime.Today;
                    monthCalendar1.SelectionStart = today;
                    monthCalendar2.SelectionStart = today;
                }                                         
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox6.Items.Clear();
            comboBox6.Text = "";
            comboBox7.Items.Clear();
            comboBox7.Text = "";
            int secilmisdil = comboBox4.SelectedIndex + 1;
            try
            {
                conn.Open();
                string sql = "select distinct(subekodu) from ogretmenler where tcno in (select tcno from ogretmendiller where dilid = " + secilmisdil+")";
                
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

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();
            comboBox7.Text = "";
            comboBox8.Items.Clear();
            comboBox8.Text = "";            
            int secilmisdil = comboBox4.SelectedIndex + 1;
            try
            {
                conn.Open();
                string sql = "select * from ogretmenler where tcno in (select tcno from ogretmendiller where dilid = " + secilmisdil + ") and subekodu="+Convert.ToInt32(comboBox6.Text);

                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {
                    
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {                        
                        comboBox7.Items.Add(reader[0].ToString());
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

            if (comboBox5.SelectedIndex==0)
            {
                try
                {
                    conn.Open();
                    string sql = "select * from derslikler where (bir=0 or iki=0 or uc=0 or dort=0 or bes=0 or alti=0 or yedi=0) and subekodu=" + Convert.ToInt32(comboBox6.Text);
                    int say = 0;
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    using (command)
                    {

                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox8.Items.Add(reader[0].ToString());
                            say++;
                        }
                        if (say == 0)
                        {
                            MessageBox.Show("Bu şubede hiç sınıf bulunamadı");
                        }
                        else
                        {
                            comboBox8.SelectedIndex = 0;
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
            else
            {
                try
                {
                    conn.Open();
                    string sql = "select * from derslikler where (sekiz=0 or dokuz=0 or kon=0 or onbir=0 or oniki=0 or onuc=0 or ondort=0) and subekodu=" + Convert.ToInt32(comboBox6.Text);
                    int say = 0;
                    NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                    using (command)
                    {

                        NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            comboBox8.Items.Add(reader[0].ToString());
                            say++;
                        }
                        if (say == 0)
                        {
                            MessageBox.Show("Bu şubede hiç sınıf bulunamadı");
                        }
                        else
                        {
                            comboBox8.SelectedIndex = 0;
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
            
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            Int64 tcno =Convert.ToInt64(comboBox7.Text);
            try
            {
                conn.Open();
                string sql = "select * from ogretmenmusait where tcno= " + tcno;

                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                using (command)
                {
                    
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {                        
                        if (reader[1].ToString()=="0")
                        {
                            checkBox1.Visible = true;
                            bir = 0;
                        }
                        else
                        {
                            checkBox1.Visible = false;
                            bir = 1;
                        }

                        if (reader[2].ToString() == "0")
                        {
                            checkBox2.Visible = true;
                            iki = 0;
                        }
                        else
                        {
                            checkBox2.Visible = false;
                            iki = 1;
                        }

                        if (reader[3].ToString() == "0")
                        {
                            checkBox3.Visible = true;
                            uc = 0;
                        }
                        else
                        {
                            checkBox3.Visible = false;
                            uc = 1;
                        }

                        if (reader[4].ToString() == "0")
                        {
                            checkBox4.Visible = true;
                            dort = 0;
                        }
                        else
                        {
                            checkBox4.Visible = false;
                            dort = 1;
                        }

                        if (reader[5].ToString() == "0")
                        {
                            checkBox5.Visible = true;
                            bes = 0;
                        }
                        else
                        {
                            checkBox5.Visible = false;
                            bes = 1;
                        }

                        if (reader[6].ToString() == "0")
                        {
                            checkBox6.Visible = true;
                            alti = 0;
                        }
                        else
                        {
                            checkBox6.Visible = false;
                            alti = 1;
                        }

                        if (reader[7].ToString() == "0")
                        {
                            checkBox7.Visible = true;
                            yedi = 0;
                        }
                        else
                        {
                            checkBox7.Visible = false;
                            yedi = 1;
                        }

                        if (checkBox1.Visible==false && checkBox2.Visible == false && checkBox3.Visible == false && checkBox4.Visible == false && checkBox5.Visible == false && checkBox6.Visible == false && checkBox7.Visible == false)
                        {
                            MessageBox.Show("Öğretmenin bütün günleri doludur. Lütfen başka bir öğretmen seçiniz:");                            
                        }


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

        private void button9_Click(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;
            checkBox7.Visible = false;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;

            panel3.Visible = false;
            comboBox4.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";

        }
    }
}
