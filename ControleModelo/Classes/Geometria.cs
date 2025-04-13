using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleModelo.Classes
{
    public class Geometria
    {

    }
    public class Ponto3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Ponto3D()
        {

        }
        public Ponto3D (double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Ponto3D(Tekla.Structures.Geometry3d.Point pontoTekla)
        {
            X = pontoTekla.X;
            Y = pontoTekla.Y;
            Z = pontoTekla.Z;

        }
        public Tekla.Structures.Geometry3d.Point RetornaPontoTekla()
        {
            return new Tekla.Structures.Geometry3d.Point (X, Y, Z);
        }

    }
}
