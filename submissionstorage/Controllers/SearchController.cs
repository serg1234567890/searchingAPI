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
        [Route("reset")]
        public IEnumerable<Control> Reset()
        {
            var allsub = this.submissionStore.GetAll();
            this.submissionStore.Delete(allsub);
            this.submissionStore.Save();

            var allsubtype = this.submissionTypeStore.GetAll();
            this.submissionTypeStore.Delete(allsubtype);
            this.submissionTypeStore.Save();

            var types = new List<string>();
            var defaultControls = new Stories.DefaultControls();
            foreach (Control control in defaultControls.Items)
            {
                if (types.Contains(control.Type)) continue;
                submissionTypeStore.Insert(new Submission_type(control.Type));
                types.Add(control.Type);
            }
            this.submissionTypeStore.Save();

            var allnewsubtype = this.submissionTypeStore.GetAll();
            foreach (Control control in defaultControls.Items)
            {
                long foreign = allnewsubtype.Where(_ => _.Name == control.Type).SingleOrDefault().Id;
                var sub = new Submission(control.Name, control.Value, foreign);
                submissionStore.Insert(sub);
            }
            submissionStore.Save();

            return defaultControls.Items;
        }
        [HttpGet]
        [Route("list")]
        public IEnumerable<Control> List()
        {
            var allsub = this.submissionStore.GetAll();
            if (allsub.Count == 0)
            {
                var defaultControls = new Stories.DefaultControls();
                return defaultControls.Items;
            }
            else
            {
                var list = allsub.Select(_ => new Control(_.Id, _.Fieldname, _.Type.Name, _.Fieldvalue));
                return list;
            }
        }
        [HttpPost]
        [Route("add")]
        public void Add(Control control)
        {
            var allnewsubtype = submissionTypeStore.GetAll();
            var exist = allnewsubtype.Where(_ => _.Name == control.Name).ToList();
            Submission_type type = null;

            if (exist.Count > 0) type = exist[0];
            {
                type = new Submission_type(control.Type);
                submissionTypeStore.Insert(type);
                submissionTypeStore.Save();
            }

            var sub = new Submission(control.Name, control.Value, type.Id);
            submissionStore.Insert(sub);
            submissionStore.Save();
        }
        [HttpPost]
        [Route("remove")]
        public void Remove(Control control)
        {
            var sub = this.submissionStore.GetByName(control.Name);
            this.submissionStore.Delete(sub);
            this.submissionStore.Save();
        }
        [HttpPost]
        [Route("change")]
        public void Change(Control control)
        {
            var sub = this.submissionStore.GetByName(control.Name);
            sub.Fieldvalue = control.Value;
            this.submissionStore.Save();
        }
    }
}
