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
    public class BentPlateModelo : PecaModelo
    {
        public List<ContourPointModelo> ContornoChapa1 { get; set; }
        public List<ContourPointModelo> ContornoChapa2 { get; set; }

        public BentPlateModelo()
        {
            ContornoChapa1 = new List<ContourPointModelo>();
            ContornoChapa2 = new List<ContourPointModelo>();
        }
        public BentPlateModelo(Tekla.Structures.Model.BentPlate bentPlate)
        {
            ID = bentPlate.Identifier.GUID;
            Nome = bentPlate.Name;
            Perfil = bentPlate.Profile.ProfileString;
            Material = bentPlate.Material.MaterialString;
            Class = bentPlate.Class;
            Finish = bentPlate.Finish;
            NumeracaoPeca = new NumeracaoControleModelo(bentPlate.PartNumber);
            NumeracaoConjunto = new NumeracaoControleModelo(bentPlate.AssemblyNumber);
            ContornoChapa1 = new List<ContourPointModelo>();
            ContornoChapa2 = new List<ContourPointModelo>();

            var Poligonos = bentPlate.Geometry.GetGeometryLegSections();
            if (Poligonos[0].GeometryNode is PolygonNode)
            {
                var ContornoChapa = Poligonos[0].GeometryNode as PolygonNode;
                foreach(ContourPoint ponto in ContornoChapa.Contour.ContourPoints)
                {
                    ContornoChapa1.Add(new ContourPointModelo(ponto));
                }
            }
            if (Poligonos[1].GeometryNode is PolygonNode)
            {
                var ContornoChapa = Poligonos[1].GeometryNode as PolygonNode;
                foreach (ContourPoint ponto in ContornoChapa.Contour.ContourPoints)
                {
                    ContornoChapa2.Add(new ContourPointModelo(ponto));
                }
            }

        }
        public Tekla.Structures.Model.BentPlate CriarBentPlateNoTekla()
        {
            var ChapaContorno1 = new ContourPlate();
            var ChapaContorno2 = new ContourPlate();

            ChapaContorno1.Name = ChapaContorno2.Name;
            ChapaContorno1.Profile.ProfileString = ChapaContorno2.Profile.ProfileString = Perfil;
            ChapaContorno1.Material.MaterialString = ChapaContorno2.Material.MaterialString = Material;
            ChapaContorno1.Finish = ChapaContorno2.Finish = Finish;
            ChapaContorno1.Class = ChapaContorno2.Class = Class;

            ChapaContorno1.PartNumber = ChapaContorno2.PartNumber = NumeracaoPeca.RetornaNumeracaoTekla();
            ChapaContorno1.AssemblyNumber = ChapaContorno2.AssemblyNumber = NumeracaoConjunto.RetornaNumeracaoTekla();

            foreach(ContourPointModelo ponto in ContornoChapa1)
            {
                ChapaContorno1.Contour.ContourPoints.Add(ponto.RetornaContourPointTekla());

            }
            foreach (ContourPointModelo ponto in ContornoChapa1)
            {
                ChapaContorno2.Contour.ContourPoints.Add(ponto.RetornaContourPointTekla());

            }
            ChapaContorno1.Insert();
            ChapaContorno2.Insert();

            var BentPlateRetornar = Tekla.Structures.Model.Operations.Operation.CreateBentPlateByParts(ChapaContorno1, ChapaContorno2);
            return BentPlateRetornar;

        }
    }
    public class PolyBeamModelo : PecaModelo
    {
        public List<ContourPointModelo> ContornoControleModelo { get; set; }
        public ControleModeloPositionPlane PosicaoPlane { get; set; }
        public ControleModeloPositionRotation PosicaoRotation { get; set; }
        public ControleModeloPositionDepth PosicaoDepth { get; set; }
        public double PosicaoPlaneOffset { get; set; }
        public double PosicaoRotationOffset { get; set; }
        public double PosicaoDepthOffset { get; set; }
        public PolyBeamModelo()
        {
            ContornoControleModelo = new List<ContourPointModelo>();
        }
        public PolyBeamModelo(PolyBeam PolyBeamTekla)
        {
            ID = PolyBeamTekla.Identifier.GUID;
            Nome = PolyBeamTekla.Name;
            Perfil = PolyBeamTekla.Profile.ProfileString;
            Material = PolyBeamTekla.Material.MaterialString;
            Finish = PolyBeamTekla.Finish;
            Class = PolyBeamTekla.Class;
            NumeracaoPeca = new NumeracaoControleModelo(PolyBeamTekla.PartNumber);
            NumeracaoConjunto = new NumeracaoControleModelo(PolyBeamTekla.AssemblyNumber);
            PosicaoPlaneOffset = PolyBeamTekla.Position.PlaneOffset;
            PosicaoRotationOffset = PolyBeamTekla.Position.RotationOffset;
            PosicaoDepthOffset = PolyBeamTekla.Position.DepthOffset;
            PosicaoPlane = Ferramentas.ConvertePositionPlane(PolyBeamTekla.Position.Plane);
            PosicaoRotation = Ferramentas.ConvertePositionRotation(PolyBeamTekla.Position.Rotation);
            PosicaoDepth = Ferramentas.ConvertePositionDepth(PolyBeamTekla.Position.Depth);
            ContornoControleModelo = new List<ContourPointModelo>();
            foreach (ContourPoint Ponto in PolyBeamTekla.Contour.ContourPoints)
            {
                ContornoControleModelo.Add(new ContourPointModelo(Ponto));
            }
        }
        public PolyBeam RetornaPolyBeamTekla()
        {
            var PB = new PolyBeam();
            PB.Name = Nome;
            PB.Profile.ProfileString = Perfil;
            PB.Finish = Finish;
            PB.Class = Class;
            PB.AssemblyNumber = NumeracaoConjunto.RetornaNumeracaoTekla();
            PB.Position.PlaneOffset = PosicaoPlaneOffset;
            PB.Position.RotationOffset = PosicaoRotationOffset;
            PB.Position.DepthOffset = PosicaoDepthOffset;
            PB.Position.Plane = Ferramentas.ConvertePositionPlaneTekla(PosicaoPlane);
            PB.Position.Rotation = Ferramentas.ConvertePositionRotationTekla(PosicaoRotation);
            PB.Position.Depth = Ferramentas.ConvertePositionDepthTekla(PosicaoDepth);
            
         
            foreach (var Ponto in ContornoControleModelo)
            {
                PB.Contour.ContourPoints.Add(Ponto.RetornaContourPointTekla());
            }
            return PB;
        }
    }
    public class ChapaContornoModelo : PecaModelo
    {
        public List<ContourPointModelo> ContornoControleModelo {  get; set; }
        public ControleModeloPositionDepth PosicaoDepth { get; set; }
        public double PosicaoDepthOffset { get; set; }
        public ChapaContornoModelo ()
        {
            ContornoControleModelo = new List<ContourPointModelo>();
        }
        public ChapaContornoModelo(ContourPlate ChapaContornoTekla)
        {
            ID = ChapaContornoTekla.Identifier.GUID;
            Nome = ChapaContornoTekla.Name;
            Perfil = ChapaContornoTekla.Profile.ProfileString;
            Finish = ChapaContornoTekla.Finish;
            Class = ChapaContornoTekla.Class;
            NumeracaoPeca = new NumeracaoControleModelo(ChapaContornoTekla.PartNumber);
            NumeracaoConjunto = new NumeracaoControleModelo(ChapaContornoTekla.AssemblyNumber);
            PosicaoDepth = Ferramentas.ConvertePositionDepth(ChapaContornoTekla.Position.Depth);
            PosicaoDepthOffset = ChapaContornoTekla.Position.DepthOffset;
            ContornoControleModelo = new List<ContourPointModelo>();
            foreach(ContourPoint Ponto in ChapaContornoTekla.Contour.ContourPoints)
            {
                ContornoControleModelo.Add(new ContourPointModelo(Ponto));
            }

        }
        public ContourPlate RetornaContourPlateTekla()
        {
            var CP = new ContourPlate();
            CP.Name = Nome;
            CP.Profile.ProfileString = Perfil;
            CP.Finish = Finish;
            CP.Class = Class;
            CP.AssemblyNumber = NumeracaoConjunto.RetornaNumeracaoTekla();
            CP.PartNumber = NumeracaoPeca.RetornaNumeracaoTekla();
            CP.Position.DepthOffset = PosicaoDepthOffset;
            CP.Position.Depth = Ferramentas.ConvertePositionDepthTekla(PosicaoDepth);
            foreach (var Ponto in ContornoControleModelo)
            {
                CP.Contour.ContourPoints.Add(Ponto.RetornaContourPointTekla());
            }
            return CP;
        }


    }

    public class ContourPointModelo : Ponto3D
    {
        public ChamferControleModelo Chanfro { get; set; }
        public ContourPointModelo () 
        {

        }
        public ContourPointModelo(ContourPoint ContourPointTekla)
        {
            X = ContourPointTekla.X;
            Y = ContourPointTekla.Y;
            Z = ContourPointTekla.Z;

            Chanfro = new ChamferControleModelo(ContourPointTekla.Chamfer);
        }
        public ContourPoint RetornaContourPointTekla()
        {
            var CP = new ContourPoint();
            CP.X = X;
            CP.Y = Y;
            CP.Z = Z;
            CP.Chamfer = Chanfro.RetornaChamferTekla();
            return CP;
        }
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
            ID = vigaTekla.Identifier.GUID;
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

    public class ChamferControleModelo
    {
        public ChamferTypeControleModelo TipoChanfro { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double DZ1 { get; set; }
        public double DZ2 { get; set; }
        public ChamferControleModelo()
        {
           
          
        }
        public ChamferControleModelo(Chamfer ChamferTekla)
        {
            X = ChamferTekla.X;
            Y = ChamferTekla.Y;
            DZ1 = ChamferTekla.DZ1;
            DZ2 = ChamferTekla.DZ2;
            var EnumRef = ChamferTypeControleModelo.CHAMFER_NONE;
            Enum.TryParse<ChamferTypeControleModelo>(ChamferTekla.Type.ToString(), out EnumRef);
            TipoChanfro = EnumRef;
        }

        public Chamfer RetornaChamferTekla()
        {
            var cf = new Chamfer();
            cf.X = X;
            cf.Y = Y;
            cf.DZ1 = DZ1;
            cf.DZ2 = DZ2;
            var EnumRef = Chamfer.ChamferTypeEnum.CHAMFER_NONE;
            Enum.TryParse<Chamfer.ChamferTypeEnum>(TipoChanfro.ToString(), out EnumRef);
            cf.Type = EnumRef;
            return cf;
        }

    }
    public enum ChamferTypeControleModelo
    {
        CHAMFER_ARC,
        CHAMFER_SQUARE_PARALLEL,
        CHAMFER_LINE_AND_ARC,
        CHAMFER_ARC_POINT,
        CHAMFER_NONE,
        CHAMFER_LINE,
        CHAMFER_SQUARE,
        CHAMFER_ROUNDING
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
    



