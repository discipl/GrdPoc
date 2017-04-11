using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GrdPoc.Models;
using GrdPoc.Models.Entities;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class BlockchainNodeServersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlockchainNodeServers
        public ActionResult Index()
        {
            return View(db.BlockchainNodeServers.ToList());
        }

        // GET: BlockchainNodeServers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlockchainNodeServer blockchainNodeServer = db.BlockchainNodeServers.Find(id);
            if (blockchainNodeServer == null)
            {
                return HttpNotFound();
            }
            return View(blockchainNodeServer);
        }

        // GET: BlockchainNodeServers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlockchainNodeServers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlockChainNodeServerId,NodeServerName,NodeServerNameAddress,NodeServerIpAddress,NodeServerActive")] BlockchainNodeServer blockchainNodeServer)
        {
            if (ModelState.IsValid)
            {
                db.BlockchainNodeServers.Add(blockchainNodeServer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blockchainNodeServer);
        }

        // GET: BlockchainNodeServers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlockchainNodeServer blockchainNodeServer = db.BlockchainNodeServers.Find(id);
            if (blockchainNodeServer == null)
            {
                return HttpNotFound();
            }
            return View(blockchainNodeServer);
        }

        // POST: BlockchainNodeServers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlockChainNodeServerId,NodeServerName,NodeServerNameAddress,NodeServerIpAddress,NodeServerActive")] BlockchainNodeServer blockchainNodeServer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blockchainNodeServer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blockchainNodeServer);
        }

        // GET: BlockchainNodeServers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlockchainNodeServer blockchainNodeServer = db.BlockchainNodeServers.Find(id);
            if (blockchainNodeServer == null)
            {
                return HttpNotFound();
            }
            return View(blockchainNodeServer);
        }

        // POST: BlockchainNodeServers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlockchainNodeServer blockchainNodeServer = db.BlockchainNodeServers.Find(id);
            db.BlockchainNodeServers.Remove(blockchainNodeServer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
