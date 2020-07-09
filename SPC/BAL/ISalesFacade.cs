using System.Data;
using SPC.DTO;

namespace SPC.BAL
{
    public interface ISalesFacade
    {
        DataTable GetSalesInfo();
        bool InsertSalesInfo(SalesDTO argSalesDTO);
    }
}