using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Causal.Server.ViewModels
{
    public class UpdateCheckResult
    {
        public string ProductId { get; set; }
        public string LatestVersion { get; set; }
        public Uri DownloadUrl { get; set; }
    }
}