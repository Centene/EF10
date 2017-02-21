using EF10.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF10.Controllers
{
    public class CabeceraFrasController : Controller
    {
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
            //

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
        public ActionResult Generar()
        {
            return View();
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
            pacien = db.Pacientes.Find(intID);
            CabeceraFras nuevafra = new CabeceraFras();
            nuevafra.Serie = SERIE;
            string ulfra = "";
            if (nuevafra.Serie == "CD")
            {
                ulfra = nuevafra.GetProximaFraCE();
            }
            else if (nuevafra.Serie == "TEA")
            {
                ulfra = nuevafra.GetProximaFraTEA();
            }
            nuevafra.IDPACIENTE = intID;
            nuevafra.Nº_FACTURA = ulfra;
            nuevafra.TOTAL = total;
            nuevafra.DNI = pacien.DNI;
            nuevafra.NOMBRE_Y_APELLIDOS = pacien.NOMBRE_Y_APELLIDOS;
            nuevafra.FECHA = Convert.ToDateTime(FECHAFRA);
            CabFrasGlobal PACI_FRA = new CabFrasGlobal();
            db.CabeceraFras.Add(nuevafra);
            db.SaveChanges();
            PACI_FRA.PacientesGlob = paciente.GetALLPacientes().ToList();
            return View(PACI_FRA);
        }


        [HttpPost]
        public ActionResult Editaoficial(FormCollection collection)
        {
            var strTotal = collection["TOTAL"];
            decimal total = Convert.ToDecimal(strTotal.Replace(".", ","));
            CabeceraFras fra = new CabeceraFras();
            IVANNEntities db = new IVANNEntities();
            fra.TOTAL = total;
            fra.FECHA = Convert.ToDateTime(collection["FECHA"]);
            fra.IDPACIENTE = Convert.ToInt32(collection["IDPACIENTE"]);
            fra.NOMBRE_Y_APELLIDOS = Convert.ToString(collection["NOMBRE_Y_APELLIDOS"]);
            fra.DNI = Convert.ToString(collection["DNI"]);
            fra.Nº_FACTURA = Convert.ToString(collection["Nº_FACTURA"]);
            int idlinea = Convert.ToInt32(collection["IDLINEAFRA"]);
            CabeceraFras framod = new CabeceraFras();
            framod = db.CabeceraFras.Find(idlinea);
            framod.IDPACIENTE = fra.IDPACIENTE;
            framod.NOMBRE_Y_APELLIDOS = fra.NOMBRE_Y_APELLIDOS;
            framod.Nº_FACTURA = fra.Nº_FACTURA;
            framod.FECHA = fra.FECHA;
            framod.DNI = fra.DNI;
            framod.TOTAL = fra.TOTAL;
            UpdateModel(framod);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string cadena;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Generar(FormCollection collection)
        {
            DateTime param_fecha = Convert.ToDateTime(collection["fecharemesa"]);
            int param_anio= param_fecha.Year;
            //string mes = fecha.Month.ToString();
            string mesminuscula = Convert.ToString(param_fecha.ToString("MMMM"));
            string param_mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mesminuscula);
            
            return View();
        }
    }
}