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

        public Submission_type GetById(long id)
        {
            return Query
                .FirstOrDefault(_ => _.Id == id);
        }

        public List<Submission_type> GetAll()
        {
            return Query
                .ToList();
        }
    }
}
