using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class PosicaoGrupo
    {

        public string PosicaoInicial { get; set; }
        public string PosicaoFinal { get; set; }


        public PosicaoGrupo(string inicial, string final)
        {
            this.PosicaoInicial = inicial;
            this.PosicaoFinal = final;
        }

    }
}
