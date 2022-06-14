using InterselMCVProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using PagedList;

namespace InterselMCVProject.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Login()
        {
           return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios usuarios)
        {
            try
            {
                var context = new InterselDBEntities();
                var ousuario = context.Usuarios.Where(f => f.TagUsuario == usuarios.TagUsuario && f.Clave == usuarios.Clave).FirstOrDefault();

                if (ousuario != null)
                {
                    var DatosLinea = context.PhoneLine.AsNoTracking().ToList();
                    ViewData["Nombre"]=ousuario.NombreUsuario;
                    return RedirectToAction("Index","Lineas", DatosLinea.ToPagedList(1,10));
                }
                else
                {
                    return View("Login");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Details/5
        public ActionResult Details()
        {
            using (var context = new InterselDBEntities())
            {
                var query = context.Usuarios.Where(x=> x.Activo?? false).AsNoTracking().ToList();
                return View(query);
            }
        }
       
        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(Usuarios usuario)
        {
            try
            {
                using (var context = new InterselDBEntities()) 
                {
                    
                    context.Usuarios.Add(usuario);
                    context.SaveChanges();
                } 
                
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var context = new InterselDBEntities();
                var ousuario = context.Usuarios.Where(f => f.idUsuario == id).FirstOrDefault();
               // context.Usuarios.Add(ousuario);

                return View(ousuario);
            }
            catch
            {
                return View();
            }
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(Usuarios usuario)
        {
            try
            {

                var context = new InterselDBEntities();
                var ousuario = context.Usuarios.Where(f => f.idUsuario == usuario.idUsuario).FirstOrDefault();
                
                ousuario = usuario;
                context.Usuarios.AddOrUpdate(ousuario);
                context.SaveChanges();
                
                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Usuarios/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var context = new InterselDBEntities();
                var ousuario = context.Usuarios.FirstOrDefault(f => f.idUsuario == id);
                ousuario.Activo = false;
                
                context.SaveChanges();
                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }
    }
}
