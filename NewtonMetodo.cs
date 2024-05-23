using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newton_Raphson
{
    internal class NewtonMetodo
    {
        public static (double root, int iterations) FindRoot(Func<double, double> f, Func<double, double> df, double x0, double tol = 1e-10, int maxIter = 100)
        {
            double x = x0;
            for (int i = 0; i < maxIter; i++)
            {
                double xNew = x - f(x) / df(x);
                if (Math.Abs(xNew - x) < tol)
                {
                    return (xNew, i + 1);
                }
                x = xNew;
            }
            return (x, maxIter);
        }
    }

    internal class Derivadas
    {
        string polinomio; //x2 - 3x - 4

        public Derivadas(string polinomio)
        {
            this.polinomio = polinomio.Replace("^", "");
        }

        public string Resolver()
        {
            string paux = polinomio.Replace("+", "_+");
            paux = paux.Replace("-", "_-");
            paux = paux.Replace(" ", "");

            string[] monomios = paux.Split('_');

            foreach(string monomio in monomios)
            {
                Console.WriteLine(monomio);
            }

            foreach (char c in polinomio)
            {

            }

            return paux;
        }
    }
}
