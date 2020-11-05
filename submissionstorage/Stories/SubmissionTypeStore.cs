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
    public class SubmissionTypeStore : StoreService<Submission_type, CommonContext>
    {
        /// //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Submission
        /// </summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////
        public SubmissionTypeStore(CommonContext context) : base(context) { }

        public async Task<Submission_type> GetById(long id)
        {
            return await Query.FirstOrDefaultAsync(_ => _.Id == id);
        }
        public async Task<List<Submission_type>> GetAll()
        {
            return await Query.OrderBy(_ => _.Name).ToListAsync();
        }
        public async Task<List<string>> GetAllNames()
        {
            return await Query.Select(_ => _.Name).OrderBy(_ => _).ToListAsync();
        }
    }
}
