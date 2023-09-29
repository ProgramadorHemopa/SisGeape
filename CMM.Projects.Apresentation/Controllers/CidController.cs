using AutoMapper;
using CCM.Projects.SisGeapeWeb2.Business.Interface;
using CMM.Projects.Apresentation.Models;
using SisGeape2.Apresentation.Messages;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CMM.Projects.Apresentation.Controllers
{
    [Filtros.AutorizarOperadorGEAPE]
    public class CidController : Controller
    {
        private infraMessage msg = new infraMessage();


        ICidBusiness cidBusiness;

        public CidController(ICidBusiness _cidBusiness)
        {
            cidBusiness = _cidBusiness;
        }

        // GET: Cid
        public ActionResult Index()
        {
            List<CidModelView> cid = new List<CidModelView>();
            Mapper.Map(cidBusiness.GetCid(), cid);
            return View(cid);
        }

        // GET: Cid/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cid/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cid/Create
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

        // GET: Cid/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cid/Edit/5
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

        // GET: Cid/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cid/Delete/5
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
