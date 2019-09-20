using Model;
using System.Web.Mvc;

namespace SubnetCalculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Alunos()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult CalcularElementos(string IPRede = "000.000.000.000", string HostInicial = "000.000.000.000", string HostFinal = "000.000.000.000",
                string IpBroadcast = "000.000.000.000", string MascaraRede = "000.000.000.000", string QuantidadeIps = "0", string QuantidadeHosts = "0",
                string QuantidadeGrupos = "0", string CIDR = "/0", string qualGrupoSeLocaliza = "0")
        {

            if (IPRede == string.Empty)
            {
                IPRede = "000.000.000.000";
            }

            if (HostInicial == string.Empty)
            {
                HostInicial = "000.000.000.000";
            }

            if (HostFinal == string.Empty)
            {
                HostFinal = "000.000.000.000";
            }

            if (IpBroadcast == string.Empty)
            {
                IpBroadcast = "000.000.000.000";
            }

            if (MascaraRede == string.Empty)
            {
                MascaraRede = "000.000.000.000";
            }

            if (QuantidadeIps == string.Empty)
            {
               QuantidadeIps = "0";
            }
            if (QuantidadeHosts == string.Empty)
            {
                QuantidadeHosts = "0";
            }
            if (QuantidadeGrupos == string.Empty)
            {
                QuantidadeGrupos = "0";
            }

            if (CIDR == string.Empty)
            {
                CIDR = "/0";
            }

            if (qualGrupoSeLocaliza == string.Empty)
            {
                qualGrupoSeLocaliza = "0";
            }


            string[] calc = new string[10] { IPRede, HostInicial, HostFinal,
                IpBroadcast, MascaraRede, QuantidadeIps, QuantidadeHosts,
                QuantidadeGrupos, CIDR, qualGrupoSeLocaliza };


            


            var Elementos = Calcula.CalcRede(calc);

            return Json(new { Resultado = Elementos});
        }


    }
}