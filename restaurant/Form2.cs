using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace restaurant
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DXY\SQLEXPRESS;Initial Catalog=dimas;Integrated Security=True");
        SqlCommand cmd;
        int isgoreng = 0;
        int iskuah = 0;


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Yakin Pengen Keluar Dek?.", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            { }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into [table_makanan] (kode,namamakanan,jumlahmakanan,tingkatkepedasan,hargamakanan,goreng,kuah,total) values ('" + guna2TextBox2.Text + "','" + cbbNamaMakanan.Text + "','" + guna2NumericUpDown1.Value + "','" + cbbTingkatKepedasan.Text + "', '" + cbbHargaMakanan.Text + "','" + isgoreng + "', '" + iskuah + "', '" + guna2TextBox4.Text + "')";
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Data Berhasil Disimpan");
            cbbNamaMakanan.ResetText();
            cbbTingkatKepedasan.ResetText();
            cbbHargaMakanan.ResetText();
            guna2NumericUpDown1.ResetText();
            cbbNamaMakanan.ResetText();
            cbbTingkatKepedasan.ResetText();
            cbbHargaMakanan.ResetText();
            guna2NumericUpDown1.ResetText();
            pctrmie.Visible = false;
            pctrbakso.Visible = false;
            pctrcapcay.Visible = false;
            pictureBox3.Visible = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            guna2TextBox2.ResetText();
            guna2TextBox4.ResetText();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            isgoreng = 1;
            iskuah = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            iskuah= 1;
            isgoreng= 0;
        }

        private void txtnamamakanan_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int hargaItem = int.Parse(cbbHargaMakanan.Text);
            int item = int.Parse(guna2NumericUpDown1.Value.ToString());
            int total = hargaItem * item;
            guna2TextBox4.Text = total.ToString();
        }

        private void cbbNamaMakanan_SelectedIndexChanged(object sender, EventArgs e)
        {
            pctrmie.Visible = false;
            pctrbakso.Visible = false;
            pctrcapcay.Visible = false;

            if (cbbNamaMakanan.Text == "Mie")
            {

                pctrmie.Visible = true;
            }
            else if (cbbNamaMakanan.Text == "Bakso")
            {

                pctrbakso.Visible = true;
            }
            else if (cbbNamaMakanan.Text == "Capcay")
            {

                pctrcapcay.Visible = true;
            }

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
            Zen.Barcode.Code128BarcodeDraw brCode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pictureBox3.Image = brCode.Draw(guna2TextBox4.Text, 40);
        }

        public void displaydata()
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [table_makanan]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            displaydata();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Apakah kamu yakin ingin menghapus data?", "Warning", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from [table_makanan] where kode = '" + int.Parse(guna2TextBox1.Text) + "'";
                cmd.ExecuteNonQuery();
                conn.Close();
                guna2TextBox1.Clear();
                displaydata();
                MessageBox.Show("Data berhasil dihapus");
            } 
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [table_makanan] where kode = '" + int.Parse(guna2TextBox1.Text) + "'";
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update [table_makanan] set kode = '" + guna2TextBox2.Text + "', namamakanan = '" + cbbNamaMakanan.Text + "' , jumlahmakanan = '" + guna2NumericUpDown1.Value + "' , tingkatkepedasan = '" + cbbTingkatKepedasan.Text + "' , hargamakanan = '" + cbbHargaMakanan.Text + "' , goreng = '" + isgoreng + "', kuah = '" + iskuah + "', total = '" + guna2TextBox4.Text + "' where kode = '" + guna2TextBox2.Text + "' ";
            cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Data berhasil diedit");
            cbbNamaMakanan.ResetText();
            cbbTingkatKepedasan.ResetText();
            cbbHargaMakanan.ResetText();
            guna2NumericUpDown1.ResetText();
            pctrmie.Visible = false;
            pctrbakso.Visible = false;
            pctrcapcay.Visible = false;
            pictureBox3.Visible = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            guna2TextBox2.ResetText();
            guna2TextBox4.ResetText();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Data Pembeli Makanan";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Dimas Bagus Adityas";
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(dataGridView1);
        }
    }
}
