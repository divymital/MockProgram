using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace SPC.DTO
{
    public class SalesDTO
    {
        public int? SalesID { get; set; }

        /// <summary>
        /// Info about the 
        /// </summary>
        public string SalesmanName { get; set; }
        public DateTime? SalesDate { get; set; }
        public decimal? SalesValue { get; set; }
        public string Location { get; set; }
    }

    /// <summary>
    /// Sales model to capture sales from the UI
    /// </summary>
    public class SalesModel
    {
        public int? SalesID { get; set; }
        [Display(Name ="Salesman Name")]
        [Required]
        public string SalesmanName { get; set; }

        [Display(Name = "Sales Date")]
        [Required]
        public DateTime? SalesDate { get; set; }

        [Display(Name = "Sales Value")]
        [Required]
        public decimal? SalesValue { get; set; }

        [Display(Name = "Sales Location")]
        [Required]
        public string Location { get; set; }

        /// <summary>
        /// Returns saled DTO obejct based on what has been posted from the UI
        /// We may apply some business processing rules if needed.
        /// </summary>
        /// <returns></returns>
        public SalesDTO GetSalesDTO()
        {
            SalesDTO l_SalesDTO = new SalesDTO();

            l_SalesDTO.Location = this.Location;
            l_SalesDTO.SalesDate = this.SalesDate;
            l_SalesDTO.SalesID = this.SalesID;
            l_SalesDTO.SalesmanName = this.SalesmanName;
            l_SalesDTO.SalesValue = this.SalesValue;

            return l_SalesDTO;
        }
    }

    /// <summary>
    /// The model is used in partial view and prepared in Graph action of home controller.
    /// </summary>
    public class GraphModel
    {
        public List<Graph> data { get; set; }

        public GraphModel()
        {
            data = new List<Graph>();
        }
    }

    public class Graph
    {
        public string SalesmanName { get; set; }
        public int? Sales { get; set; }
    }
}