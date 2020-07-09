using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPC.Utility
{
    public class Enums
    {
        
    }

    public enum CRUDOperations
    {
        Select = 1,
        Insert = 2
    }

    public static class Constants
    {
        public static readonly string ConnString = "ConnectionStringName";
        public static readonly string SP_SALESCRUD ="sp_SalesCRUD";
    }
}