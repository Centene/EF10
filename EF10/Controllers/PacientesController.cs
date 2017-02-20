using EF10.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF10.Controllers
{
    public class PacientesController : Controller
    {
        Pacientes paciente = new Pacientes();
        CabeceraFras factura = new CabeceraFras();
        // GET: Pacientes
        public ActionResult Index()
        {
            IVANNEntities db = new IVANNEntities();
            CabFrasGlobal PACI_FRA = new CabFrasGlobal();
            PACI_FRA.PacientesGlob = paciente.GetALLPacientes().ToList();
            Pacientes pacien = new Pacientes();
            return View(PACI_FRA);
        }
        public ActionResult MuestraPaciente()
        {
            List<Pacientes> pacientes = new List<Pacientes>();
            IVANNEntities db = new IVANNEntities();
            ViewBag.Pacientes = db.Get_All_Pacientes();
            //ViewDta["valor"] = paciente.IDPACIENTE;
            return View();

        }
        public ActionResult Edita(int? id)
        {
            IVANNEntities db = new IVANNEntities();
            Pacientes paciente = db.Pacientes.Find(id);
            ViewData["valor"] = paciente.IDPACIENTE;
            return View(paciente);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Pacientes info)
        {
            CabeceraFras fra = new CabeceraFras();
            Pacientes paci = new Pacientes();
            IVANNEntities db = new IVANNEntities();
            Pacientes paciente = new Pacientes();

            var id = info.IDPACIENTE;
            int idpaciente = Convert.ToInt32(id);
            CabFrasGlobal PACI_FRA = new CabFrasGlobal();
            PACI_FRA.PacientesGlob = paci.GetALLPacientes().ToList();
            //PACI_FRA.FacturasGlob = fra.GetFrasByIDPACIENTE(idpaciente);
            //return View(PACI_FRA);

            //Esta paste es nueva 07/01/2017
            CabFrasGlobal PACI = new CabFrasGlobal();
            PACI_FRA.paciente = db.Pacientes.Find(info.IDPACIENTE);
            return View(PACI_FRA);
        }
        [HttpPost]
        public ActionResult Edita(Pacientes model)
        {
            IVANNEntities db = new IVANNEntities();
            string nombre = model.NOMBRE_Y_APELLIDOS;
            Pacientes paciente_mod = new Pacientes();
            // actualizar en la base de datos
            paciente_mod = db.Pacientes.Find(model.IDPACIENTE);
            paciente_mod.NOMBRE_Y_APELLIDOS = model.NOMBRE_Y_APELLIDOS;
            UpdateModel(paciente_mod);
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
        public ActionResult Create(Pacientes model_paciente)
        {
            IVANNEntities db = new IVANNEntities();
            try
            {

                db.Pacientes.Add(model_paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

            //return View();
        }
        [HttpPost]
        public ActionResult MuestraPaciente(int? id)
        {
            IVANNEntities db = new IVANNEntities();
            Pacientes paciente = db.Pacientes.Find(id);
            ViewData["valor"] = paciente.IDPACIENTE;



            return PartialView(paciente);
        }

    }
}