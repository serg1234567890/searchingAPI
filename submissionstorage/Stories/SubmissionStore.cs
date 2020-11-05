using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using submissionstorage.Entities;
using submissionstorage.Entities.Searching;
using submissionstorage.Stories.Common;

namespace submissionstorage.Stories
{
    public class SubmissionStore : StoreService<Submission, CommonContext>
    {
        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Submission
        /// </summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////
        public SubmissionStore(CommonContext context) : base(context) { }
        public async Task<Submission> GetById(long id)
        {
            return await Query.FirstOrDefaultAsync(_ => _.Id == id);
        }
        public async Task<List<Submission>> GetAll()
        {
            return await Query.Include(_ => _.Type).OrderBy(_ => _.Id).ToListAsync();
        }
    }
}
