using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControleModelo.Classes;
using Tekla.Structures.Model;
using Tekla.Structures.ModelInternal;

namespace ControleModelo
{
    public partial class Form1 : Form
    {
        Tekla.Structures.Model.UI.ModelObjectSelector SelecionadorDeObjetos = new Tekla.Structures.Model.UI.ModelObjectSelector();
        Tekla.Structures.Model.Model Modelo = new Tekla.Structures.Model.Model();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog ArquivoSalvar = new SaveFileDialog();
            ArquivoSalvar.Filter = "Arquivo Controle Modelo | *.MMD";
            if (ArquivoSalvar.ShowDialog() == DialogResult.OK)
            {

                ControleModelo.Classes.ControleModelo ModeloCurso = new ControleModelo.Classes.ControleModelo();
                foreach (var objeto in SelecionadorDeObjetos.GetSelectedObjects())
                {
                    if (objeto is Tekla.Structures.Model.Beam)
                    {
                        var Viga = objeto as Tekla.Structures.Model.Beam;
                        var VigaSistema = new VigaModelo(Viga);
                        ModeloCurso.ObjetosModelo.Add(VigaSistema);

                    }
                    else if (objeto is Tekla.Structures.Model.ContourPlate)
                    {
                        var ChapaContorno = objeto as Tekla.Structures.Model.ContourPlate;
                        var ChapaContornoSistema = new ChapaContornoModelo(ChapaContorno);
                        ModeloCurso.ObjetosModelo.Add(ChapaContornoSistema);
                    }
                    else if (objeto is Tekla.Structures.Model.PolyBeam)
                    {
                        var PB = objeto as Tekla.Structures.Model.PolyBeam;
                        var PolyBeamSistema = new PolyBeamModelo(PB);
                        ModeloCurso.ObjetosModelo.Add(PolyBeamSistema);
                    }
                    else if (objeto is Tekla.Structures.Model.BentPlate)
                    {
                        var BP = objeto as Tekla.Structures.Model.BentPlate;
                        var BentPlateSistema = new BentPlateModelo(BP);
                        ModeloCurso.ObjetosModelo.Add(BentPlateSistema);
                    }
                }

                ModeloCurso.Salvar(ArquivoSalvar.FileName);
                MessageBox.Show("Arquivo salvo");
            }
            else
            {
                MessageBox.Show("Nenhuma Opção Selecionada, o modelo não será salvo!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ArquivoAbrir = new OpenFileDialog();
            ArquivoAbrir.Filter = "Arquivo Controle Modelo | *.MMD";
            ArquivoAbrir.Multiselect = false;
            if (ArquivoAbrir.ShowDialog() == DialogResult.OK)
            {

                var ModeloCurso = ControleModelo.Classes.ControleModelo.Carregar(ArquivoAbrir.FileName);
                foreach (var ObjetoMod in ModeloCurso.ObjetosModelo)
                {
                    if (ObjetoMod is VigaModelo)
                    {
                        var Vigamod = ObjetoMod as VigaModelo;
                        var VigaTekla = Vigamod.RetornaVigaTekla();
                        VigaTekla.Insert();
                    }
                    else if (ObjetoMod is ChapaContornoModelo)
                    {
                        var CCMod = ObjetoMod as ChapaContornoModelo;
                        var ChapaTekla = CCMod.RetornaContourPlateTekla();
                        ChapaTekla.Insert();
                        ChapaTekla.Modify();
                    }
                    else if (ObjetoMod is PolyBeamModelo)
                    {
                        var PBMod = ObjetoMod as PolyBeamModelo;
                        var PolyBeamTekla = PBMod.RetornaPolyBeamTekla();
                        PolyBeamTekla.Insert();
                    }
                    else if (ObjetoMod is BentPlateModelo)
                    {
                        var BPMod = ObjetoMod as BentPlateModelo;
                        var BentPlateTekla = BPMod.CriarBentPlateNoTekla();

                    }
                }
                Modelo.CommitChanges();
                MessageBox.Show(ModeloCurso.ObjetosModelo.Count.ToString());
            }
            else
            {
                MessageBox.Show("Nenhum arquivo selecionado!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var ObjetosModelo = SelecionadorDeObjetos.GetSelectedObjects();
            foreach (var Objeto in ObjetosModelo)
            {
                if (Objeto is Tekla.Structures.Model.Beam)
                {
                    var VigaTekla = Objeto as Tekla.Structures.Model.Beam;
                    //VigaTekla.Position.Plane = Tekla.Structures.Model.Position.PlaneEnum.LEFT;
                    //VigaTekla.Position.PlaneOffset = 100.5;

                    //VigaTekla.Position.Rotation = Tekla.Structures.Model.Position.RotationEnum.FRONT;
                    //VigaTekla.Position.RotationOffset = 45;         

                    //VigaTekla.Position.Depth = Tekla.Structures.Model.Position.DepthEnum.FRONT;
                    //VigaTekla.Position.DepthOffset = 100;

                    VigaTekla.StartPointOffset.Dx = 50;
                    VigaTekla.EndPointOffset.Dx = -50;

                    VigaTekla.Modify();

                }
                else if (Objeto is Tekla.Structures.Model.ContourPlate)
                {
                    var ChapaContorno = Objeto as Tekla.Structures.Model.ContourPlate;

                    ChapaContorno.Contour.ContourPoints.RemoveAt(5);
                    //var PontoAdicionar = new ContourPoint();
                    //PontoAdicionar.X = 2324.8;
                    //PontoAdicionar.Y = 3626.1;

                    //ChapaContorno.Contour.ContourPoints.Insert(5, PontoAdicionar);

                    //int contador = 0;
                    //foreach(ContourPoint ponto in ChapaContorno.Contour.ContourPoints)
                    //{
                    //    var vigaRef = new Beam();
                    //    vigaRef.Profile.ProfileString = "D10";
                    //    vigaRef.Material.MaterialString = "A36";
                    //    vigaRef.StartPoint = new Tekla.Structures.Geometry3d.Point(ponto);
                    //    vigaRef.EndPoint = new Tekla.Structures.Geometry3d.Point(ponto);
                    //    vigaRef.EndPoint.Z += 100;
                    //    vigaRef.Name = contador.ToString();
                    //    vigaRef.Class = contador.ToString();
                    //    vigaRef.Insert();

                    //    contador++;
                    //}
                    ChapaContorno.Modify();
                }
                else if (Objeto is Tekla.Structures.Model.PolyBeam)
                {
                    var PolyB = Objeto as Tekla.Structures.Model.PolyBeam;
                    foreach (ContourPoint ponto in PolyB.Contour.ContourPoints)
                    {
                        MessageBox.Show(ponto.ToString() + "/n" + ponto.Chamfer.Type.ToString());
                    }
                }
                else if (Objeto is Tekla.Structures.Model.BentPlate)
                {
                    var BentP = Objeto as BentPlate;
                    var Poligonos = BentP.Geometry.GetGeometryLegSections();
                    var Chapa1 = new ContourPlate();
                    var Chapa2 = new ContourPlate();

                    Chapa1.Name = Chapa2.Name = BentP.Name;
                    Chapa1.Profile = Chapa2.Profile = BentP.Profile;
                    Chapa1.Material = Chapa2.Material = BentP.Material;
                    Chapa1.Finish = Chapa2.Finish = BentP.Finish;
                    Chapa1.Class = Chapa2.Class = BentP.Class;
                    Chapa1.PartNumber = Chapa2.PartNumber = BentP.PartNumber;
                    Chapa1.AssemblyNumber = Chapa2.AssemblyNumber = BentP.AssemblyNumber;

                    if (Poligonos[0].GeometryNode is PolygonNode)
                    {
                        var ContornoChapa1 = Poligonos[0].GeometryNode as PolygonNode;
                        foreach (ContourPoint ponto in ContornoChapa1.Contour.ContourPoints)
                        {
                            Chapa1.Contour.ContourPoints.Add(ponto);
                        }

                    }
                    Chapa1.Insert();
                    if (Poligonos[1].GeometryNode is PolygonNode)
                    {
                        var ContornoChapa2 = Poligonos[1].GeometryNode as PolygonNode;
                        foreach (ContourPoint ponto in ContornoChapa2.Contour.ContourPoints)
                        {
                            Chapa2.Contour.ContourPoints.Add(ponto);
                        }
                    }
                    Chapa2.Insert();
                }
            }
            Modelo.CommitChanges();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //P1 = 0,0,0
            //P2 = 1000,0,0
            //P3 = 1000,1000,0
            //P4 = 0,1000,0

            var PolyViga = new PolyBeam();
            PolyViga.Profile.ProfileString = "CH.9";
            PolyViga.Material.MaterialString = "A36";
            PolyViga.Position.Depth = Position.DepthEnum.FRONT;

            //ContourPoint = Geometry3d.Point + Chamfer
            //Ponto1
            var Ponto1 = new ContourPoint();
            Ponto1.X = 0;
            Ponto1.Y = 0;
            Ponto1.Z = 0;
            Ponto1.Chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_LINE;
            Ponto1.Chamfer.X = 100;
            Ponto1.Chamfer.Y = 100;

            //Ponto2
            var Ponto2 = new ContourPoint();
            Ponto2.X = 1000;

            //Ponto3
            var Ponto3Coordenada = new Tekla.Structures.Geometry3d.Point(1000, 1000, 0);
            var Ponto3Chamfer = new Chamfer();
            Ponto3Chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_SQUARE;
            Ponto3Chamfer.X = 400;
            Ponto3Chamfer.Y = 100;
            var Ponto3 = new ContourPoint(Ponto3Coordenada, Ponto3Chamfer);

            //Ponto4
            var Ponto4 = new ContourPoint(new Tekla.Structures.Geometry3d.Point(0, 1000, 0), new Chamfer());

            PolyViga.Contour.AddContourPoint(Ponto1);
            PolyViga.Contour.AddContourPoint(Ponto2);
            PolyViga.Contour.AddContourPoint(Ponto3);
            PolyViga.Contour.AddContourPoint(new Tekla.Structures.Geometry3d.Point(0, 1000, 0) as ContourPoint);
            PolyViga.Insert();

            Modelo.CommitChanges();



        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<ContourPlate> Chapas = new List<ContourPlate>();
            foreach (ContourPlate chapa in SelecionadorDeObjetos.GetSelectedObjects())
            {
                Chapas.Add(chapa);
            }
            var BentPlate = Tekla.Structures.Model.Operations.Operation.CreateBentPlateByParts(Chapas[0], Chapas[1]);
            Modelo.CommitChanges();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (var ObjetoModelo in SelecionadorDeObjetos.GetSelectedObjects())
            {
                if (ObjetoModelo is Tekla.Structures.Model.Part)
                {
                    var Peca = ObjetoModelo as Tekla.Structures.Model.Part;
                    
                    //var Fit = new Tekla.Structures.Model.Fitting();

                    //Fit.Father = Peca;

                    //var PlanodeCorte = new Tekla.Structures.Model.Plane();
                    //PlanodeCorte.Origin = new Tekla.Structures.Geometry3d.Point(0, 0, 0);
                    //PlanodeCorte.AxisX = new Tekla.Structures.Geometry3d.Vector(0, 100, 0);
                    //PlanodeCorte.AxisY = new Tekla.Structures.Geometry3d.Vector(0, 0, 100);
                    //Fit.Plane = PlanodeCorte;

                    //Fit.Insert();

                    foreach (var boo in Peca.GetBooleans())
                    {
                        var BooleanBase = boo as Tekla.Structures.Model.Boolean;
                        var fit = BooleanBase as Tekla.Structures.Model.Fitting;

                    }
                    
                    

                }

            }
            Modelo.CommitChanges();
        }
    }
}
