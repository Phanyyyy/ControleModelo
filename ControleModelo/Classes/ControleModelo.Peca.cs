using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public NumeracaoControleModelo NumeracaoPeca { get; set; }
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
        public ControleModeloOffset PontoInicialOffset { get; set; }
        public ControleModeloOffset PontoFinalOffset { get; set; }



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
            PosicaoDepth = Ferramentas.ConvertePositionDepth(vigaTekla.Position.Depth);
            PontoInicialOffset = new ControleModeloOffset(vigaTekla.StartPointOffset);
            PontoFinalOffset = new ControleModeloOffset(vigaTekla.EndPointOffset);





        }
        public Tekla.Structures.Model.Beam RetornaVigaTekla()
        {
            var VigaTekla = new Tekla.Structures.Model.Beam();
            VigaTekla.Name = Nome;
            VigaTekla.Profile.ProfileString = Perfil;
            VigaTekla.Material.MaterialString = Material;
            VigaTekla.Finish = Finish;
            VigaTekla.Class = Class;
            VigaTekla.StartPoint = PontoInicial.RetornaPontoTekla();
            VigaTekla.EndPoint = PontoFinal.RetornaPontoTekla();
            VigaTekla.AssemblyNumber = NumeracaoConjunto.RetornaNumeracaoTekla();
            VigaTekla.PartNumber = NumeracaoPeca.RetornaNumeracaoTekla();
            VigaTekla.Position.PlaneOffset = PosicaoPlaneOffset;
            VigaTekla.Position.RotationOffset = PosicaoRotationOffset;
            VigaTekla.Position.DepthOffset = PosicaoDepthOffset;
            VigaTekla.Position.Plane = Ferramentas.ConvertePositionPlaneTekla(PosicaoPlane);
            VigaTekla.Position.Rotation = Ferramentas.ConvertePositionRotationTekla(PosicaoRotation);
            VigaTekla.Position.Depth = Ferramentas.ConvertePositionDepthTekla(PosicaoDepth);
            VigaTekla.StartPointOffset = PontoInicialOffset.RetornaOffsetTekla();
            VigaTekla.EndPointOffset = PontoFinalOffset.RetornaOffsetTekla();
            return VigaTekla;
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
        public double Dz { get; set; }
        public ControleModeloOffset() { }
        public ControleModeloOffset(Offset offsetTekla)
        {
            Dx = offsetTekla.Dx;
            Dy = offsetTekla.Dy;
            Dz = offsetTekla.Dz;

        }
        public Offset RetornaOffsetTekla()
        {
            var offset = new Offset();
            offset.Dx = Dx;
            offset.Dy = Dy;
            offset.Dz = Dz;
            return offset;
        }
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
    



