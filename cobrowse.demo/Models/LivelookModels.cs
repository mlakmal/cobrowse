using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cobrowse.demo.Models
{
    public class AgentModel
    {
        [Display(Name = "Push type?")]
        public string PushType { get; set; }

        [Display(Name = "Members username?")]
        public string Username { get; set; }

        public bool? EnableSuccess { get; set; }
    }

    public class MemberModel
    {
        public string PushType { get; set; }
    }

    public class EmulateModel
    {
        public string PushType { get; set; }
        public string Username { get; set; }
    }
}