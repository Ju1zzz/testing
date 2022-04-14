using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing_figures
{
    public partial class Form1 : Form
    {
        const double p = 37.936;
        Point center = new Point();
        
        
        public Form1()
        {
            InitializeComponent();
            center.X = pictureBox1.Width / 2;
            center.Y = pictureBox1.Height / 2;          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.top.Text = "1";
            this.top.Text = "1";
        }
        
        void button1_Click(object sender, EventArgs e)
        {
            
            double fe = checkNumber(top.Text);
            double ad = checkNumber(bottom.Text);
            if (fe == -2 )
            {
                label2.Visible = false;
                bottom.Visible = false;
                errTop.Visible = true;
                errTop.Text = "Верхнее основание трапеции должно\n быть числом. \nНе целое число вводится с запятой";
            } 
            else if (fe == -1)
            {
                label2.Visible = false;
                bottom.Visible = false;
                errTop.Visible = true;
                errTop.Text = "Верхнее основание трапеции должно быть \n в диапазоне от 0 до 100";
            }
            else if (ad == -2 )
            {
                label2.Visible = true;
                bottom.Visible = true;
                errTop.Visible = false;
                errBottom.Text = "Нижнее основание трапеции должно\n быть числом. \nНе целое число вводится с запятой";
            }
            else if (ad == -1)
            {
                label2.Visible = true;
                bottom.Visible = true;
                errTop.Visible = false;
                errBottom.Text = "Нижнее основание трапеции должно быть \n в диапазоне от 0 до 100 \n";
            }
            else if (fe == 0 || ad == 0)
            {
                textBox3.Text = "0 см";
            }
            else
            {
                label2.Visible = true;
                bottom.Visible = true;
                errTop.Text = ""; errBottom.Text = "";
                
              double change_size = trackBar1.Value;
              pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
              using (Graphics g = Graphics.FromImage(pictureBox1.Image))
              {

                Pen wPen = new Pen(Color.Black, 2);
                Brush brush1 = Brushes.White;
                Brush brush2 = Brushes.Blue;
                double pSm = g.DpiX / p;
                double FE = fe * pSm*change_size;
                double AD = ad * pSm*change_size;      
                         
                float radius = (float)(answer(fe)[0] * pSm*change_size);
                double Answer = answer(fe)[1];

                PointF p1 = new PointF((float)(center.X - (FE / 2)), (float)(center.Y - (FE / 2)));
                PointF p2 = new PointF((float)(center.X + (FE / 2)), (float)(center.Y - (FE / 2)));
                PointF p3 = new PointF((float)(center.X + (AD / 2)), (float)(center.Y + (FE / 2)));
                PointF p4 = new PointF((float)(center.X - (AD / 2)), (float)(center.Y + (FE / 2)));
                PointF[] trapeze ={p1,p2,p3,p4};

                g.FillPolygon(brush1, trapeze);
                g.DrawPolygon(wPen, trapeze);

                PointF pT1 = new PointF((float)(center.X - (FE / 2)), (float)(center.Y - (FE / 2)));
                PointF pT2 = new PointF((float)(center.X + (FE / 2)), (float)(center.Y - (FE / 2)));
                PointF pT3 = new PointF(center.X, (float)(center.Y + (FE / 2)));
                PointF[] triangle ={pT1,pT2,pT3};
                g.FillPolygon(brush2, triangle);
                g.DrawPolygon(wPen, triangle);
                Rectangle rect = new Rectangle((int)(center.X-radius), (int)(center.Y - (FE / 2)), (int)(radius*2), (int)(radius*2));
                g.FillEllipse(brush1, rect);
                g.DrawEllipse(wPen, rect);

                textBox3.Text = Answer.ToString() + " см";
                pictureBox1.Invalidate();
                pictureBox1.Refresh();
               
              }
           

            }
        }

        private double [] answer(double side)
        {
            double between_result = 1.25 * side * side;
            double c = Math.Sqrt(between_result);
            double square_tr = 0.5 * side * side;
            double r = square_tr / ((c + c + side) / 2);
            double square_r = Math.PI * r * r;
            double answer = Math.Round(square_tr - square_r, 2);
            return new double [] { r, answer };
        }

        private double checkNumber(string str)
        {
            double num;
            if(!Double.TryParse(str, out num))
            {
                return -2;
            }
            else if (Double.Parse(str) < 0 || Double.Parse(str) > 100)
            {
                return -1;
            }
            else
            {
                return Double.Parse(str);
            }
        }

        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        
    }
}
