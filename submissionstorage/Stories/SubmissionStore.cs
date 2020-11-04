﻿using System;
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

        public Submission GetLastId()
        {
            return Query
                .OrderByDescending(_ => _.Id).FirstOrDefault();
        }

        public Submission GetById(long id)
        {
            return Query
                .FirstOrDefault(_ => _.Id == id);
        }

        public List<Submission> GetAll()
        {
            return Query
                .Include(_=>_.Type)
                .ToList();
        }
    }
}
