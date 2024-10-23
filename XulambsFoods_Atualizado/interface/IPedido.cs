using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XulambsFoods_Atualizado;

namespace XulambsFoods_Atualizado
{
    public interface IPedido
    {
        public bool PodeAdicionar();
        public double ValorTaxa();
        public string Relatorio();

    }
}
