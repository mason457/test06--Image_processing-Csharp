using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mc
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap newbmp;
        Bitmap newbmp1;
        int[,] img1;
        int[,] img2;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Bmp File(*.bmp)|*.bmp|jpg File(*.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);                //圖片像素資料存於變數bmp
                pictureBox1.Image = bmp;                                   //顯示於pictureBox1.
                img1 = mc.BmpToAry.Transfer(bmp);                         //將相速資料置入test.BmpToAry.Transfer函式，輸出陣列img            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                newbmp.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);   //圖檔newbmp輸出
            }
        }

        private void oPENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Bmp File(*.bmp)|*.bmp|jpg File(*.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);                //圖片像素資料存於變數bmp
                pictureBox1.Image = bmp;                                   //顯示於pictureBox1.
                img1 = mc.BmpToAry.Transfer(bmp);                         //將相速資料置入test.BmpToAry.Transfer函式，輸出陣列img            
            }
        }

        private void sAVEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                newbmp.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);   //圖檔newbmp輸出
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
             Bitmap bmp = new Bitmap(img1.GetLength(1), img1.GetLength(0));
                Color c1 = new Color();
                Color c2 = new Color();
                double mse = 0;
                double MSE;
                double PSNR = 0;
                double RMSE = 0;
                int box1i;
                int box1j;
                int box2i;
                int box2j;
                //tx = int.Parse(textBox4.Text);
                int HEIGHT = img1.GetLength(0);
                int WIDTH = img1.GetLength(1);
                Bitmap box1 = new Bitmap(pictureBox1.Image);
                Bitmap box2 = new Bitmap(pictureBox2.Image);

                double I, H, Hm,P;
                int[,] r1 = new int[256, 256];
                int[,] g1 = new int[256, 256];
                int[,] b1 = new int[256, 256];
                int[,] r2 = new int[256, 256];
                int[,] g2 = new int[256, 256];
                int[,] b2 = new int[256, 256];
                double[] his = new double[256];
                double[,] r3 = new double[256, 256];
                double[,] g3 = new double[256, 256];
                double[,] b3 = new double[256, 256];
   
                for (box1i = 0; box1i <= WIDTH - 1; box1i++)
                {
                    for (box1j = 0; box1j <= HEIGHT - 1; box1j++)
                    {
                        c1 = box1.GetPixel(box1i, box1j);
                        r1[box1i, box1j] = c1.R;
                        g1[box1i, box1j] = c1.G;
                        b1[box1i, box1j] = c1.B;
                    }
                }
                  
                for (box2i = 0; box2i <= box2.Width - 1; box2i++)
                {
                    for (box2j = 0; box2j <= box2.Height - 1; box2j++)
                    {
                        c2 = box2.GetPixel(box2i, box2j);
                        r2[box2i, box2j] = c2.R;
                        g2[box2i, box2j] = c2.G;
                        b2[box2i, box2j] = c2.B;
                    }
                }


                for (int h = 0; h < 256; h++)
                {
                    his[h] = 0;
                    for (int a = 0; a <= box1.Width - 1; a++)
                    {
                        for (int b = 0; b <= box1.Height - 1; b++)
                        {
                            if (r2[a, b] == h)
                            {
                                his[h] ++;
                            }
                        }
                    }
                }
            
                for (int a = 0; a < box1.Width - 1; a++)
                {
                    for (int b = 0;b < box1.Height - 1; b++)
                    {
                       mse += ((r1[a, b] - r2[a, b]) * (r1[a, b] - r2[a, b]));
                    }
                }

                Hm = 0;
                for (int x = 0; x < 256; x++)
                {
                    P = (his[x] / (256 * 256));
                    I = (Math.Log((1 / P),2));
                    H = P * I;
                    Hm += H;
                }
               
                MSE = mse / (256 * 256);
                PSNR = 10 * Math.Log10((255*255)/MSE);
                RMSE = Math.Sqrt(MSE);
                label1.Text = "PSNR : " + PSNR;
                label2.Text = "RMSE : " + RMSE;
                label3.Text = "Entropy : " + Math.Round(Hm,5);
        }

        private void lOADjpgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Bmp File(*.bmp)|*.bmp|jpg File(*.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);                //圖片像素資料存於變數bmp
                pictureBox2.Image = bmp;                                   //顯示於pictureBox1.
                img2 = mc.BmpToAry.Transfer(bmp);                         //將相速資料置入test.BmpToAry.Transfer函式，輸出陣列img            
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Bmp File(*.bmp)|*.bmp|jpg File(*.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);                //圖片像素資料存於變數bmp
                pictureBox2.Image = bmp;                                   //顯示於pictureBox1.
                img2 = mc.BmpToAry.Transfer(bmp);                         //將相速資料置入test.BmpToAry.Transfer函式，輸出陣列img            
            }
        }
    }
}
