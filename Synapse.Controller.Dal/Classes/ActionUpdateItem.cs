﻿using System;

using Synapse.Core;

namespace Synapse.Services.Controller.Dal
{
    public class ActionUpdateItem
    {
        public string PlanUniqueName { get; set; }
        public long PlanInstanceId { get; set; }
        public ActionItem ActionItem { get; set; }
        public int RetryAttempts { get; set; }
    }
}