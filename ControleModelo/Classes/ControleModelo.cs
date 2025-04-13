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
        public void Salvar(string CaminhoArquivo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ControleModelo), new Type[] { typeof(ControleModelo), typeof(VigaModelo) });

            TextWriter writer = new StreamWriter(CaminhoArquivo);
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
   

}
