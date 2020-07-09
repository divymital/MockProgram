using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC.BAL
{
    /// <summary>
    /// This class creates objects for facade or any other related obejcts if any.
    /// </summary>
    public class FacadeFactory
    {
        /// <summary>
        /// Returns the object for sales facade
        /// </summary>
        /// <returns></returns>
        public static ISalesFacade GetSalesFacade()
        {
            return new SalesFacade();
        }
    }
}