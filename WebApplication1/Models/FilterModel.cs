using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Models
{
    public class FilterModel
    {
        public DataTable ATable { get; set; }
    }
}