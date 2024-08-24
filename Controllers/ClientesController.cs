using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using System.Configuration;
using crudmongo1.App_Start;
using MongoDB.Driver;
using crudmongo1.Models;

namespace crudmongo1.Controllers
{
    public class ClientesController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<ClienteModel> clienteCollection;

        public ClientesController()
        {
            dbcontext = new MongoDBContext();
            clienteCollection = dbcontext.database.GetCollection<ClienteModel>("Cliente");
        }

        // GET: Clientes
        public ActionResult Index()
        {
            List<ClienteModel> clientes = clienteCollection.AsQueryable<ClienteModel>().ToList();
            return View(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(string id)
        {
            var cedulaCliente = new ObjectId(id);
            var cliente = clienteCollection.AsQueryable<ClienteModel>().SingleOrDefault(x => x.cedulaCliente == cedulaCliente);
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(ClienteModel cliente)
        {
            try
            {
                clienteCollection.InsertOne(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(string id)
        {
            var cedulaCliente = new ObjectId(id);
            var cliente = clienteCollection.AsQueryable<ClienteModel>().SingleOrDefault(x => x.cedulaCliente == cedulaCliente);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ClienteModel cliente)
        {
            try
            {
                var filter = Builders<ClienteModel>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<ClienteModel>.Update
                    .Set("nombreCliente", cliente.nombreCliente)
                    .Set("apellidoCliente", cliente.apellidoCliente);

                var result = clienteCollection.UpdateOne(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(string id)
        {
            var cedulaCliente = new ObjectId(id);
            var cliente = clienteCollection.AsQueryable<ClienteModel>().SingleOrDefault(x => x.cedulaCliente == cedulaCliente);
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, ClienteModel cliente)
        {
            try
            {
                clienteCollection.DeleteOne(Builders<ClienteModel>.Filter.Eq("_id", ObjectId.Parse(id)));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
