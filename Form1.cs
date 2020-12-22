using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        Tuple<Func<double, Vector>, Func<double, Vector, Vector>> f = new Tuple<Func<double, Vector>, Func<double, Vector, Vector>>
            ((x)=> { 
                Vector res = new Vector(0, 0);
                res.x1 = 0.5 * Math.Exp(0.25 * (x * x - 1));
                res.x2 = x * Math.Exp(0.25 * (1 - x * x));
                return res;
            },  
            (x, y)=> {
                Vector res = new Vector(0, 0);
                res.x1 = (y.x1 * y.x1) * y.x2;
                res.x2 = (y.x2 / x) - (y.x1 * y.x2 * y.x2);
                return res;
            });
        
        public Form1()
        {
            InitializeComponent();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double X0 = Double.Parse(this.textBox1.Text);
            double X1 = Double.Parse(this.textBox2.Text);
            double Y01 = Double.Parse(this.textBox4.Text);
            double Y02 = Double.Parse(this.textBox5.Text);
            Vector Y = new Vector(Y01, Y02);
            int N = Int32.Parse(this.textBox3.Text);
            FindPoints points = new FindPoints(X0, X1, N, Y, f.Item2, f.Item1);
            points.FindY();
            Tuple<double[], Vector[]> RealGrapgh = new Tuple<double[], Vector[]>(points.GetXCoords(), points.GetReal());
            Tuple<double[], Vector[]> Created = new Tuple<double[], Vector[]>(points.GetXCoords(), points.GetY());


            ClearGraphControl();
            PrintPolyline("Real Y1","Real Y2", RealGrapgh.Item1, RealGrapgh.Item2, Color.Black, Color.Cyan);
            PrintPolyline("Created Y1", "Created Y2", Created.Item1, Created.Item2, Color.Brown, Color.Pink);
            Vector err = points.GetInacuracy();
            textBox6.Text = String.Format("{0:e6}", (err.x1));
            textBox7.Text = String.Format("{0:e6}", (err.x2));
        }
        private void ClearGraphControl()
        {
            this.zedGraphControl1.GraphPane.CurveList.Clear();
            this.zedGraphControl1.GraphPane.XAxis.Scale.MinAuto = true;
            this.zedGraphControl1.GraphPane.XAxis.Scale.MaxAuto = true;
            this.zedGraphControl1.GraphPane.YAxis.Scale.MinAuto = true;
            this.zedGraphControl1.GraphPane.YAxis.Scale.MaxAuto = true;
            this.zedGraphControl1.GraphPane.AxisChange();
        }
        void PrintPolyline(string legend1, string legend2, double[] x, Vector[] y, Color color1, Color color2)
        {

            double[] _y1 = (from c in y select c.x1).ToArray();
            double[] _y2 = (from c in y select c.x2).ToArray();
            LineItem curve1 = this.zedGraphControl1.GraphPane.AddCurve(legend1, x, _y1, color1, SymbolType.None);
            curve1.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            curve1.Line.Width = 1f;
            LineItem curve2 = this.zedGraphControl1.GraphPane.AddCurve(legend2, x, _y2, color2, SymbolType.None);
            curve2.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            curve2.Line.Width = 1f;
        }


    }
}
