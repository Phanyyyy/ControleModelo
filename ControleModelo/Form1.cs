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
            ControleModelo.Classes.ControleModelo ModeloCurso = new ControleModelo.Classes.ControleModelo();
                
            Tekla.Structures.Model.UI.ModelObjectSelector SelecionadorDeObjetos = new Tekla.Structures.Model.UI.ModelObjectSelector();
            foreach (var objeto in SelecionadorDeObjetos.GetSelectedObjects())
            {
                if (objeto is Tekla.Structures.Model.Beam)
                {
                    var Viga = objeto as Tekla.Structures.Model.Beam;
                    var VigaSistema = new VigaModelo(Viga);
                    ModeloCurso.ObjetosModelo.Add(VigaSistema);
                  

                }


            }
            ModeloCurso.Salvar();
            MessageBox.Show(ModeloCurso.ObjetosModelo.Count.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var ModeloTekla = new Tekla.Structures.Model.Model();
            var ModeloCurso = ControleModelo.Classes.ControleModelo.Carregar("C:\\arquivoteste.MMD");
            foreach (var viga in ModeloCurso.ObjetosModelo)
            {
                if(viga is VigaModelo)
                {
                    var Vigamod = viga as VigaModelo;
                    var VigaTekla = new Tekla.Structures.Model.Beam();
                    VigaTekla.Profile.ProfileString = Vigamod.Perfil;
                    VigaTekla.Material.MaterialString = Vigamod.Material;
                    VigaTekla.Name = Vigamod.Nome;
                    VigaTekla.Finish = Vigamod.Finish;
                    VigaTekla.Class = Vigamod.Class;

                    VigaTekla.StartPoint = new Tekla.Structures.Geometry3d.Point(Vigamod.PontoInicial.X, Vigamod.PontoInicial.Y, Vigamod.PontoInicial.Z);
                    VigaTekla.EndPoint = new Tekla.Structures.Geometry3d.Point(Vigamod.PontoFinal.X, Vigamod.PontoFinal.Y, Vigamod.PontoFinal.Z);
                    VigaTekla.Insert();

                }
            }
            ModeloTekla.CommitChanges();
            MessageBox.Show(ModeloCurso.ObjetosModelo.Count.ToString());
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

                    VigaTekla.Position.Depth = Tekla.Structures.Model.Position.DepthEnum.FRONT;
                    VigaTekla.Position.DepthOffset = 100;
                    VigaTekla.Modify();
                  
                }
            }
            Modelo.CommitChanges();
        }
    }
}
