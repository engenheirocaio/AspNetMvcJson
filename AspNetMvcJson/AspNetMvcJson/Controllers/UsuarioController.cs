using AspNetMvcJson.Models;
using AspNetMvcJson.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvcJson.Controllers
{
    public class UsuarioController : Controller
    {


        string caminhoJson = ConfigurationManager.AppSettings[@"CaminhoArquivo"];


        // GET: Usuario
        public ActionResult Index()
        {
            var Rap = new JsonRepository<Usuarios>(caminhoJson);
            List<Usuarios> userList = new List<Usuarios>(Rap.GetAll());
            return View(userList);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            var Rap = new JsonRepository<Usuarios>(caminhoJson);
            var DetailObject = Rap.GetById(id);
            return View(DetailObject);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(string Name, string Email)
        {
            try
            {
                // TODO: Add insert logic here
                var GeraId = ObjectId.GenerateNewId().ToString();
                var Rap = new JsonRepository<Usuarios>(caminhoJson);
                Rap.Insert(new Usuarios
                {
                    Id = GeraId,
                    Name =  Name,
                    Email = Email

                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            var Rap = new JsonRepository<Usuarios>(caminhoJson);
            var EditObject = Rap.GetById(id);
            return View(EditObject);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(Usuarios u)
        {
            try
            {
                // TODO: Add update logic here
                var Rap = new JsonRepository<Usuarios>(caminhoJson);
                Rap.Update(u);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(string id)
        {
            var Rap = new JsonRepository<Usuarios>(caminhoJson);
            var DeleteObject = Rap.GetById(id);

            return View(DeleteObject);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var Rap = new JsonRepository<Usuarios>(caminhoJson);
                Rap.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
