using EF10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF10.Controllers
{
    public class HomeController : Controller
    {
        Pacientes paciente = new Pacientes();
        CabeceraFras factura = new CabeceraFras();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Facturacion()
        {
            IVANNEntities db = new IVANNEntities();
            CabFrasGlobal PACI_FRA = new CabFrasGlobal();
            PACI_FRA.PacientesGlob = paciente.GetALLPacientes().ToList();
            Pacientes pacien = new Pacientes();
            return View(PACI_FRA);
        }
        public ActionResult EdicionFras()
        {
            IVANNEntities db = new IVANNEntities();
            CabFrasGlobal PACI_FRA = new CabFrasGlobal();
            PACI_FRA.PacientesGlob = paciente.GetALLPacientes().ToList();
            PACI_FRA.fechainicio = DateTime.Now;
            PACI_FRA.fechafin = DateTime.Now;
            return View(PACI_FRA);
        }

        [HttpPost]
        public ActionResult Facturacion(string Serie, DateTime fecha)
        {
            //EFacturaController otrafra = new EFacturaController();

            string fechita;
            CabeceraFras factura = new CabeceraFras();

            fechita = fecha.ToShortDateString();
            string serie = Serie;

            return RedirectToAction("Index", "CabeceraFras", new { Serie, fechafra = fechita });
        }
        [HttpPost]
        public ActionResult EdicionFras(int? IDPACIENTE, DateTime? fechainicio, DateTime fechafin)
        {

            return View();


        }
    }
}