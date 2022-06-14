using InterselMCVProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace InterselMCVProject.Controllers
{
    public class LineasController : Controller
    {
        // GET: Lineas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int page=1)
        {   

            return View(Consultar( sortOrder,  currentFilter,  searchString, page));
        }

        public IEnumerable<PhoneLine> Consultar(string sortOrder, string currentFilter, string searchString, int page=1) {

            var context = new InterselDBEntities();
            if (!String.IsNullOrEmpty(searchString))
            {
                IEnumerable<PhoneLine> listPhone = context.PhoneLine.Where(s => s.MobileLine.Contains(searchString)
                                       || s.Description.Contains(searchString)).OrderBy(s=> s.MobileLine);
                return listPhone.ToPagedList(page, 10);
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            using (InterselDBEntities datos = new InterselDBEntities()) {
                IEnumerable<PhoneLine> listPhone = datos.PhoneLine.AsNoTracking().ToList();
                return listPhone.ToPagedList(page, 10);
             }
        }

        public ActionResult IndexFiltro(string line)
        {
            return View(FiltrarLine(line));
        }
        public IEnumerable<PhoneLine> FiltrarLine(string line)
        {
            using (InterselDBEntities datosDetails = new InterselDBEntities())
            {
                var datos = datosDetails.PhoneLine.AsNoTracking().Where(d => d.MobileLine == line).ToList();
                return datos;
            }
        }

        // GET: Lineas/Details/5
        public ActionResult Details(string line, string sortOrder, string currentFilter, string searchString, int page = 1)
        {
            //TempData.Keep("line");
            var context = new InterselDBEntities();
            var name = context.PhoneLine.Where(f => f.MobileLine == line).FirstOrDefault();
            
            ViewData["name"] = name.Description;
            ViewData["phoneline"] = line;
            return View(ConsultarDetalle(line, sortOrder, currentFilter, searchString, page));
        }

        public  IEnumerable<PhoneDetail>  ConsultarDetalle(string line, string sortOrder, string currentFilter, string searchString, int page = 1)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            using (InterselDBEntities datos = new InterselDBEntities())
            {
                IEnumerable<PhoneDetail> datadetail = datos.PhoneDetail.AsNoTracking().Where(d => d.MobileLine == line).ToList();

                return datadetail.ToPagedList(page, 10);
            }
        }

    }
}
