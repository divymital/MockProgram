using SPC.BAL;
using SPC.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SPC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SalesModel argSalesModel)
        {
            StringBuilder sb = new StringBuilder();

            if (ModelState.IsValid) // Additional validations can be executed in a function within if block and the error may be displayed in else block accordingly 
            {
                ISalesFacade l_SalesFacade = FacadeFactory.GetSalesFacade();
                bool l_status = l_SalesFacade.InsertSalesInfo(argSalesModel.GetSalesDTO());

                if(l_status == true)
                {
                    argSalesModel = new SalesModel();
                    ViewBag.Success = "sales inserted !";
                    ModelState.Clear();
                    return View("Index",argSalesModel);
                }
                else
                {
                    ViewBag.Error = "sales can not be inserted !";
                    return View("Index", argSalesModel);
                }
            }
            else
            {                
                foreach (var item in ModelState.Values)
                {
                    foreach (var innerItem in item.Errors)
                    {
                        sb.AppendLine(innerItem.ErrorMessage);
                    }
                }
                ViewBag.Error = "sales can not be inserted !" + sb.ToString();
                return View("Index", argSalesModel);
            }
        }

        /// <summary>
        /// Retunrs the graph view
        /// </summary>
        /// <returns></returns>
        public ActionResult Graph()
        {
            return View();
        }

        /// <summary>
        /// Returns partial view with the model having the source data for the graph
        /// </summary>
        /// <returns></returns>
        public ActionResult GraphPartial()
        {
            GraphModel l_GraphModel = new GraphModel();
            ISalesFacade l_SalesFacade = FacadeFactory.GetSalesFacade();
            //bool l_status = l_SalesFacade.InsertSalesInfo(argSalesModel.GetSalesDTO());

            DataTable l_dt = l_SalesFacade.GetSalesInfo();

            foreach (DataRow row in l_dt.Rows)
            {
                Graph l_Graph = new Graph();
                l_Graph.SalesmanName = Convert.ToString(row["SalesmanName"]);
                l_Graph.Sales = Convert.ToInt32(row["Sales"]);
                l_GraphModel.data.Add(l_Graph);
            }

            ModelState.Clear();

            return PartialView("_GraphPartial",l_GraphModel);
        }        
    }
}