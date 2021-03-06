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

    public partial class Sub_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sub_Products()
        {
            this.Activities = new HashSet<Activity>();
        }

        [Display(Name = "ID")] public int Sub_Product_ID { get; set; }
        [Required] [Display(Name = "Product")] public int ProductID { get; set; }
        [Required] [Display(Name = "Sub-Product Name")] public string Sub_Product_Name { get; set; }
        [Required] [Display(Name = "Duration")] public int Sub_Product_Duration { get; set; }
        [Display(Name = "Other Details")] public string Sub_Product_Details { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual Product Product { get; set; }
    }
}
