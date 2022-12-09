using System;
using System.Collections.Generic;
using System.Linq;
using Stx.Shared.Extensions.Common;
using System.Threading.Tasks;

namespace Stx.Web.JP.Core
{
    public class PageStatus
    {
        private bool _IsLoading = false;
        private int _StatusCode = 0;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; }
        }
        public int StatusCode
        {
            get { return _StatusCode; }
            set { _StatusCode = value; }
        }
        public bool IsDisplyErrorBanner
        {
            get { return _StatusCode.InInteger(400, 401, 402, 403); }
        }

    }
}
