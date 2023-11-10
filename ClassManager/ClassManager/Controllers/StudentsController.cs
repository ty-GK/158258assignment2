using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using ClassManager.Data;
using ClassManager.Models;
using ClassManager.ViewModels;
using PagedList;

namespace ClassManager.Controllers
{
    public class StudentsController : Controller
    {
        private ClassManagerContext db = new ClassManagerContext();

        // GET: Students
        public ActionResult Index(string enrollmentYear, string search, int? page, string sortBy)
        {

            if (Session["Username"] != null)
            {
                string userId = Session["Username"].ToString();
                var students = db.Students.Where(c => c.TeacherId == userId);

                StudentIndexViewModel viewModel = new StudentIndexViewModel();

                if (!String.IsNullOrEmpty(search))
                {
                    students = students.Where(p => p.Name.Contains(search) ||
                    p.StudentNumber.Contains(search));
                    viewModel.Search = search;
                }

                viewModel.EnYearWithCount = from mathchingStudents in students
                                            group mathchingStudents by
                                            mathchingStudents.EnrollmentYear into
                                            enYearGroup
                                            select new EnrollmentYearWithCount()
                                            {
                                                EnrollmentYearStr = enYearGroup.Key.ToString(),
                                                StudentCount = enYearGroup.Count()
                                            };

                if (!String.IsNullOrEmpty(enrollmentYear))
                {
                    students = students.Where(p => p.EnrollmentYear.ToString() == enrollmentYear);
                    viewModel.EnrollmentYear = enrollmentYear;
                }

                switch(sortBy)
                {
                    case "studentId_ascending":
                        students = students.OrderBy(s => s.StudentNumber);
                        break;
                    case "studentId_descending":
                        students = students.OrderByDescending(s => s.StudentNumber);
                        break;
                    default:
                        students = students.OrderBy(s => s.Name);
                        break;
                }


                const int PageItems = 3;
                int currentPage = (page ?? 1);
                viewModel.Students = students.ToPagedList(currentPage, PageItems);

                viewModel.SortBy = sortBy;
                viewModel.Sorts = new Dictionary<string, string>
                {
                    {"Sort Students ID Ascending", "studentId_ascending"},
                    {"Sort Students ID Descending", "studentId_descending" }
                };

                ViewBag.UserName = Session["Username"].ToString();
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,Name,StudentNumber,EnrollmentYear,Gender,Age,TeacherId")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.TeacherId = Session["Username"].ToString();
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,Name,StudentNumber,EnrollmentYear,Gender,Age,TeacherId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
