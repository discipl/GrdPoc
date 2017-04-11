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
    public class BlockchainAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlockchainAccounts
        public ActionResult Index()
        {
            var blockchainAccounts = db.BlockchainAccounts.Include(b => b.PreferedBlockChainNodeServer);
            return View(blockchainAccounts.ToList());
        }

        // GET: BlockchainAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlockchainAccount blockchainAccount = db.BlockchainAccounts.Find(id);
            if (blockchainAccount == null)
            {
                return HttpNotFound();
            }
            return View(blockchainAccount);
        }

        // GET: BlockchainAccounts/Create
        public ActionResult Create()
        {
            ViewBag.PreferedBlockChainNodeServerId = new SelectList(db.BlockchainNodeServers, "BlockChainNodeServerId", "NodeServerName");
            return View();
        }

        // POST: BlockchainAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlockChainAccountId,AccountName,AccountAddress,AccountPassword,AccountType,AccountActive,PreferedBlockChainNodeServerId")] BlockchainAccount blockchainAccount)
        {
            if (ModelState.IsValid)
            {
                db.BlockchainAccounts.Add(blockchainAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PreferedBlockChainNodeServerId = new SelectList(db.BlockchainNodeServers, "BlockChainNodeServerId", "NodeServerName", blockchainAccount.PreferedBlockChainNodeServerId);
            return View(blockchainAccount);
        }

        // GET: BlockchainAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlockchainAccount blockchainAccount = db.BlockchainAccounts.Find(id);
            if (blockchainAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.PreferedBlockChainNodeServerId = new SelectList(db.BlockchainNodeServers, "BlockChainNodeServerId", "NodeServerName", blockchainAccount.PreferedBlockChainNodeServerId);
            return View(blockchainAccount);
        }

        // POST: BlockchainAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlockChainAccountId,AccountName,AccountAddress,AccountPassword,AccountType,AccountActive,PreferedBlockChainNodeServerId")] BlockchainAccount blockchainAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blockchainAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PreferedBlockChainNodeServerId = new SelectList(db.BlockchainNodeServers, "BlockChainNodeServerId", "NodeServerName", blockchainAccount.PreferedBlockChainNodeServerId);
            return View(blockchainAccount);
        }

        // GET: BlockchainAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlockchainAccount blockchainAccount = db.BlockchainAccounts.Find(id);
            if (blockchainAccount == null)
            {
                return HttpNotFound();
            }
            return View(blockchainAccount);
        }

        // POST: BlockchainAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlockchainAccount blockchainAccount = db.BlockchainAccounts.Find(id);
            db.BlockchainAccounts.Remove(blockchainAccount);
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
