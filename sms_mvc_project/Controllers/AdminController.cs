using MongoDB.Driver;
using sms_mvc_project.App_Start;
using sms_mvc_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sms_mvc_project.Controllers
{
    public class AdminController : Controller
    {

        MongoDBContext dbcontext;
        IMongoCollection<Student> studentCollection;
        IMongoCollection<Admin> adminCollection;
        IMongoCollection<Match> matchCollection;


        public AdminController()
        {
            dbcontext = new MongoDBContext();
            studentCollection = dbcontext.database.GetCollection<Student>("StudentCollection");
            adminCollection = dbcontext.database.GetCollection<Admin>("Admin");
            matchCollection = dbcontext.database.GetCollection<Match>("Matches");
        }

        [HttpGet]
        public ActionResult ManageStudents()
        {
            List<Student> students = studentCollection.AsQueryable<Student>().ToList();

            return View(students);
        }

        [HttpGet]
        public ActionResult ManageMatching()
        {
            MatchingController MatchingObject = new MatchingController();

            MatchingObject.Algorithm();

            List<Match> matchStudents = matchCollection.AsQueryable<Match>().ToList();

            return View(matchStudents);
        }

    }
}