using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class GrupoControle
    {
        public string QuantidadIps { get; set; }
        public string Inicial { get; set; }
        public string Final { get; set; }

        public GrupoControle(string quantidadeIps, string inicial, string final)
        {
            this.QuantidadIps = quantidadeIps;
            this.Inicial = inicial;
            this.Final = final;
        }
    }
}
