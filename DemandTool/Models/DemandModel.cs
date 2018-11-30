using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemandTool.MVC.Models
{
    public class DemandModel
    {
        public long Id { get; set; }
        public Priority Priority { get; set; }
        public RAG RAG { get; set; }
        public DemandStatus DemandStatus { get; set; }
        public ServiceLine ServiceLine { get; set; }
        public Phase Phase { get; set; }
        public AssignedTeam AssignedTeam { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public string Comments { get; set; }
    }
}