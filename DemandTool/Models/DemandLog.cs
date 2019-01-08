using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DemandTool.MVC.Models
{
    public class DemandLog
    {
        public long Id { get; set; }
        public AssignedTeam AssignedTeam { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public string Comments { get; set; }
        public DateTime  UpdatedDate { get; set; }
        public ServiceLine ServiceLine { get; set; }
        public Priority Priority { get; set; }
        public RAG RAG { get; set; }
        public DemandStatus DemandStatus { get; set; }
        public Phase Phase { get; set; }
        
        #region Demand
        public long DemandId { get; set; }
        public virtual Demand Demand { get; set; }
        #endregion


    }
    public enum AssignedTeam
    {
        [Display(Name = "Amelia CEO")]
        AmeliaCEO,
        [Display(Name = "ITSM COE")]
        ITSMCOE,
        [Display(Name = "Service Broker")]
        ServiceBroker,
        [Display(Name = "Monitoring COE")]
        MonitoringCOE,
        [Display(Name = "Analytics COE")]
        AnalyticsCOE,
        [Display(Name = "Product Management")]
        ProductMgt,
        Architecture,
        Vendor,
        Customer,
        [Display(Name = "Future Plan")]
        FuturePlan,
        [Display(Name = "Networks Team")]
        NetworksTeam,
        [Display(Name = "Program Management")]
        ProgramManagement,
        [Display(Name = "Automation CoE")]
        AutomationCoE,
        [Display(Name = "Testing CoE")]
        TestingCoE,
        [Display(Name = "Tools Team")]
        ToolsTeam,
        Digitaization,
        [Display(Name = "Non COE Team")]
        NonCOETeam,
        [Display(Name = "Robotics CoE")]
        RoboticsCoE,
        OnHold


    }

    public enum TeamStatus { Completed, Assigned, Rejected, Cancelled, onHold }




}