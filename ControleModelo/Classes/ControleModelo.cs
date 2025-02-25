using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ControleModelo.Classes
{
    public class ControleModelo
    {
        public List<ObjetoModelo> ObjetosModelo {  get; set; }
        public ControleModelo()
        {
            ObjetosModelo = new List<ObjetoModelo>();
        }
        public void Salvar()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ControleModelo), new Type[] { typeof(ControleModelo), typeof(VigaModelo) });

            TextWriter writer = new StreamWriter("C:\\arquivoteste.MMD");
            serializer.Serialize(writer, this);
            writer.Close();

        }
        public static ControleModelo Carregar(string CaminhoArquivo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ControleModelo), new Type[] { typeof(ControleModelo), typeof(VigaModelo) });
            TextReader reader = new StreamReader(CaminhoArquivo);
            ControleModelo ModeloCarregado = serializer.Deserialize(reader) as ControleModelo;
            reader.Close();
            return ModeloCarregado;
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
        public VigaModelo(Tekla.Structures.Model.Beam vigaTekla)
        {
           Perfil = vigaTekla.Profile.ProfileString;
           Material = vigaTekla.Material.MaterialString;
           Nome = vigaTekla.Name;
           Finish = vigaTekla.Finish;
           Class = vigaTekla.Class;
           PontoInicial = new Ponto3D(vigaTekla.StartPoint);
           PontoFinal = new Ponto3D(vigaTekla.EndPoint);
        }
    }

}
