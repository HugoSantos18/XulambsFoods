using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    public interface IPedido
    {
        bool PodeAdicionar();
        double ValorTaxa();
        string Relatorio();
    }
}
