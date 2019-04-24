using MongoDB.Bson;
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
    public class MatchingController : Controller
    {
        MongoDBContext dbcontext;
        IMongoCollection<Student> studentCollection;
        IMongoCollection<Match> matchCollection;


        public MatchingController()
        {
            dbcontext = new MongoDBContext();
            studentCollection = dbcontext.database.GetCollection<Student>("StudentCollection");
            matchCollection = dbcontext.database.GetCollection<Match>("Matches");
        }



        /*Deneme değişiklikleri görmek için oluşturuldu*/
        [HttpGet]
        public ActionResult GetStudents()
        {
            List<Student> students = studentCollection.AsQueryable<Student>().ToList();

            return View(students);
        }



        [HttpGet]
        public ActionResult SelectInterestArea(string id)
        {
            var studentId = new ObjectId(id);
            var student = studentCollection.AsQueryable<Student>().SingleOrDefault(x => x._id == studentId);

            return View(student);
        }



        [HttpPost]
        public ActionResult SelectInterestArea(string id, Student student, string list1)
        {
            var t1 = Request.Form["list1"];

            var filter = Builders<Student>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<Student>.Update.Set("InterestAreas", t1);
            /*
            var stu = (from s in studentCollection.AsQueryable<Student>()
                       where s.InterestAreas == t1 select s).FirstOrDefault();
            */
            var result = studentCollection.UpdateOne(filter, update);

            return RedirectToAction("GetStudents");
        }



        [HttpGet]
        public ActionResult StartMatching(string id)
        {
            var studentId = new ObjectId(id);
            var student = studentCollection.AsQueryable<Student>().SingleOrDefault(x => x._id == studentId);

            return View(student);
        }




        [HttpPost]
        public ActionResult StartMatching(string id, Student student)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<Student>.Update.Set("MatchingStatus", student.MatchingStatus);

                var result = studentCollection.UpdateOne(filter, update);

                return RedirectToAction("GetStudents");
            }
            catch
            {
                return View();
            }
        }


        public void Algorithm()
        {
            List<Student> students = studentCollection.AsQueryable<Student>().ToList();

            Dictionary<ObjectId, string> studentDictionary = new Dictionary<ObjectId, string>();

            foreach (var item in students)
            {
                if(item.MatchingStatus == true && item.CountOfMatch<3)
                {
                    studentDictionary.Add(item._id, item.InterestAreas);
                }
            }

            foreach(KeyValuePair<ObjectId,string> stu in studentDictionary.ToList())
            {
                ObjectId id = stu.Key;
                string interest = stu.Value;
                studentDictionary.Remove(stu.Key);

                foreach (KeyValuePair<ObjectId,string> stuFriend in studentDictionary.ToList())
                {
                    // if( )
                    ObjectId idFriend = stuFriend.Key;
                    string interestFriend = stuFriend.Value;

                    if (interest == interestFriend)
                    {
                        studentDictionary.Remove(stuFriend.Key);

                        try
                        {
                            var student = studentCollection.AsQueryable<Student>().SingleOrDefault(x => x._id == id);
                            var studentFriend = studentCollection.AsQueryable<Student>().SingleOrDefault(x => x._id == idFriend);

                            Student stuZZ = new Student();
                            stuZZ = student;
                            Student stuZZ2 = new Student();
                            stuZZ2 = studentFriend;
                            DateTime time = DateTime.Now;
                            Match match = new Match(idFriend, time);
                            stuZZ.Mathces.Add(match);
                            Match match2 = new Match(id, time);
                            stuZZ2.Mathces.Add(match2);

                            int a = student.CountOfMatch;
                            var filter = Builders<Student>.Filter.Eq("_id", id);
                            var update = Builders<Student>.Update.Set("CountOfMatch", a + 1).Set("Mathces", stuZZ.Mathces);
                            var result = studentCollection.UpdateOne(filter, update);
                       
                            int b = studentFriend.CountOfMatch;
                            var filterFriend = Builders<Student>.Filter.Eq("_id", idFriend);
                            var updateFriend = Builders<Student>.Update.Set("CountOfMatch", b + 1).Set("Mathces", stuZZ2.Mathces);
                            var resultFriend = studentCollection.UpdateOne(filterFriend, updateFriend);




                            /*
                            Match match = new Match();
                            match.StudentFriendId = idFriend;
                            matchCollection.InsertOne(match);                        
                            */

                        }
                        catch (Exception ex)
                        {
                            string a = ex.Message;
                        }

                    }
                    
                }
            }
        }
    }
}