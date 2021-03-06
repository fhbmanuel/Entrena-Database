//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Organizations_Relationship
    {
        [Display(Name = "ID")] public int Organization_Relationship_ID { get; set; }
        [Required] [Display(Name = "Organization")] public int OrganizationID { get; set; }
        [Required] [Display(Name = "Partnership Type")] public int PartnershipTypeID { get; set; }
        [Display(Name = "Other Details")] public string Organization_Relationship_Details { get; set; }

        public IEnumerable<Organization> Organizations_Collection { get; set; }
        public IEnumerable<Partnership_Types> Partnerships_Collection { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Partnership_Types Partnership_Types { get; set; }
    }
}
