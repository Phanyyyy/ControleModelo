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
            if (ArquivoSalvar.ShowDialog()==DialogResult.OK)
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
                foreach (var viga in ModeloCurso.ObjetosModelo)
                {
                    if (viga is VigaModelo)
                    {
                        var Vigamod = viga as VigaModelo;
                        var VigaTekla = Vigamod.RetornaVigaTekla();
                        VigaTekla.Insert();

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
                if(Objeto is Tekla.Structures.Model.Beam)
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
                else if(Objeto is Tekla.Structures.Model.ContourPlate)
                {
                    var ChapaContorno = Objeto as Tekla.Structures.Model.ContourPlate;
                    foreach(ContourPoint ponto in ChapaContorno.Contour.ContourPoints)
                    {
                        ponto.Chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_ARC;
                        ponto.Chamfer.X = 300;
                    }
                    ChapaContorno.Modify();
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

            var ChapaContorno = new ContourPlate();
            ChapaContorno.Profile.ProfileString = "CH9.5";
            ChapaContorno.Material.MaterialString = "A36";
            ChapaContorno.Position.Depth = Position.DepthEnum.FRONT;

            //ContourPoint = Geometry3d.Point + Chamfer

            var Ponto1 = new ContourPoint();
            Ponto1.Chamfer.Type = Chamfer.ChamferTypeEnum.CHAMFER_LINE;
            Ponto1.Chamfer.X = 100;
            Ponto1.Chamfer.Y = 100;
        }
    }
}
