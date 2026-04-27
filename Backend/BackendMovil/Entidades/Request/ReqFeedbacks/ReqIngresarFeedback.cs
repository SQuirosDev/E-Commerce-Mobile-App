using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendMovil.Entidades
{
    public class ReqIngresarFeedback : ReqBase
    {
        public FeedBacks Feedback { get; set; }
    }
}
