using System;

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
        Monomio[] monomios;

        /// <summary>
        /// Crea un polinomio listo para ser derivado. 
        /// </summary>
        /// <param name="polinomio">Si el polinomio contiene '^' será eliminado automaticamente </param>
        public Derivadas(string polinomio)
        {
            this.polinomio = polinomio.Replace("^", "");
        }

        /// <summary>
        /// Algoritmo de derivada, solo para derivadas sencillas.
        /// </summary>
        /// <returns></returns>
        public string Derivar()
        {
            string paux = polinomio.Replace("+", "_+");
            paux = paux.Replace("-", "_-");
            paux = paux.Replace(" ", "");
            string[] monomios = paux.Split('_');
            this.monomios = new Monomio[monomios.Length];
            string resultado = "";

            for (int i = 0; i < monomios.Length; i++)
            {
                this.monomios[i] = new Monomio(monomios[i]);

                if (this.monomios[i].variable != ' ')
                    this.monomios[i] = new Monomio(this.monomios[i].coeficiente * this.monomios[i].potencia,
                        this.monomios[i].variable, --this.monomios[i].potencia);
                else this.monomios[i] = new Monomio(0,' ',0);
            }

            foreach (Monomio monomio in this.monomios)
            {
                if (monomio.coeficiente > 0) { resultado += " + " + monomio; }
                else resultado += " " + monomio;
            }

            if (resultado[1] == '+') resultado = resultado.Substring(2, resultado.Length - 3);

            return resultado;
        }

        /// <summary>
        /// Regresa el polinomio combinando todos los monomios derivados
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string derivada = "";

            foreach (Monomio monomio in this.monomios)
            {
                if (monomio.coeficiente > 0) { derivada += "+" + monomio; }
                else derivada += monomio;
            }

            if (derivada[0] == '+') derivada = derivada.Substring(1, derivada.Length - 2);

            return derivada;
        }
    }

    internal class Monomio
    {
        public double coeficiente, potencia;
        public char variable;

        /// <summary>
        /// Consigue un monomio. 
        /// </summary>
        /// <param name="coeficiente">coeficiente o 'número' de la incognita</param>
        /// <param name="potencia">superindice que indica la pontencia de la incognita</param>
        /// <param name="variable">la incógnita, suele ser 'x'</param>
        public Monomio(double coeficiente, char variable, double potencia)
        {
            this.coeficiente = coeficiente;
            this.potencia = potencia;
            this.variable = variable;
        }

        /// <summary>
        /// Recibe un string general y lo convierte a monomio
        /// </summary>
        /// <param name="monomio">String con estructura ax^n</param>
        public Monomio(string monomio)
        {
            int inc = 0;
            foreach (char c in monomio)
            {
                inc++;
                if (char.IsLetter(c)) break;
            }

            if (inc - 1 > 0) try { coeficiente = double.Parse(monomio.Substring(0, inc - 1)); }
                catch { coeficiente = double.Parse(monomio.Substring(0, inc)); }
            else coeficiente = 1;
            if (!char.IsDigit(monomio[inc - 1])) variable = monomio[inc - 1];
            else variable = ' ';
            if (monomio.Length - inc > 0) { potencia = double.Parse(monomio.Substring(inc, monomio.Length - inc)); }
            else potencia = 1;
        }

        /// <summary>
        /// Regresa el monomio de la forma mas limpia posible
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (coeficiente == 0) return "0";
            else
            {
                string monomio = "";
                if (coeficiente != 1) monomio += coeficiente.ToString();
                if (coeficiente < 0) monomio = "- " + coeficiente * -1;
                if (potencia != 0)
                {
                    monomio += $"{variable}";
                    if (potencia != 1) monomio += "^" + potencia.ToString();
                }
                return monomio;
            }
        }
    }
}
