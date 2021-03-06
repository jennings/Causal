﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Causal.Model.Updater.v1;
using Causal.Updater.Updates;

namespace Causal.Updater.v1
{
    public class UpdateController : ApiController
    {
        private readonly UpdateRunner updateRunner;

        public UpdateController()
        {
            this.updateRunner = new UpdateRunner();
        }

        public UpdateStatus Get(string id)
        {
            return new UpdateStatus
            {
                ProductId = id
            };
        }

        public UpdateRequestResponse Post(string id)
        {
            var willUpdate = this.updateRunner.BeginUpdate(id);
            return new UpdateRequestResponse
            {
                ProductId = id,
                UpdateBeginning = willUpdate
            };
        }
    }
}
