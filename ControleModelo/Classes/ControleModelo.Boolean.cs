using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace ControleModelo.Classes
{
    public class BooleanModelo : ObjetoModelo
    {
        public Guid Father { get; set; }
        public BooleanModelo() 
        { 

        }
	}
    public class FittingModelo : BooleanModelo
	{
		public PlanoControleModelo PlanoRecorte { get; set; }
		public FittingModelo()
		{
			
		}
		public FittingModelo(Tekla.Structures.Model.Fitting FittingTekla)
		{
			Father = FittingTekla.Father.Identifier.GUID;
			PlanoRecorte = new PlanoControleModelo(FittingTekla.Plane);
		}
		public Tekla.Structures.Model.Fitting RetornaFittingTekla()
		{
			var FittingRetornar = new Fitting();
			FittingRetornar.Plane = PlanoRecorte.RetornaPlanoTekla();
			return FittingRetornar;
		}
	}
}
