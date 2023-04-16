using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastReport;
using FastReport.Web;
using System.IO;
using System.Web.Http;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace Shipping.MvcApp.ReportMainClass
{
    public class ReportMainClassController : Controller
    {


        [System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        public ActionResult PrintReport([FromBody] ReportClass pReportClass)
        {

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

            string[] str_arr_Keys = pReportClass.arr_Keys.Split(',').ToArray();
            string[] str_arr_Values = pReportClass.arr_Values.Split(',').ToArray();
            ReportMainClassController objReportMainClass = new ReportMainClassController();

            WebReport webReport = new WebReport();
            string report_path = "";
            report_path = Path.Combine(HttpRuntime.AppDomainAppPath, "Reports\\" + pReportClass.pReportName + ".frx");

            webReport.Report.Load(report_path);
            webReport.Report.Dictionary.Connections[0].ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            webReport.Report.Dictionary.Connections[0].CommandTimeout = 300;


            for (int i = 0; i < str_arr_Keys.Length; i++)
            {
                if (str_arr_Values[i].Length >= 1)
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                else
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                webReport.Report.SetParameterValue(str_arr_Keys[i].ToString(), str_arr_Values[i].ToString());
            }

            webReport.Width = 1250;
            webReport.Height = 800;
            webReport.Zoom = 1;
            webReport.Prepare();

            ViewBag.WebReport = webReport;
            ViewBag.ReportTitle = pReportClass.pTitle;
            return View("~/Views/ReportMainClass/ReportMainClass.cshtml", ViewBag.WebReport);
        }

        [System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        public ActionResult PrintInqueryReport([FromBody] ReportClass PPrintInqueryReport)
        {
            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

            string[] str_arr_Keys = PPrintInqueryReport.arr_Keys.Trim().Split(',').ToArray();
            string[] str_arr_Values = PPrintInqueryReport.arr_Values.Trim().Split(',').ToArray();
            ReportMainClassController objReportMainClass = new ReportMainClassController();

            WebReport webReport = new WebReport();
            string report_path = "";
            report_path = Path.Combine(HttpRuntime.AppDomainAppPath, "Reports\\" + PPrintInqueryReport.pReportName + ".frx");

            webReport.Report.Load(report_path);
            webReport.Report.Dictionary.Connections[0].ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            for (int i = 0; i < str_arr_Keys.Length; i++)
            {
                //string ID = str_arr_Keys[i].ToString();
                //string Name = str_arr_Values[i].Replace("*", ",");
                if (str_arr_Values[i].Length >= 1 && str_arr_Keys[i] != ("FromDate") && str_arr_Keys[i] != ("ToDate") && str_arr_Keys[i] != ("WithSubAccounts") && str_arr_Keys[i] != ("WithSubAccounts"))
                //if (str_arr_Values[i].Length >= 1 && new[] { 2, 3, 4,5,7 }.Contains(i))
                {
                    str_arr_Values[i] = "," + str_arr_Values[i] + ",";
                    //str_arr_Values[3] = "," + str_arr_Values[3] + ",";
                    //str_arr_Values[4] = "," + str_arr_Values[4] + ",";
                    //str_arr_Values[5] = "," + str_arr_Values[5] + ",";
                    //str_arr_Values[7] = "," + str_arr_Values[7] + ",";
                }
                else
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                webReport.Report.SetParameterValue(str_arr_Keys[i].ToString(), str_arr_Values[i].Replace("*", ","));
            }

            webReport.Width = 1250;
            webReport.Height = 800;
            webReport.Zoom = 1;
            webReport.Prepare();

            ViewBag.WebReport = webReport;
            ViewBag.ReportTitle = PPrintInqueryReport.pTitle;
            return View("~/Views/ReportMainClass/ReportMainClass.cshtml", ViewBag.WebReport);
        }

        [System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        public ActionResult PrintPS_Payment([FromBody] ReportClass PPrintPS_Payment)
        {
            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

            string[] str_arr_Keys = PPrintPS_Payment.arr_Keys.Split(',').ToArray();
            string[] str_arr_Values = PPrintPS_Payment.arr_Values.Split(',').ToArray();
            ReportMainClassController objReportMainClass = new ReportMainClassController();

            WebReport webReport = new WebReport();
            string report_path = "";
            report_path = Path.Combine(HttpRuntime.AppDomainAppPath, "Reports\\" + PPrintPS_Payment.pReportName + ".frx");

            webReport.Report.Load(report_path);
            webReport.Report.Dictionary.Connections[0].ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            for (int i = 0; i < str_arr_Keys.Length; i++)
            {
                //string ID = str_arr_Keys[i].ToString();
                //string Name = str_arr_Values[i].Replace("*", ",");
                if (str_arr_Values[i].Length >= 1)
                //if (str_arr_Values[i].Length >= 1 && new[] { 2, 3, 4,5,7 }.Contains(i))
                {
                    str_arr_Values[i] = str_arr_Values[i];
                    //str_arr_Values[3] = "," + str_arr_Values[3] + ",";
                    //str_arr_Values[4] = "," + str_arr_Values[4] + ",";
                    //str_arr_Values[5] = "," + str_arr_Values[5] + ",";
                    //str_arr_Values[7] = "," + str_arr_Values[7] + ",";
                }
                else
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                webReport.Report.SetParameterValue(str_arr_Keys[i].ToString(), str_arr_Values[i].ToString());
            }

            webReport.Width = 1250;
            webReport.Height = 800;
            webReport.Zoom = 1;
            webReport.Prepare();

            ViewBag.WebReport = webReport;
            ViewBag.ReportTitle = PPrintPS_Payment.pTitle;
            return View("~/Views/ReportMainClass/ReportMainClass.cshtml", ViewBag.WebReport);
        }

        [System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        public ActionResult PrintWithIDs([FromBody] ReportClass PPrintWithIDs)
        {
            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

            string[] str_arr_Keys = PPrintWithIDs.arr_Keys.Trim().Split(',').ToArray();
            string[] str_arr_Values = PPrintWithIDs.arr_Values.Trim().Split(',').ToArray();
            ReportMainClassController objReportMainClass = new ReportMainClassController();

            WebReport webReport = new WebReport();
            string report_path = "";
            report_path = Path.Combine(HttpRuntime.AppDomainAppPath, "Reports\\" + PPrintWithIDs.pReportName + ".frx");

            webReport.Report.Load(report_path);
            webReport.Report.Dictionary.Connections[0].ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            for (int i = 0; i < str_arr_Keys.Length; i++)
            {
                //string ID = str_arr_Keys[i].ToString();
                //string Name = str_arr_Values[i].Replace("*", ",");
                if (str_arr_Values[i].Length >= 1 && str_arr_Keys[i] != ("FromDate") && str_arr_Keys[i] != ("ToDate") && str_arr_Keys[i] != ("HideProfitLossJV")  && str_arr_Keys[i] != ("From_Date") && str_arr_Keys[i] != ("To_Date") && str_arr_Keys[i] != ("FromDate") && str_arr_Keys[i] != ("ToDate") && str_arr_Keys[i] != ("posted") && str_arr_Keys[i] != ("CurrencyID"))
                //if (str_arr_Values[i].Length >= 1 && new[] { 2, 3, 4,5,7 }.Contains(i))
                {
                    str_arr_Values[i] = "," + str_arr_Values[i] + ",";
                    //str_arr_Values[3] = "," + str_arr_Values[3] + ",";
                    //str_arr_Values[4] = "," + str_arr_Values[4] + ",";
                    //str_arr_Values[5] = "," + str_arr_Values[5] + ",";
                    //str_arr_Values[7] = "," + str_arr_Values[7] + ",";
                }
                else
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                webReport.Report.SetParameterValue(str_arr_Keys[i].ToString(), str_arr_Values[i].Replace("*", ","));
            }

            webReport.Width = 1250;
            webReport.Height = 800;
            webReport.Zoom = 1;
            webReport.Prepare();

            ViewBag.WebReport = webReport;
            ViewBag.ReportTitle = PPrintWithIDs.pTitle;
            return View("~/Views/ReportMainClass/ReportMainClass.cshtml", ViewBag.WebReport);
        }

        [System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        public ActionResult PrintReportTwoQueries([FromBody] ReportWithQuery pReportClass)
        {

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

            string[] str_arr_Keys = pReportClass.arr_Keys.Split(',').ToArray();
            string[] str_arr_Values = pReportClass.arr_Values.Split(',').ToArray();
            ReportMainClassController objReportMainClass = new ReportMainClassController();

            WebReport webReport = new WebReport();
            string report_path = "";
            report_path = Path.Combine(HttpRuntime.AppDomainAppPath, "Reports\\" + pReportClass.pReportName + ".frx");

            webReport.Report.Load(report_path);
            webReport.Report.Dictionary.Connections[0].ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            webReport.Report.SetParameterValue("query", pReportClass.query);
            webReport.Report.SetParameterValue("query2",pReportClass.query2);
            for (int i = 0; i < str_arr_Keys.Length; i++)
            {
                if (str_arr_Values[i].Length >= 1)
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                else
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                webReport.Report.SetParameterValue(str_arr_Keys[i].ToString(), str_arr_Values[i].ToString());
            }

            webReport.Width = 1250;
            webReport.Height = 800;
            webReport.Zoom = 1;
            webReport.Prepare();
            var html = webReport.GetHtml();
            ViewBag.WebReport = webReport;
            ViewBag.ReportTitle = pReportClass.pTitle;

            if (pReportClass.pReportType == "Excel")
                webReport.ExportExcel2007();

            return View("~/Views/ReportMainClass/ReportMainClass.cshtml", ViewBag.WebReport);
        }

        [System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        public ActionResult PrintReportQueryAndParams([FromBody] ReportWithParams pReportClass)
        {

            JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

            string[] str_arr_Keys = pReportClass.arr_Keys.Split(',').ToArray();
            string[] str_arr_Values = pReportClass.arr_Values.Split(',').ToArray();
            ReportMainClassController objReportMainClass = new ReportMainClassController();

            WebReport webReport = new WebReport();
            string report_path = "";
            report_path = Path.Combine(HttpRuntime.AppDomainAppPath, "Reports\\" + pReportClass.pReportName + ".frx");

            webReport.Report.Load(report_path);
            webReport.Report.Dictionary.Connections[0].ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            webReport.Report.SetParameterValue("query", pReportClass.query);
            for (int i = 0; i < str_arr_Keys.Length; i++)
            {
                if (str_arr_Values[i].Length >= 1)
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                else
                {
                    str_arr_Values[i] = str_arr_Values[i];
                }
                webReport.Report.SetParameterValue(str_arr_Keys[i].ToString(), str_arr_Values[i].ToString());
            }

            webReport.Width = 1250;
            webReport.Height = 800;
            webReport.Zoom = 1;
            webReport.Prepare();
            var html = webReport.GetHtml();
            ViewBag.WebReport = webReport;
            ViewBag.ReportTitle = pReportClass.pTitle;

            if (pReportClass.pReportType == "Excel")
                webReport.ExportExcel2007();

            return View("~/Views/ReportMainClass/ReportMainClass.cshtml", ViewBag.WebReport);
        }


        //[System.Web.Http.HttpPost, System.Web.Http.HttpGet]
        //public ActionResult TEST_PrintReportFromJSON([FromBody] ReportClass pReportClass)
        //{

        //    JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();

        //    string[] str_arr_Keys = pReportClass.arr_Keys.Split(',').ToArray();
        //    string[] str_arr_Values = pReportClass.arr_Values.Split(',').ToArray();
        //    ReportMainClassController objReportMainClass = new ReportMainClassController();

        //    WebReport webReport = new WebReport();
        //    string report_path = "";
        //    report_path = Path.Combine(HttpRuntime.AppDomainAppPath, "Reports\\" + pReportClass.pReportName + ".frx");

        //    webReport.Report.Load(report_path);
        //    webReport.Report.Dictionary.Connections[0].ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        //    for (int i = 0; i < str_arr_Keys.Length; i++)
        //    {
        //        if (str_arr_Values[i].Length >= 1)
        //        {
        //            str_arr_Values[i] = str_arr_Values[i];
        //        }
        //        else
        //        {
        //            str_arr_Values[i] = str_arr_Values[i];
        //        }
        //        webReport.Report.SetParameterValue(str_arr_Keys[i].ToString(), str_arr_Values[i].ToString());
        //    }

        //    webReport.Width = 1250;
        //    webReport.Height = 800;
        //    webReport.Zoom = 1;






        //    //-----------------
        //    List<Product> Products = new List<Product>();

        //    Products.Add( new Product() { UnitPrice = 50 , Name = "Item1" });
        //    Products.Add(new Product() { UnitPrice = 14, Name = "Item2" });
        //    Products.Add(new Product() { UnitPrice = 1211, Name = "Item3" });
        //    Products.Add(new Product() { UnitPrice = 4550, Name = "Item4" });
        //    Products.Add(new Product() { UnitPrice = 433, Name = "Item5" });
        //    Products.Add(new Product() { UnitPrice = 344450, Name = "Item6" });
        //    webReport.RegisterData(Products, "Products");

        //    webReport.Prepare();

        //    ViewBag.WebReport = webReport;
        //    ViewBag.ReportTitle = pReportClass.pTitle;
        //    return View("~/Views/ReportMainClass/ReportMainClass.cshtml", ViewBag.WebReport);
        //}


    }


    //public class Product
    //{

    //    public string Name { get; set; }
    //    public int UnitPrice { get; set; }
    //}

    //public class Category
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public List<Product> Products { get; set; }
    //}

    public class ReportClass
    {
        public string arr_Keys { get; set; }
        public string arr_Values { get; set; }
        public String pTitle { get; set; }
        public String pReportName { get; set; }
    }
    public class ReportWithParams : ReportClass
    {
        public string query { get; set; }
        public String pReportType { get; set; }
    }

    public class ReportWithQuery : ReportClass
    {
        public string query { get; set; }
        public string query2 { get; set; }
        public string pReportType { get; set; } = "";
    }
}

