using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Rede
    {
        public string IPRede { get; set; }
        public string HostInicial { get; set; }
        public string HostFinal { get; set; }
        public string IpBroadcast { get; set; }
        public string MascaraRede { get; set; }
        public string QuantidadeIps { get; set; }
        public string QuantidadeHosts { get; set; }
        public string QuantidadeGrupos { get; set; }
        public string CIDR { get; set; }
        public string qualGrupoSeLocaliza { get; set; }
        public string Mensagem { get; set; }


        public Rede(string ipRede, string hostInicial, string hostFinal, string ipBroadcast, string mascaraRede, string quantidadeIps, string quantidadeHosts,
            string quantidadeGrupos, string cidr, string qualGrupo, string mensagem)
        {
            this.IPRede = ipRede;
            this.HostInicial = hostInicial;
            this.HostFinal = hostFinal;
            this.IpBroadcast = ipBroadcast;
            this.MascaraRede = mascaraRede;
            this.QuantidadeIps = quantidadeIps;
            this.QuantidadeHosts = quantidadeHosts;
            this.QuantidadeGrupos = quantidadeGrupos;
            this.CIDR = cidr;
            this.qualGrupoSeLocaliza = qualGrupo;
            this.Mensagem = mensagem;
        }


    }
}
