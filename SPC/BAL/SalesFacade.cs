using SPC.DTO;
using SPC.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SPC.BAL
{
    public class SalesFacade : ISalesFacade
    {
        /// <summary>
        /// Inserts the sales info from the UI
        /// </summary>
        /// <param name="argSalesDTO">Sales DTO prepared from the sales model.</param>
        /// <returns></returns>
        public bool InsertSalesInfo(SalesDTO argSalesDTO)
        {
            bool l_Status = false;
            l_Status = (bool)SalesInfoCRUD(argSalesDTO,CRUDOperations.Insert);
            return l_Status;
        }

        /// <summary>
        /// returns the sales data to generate the graph
        /// </summary>
        /// <returns></returns>
        public DataTable GetSalesInfo()
        {
            DataTable l_dt;

            try
            {
                l_dt = (DataTable)SalesInfoCRUD(new SalesDTO(), CRUDOperations.Select);
            }
            catch(Exception ex)
            {
                l_dt = null;
            }

            return l_dt;
        }

        private object SalesInfoCRUD(SalesDTO argSalesDTO, CRUDOperations argCRUDOperations)
        {
            #region vars
            SqlConnection conn;
            SqlCommand comm;            
            #endregion

            string connstring = ConfigurationManager.ConnectionStrings[Constants.ConnString].ConnectionString.ToString();
            conn = new SqlConnection(connstring);
            comm = new SqlCommand();

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = Constants.SP_SALESCRUD;
            comm.Connection = conn;

            if (argCRUDOperations == CRUDOperations.Insert)
            {
                // Insert the row
                bool l_Status = false;               

                AddSQLParameter(comm,"@CrudType", argCRUDOperations);
                AddSQLParameter(comm,"@SalesmanName", argSalesDTO.SalesmanName);
                AddSQLParameter(comm,"@SalesDate", argSalesDTO.SalesDate);
                AddSQLParameter(comm,"@SalesValue", argSalesDTO.SalesValue);
                AddSQLParameter(comm,"@Location", argSalesDTO.Location);

                conn.Open();
                try
                {
                    comm.ExecuteNonQuery();
                    l_Status = true;
                }
                catch(Exception ex)
                {
                    l_Status = false;
                }
                conn.Close();
                return l_Status;
            }
            else
            {
                // select all the rows
                AddSQLParameter(comm, "@CrudType", argCRUDOperations);

                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
        }

        private void AddSQLParameter(SqlCommand argComm, string argParamName, object argParamValue)
        {
            SqlParameter prm = new SqlParameter(argParamName, argParamValue);
            argComm.Parameters.Add(prm);
        }
    }
}