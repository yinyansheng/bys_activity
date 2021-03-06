﻿using bys.activity.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bys.activity.web.Models
{
    public class CreateActivityVM
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid ActivityTypeID { get; set; }
        public IEnumerable<ActivityType> ActivityTypes { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }


    }
}