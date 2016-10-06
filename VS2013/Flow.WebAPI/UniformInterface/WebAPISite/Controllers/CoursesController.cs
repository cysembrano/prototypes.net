using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPISite.Controllers
{
    public class CoursesController : ApiController
    {
        public IEnumerable<Course> Get()
        {
            return courses;
        }

        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage msg = null;
            var ret = (from c in courses where c.id == id select c).FirstOrDefault();
            if(ret == null)
            {
                msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course not Found");
            }
            else
            {
                msg = Request.CreateResponse<Course>(HttpStatusCode.OK, ret);
            }
            return msg;
        }

        public HttpResponseMessage Post([FromBody]Course c)
        {
            c.id = courses.Count;
            courses.Add(c);

            var msg = Request.CreateResponse(statusCode: HttpStatusCode.Created);
            msg.Headers.Location = new Uri(Request.RequestUri + c.id.ToString());

            return msg;

        }

        public void Put(int id, [FromBody]Course c)
        {
            var ret = (from cors in courses where cors.id == id select cors).FirstOrDefault();
            ret.title = c.title;
        }

        public void Delete(int id)
        {
            var ret = (from cors in courses where cors.id == id select cors).FirstOrDefault();
            courses.Remove(ret);
        }

        static List<Course> courses = InitCourses();

        private static List<Course> InitCourses()
        {
            var ret = new List<Course>();
            ret.Add(new Course { id=0, title="Math"});
            ret.Add(new Course { id = 1, title = "English" });
            ret.Add(new Course { id = 2, title = "Language" });
            return ret;
        }

    }

    public class Course
    {
        public int id;
        public string title;
    }
}
