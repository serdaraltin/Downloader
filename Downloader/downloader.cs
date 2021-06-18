using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
namespace ToolBox
{
    public partial class downloader : Form
    {
        WebClient wc = new WebClient();
        public downloader()
        {
            InitializeComponent();
        }
        int Move;
        int Move_x;
        int Move_y;
        private void downloader_Load(object sender, EventArgs e)
        {
            textBox3.Text=@"C:\Users\"+Environment.UserName+"\\Downloads";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                comboBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button6.Enabled = true;
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(filedownloadcoplate);
                Uri fileurl = new Uri(textBox1.Text);
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback);
                wc.DownloadFileAsync(fileurl, textBox3.Text+"\\"+textBox2.Text+"."+comboBox1.Text);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message.ToString() + "\nİndirilebilir bir dosya değil", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                comboBox1.Enabled = true;
                button6.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = true;
            }
          
        }
     
        private void filedownloadcoplate(object sender, AsyncCompletedEventArgs e)
        {
           // MessageBox.Show("İndirme tamamlandı.","Başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            button3.Enabled = true;
            button4.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button6.Enabled = false;
            button1.Enabled = true;
            
        }
        private  void DownloadProgressCallback(object sender, DownloadProgressChangedEventArgs e)
        {
            double klobyte =Convert.ToDouble(e.BytesReceived) / 1024;
            double klobyte2 = Convert.ToDouble(e.TotalBytesToReceive) / 1024;
            /*int klbyte = Convert.ToInt32(klobyte);
            int klbyte2 = Convert.ToInt32(klobyte2);*/
            string indirilen = klobyte.ToString("N0");
            string tamboyut = klobyte2.ToString("N0");


            if (tamboyut.Length < 4)
            {
                label2.Text = indirilen + " / " + tamboyut + " Kbyte.  %" + e.ProgressPercentage.ToString() + " tamamlandı...";
                progressBar1.Value = e.ProgressPercentage;
            }

            else if (tamboyut.Length > 4 && tamboyut.Length < 7)
            {
                tamboyut= tamboyut.Remove(1,4);
                
                label2.Text = indirilen + " / " + tamboyut + " Mbyte.  %" + e.ProgressPercentage.ToString() + " tamamlandı...";
                progressBar1.Value = e.ProgressPercentage;
            }
            else if (tamboyut.Length >= 7)
            {
                tamboyut = tamboyut.Remove(3, 6);
                label2.Text = indirilen + " / " + tamboyut + " Gbyte.  %" + e.ProgressPercentage.ToString() + " tamamlandı...";
                progressBar1.Value = e.ProgressPercentage;
            }
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog kayıtyeri = new FolderBrowserDialog();
            if (kayıtyeri.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = kayıtyeri.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "")
                {
                    System.Diagnostics.Process.Start(textBox3.Text);
                }
                else
                {
                    button4.Enabled = false;
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != "")
                {
                    System.Diagnostics.Process.Start(textBox3.Text + "\\" + textBox2.Text + "." + comboBox1.Text);
                }
                else
                {
                    button3.Enabled = false;
                }
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            wc.CancelAsync();
            button3.Enabled = true;
            button4.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button6.Enabled = false;
            button1.Enabled = true;
            textBox2.Text = "";
            System.IO.File.Delete(textBox3.Text + "\\" + textBox2.Text + "." + comboBox1.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button1.Enabled == false)
            {
                notifyIcon1.Visible = true;
                this.Visible = false;
            }
        }

        private void indirmeyiGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Visible = true;
        }

        private void downloader_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void downloader_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult indirme = MessageBox.Show("Kapatmak istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (indirme == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void downloader_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }

        private void downloader_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Move_x = e.X;
            Move_y = e.Y;
        }

        private void downloader_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Move_x, MousePosition.Y - Move_y);
            }
        }

        private void iptalEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wc.CancelAsync();
            button3.Enabled = true;
            button4.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            button6.Enabled = false;
            button1.Enabled = true;
            textBox2.Text = "";
            System.IO.File.Delete(textBox3.Text + "\\" + textBox2.Text + "." + comboBox1.Text);
        }

       
    }
}
