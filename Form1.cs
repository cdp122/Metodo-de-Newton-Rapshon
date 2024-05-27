using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Newton_Raphson
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Derivadas d = new Derivadas(txtFx.Text);
            lblDerivada.Text = d.Derivar();

            txtX1.Text = BuscarX1(new Polinomio(txtFx.Text)) + "";
        }

        private int BuscarX1(Polinomio p)
        {
            double aux1, aux2;
            aux1 = ResolverPolinomio(p, -1);
            aux2 = ResolverPolinomio(p, 1);
            int suma = 1;
            int resta = -1;
            int num = 0;

            while(Math.Abs(aux1) > 1 && Math.Abs(aux2) > 1)
            {
                aux1 = ResolverPolinomio(p, --resta);
                aux2 = ResolverPolinomio(p, ++suma);
            }

            if(Math.Abs(aux1) > Math.Abs(aux2)) num = suma; else num = resta;

            return num;
        }

        private double ResolverPolinomio(Polinomio p, double x)
        {
            double resultado = 0;
            foreach(Monomio monomio in p.Monomios)
            {
                resultado += monomio.Resolver(x);
            }
            return resultado;
        }
    }
}
