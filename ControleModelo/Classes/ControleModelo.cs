using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleModelo.Classes
{
    public class ControleModelo
    {
        public List<ObjetoModelo> ObjetosModelo {  get; set; }
        public ControleModelo()
        {
            ObjetosModelo = new List<ObjetoModelo>();
        }
    }
    public class ObjetoModelo
    {
        public Guid ID { get; set; }
        public ObjetoModelo() 
        {
            ID = Guid.NewGuid();
        }
    }
    public class PecaModelo : ObjetoModelo
    {
        public string Nome { get; set; }
        public string Perfil { get; set; }
        public string Material { get; set; }
        public string Finish { get; set; }
        public string Class { get; set; }
        public PecaModelo() { }


    }

    public class VigaModelo : PecaModelo
    {
        public Ponto3D PontoInicial { get; set; }
        public Ponto3D PontoFinal { get; set; }
        public VigaModelo() { }
    }

}
