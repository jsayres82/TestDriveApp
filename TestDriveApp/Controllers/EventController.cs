using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;
using TestDriveApp.Models;

namespace TestDriveApp.Controllers
{
    public class EventController : Controller
    {
        TestDriveDataContext dc = new TestDriveDataContext();

        public ActionResult Edit(string id)
        {
            var ids = Convert.ToInt32(id);
            var t = (from tr in dc.Events where tr.Id == ids select tr).First();
            var ev = new EventData
            {
                Id = t.Id,
                Start = t.Start,
                End = t.End,
                Text = t.Text
            };
            return View(ev);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            int id = Convert.ToInt32(form["Id"]);
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            string text = form["Text"];

            var record = (from e in dc.Events where e.Id == id select e).First();
            record.Start = start;
            record.End = end;
            record.Text = text;
            dc.SubmitChanges();

            return new JsonResult { Data = new Dictionary<string, object> { { "result", "OK"} } };
        }

        public class EventData
        {
            public int Id { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public SelectList Resource { get; set; }
            public string Text { get; set; }
        }

    }
}
