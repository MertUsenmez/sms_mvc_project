using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using sms_mvc_project.App_Start;
using sms_mvc_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace sms_mvc_project.Controllers
{
    public class MembershipController : Controller
    {
        MongoDBContext dbcontext;
        IMongoCollection<Student> studentCollection;

        public MembershipController()
        {
            dbcontext = new MongoDBContext();
            studentCollection = dbcontext.database.GetCollection<Student>("StudentCollection");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registration(Student student)
        {

            var stu = (from s in studentCollection.AsQueryable<Student>()
                       where s.StudentEmail == student.StudentEmail &&
                       s.StudentPassword == student.StudentPassword
                       select s).Count();

            if (stu == 1)
            {
                TempData["Message"] = "Student already exist ! ";
            }
            else if (stu == 0)
            {

                string randomPassword = CreateRandomPassword();

                DateTime nowDate = DateTime.Now;
                string date = nowDate.ToString();

                studentCollection.InsertOne(student);

                bool result = false;

                
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                //Burada mail hesabında bir ayarlama yapmak gerekiyor
                WebMail.UserName = "umutavci.se@gmail.com";
                WebMail.Password = "gencbeyinlerisikeyim!";    // Şuan bilinmiyor
                WebMail.EnableSsl = true;

                try
                {
                    WebMail.Send(
                           to: student.StudentEmail,
                           subject: "Student Matching Sistem Giriş Doğrulaması",
                           body: "<strong> Merhaba </strong><p> SMS sitesine giriş yapabilmeniz " +
                           "için aşağıdaki kodu web sayfanızda bulunan boşluğa giriniz.</p><br/> " +
                           "<p><strong>Doğrulama Kodu : " + randomPassword + "</strong></p>",
                           replyTo: student.StudentEmail, isBodyHtml: true);

                    result = true;

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Registration");
                }
                
            }
            else{

                TempData["Message"] = "Have a Exception ! ";
            }

            return RedirectToAction("Student/AllStudents");
        }


        [HttpGet]
        public ActionResult ConfirmRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmRegister(Student student, string id)
        {
            DateTime EnterConfirmCodeTime = DateTime.Now;


            var stu = (from s in studentCollection.AsQueryable<Student>()
                       where s.ConfirmCode == student.ConfirmCode &&
                       s.StudentEmail == student.StudentEmail &&
                       s.SendConfirmCodeTime == student.SendConfirmCodeTime
                       select s).FirstOrDefault();

            if (stu != null)
            {
                //Student stud = new Student();

                var studentId = new ObjectId(id);
                var studentIdQuery = studentCollection.AsQueryable<Student>().SingleOrDefault(x => x._id == studentId);

                string SendConfirmTime = studentIdQuery.SendConfirmCodeTime;

                if ((EnterConfirmCodeTime - Convert.ToDateTime(SendConfirmTime)).TotalMinutes > 3600)
                {
                        studentCollection.DeleteOne(Builders<Student>.Filter.Eq("_id", ObjectId.Parse(id)));
                        return RedirectToAction("AllStudents");
                }
                else
                {
                    string confirmCode = studentIdQuery.ConfirmCode;

                    // Girişi kabul edeceğiz
                    if (confirmCode == student.ConfirmCode)
                    {
                        TempData["Message"] = "Have a Exception ! ";
                    }
                    else
                    {
                        studentCollection.DeleteOne(Builders<Student>.Filter.Eq("_id", ObjectId.Parse(id)));
                    }
                }
            }

            return View();
        }




        // Create random password method
        public string CreateRandomPassword()
        {
            int length = 6;
            string characters = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random choiceCharacter = new Random();
            char[] character = new char[length];
            for (int i = 0; i < length; i++)
            {
                character[i] = characters[Convert.ToInt32((characters.Length - 1) * choiceCharacter.NextDouble())];
            }
            return new string(character);
        }
    }


    /*
     * 
     * //var count = studentCollection.Find<Student>(queryEmail).Count();
     var query = Query.And(Query.EQ("StudentEmail", student.StudentEmail),
                                       Query.Matches("EmailLower", student.StudentEmail.ToLowerInvariant()));

            var bsonDocument = this.studentCollection.FindAs<BsonDocument>(query);
     
     
     */
}