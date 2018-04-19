﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class FilterController : Controller
    {
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();
        // GET: Filter
        public ActionResult Index()
        {
            string query = "select Activity_Name, Starting_Date, Participant_Name, Participant_Last_Name, " +
                "Gender_Name, Participant_Date_Of_Birth, Sub_Product_Name, Product_Name, " +
                "Country_Name, Province_Name, Location_Name " +
                "from((((((((Activities a join Activities_Participation ap on ap.ActivityID = a.Activity_ID) " +
                "join Participants p on p.Participant_ID = ap.ParticipantID) " +
                "join Genders g on g.Gender_ID = p.GenderID) " +
                "join Sub_Products sp on sp.Sub_Product_ID = a.SubProductID) " +
                "join Products pr on pr.Product_ID = sp.ProductID) " +
                "join Locations l on l.Location_ID = a.LocationID) " +
                "join Provinces pv on pv.Province_ID = l.ProvinceID) " +
                "join Countries c on c.Country_ID = pv.CountryID)";
            SqlConnection conn = new SqlConnection(@"Data Source=MANUELHDEZ\MSSQLSERVER01;Initial Catalog=Entrena_QL_Database;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            DataTable dt = new DataTable();
            SqlCommand objCommand = new SqlCommand(query, conn);
            SqlDataAdapter sdp = new SqlDataAdapter(objCommand);
            sdp.Fill(dt);
            FilterModel dtw = new FilterModel { ATable = dt };
            objCommand.Dispose();
            sdp.Dispose();
            conn.Close();
            conn.Dispose();
            return View(dtw);
        }
    }
}