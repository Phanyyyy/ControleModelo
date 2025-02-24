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
                    ModeloCurso.Salvar();

                }


            }
            MessageBox.Show(ModeloCurso.ObjetosModelo.Count.ToString());
        }
    }
}
