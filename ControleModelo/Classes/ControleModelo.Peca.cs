using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace ControleModelo.Classes
{
    public class PecaModelo : ObjetoModelo
    {
        public string Nome { get; set; }
        public string Perfil { get; set; }
        public string Material { get; set; }
        public string Finish { get; set; }
        public string Class { get; set; }
        public NumeracaoControleModelo NumeracaoPeca {  get; set; }
        public NumeracaoControleModelo NumeracaoConjunto { get; set; }

        public PecaModelo() { }


    }

    public class VigaModelo : PecaModelo
    {
        public Ponto3D PontoInicial { get; set; }
        public Ponto3D PontoFinal { get; set; }
        public ControleModeloPositionPlane PosicaoPlane { get; set; }
        public ControleModeloPositionRotation PosicaoRotation { get; set; }
        public ControleModeloPositionDepth PosicaoDepth { get; set; }
        public double PosicaoPlaneOffset { get; set; }
        public double PosicaoRotationOffset { get; set; }
        public double PosicaoDepthOffset { get; set; }


        public VigaModelo() { }
        public VigaModelo(Tekla.Structures.Model.Beam vigaTekla)
        {
            Perfil = vigaTekla.Profile.ProfileString;
            Material = vigaTekla.Material.MaterialString;
            Nome = vigaTekla.Name;
            Finish = vigaTekla.Finish;
            Class = vigaTekla.Class;
            PontoInicial = new Ponto3D(vigaTekla.StartPoint);
            PontoFinal = new Ponto3D(vigaTekla.EndPoint);
            NumeracaoPeca = new NumeracaoControleModelo(vigaTekla.PartNumber);
            NumeracaoConjunto = new NumeracaoControleModelo(vigaTekla.AssemblyNumber);
            PosicaoPlaneOffset = vigaTekla.Position.PlaneOffset;
            PosicaoRotationOffset = vigaTekla.Position.RotationOffset;
            PosicaoDepthOffset = vigaTekla.Position.DepthOffset;
            PosicaoPlane = Ferramentas.ConvertePositionPlane(vigaTekla.Position.Plane);
            PosicaoRotation = Ferramentas.ConvertePositionRotation(vigaTekla.Position.Rotation);



            
            

        }
    }
    public enum ControleModeloPositionPlane
    {
        Middle,
        Right,
        Left

    }
    public enum ControleModeloPositionRotation
    {
        Front,
        Top,
        Back,
        Below

    }
    public enum ControleModeloPositionDepth
    {
        Middle,
        Front,
        Behind

    }
    public class ControleModeloOffset
    {
        public double Dx { get; set; }
        public double Dy { get; set; }
        public double Dz {  get; set; }
        public ControleModeloOffset() { }   
        public ControleModeloOffset(Offset offsetTekla)
        {
            Dx = offsetTekla.Dx;
            Dy = offsetTekla.Dy;
            Dz = offsetTekla .Dz;

        }
    public class NumeracaoControleModelo
    {
        public string Prefixo { get; set; }
        public int StartNumber { get; set; }
        public NumeracaoControleModelo()
        { }
        public NumeracaoControleModelo(NumberingSeries NumeracaoTekla)
        {
            Prefixo = NumeracaoTekla.Prefix;
            StartNumber = NumeracaoTekla.StartNumber;

        }
        public NumberingSeries RetornaNumeracaoTekla()
        {
            var NumberingTekla = new NumberingSeries();
            NumberingTekla.StartNumber = StartNumber;
            NumberingTekla.Prefix = Prefixo;
            return NumberingTekla;
        }
    }
}



