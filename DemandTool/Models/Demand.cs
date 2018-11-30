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
        public string Customer { get; set; }
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

    //    public static string GetDescription<T>(this T enumerationValue)
    //where T : struct
    //    {
    //        Type type = enumerationValue.GetType();
    //        if (!type.IsEnum)
    //        {
    //            throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
    //        }

    //        //Tries to find a DescriptionAttribute for a potential friendly name
    //        //for the enum
    //        MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
    //        if (memberInfo != null && memberInfo.Length > 0)
    //        {
    //            object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

    //            if (attrs != null && attrs.Length > 0)
    //            {
    //                //Pull out the description value
    //                return ((DescriptionAttribute)attrs[0]).Description;
    //            }
    //        }
    //        //If we have no description attribute, just return the ToString of the enum
    //        return enumerationValue.ToString();

    //}

    //    public static string GetDescription(Enum @enum)
    //    {
    //        if (@enum == null)
    //            return null;

    //        string description = @enum.ToString();

    //        try
    //        {
    //            FieldInfo fi = @enum.GetType().GetField(@enum.ToString());

    //            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

    //            if (attributes.Length > 0)
    //                description = attributes[0].Description;
    //        }
    //        catch
    //        {
    //        }

    //        return description;

    //}
}