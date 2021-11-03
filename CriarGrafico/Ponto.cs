using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriarGrafico
{
    public class Ponto
    {
        private int cor;
        private double x;
        private double y;

        public Ponto(double x, double y, int cor)
        {
            this.cor = cor;
            this.x = x;
            this.y = y;
        }

        public double getX()
        {
            return this.x;
        }

        public double getY()
        {
            return this.y;
        }

        public int getCor()
        {
            return this.cor;
        }
    }

}
