using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Calcula
    {

        public static Rede CalcRede(string[] dados)
        {
            try
            {

                //dados: IPRede, HostInicial, HostFinal, IpBroadcast, MascaraRede, QuantidadeIps, QuantidadeHosts, QuantidadeGrupos, CIDR, qualGrupoSeLocaliza
                //  IPRede, HostInicial, HostFinal, IpBroadcast, MascaraRede, QuantidadeIps, QuantidadeHosts, QuantidadeGrupos, CIDR, qualGrupoSeLocaliza
                string ipRede = string.Empty, hostInicial = string.Empty,
                    hostFinal = string.Empty, ipBroadcast = string.Empty,
                    mascaraRede = string.Empty, quantidadeIps = string.Empty,
                    quantidadeHosts = string.Empty, quantidadeGrupos = string.Empty,
                    cidr = string.Empty, qualGrupoSeLocaliza = string.Empty;

                //IP REDE E HOST INICIAL
                if (!dados[0].Equals("000.000.000.000") & !dados[1].Equals("000.000.000.000"))
                {
                    ipRede = dados[0];
                    hostInicial = dados[1];

                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {
                        mascaraRede = "255.255.255.0";

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 255;
                            hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 254;

                            IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassCRecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {
                        mascaraRede = "255.255.0.0";


                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];
                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + 255 + "." + 255;
                            hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + 255 + "." + 254;

                            IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassBRecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {
                        mascaraRede = "255.0.0.0";

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];
                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            ipBroadcast = ipRedeRateado[0] + "." + 255 + "." + 255 + "." + 255;
                            hostFinal = ipRedeRateado[0] + "." + 255 + "." + 255 + "." + 254;

                            IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassARecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassARecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }

                    Rede r1 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r1;



                }

                //IP REDE E HOST FINAL
                if (!dados[0].Equals("000.000.000.000") & !dados[2].Equals("000.000.000.000"))
                {
                    ipRede = dados[0];
                    hostFinal = dados[2];


                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {
                        mascaraRede = "255.255.255.0";

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 255;
                            hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;

                            IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassCRecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {
                        mascaraRede = "255.255.0.0";


                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];
                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + 255 + "." + 255;
                            hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;

                            IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassBRecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {
                        mascaraRede = "255.0.0.0";

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];
                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            ipBroadcast = ipRedeRateado[0] + "." + 255 + "." + 255 + "." + 255;
                            hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[0] + "." + ipRedeRateado[0] + "." + 1;

                            IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassARecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassARecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }

                    Rede r2 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
              quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r2;
                }

                //IP REDE E IP BROADCAST
                if (!dados[0].Equals("000.000.000.000") & !dados[3].Equals("000.000.000.000"))
                {
                    ipRede = dados[0];
                    ipBroadcast = dados[3];


                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {
                        mascaraRede = "255.255.255.0";

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                            hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 254;

                            IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassCRecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {
                        mascaraRede = "255.255.0.0";


                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];
                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                            hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + 255 + "." + 254;

                            IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassBRecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {
                        mascaraRede = "255.0.0.0";

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];
                        if (mascaraRedeRateada[3].Equals("0"))
                        {
                            hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                            hostFinal = ipRedeRateado[0] + "." + 255 + "." + 255 + "." + 254;

                            IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                            quantidadeIps = ClassARecuperado.QuantidadeIPS;
                            quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                            quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                            cidr = ClassARecuperado.CIDR;
                            qualGrupoSeLocaliza = "1º";

                        }
                    }


                    Rede r3 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r3;
                }

                //IP REDE E MASCARA
                if (!dados[0].Equals("000.000.000.000") & !dados[4].Equals("000.000.000.000"))
                {
                    ipRede = dados[0];
                    mascaraRede = dados[4];

                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }


                    Rede r4 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
               quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r4;

                    //Console.WriteLine("IP Rede: {0}", ipRede);
                    //Console.WriteLine("Host Inicial: {0}", hostInicial);
                    //Console.WriteLine("Host Final: {0}", hostFinal);
                    //Console.WriteLine("IP BroadCast: {0}", ipBroadcast);
                    //Console.WriteLine("Mascara Rede: {0}", mascaraRede);
                    //Console.WriteLine("Quantidade Ips: {0}", quantidadeIps);
                    //Console.WriteLine("Quantidade Hosts: {0}", quantidadeHosts);
                    //Console.WriteLine("Quantidade Grupos: {0}", quantidadeGrupos);
                    //Console.WriteLine("CIDR: {0}", cidr);
                    //Console.WriteLine("Qual grupo se encontra: {0}", qualGrupoSeLocaliza);
                }

                //IP REDE E QUANTIDADE IPS
                if (!dados[0].Equals("000.000.000.000") & !dados[5].Equals("0"))
                {
                    ipRede = dados[0];
                    quantidadeIps = dados[5];

                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];





                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {
                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r5 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
               quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r5;


                }

                //IP REDE E QUANTIDADE HOSTS
                if (!dados[0].Equals("000.000.000.000") & !dados[6].Equals("0"))
                {
                    ipRede = dados[0];
                    quantidadeHosts = dados[6];
                    quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();

                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];





                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {
                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }

                    Rede r6 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                             quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r6;
                }

                //IP REDE E QUANTIDADE GRUPOS
                if (!dados[0].Equals("000.000.000.000") & !dados[7].Equals("0"))
                {
                    ipRede = dados[0];
                    quantidadeGrupos = dados[7];


                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (256 / Convert.ToInt32(quantidadeGrupos)).ToString();


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');

                        quantidadeIps = ((256 * 256) / Convert.ToInt32(quantidadeGrupos)).ToString();

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];





                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {
                        string[] ipRedeRateado = ipRede.Split('.');

                        quantidadeIps = ((256 * 256 * 256) / Convert.ToInt32(quantidadeGrupos)).ToString();

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r7 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
               quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r7;
                }

                //IP REDE E CIDR
                if (!dados[0].Equals("000.000.000.000") & !dados[8].Equals("/0"))
                {
                    ipRede = dados[0];
                    cidr = dados[8];

                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];





                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {
                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r8 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                 quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r8;
                }

                //IP REDE E Qual Grupo Se Localiza
                if (!dados[0].Equals("000.000.000.000") & !dados[9].Equals("0"))
                {

                    Rede r9 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
             quantidadeGrupos, cidr, qualGrupoSeLocaliza, "Não foi possível calcular os demais elementos, pois é impossível determinar apenas com o Ip Rede e Qual grupo se localiza");


                    return r9;

                }

                //HOST INICIAL E IP BROADCAST
                if (!dados[1].Equals("000.000.000.000") & !dados[3].Equals("000.000.000.000"))
                {

                    hostInicial = dados[1];
                    ipBroadcast = dados[3];


                    if (Calcula.DescobrirClasse(hostInicial).Equals('C'))
                    {
                        string[] ipBroadCastRateado = ipBroadcast.Split('.');
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        //quantidadeIps = (Convert.ToInt32(ipBroadCastRateado[3]) + 1).ToString();
                        quantidadeIps = (((Convert.ToInt32(ipBroadCastRateado[3]) + 2) - (Convert.ToInt32(hostInicialRateado[3])))).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();


                        //Recuperar a Máscara

                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));

                        quartoOcteto = ipBroadCastRateado[3];
                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }

                    else if (Calcula.DescobrirClasse(hostInicial).Equals('B'))
                    {

                        string[] ipBroadCastRateado = ipBroadcast.Split('.');
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (((Convert.ToInt32(ipBroadCastRateado[2]) + 1) - (Convert.ToInt32(hostInicialRateado[2]))) * 256).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();


                        //Recuperar a Máscara

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));

                        quartoOcteto = ipBroadCastRateado[3];

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + ((Convert.ToInt32(quartoOcteto) - 1));


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostInicial).Equals('A'))
                    {
                        string[] ipBroadCastRateado = ipBroadcast.Split('.');
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (((Convert.ToInt32(ipBroadCastRateado[1]) + 1) - (Convert.ToInt32(hostInicialRateado[1]))) * 256 * 256).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r10 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
        quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r10;
                }

                //HOST INICIAL E MASCARA
                if (!dados[1].Equals("000.000.000.000") & !dados[4].Equals("000.000.000.000"))
                {

                    hostInicial = dados[1];
                    mascaraRede = dados[4];


                    if (Calcula.DescobrirClasse(hostInicial).Equals('C'))
                    {

                        string[] hostInicialRateado = hostInicial.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));
                        string quartoOcteto = hostInicialRateado[3];


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }

                    else if (Calcula.DescobrirClasse(hostInicial).Equals('B'))
                    {

                        string[] hostInicialRateado = hostInicial.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipBroadcast = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + 254;


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostInicial).Equals('A'))
                    {
                        string[] hostInicialRateado = hostInicial.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r11 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
       quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r11;
                }

                //HOST INICIAL E Quantidade IPS
                if (!dados[1].Equals("000.000.000.000") & !dados[5].Equals("0"))
                {

                    hostInicial = dados[1];
                    quantidadeIps = dados[5];


                    if (Calcula.DescobrirClasse(hostInicial).Equals('C'))
                    {
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));



                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');

                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));

                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }

                    else if (Calcula.DescobrirClasse(hostInicial).Equals('B'))
                    {


                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        cidr = ClassBRecuperado.CIDR;
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipBroadcast = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');

                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + ipBroadcastRateado[3];


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostInicial).Equals('A'))
                    {

                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r12 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
         quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r12;
                }

                //HOST INICIAL E Quantidade Host
                if (!dados[1].Equals("000.000.000.000") & !dados[6].Equals("0"))
                {

                    hostInicial = dados[1];
                    quantidadeHosts = dados[6];


                    if (Calcula.DescobrirClasse(hostInicial).Equals('C'))
                    {
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));



                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');

                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));

                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }

                    else if (Calcula.DescobrirClasse(hostInicial).Equals('B'))
                    {


                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        cidr = ClassBRecuperado.CIDR;
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipBroadcast = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');

                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + ipBroadcastRateado[3];


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostInicial).Equals('A'))
                    {

                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r13 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
         quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r13;
                }

                //HOST INICIAL E Quantidade Grupos
                if (!dados[1].Equals("000.000.000.000") & !dados[7].Equals("0"))
                {

                    hostInicial = dados[1];
                    quantidadeGrupos = dados[7];


                    if (Calcula.DescobrirClasse(hostInicial).Equals('C'))
                    {
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (256 / Convert.ToInt32(quantidadeGrupos)).ToString();


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));



                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');

                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));

                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }

                    else if (Calcula.DescobrirClasse(hostInicial).Equals('B'))
                    {


                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = ((256 * 256) / Convert.ToInt32(quantidadeGrupos)).ToString();


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        cidr = ClassBRecuperado.CIDR;
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipBroadcast = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');

                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + ipBroadcastRateado[3];


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostInicial).Equals('A'))
                    {

                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = ((256 * 256 * 256) / Convert.ToInt32(quantidadeGrupos)).ToString();


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r14 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
       quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r14;
                }

                //HOST INICIAL E HOST FINAL
                if (!dados[1].Equals("000.000.000.000") & !dados[2].Equals("000.000.000.000"))
                {

                    hostInicial = dados[1];
                    hostFinal = dados[2];

                    string[] hostFinalRateado = hostFinal.Split('.');
                    ipBroadcast = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) + 1);

                    if (Calcula.DescobrirClasse(hostInicial).Equals('C'))
                    {



                        string[] ipBroadCastRateado = ipBroadcast.Split('.');
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        //quantidadeIps = (Convert.ToInt32(ipBroadCastRateado[3]) + 1).ToString();
                        quantidadeIps = (((Convert.ToInt32(ipBroadCastRateado[3]) + 2) - (Convert.ToInt32(hostInicialRateado[3])))).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();


                        //Recuperar a Máscara

                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));

                        quartoOcteto = ipBroadCastRateado[3];
                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }

                    else if (Calcula.DescobrirClasse(hostInicial).Equals('B'))
                    {

                        string[] ipBroadCastRateado = ipBroadcast.Split('.');
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (((Convert.ToInt32(ipBroadCastRateado[2]) + 1) - (Convert.ToInt32(hostInicialRateado[2]))) * 256).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();


                        //Recuperar a Máscara

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        string quartoOcteto = hostInicialRateado[3];
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + ((Convert.ToInt32(quartoOcteto) - 1));

                        quartoOcteto = ipBroadCastRateado[3];

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        hostFinal = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + posicaoGrupo[1] + "." + ((Convert.ToInt32(quartoOcteto) - 1));


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostInicial).Equals('A'))
                    {
                        string[] ipBroadCastRateado = ipBroadcast.Split('.');
                        string[] hostInicialRateado = hostInicial.Split('.');
                        ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1).ToString();
                        string[] ipRedeRateado = ipRede.Split('.');
                        quantidadeIps = (((Convert.ToInt32(ipBroadCastRateado[1]) + 1) - (Convert.ToInt32(hostInicialRateado[1]))) * 256 * 256).ToString();
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidade", quantidadeIps);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r15 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
       quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r15;
                }

                //HOST INICIAL E CIDR
                if (!dados[1].Equals("000.000.000.000") & !dados[8].Equals("/0"))
                {
                    hostInicial = dados[1];
                    cidr = dados[8];

                    string[] hostInicialRateado = hostInicial.Split('.');
                    ipRede = hostInicialRateado[0] + "." + hostInicialRateado[1] + "." + hostInicialRateado[2] + "." + (Convert.ToInt32(hostInicialRateado[3]) - 1);

                    if (Calcula.DescobrirClasse(ipRede).Equals('C'))
                    {



                        string[] ipRedeRateado = ipRede.Split('.');


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[3]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (Calcula.DescobrirClasse(ipRede).Equals('B'))
                    {

                        string[] ipRedeRateado = ipRede.Split('.');


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];





                    }
                    else if (Calcula.DescobrirClasse(ipRede).Equals('A'))
                    {
                        string[] ipRedeRateado = ipRede.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[1]));

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];


                    }


                    Rede r16 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
       quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r16;
                }

                //HOST INICIAL E QUAL GRUPO SE LOCALIZA
                if (!dados[1].Equals("000.000.000.000") & !dados[9].Equals("0"))
                {

                    Rede r17 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
          quantidadeGrupos, cidr, qualGrupoSeLocaliza, "Infelizmente não é possível calcular os demais elementos apenas informando o a posição e o ip inicial");


                    return r17;



                }

                //HOST FINAL E IP BROADCAST
                if (!dados[2].Equals("000.000.000.000") & !dados[3].Equals("000.000.000.000"))
                {

                    Rede r18 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
       quantidadeGrupos, cidr, qualGrupoSeLocaliza, "Infelizmente não é possível calcular os demais elementos apenas informando o host final e o ip de broadcast");


                    return r18;
                }

                //HOST FINAL E MASCARA 
                if (!dados[2].Equals("000.000.000.000") & !dados[4].Equals("000.000.000.000"))
                {

                    hostFinal = dados[2];
                    string[] hostFinalRateado = hostFinal.Split('.');
                    mascaraRede = dados[4];
                    string[] mascaraRedeRateada = mascaraRede.Split('.');


                    if (Calcula.DescobrirClasse(hostFinal).Equals('C'))
                    {
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);
                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('B'))
                    {
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;

                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('A'))
                    {
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;

                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r19 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r19;
                }

                //HOST FINAL E Quantidade IPS
                if (!dados[2].Equals("000.000.000.000") & !dados[5].Equals("0"))
                {

                    hostFinal = dados[2];
                    string[] hostFinalRateado = hostFinal.Split('.');
                    quantidadeIps = dados[5];



                    if (Calcula.DescobrirClasse(hostFinal).Equals('C'))
                    {

                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);

                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('B'))
                    {
                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;
                        cidr = ClassBRecuperado.CIDR;

                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('A'))
                    {

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;


                        cidr = ClassARecuperado.CIDR;
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (ipRedeRateado[2]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r20 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r20;
                }

                //HOST FINAL E Quantidade Host
                if (!dados[2].Equals("000.000.000.000") & !dados[6].Equals("0"))
                {

                    hostFinal = dados[2];
                    string[] hostFinalRateado = hostFinal.Split('.');
                    quantidadeHosts = dados[6];
                    quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();



                    if (Calcula.DescobrirClasse(hostFinal).Equals('C'))
                    {

                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);

                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('B'))
                    {
                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;
                        cidr = ClassBRecuperado.CIDR;

                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('A'))
                    {

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;


                        cidr = ClassARecuperado.CIDR;
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (ipRedeRateado[2]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r21 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r21;
                }

                //HOST FINAL E Quantidade Grupos
                if (!dados[2].Equals("000.000.000.000") & !dados[7].Equals("0"))
                {

                    hostFinal = dados[2];
                    string[] hostFinalRateado = hostFinal.Split('.');
                    quantidadeGrupos = dados[7];




                    if (Calcula.DescobrirClasse(hostFinal).Equals('C'))
                    {
                        quantidadeIps = (256 / Convert.ToInt32(quantidadeGrupos)).ToString();

                        //IPClasse ClassCRecuperado = BuscarPorCIDROuFinalMascaraClasseC("Noi", "Noi", quantidadeIps);
                        //mascaraRede = "255.255.255." + ClassCRecuperado.FinalMascara;
                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;


                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);

                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('B'))
                    {
                        quantidadeIps = ((256 * 256) / Convert.ToInt32(quantidadeGrupos)).ToString();
                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;
                        cidr = ClassBRecuperado.CIDR;

                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('A'))
                    {
                        quantidadeIps = ((256 * 256 * 256) / Convert.ToInt32(quantidadeGrupos)).ToString();

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;


                        cidr = ClassARecuperado.CIDR;
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }

                    Rede r22 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                  quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r22;
                }

                //HOST FINAL E CIDR 
                if (!dados[2].Equals("000.000.000.000") & !dados[8].Equals("/0"))
                {

                    hostFinal = dados[2];
                    string[] hostFinalRateado = hostFinal.Split('.');
                    cidr = dados[8];

                    if (Calcula.DescobrirClasse(hostFinal).Equals('C'))
                    {


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('B'))
                    {

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;
                        cidr = ClassBRecuperado.CIDR;

                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(hostFinal).Equals('A'))
                    {


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;


                        cidr = ClassARecuperado.CIDR;
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (ipRedeRateado[2]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r23 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r23;
                }

                //HOST FINAL E QUAL GRUPO SE LOCALIZA
                if (!dados[2].Equals("000.000.000.000") & !dados[9].Equals("0"))
                {



                    Rede r24 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "Infelizmente não é possível calcular os demais elementos apenas informando o a posição e o ip final.");


                    return r24;



                }

                //IP BROADCAST E MASCARA 
                if (!dados[3].Equals("000.000.000.000") & !dados[4].Equals("000.000.000.000"))
                {
                    ipBroadcast = dados[3];
                    mascaraRede = dados[4];

                    if (Calcula.DescobrirClasse(ipBroadcast).Equals('C'))
                    {

                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');
                        mascaraRede = dados[4];
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);
                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        mascaraRede = ClassCRecuperado.FinalMascara;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('B'))
                    {

                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');
                        mascaraRede = dados[4];
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;

                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        cidr = ClassBRecuperado.CIDR;
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('A'))
                    {

                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');
                        mascaraRede = dados[4];
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;

                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        cidr = ClassARecuperado.CIDR;
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r25 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r25;
                }

                //IP BROADCAST E Quantidade IPS
                if (!dados[3].Equals("000.000.000.000") & !dados[5].Equals("0"))
                {

                    ipBroadcast = dados[3];
                    quantidadeIps = dados[5];



                    if (Calcula.DescobrirClasse(ipBroadcast).Equals('C'))
                    {
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');
                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);

                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('B'))
                    {

                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;
                        cidr = ClassBRecuperado.CIDR;

                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('A'))
                    {
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;


                        cidr = ClassARecuperado.CIDR;
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r26 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r26;
                }

                //IP BROADCAST E Quantidade HOST
                if (!dados[3].Equals("000.000.000.000") & !dados[6].Equals("0"))
                {

                    ipBroadcast = dados[3];
                    quantidadeHosts = dados[6];
                    quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();


                    if (Calcula.DescobrirClasse(ipBroadcast).Equals('C'))
                    {
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');
                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);

                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('B'))
                    {

                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;
                        cidr = ClassBRecuperado.CIDR;

                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('A'))
                    {
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;


                        cidr = ClassARecuperado.CIDR;
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r27 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r27;
                }

                //IP BROADCAST E Quantidade Grupos
                if (!dados[3].Equals("000.000.000.000") & !dados[7].Equals("0"))
                {

                    ipBroadcast = dados[3];
                    quantidadeGrupos = dados[7];



                    if (Calcula.DescobrirClasse(ipBroadcast).Equals('C'))
                    {
                        quantidadeIps = (256 / Convert.ToInt32(quantidadeGrupos)).ToString();

                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');
                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);

                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('B'))
                    {
                        quantidadeIps = ((256 * 256) / Convert.ToInt32(quantidadeGrupos)).ToString();
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;
                        cidr = ClassBRecuperado.CIDR;

                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('A'))
                    {
                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;


                        cidr = ClassARecuperado.CIDR;
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r28 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r28;
                }

                //IP BROADCAST E CIDR
                if (!dados[3].Equals("000.000.000.000") & !dados[8].Equals("/0"))
                {

                    ipBroadcast = dados[3];
                    cidr = dados[8];



                    if (Calcula.DescobrirClasse(ipBroadcast).Equals('C'))
                    {


                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');
                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + hostFinalRateado[2] + "." + (Convert.ToInt32(hostFinalRateado[3]) - PularGrupo + 2);

                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();
                        cidr = ClassCRecuperado.CIDR;

                        string[] ipRedeRateado = ipRede.Split('.');


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));



                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('B'))
                    {

                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');

                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));
                        ipRede = hostFinalRateado[0] + "." + hostFinalRateado[1] + "." + (Convert.ToInt32(hostFinalRateado[2]) - PularGrupo + 1) + "." + 0;
                        cidr = ClassBRecuperado.CIDR;

                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();

                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[1]) + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + (posicaoGrupo[0]) + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];



                    }
                    else if (Calcula.DescobrirClasse(ipBroadcast).Equals('A'))
                    {


                        string[] ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipBroadcastRateado[0] + "." + ipBroadcastRateado[1] + "." + ipBroadcastRateado[2] + "." + ((Convert.ToInt32(ipBroadcastRateado[3]) - 1));
                        string[] hostFinalRateado = hostFinal.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));

                        ipRede = hostFinalRateado[0] + "." + (Convert.ToInt32(hostFinalRateado[1]) - PularGrupo + 1) + "." + 0 + "." + 0;


                        cidr = ClassARecuperado.CIDR;
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        //int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRateada[2]));

                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(quantidadeIps) - 2).ToString();

                        string[] ipRedeRateado = ipRede.Split('.');

                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');

                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        ipBroadcastRateado = ipBroadcast.Split('.');
                        hostFinal = ipRedeRateado[0] + "." + (posicaoGrupo[1]) + "." + 255 + "." + 254;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(ipRedeRateado[3]) + 1);


                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];




                    }


                    Rede r29 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r29;
                }

                //IP BROADCAST E QUAL GRUPO SE LOCALIZA
                if (!dados[3].Equals("000.000.000.000") & !dados[9].Equals("0"))
                {


                    Rede r30 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "Infelizmente não é possível calcular os demais elementos apenas informando o a posição e o ip de broadcast.");


                    return r30;




                }

                //MASCARA E QUANTIDADE IPS
                if (!dados[4].Equals("000.000.000.000") & !dados[5].Equals("0"))
                {


                    quantidadeIps = dados[5];
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    mascaraRede = dados[4];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeA;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }






                    Rede r31 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r31;

                }

                //MASCARA E QUANTIDADE HOST
                if (!dados[4].Equals("000.000.000.000") & !dados[6].Equals("0"))
                {

                    quantidadeHosts = dados[6];
                    quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    mascaraRede = dados[4];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeC;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }




                    Rede r32 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r32;


                }

                //MASCARA E QUANTIDADE HOST
                if (!dados[4].Equals("000.000.000.000") & !dados[6].Equals("0"))
                {

                    quantidadeHosts = dados[6];
                    quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    mascaraRede = dados[4];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeA;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }



                    Rede r33 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r33;


                }

                //MASCARA E QUANTIDADE GRUPOS
                if (!dados[4].Equals("000.000.000.000") & !dados[7].Equals("0"))
                {
                    quantidadeGrupos = dados[7];
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    mascaraRede = dados[4];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeC;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }




                    Rede r34 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r34;


                }

                //MASCARA E CIDR
                if (!dados[4].Equals("000.000.000.000") & !dados[8].Equals("/0"))
                {
                    cidr = dados[8];
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    mascaraRede = dados[4];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeA;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }




                    Rede r35 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r35;



                }

                //MASCARA E QUAL GRUPO SE ENCONTRA
                if (!dados[4].Equals("000.000.000.000") & !dados[9].Equals("0"))
                {
                    qualGrupoSeLocaliza = dados[9];
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    mascaraRede = dados[4];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');


                        ipRede = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[0];
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');
                        ipRede = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[0] + "." + 0;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[0] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        string finalMascara = mascaraRedeRateada[1] + "." + mascaraRedeRateada[2] + "." + mascaraRedeRateada[3];

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("mascara", mascaraRede);
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');
                        ipRede = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + 0 + "." + 0;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + 0 + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }




                    Rede r36 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r36;

                }

                //QUANTIDADE IPS E QUANTIDADE HOSTS
                if (!dados[5].Equals("0") & !dados[6].Equals("0"))
                {


                    quantidadeIps = dados[5];
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    quantidadeHosts = dados[6];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');



                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeA;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }




                    Rede r37 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r37;

                }

                //QUANTIDADE IPS E QUANTIDADE GRUPOS
                if (!dados[5].Equals("0") & !dados[7].Equals("0"))
                {


                    quantidadeIps = dados[5];
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    quantidadeGrupos = dados[7];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');



                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeA;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }



                    Rede r38 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r38;

                }

                //QUANTIDADE IPS E CIDR
                if (!dados[5].Equals("0") & !dados[8].Equals("/0"))
                {


                    quantidadeIps = dados[5];
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    cidr = dados[8];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');



                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeC;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }


                    Rede r39 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r39;


                }

                //QUANTIDADE HOSTS E CIDR
                if (!dados[6].Equals("0") & !dados[8].Equals("/0"))
                {

                    quantidadeHosts = dados[6];
                    quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();
                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    cidr = dados[8];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');



                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeA;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }



                    Rede r40 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r40;


                }

                //QUANTIDADE GRUPOS E CIDR
                if (!dados[7].Equals("0") & !dados[8].Equals("/0"))
                {


                    string IpRedeC = "192.168.1.0";
                    string[] mascaraRedeRateadaComplemento = mascaraRede.Split('.');
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    cidr = dados[8];
                    quantidadeGrupos = dados[7];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');



                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');

                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[3]), cidr).Split('-');


                        ipRede = IpRedeC;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');



                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[2]), cidr).Split('-');
                        ipRede = IpRedeB;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');

                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupo(PularGrupo, Convert.ToInt32(ipRedeRateado[1]), cidr).Split('-');
                        ipRede = IpRedeA;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }



                    Rede r41 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r41;


                }





                //QUANTIDADE IPS E QUAL GRUPO SE ENCONTRA
                if (!dados[5].Equals("0") & !dados[9].Equals("0"))
                {
                    qualGrupoSeLocaliza = dados[9];
                    string IpRedeC = "192.168.1.0";
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    quantidadeIps = dados[5];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');


                        ipRede = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[0];
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');



                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');
                        ipRede = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[0] + "." + 0;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[0] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');
                        ipRede = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + 0 + "." + 0;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + 0 + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }


                    Rede r42 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
                quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r42;

                }

                //QUANTIDADE HOST E QUAL GRUPO SE ENCONTRA
                if (!dados[6].Equals("0") & !dados[9].Equals("0"))
                {
                    qualGrupoSeLocaliza = dados[9];
                    string IpRedeC = "192.168.1.0";
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    quantidadeHosts = dados[6];
                    quantidadeIps = (Convert.ToInt32(quantidadeHosts) + 2).ToString();

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');


                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');


                        ipRede = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[0];
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');



                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');
                        ipRede = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[0] + "." + 0;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[0] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("quantidadeips", quantidadeIps);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');
                        ipRede = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + 0 + "." + 0;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + 0 + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }



                    Rede r43 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r43;



                }

                //QUANTIDADE GRUPOS E QUAL GRUPO SE ENCONTRA
                if (!dados[6].Equals("0") & !dados[9].Equals("0"))
                {




                    Rede r44 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "Não é possíve calcular os demais elementos, pois para se calcular o IP a partir da quantidade de grupos é necessário saber a classe de ip que está trabalhando.");


                    return r44;



                }

                //CIDR E QUAL GRUPO SE ENCONTRA
                if (!dados[8].Equals("/0") & !dados[9].Equals("0"))
                {
                    qualGrupoSeLocaliza = dados[9];
                    string IpRedeC = "192.168.1.0";
                    string IpRedeB = "172.169.0.0";
                    string IpRedeA = "10.0.0.0";
                    cidr = dados[8];

                    IPClasse DescobrirIPPorMascara = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);

                    if (DescobrirIPPorMascara.ClassePadrao.Equals("C"))
                    {

                        string[] ipRedeRateado = IpRedeC.Split('.');




                        IPClasse ClassCRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassCRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassCRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassCRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32(256 / Convert.ToInt32(ClassCRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassCRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[3]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');


                        ipRede = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[0];
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[0]) + 1);
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + (Convert.ToInt32(posicaoGrupo[1]) - 1);
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + ipRedeRateado[2] + "." + posicaoGrupo[1];

                    }

                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("B"))
                    {

                        string[] ipRedeRateado = IpRedeB.Split('.');


                        IPClasse ClassBRecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassBRecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassBRecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassBRecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256) / Convert.ToInt32(ClassBRecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassBRecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[2]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');
                        ipRede = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[0] + "." + 0;
                        hostInicial = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[0] + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + ipRedeRateado[1] + "." + posicaoGrupo[1] + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }
                    else if (DescobrirIPPorMascara.ClassePadrao.Equals("A"))
                    {

                        string[] ipRedeRateado = IpRedeA.Split('.');


                        IPClasse ClassARecuperado = Calcula.BuscarPorCIDROuFinalMascara("cidr", cidr);
                        mascaraRede = ClassARecuperado.FinalMascara;
                        string[] mascaraRedeRateada = mascaraRede.Split('.');
                        quantidadeIps = ClassARecuperado.QuantidadeIPS;
                        quantidadeHosts = (Convert.ToInt32(ClassARecuperado.QuantidadeIPS) - 2).ToString();
                        quantidadeGrupos = Convert.ToInt32((256 * 256 * 256) / Convert.ToInt32(ClassARecuperado.QuantidadeIPS)).ToString();
                        cidr = ClassARecuperado.CIDR;
                        int PularGrupo = Convert.ToInt32(256 - Convert.ToInt32(mascaraRedeRateada[1]));


                        string[] posicaoGrupo = PosicaoGrupoReverse(PularGrupo, (Convert.ToInt32(qualGrupoSeLocaliza))).Split('-');
                        ipRede = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + 0 + "." + 0;
                        hostInicial = ipRedeRateado[0] + "." + posicaoGrupo[0] + "." + 0 + "." + 1;
                        hostFinal = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 254;
                        ipBroadcast = ipRedeRateado[0] + "." + posicaoGrupo[1] + "." + 255 + "." + 255;
                        qualGrupoSeLocaliza = posicaoGrupo[2] + "º --- " + posicaoGrupo[0] + ":" + posicaoGrupo[1];

                    }






                    Rede r45 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "OK");


                    return r45;


                }



                Rede r46 = new Rede(ipRede, hostInicial, hostFinal, ipBroadcast, mascaraRede, quantidadeIps, quantidadeHosts,
    quantidadeGrupos, cidr, qualGrupoSeLocaliza, "Ocorreu um erro, aparentemente nenhum elemento foi selecionado");

                return r46;
            }
            catch (Exception ex)
            {
                Rede r47 = new Rede(null, null, null, null, null, null, null,
  null, null, null, "Ocorreu um erro, aparentemente nenhum elemento foi selecionado");
                return r47;
            }
        }

        public static IPClasse BuscarPorCIDROuFinalMascara(string tipo, string dado)
        {
            //Gerando Dados
            List<IPClasse> listaIPS = new List<IPClasse>();
            IPClasse Item1 = new IPClasse { CIDR = "/8", FinalMascara = "255.255.0.0", QuantidadeIPS = "16777216", QuantidadeHosts = "16777214", ClassePadrao = "A", QuantidadeGrupos = "1" };
            IPClasse Item2 = new IPClasse { CIDR = "/9", FinalMascara = "255.128.0.0", QuantidadeIPS = "8388608", QuantidadeHosts = "8388606", ClassePadrao = "A", QuantidadeGrupos = "2" };
            IPClasse Item3 = new IPClasse { CIDR = "/10", FinalMascara = "255.192.0.0", QuantidadeIPS = "4194304", QuantidadeHosts = "4194302", ClassePadrao = "A", QuantidadeGrupos = "4" };
            IPClasse Item4 = new IPClasse { CIDR = "/11", FinalMascara = "255.224.0.0", QuantidadeIPS = "2097152", QuantidadeHosts = "2097150", ClassePadrao = "A", QuantidadeGrupos = "8" };
            IPClasse Item5 = new IPClasse { CIDR = "/12", FinalMascara = "255.240.0.0", QuantidadeIPS = "1048576", QuantidadeHosts = "1048574", ClassePadrao = "A", QuantidadeGrupos = "16" };
            IPClasse Item6 = new IPClasse { CIDR = "/13", FinalMascara = "255.248.0.0", QuantidadeIPS = "524288", QuantidadeHosts = "524286", ClassePadrao = "A", QuantidadeGrupos = "32" };
            IPClasse Item7 = new IPClasse { CIDR = "/14", FinalMascara = "255.252.0.0", QuantidadeIPS = "262144", QuantidadeHosts = "262142", ClassePadrao = "A", QuantidadeGrupos = "64" };
            IPClasse Item8 = new IPClasse { CIDR = "/15", FinalMascara = "255.254.0.0", QuantidadeIPS = "131072", QuantidadeHosts = "131070", ClassePadrao = "A", QuantidadeGrupos = "128" };
            IPClasse Item9 = new IPClasse { CIDR = "/16", FinalMascara = "255.255.0.0", QuantidadeIPS = "65536", QuantidadeHosts = "65534", ClassePadrao = "B", QuantidadeGrupos = "1" };
            IPClasse Item11 = new IPClasse { CIDR = "/17", FinalMascara = "255.255.128.0", QuantidadeIPS = "32768", QuantidadeHosts = "32766", ClassePadrao = "B", QuantidadeGrupos = "2" };
            IPClasse Item12 = new IPClasse { CIDR = "/18", FinalMascara = "255.255.192.0", QuantidadeIPS = "16384", QuantidadeHosts = "16382", ClassePadrao = "B", QuantidadeGrupos = "4" };
            IPClasse Item13 = new IPClasse { CIDR = "/19", FinalMascara = "255.255.224.0", QuantidadeIPS = "8192", QuantidadeHosts = "8190", ClassePadrao = "B", QuantidadeGrupos = "8" };
            IPClasse Item14 = new IPClasse { CIDR = "/20", FinalMascara = "255.255.240.0", QuantidadeIPS = "4096", QuantidadeHosts = "4094", ClassePadrao = "B", QuantidadeGrupos = "16" };
            IPClasse Item15 = new IPClasse { CIDR = "/21", FinalMascara = "255.255.248.0", QuantidadeIPS = "2048", QuantidadeHosts = "2046", ClassePadrao = "B", QuantidadeGrupos = "32" };
            IPClasse Item16 = new IPClasse { CIDR = "/22", FinalMascara = "255.255.252.0", QuantidadeIPS = "1024", QuantidadeHosts = "1022", ClassePadrao = "B", QuantidadeGrupos = "64" };
            IPClasse Item17 = new IPClasse { CIDR = "/23", FinalMascara = "255.255.254.0", QuantidadeIPS = "512", QuantidadeHosts = "510", ClassePadrao = "B", QuantidadeGrupos = "128" };
            IPClasse Item18 = new IPClasse { CIDR = "/24", FinalMascara = "255.255.255.0", QuantidadeIPS = "256", QuantidadeHosts = "254", ClassePadrao = "C", QuantidadeGrupos = "1" };
            IPClasse Item19 = new IPClasse { CIDR = "/25", FinalMascara = "255.255.255.128", QuantidadeIPS = "128", QuantidadeHosts = "126", ClassePadrao = "C", QuantidadeGrupos = "2" };
            IPClasse Item20 = new IPClasse { CIDR = "/26", FinalMascara = "255.255.255.192", QuantidadeIPS = "64", QuantidadeHosts = "62", ClassePadrao = "C", QuantidadeGrupos = "4" };
            IPClasse Item21 = new IPClasse { CIDR = "/27", FinalMascara = "255.255.255.224", QuantidadeIPS = "32", QuantidadeHosts = "30", ClassePadrao = "C", QuantidadeGrupos = "8" };
            IPClasse Item22 = new IPClasse { CIDR = "/28", FinalMascara = "255.255.255.240", QuantidadeIPS = "16", QuantidadeHosts = "14", ClassePadrao = "C", QuantidadeGrupos = "16" };
            IPClasse Item23 = new IPClasse { CIDR = "/29", FinalMascara = "255.255.255.248", QuantidadeIPS = "8", QuantidadeHosts = "6", ClassePadrao = "C", QuantidadeGrupos = "32" };
            IPClasse Item24 = new IPClasse { CIDR = "/30", FinalMascara = "255.255.255.252", QuantidadeIPS = "4", QuantidadeHosts = "2", ClassePadrao = "C", QuantidadeGrupos = "64" };
            IPClasse Item25 = new IPClasse { CIDR = "/31", FinalMascara = "255.255.255.254", QuantidadeIPS = "2", QuantidadeHosts = "0", ClassePadrao = "C", QuantidadeGrupos = "128" };
            IPClasse Item26 = new IPClasse { CIDR = "/32", FinalMascara = "255.255.255.255", QuantidadeIPS = "1", QuantidadeHosts = "0", ClassePadrao = "C", QuantidadeGrupos = "256" };
            //Adicionando na Lista
            listaIPS.Add(Item1);
            listaIPS.Add(Item2);
            listaIPS.Add(Item3);
            listaIPS.Add(Item4);
            listaIPS.Add(Item5);
            listaIPS.Add(Item6);
            listaIPS.Add(Item7);
            listaIPS.Add(Item8);
            listaIPS.Add(Item9);
            listaIPS.Add(Item11);
            listaIPS.Add(Item12);
            listaIPS.Add(Item13);
            listaIPS.Add(Item14);
            listaIPS.Add(Item15);
            listaIPS.Add(Item16);
            listaIPS.Add(Item17);
            listaIPS.Add(Item18);
            listaIPS.Add(Item19);
            listaIPS.Add(Item20);
            listaIPS.Add(Item21);
            listaIPS.Add(Item22);
            listaIPS.Add(Item23);
            listaIPS.Add(Item24);
            listaIPS.Add(Item25);
            listaIPS.Add(Item25);


            if (tipo.Equals("cidr"))
            {
                return listaIPS.Where(x => x.CIDR.Equals(dado)).FirstOrDefault();
            }
            else if (tipo.Equals("mascara"))
            {
                return listaIPS.Where(x => x.FinalMascara.Equals(dado)).FirstOrDefault();
            }
            else if (tipo.Equals("quantidadeips"))
            {
                return listaIPS.Where(x => x.QuantidadeIPS.Equals(dado)).FirstOrDefault();
            }
            else if (tipo.Equals("quantidadegrupos"))
            {
                return listaIPS.Where(x => x.QuantidadeGrupos.Equals(dado)).FirstOrDefault();
            }

            return null;

        }
        public static char DescobrirClasse(string ip)
        {
            string[] Octetos = ip.Split('.');


            int primeiroOctetoConvertido = Convert.ToInt32(Octetos[0]);
            if (primeiroOctetoConvertido >= 0 && primeiroOctetoConvertido <= 127)
            {
                return 'A';
            }
            else if (primeiroOctetoConvertido >= 128 && primeiroOctetoConvertido <= 191)
            {
                return 'B';
            }
            else if (primeiroOctetoConvertido >= 192 && primeiroOctetoConvertido <= 223)
            {
                return 'C';
            }
            return 'Z';

        }
        public static string PosicaoGrupo(int PularGrupo, int terceiroOctetoIpRede, string cidr)
        {
            int qtVezesFor = 0;

            for (int i = 0; i <= 255; i = i + PularGrupo)
            {
                qtVezesFor++;
                if (i + 1 > 7)
                {
                    if (terceiroOctetoIpRede >= i && terceiroOctetoIpRede <= (i + (PularGrupo - 1)))
                    {
                        return i + "-" + (i + (PularGrupo - 1)) + "-" + qtVezesFor;
                    }
                    //Console.WriteLine(i + " - " + (i + (PularGrupo - 1)));
                }
                else
                {
                    if (terceiroOctetoIpRede >= i && terceiroOctetoIpRede <= (i + (PularGrupo - 1)))
                    {

                        return i + "-" + (i + (PularGrupo - 1)) + "-" + qtVezesFor;
                    }
                    //Console.WriteLine(i + " - " + (i + (PularGrupo - 1)));
                }
            }



            return null;
        }
        public static string PosicaoGrupoReverse(int PularGrupo, int posicaoDesejada)
        {
            int qtVezesFor = 0;

            for (int i = 0; i <= 255; i = i + PularGrupo)
            {
                qtVezesFor++;




                if (i + 1 > 7)
                {

                    if (qtVezesFor == posicaoDesejada)
                    {
                        return i + "-" + (i + (PularGrupo - 1)) + "-" + qtVezesFor;
                    }
                    //Console.WriteLine(i + " - " + (i + (PularGrupo - 1)));
                }
                else
                {
                    if (qtVezesFor == posicaoDesejada)
                    {
                        return i + "-" + (i + (PularGrupo - 1)) + "-" + qtVezesFor;
                    }
                    //Console.WriteLine(i + " - " + (i + (PularGrupo - 1)));
                }
            }



            return null;
        }
    }
}
