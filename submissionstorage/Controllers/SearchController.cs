using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using submissionstorage.Entities.Searching;
using submissionstorage.Stories;

namespace submissionstorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SubmissionStore submissionStore;
        private readonly SubmissionTypeStore submissionTypeStore;
        public SearchController(
            SubmissionStore submissionStore_,
            SubmissionTypeStore submissionTypeStore_
        )
        {
            this.submissionStore = submissionStore_;
            this.submissionTypeStore = submissionTypeStore_;
        }
        [HttpGet]
        [Route("lastid")]
        public long Lastid()
        {
            var last = this.submissionStore.GetLastId();
            return last!=null?last.Id:0;
        }
        [HttpGet]
        [Route("clear")]
        public IEnumerable<Control> Clear()
        {
            var allsub = this.submissionStore.GetAll();
            this.submissionStore.Delete(allsub);
            this.submissionStore.Save();

            var allsubtype = this.submissionTypeStore.GetAll();
            this.submissionTypeStore.Delete(allsubtype);
            this.submissionTypeStore.Save();

            return new List<Control>();
        }
        [HttpPost]
        [Route("set")]
        public IEnumerable<Control> Set(List<Control> controls)
        {
            var types = this.submissionTypeStore.GetAll().Select(_=>_.Name).ToList();
            foreach (Control control in controls)
            {
                if (types.Contains(control.Type)) continue;
                submissionTypeStore.Insert(new Submission_type(control.Type));
                types.Add(control.Type);
            }
            this.submissionTypeStore.Save();

            var allsub = this.submissionStore.GetAll().ToList();
            var allnewsubtype = this.submissionTypeStore.GetAll();
            foreach (Control control in controls)
            {
                var subexists = allsub.Where(_ => control.Name == "field" + _.Id).FirstOrDefault();
                if (subexists != null) continue;

                long foreign = allnewsubtype.Where(_ => _.Name == control.Type).SingleOrDefault().Id;
                var sub = new Submission(control.Value, foreign);
                submissionStore.Insert(sub);
            }
            submissionStore.Save();

            var allnewsub = this.submissionStore.GetAll().ToList();
            var list = allnewsub.Select(_ => new Control(_.Id, "field" + _.Id, _.Type.Name, _.Fieldvalue));
            return list;
        }
        [HttpGet]
        [Route("list")]
        public IEnumerable<Control> List()
        {
            var allsub = this.submissionStore.GetAll().ToList();
            var list = allsub.Select(_ => new Control(_.Id, "field" + _.Id, _.Type.Name, _.Fieldvalue));
            return list;
        }
        [HttpPost]
        [Route("add")]
        public void Add(Control control)
        {
            var allnewsubtype = submissionTypeStore.GetAll();
            var exist = allnewsubtype.Where(_ => _.Name == control.Type).ToList();
            Submission_type type = null;

            if (exist.Count > 0)
            {
                type = exist[0];
            }
            else
            {
                type = new Submission_type(control.Type);
                submissionTypeStore.Insert(type);
                submissionTypeStore.Save();
            }

            var sub = new Submission(control.Value, type.Id);
            submissionStore.Insert(sub);
            submissionStore.Save();
        }
        [HttpPost]
        [Route("remove")]
        public void Remove(Control control)
        {
            var sub = this.submissionStore.GetById(control.Id);
            this.submissionStore.Delete(sub);
            this.submissionStore.Save();
        }
        [HttpPost]
        [Route("change")]
        public void Change(Control control)
        {
            var sub = this.submissionStore.GetById(control.Id);
            sub.Fieldvalue = control.Value;
            this.submissionStore.Save();
        }
    }
}
