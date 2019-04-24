using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using System.Configuration;
using sms_mvc_project.App_Start;
using MongoDB.Driver;
using sms_mvc_project.Models;
using MongoDB.Driver.Builders;

namespace sms_mvc_project.Controllers
{
    public class StudentController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<Student> studentCollection;
        

        public StudentController()
        {
            dbcontext = new MongoDBContext();
            studentCollection = dbcontext.database.GetCollection<Student>("StudentCollection");

        }

        // GET: Student
        [HttpGet]
        public ActionResult AllStudents()
        {
            List<Student> students = studentCollection.AsQueryable<Student>().ToList();

            return View(students);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var studentId = new ObjectId(id);
            var student = studentCollection.AsQueryable<Student>().SingleOrDefault(x => x._id == studentId);

            return View(student);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            try{

                var stu = (from s in studentCollection.AsQueryable<Student>()
                           where s.StudentEmail == student.StudentEmail &&
                           s.StudentPassword == student.StudentPassword select s).Count();

                if(stu == 0)
                {
                    return View();
                }
                else if(stu != 0)
                {

                }

               studentCollection.InsertOne(student);

                return RedirectToAction("AllStudents");
            }
            catch
            {
                return View();
            }            
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var studentId = new ObjectId(id);
            var student = studentCollection.AsQueryable<Student>().SingleOrDefault(x => x._id == studentId);

            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(string id, Student student)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<Student>.Update
                    .Set("StudentName", student.StudentName)
                    .Set("StudentSurname", student.StudentSurname)
                    .Set("StudentEmail", student.StudentEmail)
                    .Set("StudentPassword", student.StudentPassword)
                    .Set("DOB", student.DOB)
                    .Set("ModifiedOn", DateTime.Now)
                    .Set("Class", student.Class)
                    .Set("DepartmentName", student.DepartmentName)
                    .Set("University", student.University);

                var result = studentCollection.UpdateOne(filter, update);
                

                return RedirectToAction("AllStudents");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var studentId = new ObjectId(id);
            var student = studentCollection.AsQueryable<Student>().SingleOrDefault(x => x._id == studentId);

            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                studentCollection.DeleteOne(Builders<Student>.Filter.Eq("_id",ObjectId.Parse(id)));
                return RedirectToAction("AllStudents");
            }
            catch
            {
                return View();
            }
        }


    }
}