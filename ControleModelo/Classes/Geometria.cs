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
        public Ponto3D(double x, double y, double z)
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
            return new Tekla.Structures.Geometry3d.Point(X, Y, Z);
        }

    }
    public class Vetor3D : Ponto3D
    {
        public Vetor3D()
        {

        }
        public Vetor3D(Tekla.Structures.Geometry3d.Vector vetorTekla)
        {
            X = vetorTekla.X;
            Y = vetorTekla.Y;
            Z = vetorTekla.Z;
        }
        public Tekla.Structures.Geometry3d.Vector RetornaVetorTekla()
        {
            return new Tekla.Structures.Geometry3d.Vector(X, Y, Z);
        }
    }
    public class PlanoControleModelo
    {
        public Ponto3D Origem { get; set; }
        public Vetor3D EixoX { get; set; }
        public Vetor3D EixoY { get; set; }
        public PlanoControleModelo()
        { 

        }
        public PlanoControleModelo(Tekla.Structures.Model.Plane planoTekla)
        {
            Origem = new Ponto3D(planoTekla.Origin);
            EixoX = new Vetor3D(planoTekla.AxisX);
            EixoY = new Vetor3D(planoTekla.AxisY);          
        }
        public Tekla.Structures.Model.Plane RetornaPlanoTekla()
        {
            var PlanoRetornar = new Tekla.Structures.Model.Plane();
            PlanoRetornar.Origin = Origem.RetornaPontoTekla();
            PlanoRetornar.AxisX = EixoX.RetornaVetorTekla();
            PlanoRetornar.AxisY = EixoY.RetornaVetorTekla();

            return PlanoRetornar;
        }
    }
}
