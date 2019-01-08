using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;


namespace DemandTool.MVC.Models
{
    public class Demand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string DemandNumber { get; set; } // question on why we need demand number
        public DateTime? SubmissionDate { get; set; }
        [DataType(DataType.MultilineText)]
        public string DemandDesc { get; set; }
        public Priority Priority { get; set; }
        public RequestType RequestType { get; set; }
        public RAG RAG { get; set; }
        public ServiceLine ServiceLine { get; set; }
        //public string Customer { get; set; }
        public string CustomerCompany { get; set; }
        public DemandStatus DemandStatus { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string RequesterName { get; set; }
        public bool Blocked { get; set; }
        public string ReasonOfBlockage { get; set; }
        public Phase Phase { get; set; }
        public virtual List<DemandLog> DemandLogs { get; set; }
    }

    public enum Priority { high, medium, low }

    public enum RequestType
    {
        [Display(Name = "BAU Demands")]
        BAUdemands,
        [Display(Name = "BAU Projects")]
        BAUprojects
    }

    public enum RAG { RedColour, AmberColour, GreenColour }

    public enum ServiceLine
    {
        Amelia,
        Automation,

        Rocotics,
        ITSM,
        Analytics,
        Monitoring,
        [Display(Name = "Transformation and on boarding projects")]
        TransformationProject,
        [Display(Name = "Tools and Deployments")]
        EnvironmentsDeployments
    }


    public enum DemandStatus { Completed, Assigned, Rejected, Cancelled, onHold }
    public enum Phase
    {
        InitialReview,
        [Display(Name = "Pre-Assessment Validation")]
        PreAssessmentValidation,
        [Display(Name = "High Level Assessment")]
        HighLevelAssessment,
        [Display(Name = "Business Approval")]
        BusinessApproval,
        [Display(Name = "A waiting Delivery")]
        AwaitingDelivery,
        InDelivery
    }

  
}