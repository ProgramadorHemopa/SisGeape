using CCM.Projects.SisGeape2.Domain;
using CCM.Projects.SisGeapeWeb2.Business.Interface.SASS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Areas.SASS.Controllers
{
    [Filtros.AutorizarOperadorSASS]
    [RouteArea("SASS")]
    public class HomeController : Controller
    {

        private readonly ICartaoSaudeBusiness cartaoSaudeBusiness;


        public HomeController(ICartaoSaudeBusiness _cartaoSaudeBusiness)
        {
            cartaoSaudeBusiness = _cartaoSaudeBusiness;
        }


        // GET: SASS/Home
        public async Task<ActionResult> Index()
        {
            List<VinculoDomainModel> domainModel = await cartaoSaudeBusiness.buscarVinculoPendenteDeAtendimentoPorData(DateTime.Today.AddMonths(-6), null);
            TempData["ServidorPendente"] = domainModel.Count();
            return View();
        }




        public ActionResult InformativosTopSASS()
        {

            return View("InformativosTop_SASS");
        }

        // GET: SASS/Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SASS/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SASS/Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SASS/Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SASS/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SASS/Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SASS/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
