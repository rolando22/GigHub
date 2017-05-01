using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Dtos
{
    public class InboxFormViewModelDto
    {
        public List<InboxDto> Vieweds { get; set; }
        public List<InboxDto> NotVieweds { get; set; }

        public InboxFormViewModelDto()
        {
            Vieweds = new List<InboxDto>();
            NotVieweds = new List<InboxDto>();
        }
    }
}