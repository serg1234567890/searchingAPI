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
        public async Task<ActionResult> SaveControls(List<Control> controls)
        {
            var types = await this.submissionTypeStore.GetAllNames();
            foreach (Control control in controls)
            {
                if (types.Contains(control.Type)) continue;
                submissionTypeStore.Insert(new Submission_type(control.Type));
                types.Add(control.Type);
            }
            this.submissionTypeStore.Save();

            var allsub = await this.submissionStore.GetAll();
            var allnewsubtype = await this.submissionTypeStore.GetAll();
            foreach (Control control in controls)
            {
                long foreign = allnewsubtype.Where(_ => _.Name == control.Type).SingleOrDefault().Id;
                var sub = new Submission(control.Value, foreign);
                submissionStore.Insert(sub);
            }
            submissionStore.Save();

            return Ok();
        }
        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<Control>>> Search()
        {
            var allsub = await this.submissionStore.GetAll();
            var list = allsub.Select(_ => new Control(_.Id, "field" + _.Id, _.Type.Name, _.Fieldvalue));
            return Ok(list);
        }
        [HttpPost]
        [Route("clear")]
        public async Task<ActionResult<IEnumerable<Control>>> Clear(List<Control> controls)
        {
            var allsub = await this.submissionStore.GetAll();
            this.submissionStore.Delete(allsub);
            this.submissionStore.Save();

            var allsubtype = await this.submissionTypeStore.GetAll();
            this.submissionTypeStore.Delete(allsubtype);
            this.submissionTypeStore.Save();

            await SaveControls(controls);

            var allnewsub = await this.submissionStore.GetAll();
            var list = allnewsub.Select(_ => new Control(_.Id, "field" + _.Id, _.Type.Name, _.Fieldvalue));
            return Ok(list);
        }
        [HttpPost]
        [Route("set")]
        public async Task<ActionResult<IEnumerable<Control>>> Set(List<Control> controls)
        {
            await SaveControls(controls);

            var allnewsub = await  this.submissionStore.GetAll();
            var list = allnewsub.Select(_ => new Control(_.Id, "field" + _.Id, _.Type.Name, _.Fieldvalue));
            return Ok(list);
        }
        [HttpPost]
        [Route("list")]
        public async Task<ActionResult<IEnumerable<Control>>> List(List<Control> defaults)
        {
            var allsub = await this.submissionStore.GetAll();
            if (allsub.Count == 0)
            {
                await SaveControls(defaults);
                allsub = await  this.submissionStore.GetAll();
            }

            var list = allsub.Select(_ => new Control(_.Id, "field" + _.Id, _.Type.Name, _.Fieldvalue));
            return Ok(list);
        }
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<Control>> Add(Control control)
        {
            var allnewsubtype = await submissionTypeStore.GetAll();
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

            return Ok(new Control(sub, type.Name));
        }
        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> Remove(Control control)
        {
            if (!control.Id.HasValue)
            {
                return BadRequest("Control ID not defined");
            }
            var sub = await this.submissionStore.GetById(control.Id.Value);
            this.submissionStore.Delete(sub);
            this.submissionStore.Save();

            return Ok(control);
        }
        [HttpPost]
        [Route("change")]
        public async Task<ActionResult> Change(Control control)
        {
            if (!control.Id.HasValue)
            {
                return BadRequest("Control ID not defined");
            }

            var sub = await this.submissionStore.GetById(control.Id.Value);
            sub.Fieldvalue = control.Value;
            this.submissionStore.Save();

            return Ok(control);
        }
    }
}
