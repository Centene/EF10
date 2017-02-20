using EF10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF10.Controllers
{
    public class CabeceraFrasController : Controller
    {
        // GET: CabeceraFras
        static string FECHAFRA;
        static string SERIE;

        CabeceraFras factura = new CabeceraFras();
        // GET: CabeceraFras
        public ActionResult Index(string Serie, string fechafra)
        {

            CabeceraFras fra = new CabeceraFras();
            IVANNEntities db = new IVANNEntities();
            SERIE = Serie;
            ViewBag.Serie = Serie;
            FECHAFRA = fechafra;
            Pacientes paciente = new Pacientes();
            CabFrasGlobal PACI_FRA = new CabFrasGlobal();
            PACI_FRA.PacientesGlob = paciente.GetALLPacientes().ToList();
            Pacientes pacien = new Pacientes();


            return View(PACI_FRA);
        }

        public ActionResult Editaoficial(int? id)
        {
            int IDLINEAFRA = Convert.ToInt32(id);
            CabeceraFras framod = new CabeceraFras();
            IVANNEntities db = new IVANNEntities();
            framod = db.CabeceraFras.Find(IDLINEAFRA);


            return View(framod);
        }


        [HttpPost]

        public ActionResult Index(FormCollection collection)
        {
            string serie = SERIE;
            ViewBag.Serie = SERIE.ToString();
            IVANNEntities db = new IVANNEntities();

            Pacientes paciente = new Pacientes();

            Pacientes pacien = new Pacientes();
            var varID = collection["IDPACIENTE"];
            string strID = varID.ToString();
            decimal decimalID = Convert.ToDecimal(varID.ToString());
            int intID = Convert.ToInt32(decimalID);
            var strTotal = collection["TOTAL"];
            decimal total = Convert.ToDecimal(strTotal.Replace(".", ","));
            // pacien = db.Pacientes.Find(ModelBinderAttribute.IDPACIENTE);
            CabeceraFras nuevafra = new CabeceraFras();
            CabFrasGlobal PACI_FRA = new CabFrasGlobal();
            PACI_FRA.PacientesGlob = paciente.GetALLPacientes().ToList();
            return View(PACI_FRA);
        }


        [HttpPost]
        public ActionResult Editaoficial(FormCollection collection)
        {
            var strTotal = collection["TOTAL"];
            decimal total = Convert.ToDecimal(strTotal.Replace(".", ","));

            return View();
        }
    }
}