using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.Shared;

namespace SIS.Course
{
    [Transaction(TransactionOption.Required)]
    public class SectionR
    {

        public void RemoveSeat()
        {
            try
            {
                Helper.RunSql("UPDATE transaction_demo_sections SET open_seats = open_seats - 1 WHERE open_seats > 0 IF @@ROWCOUNT <> 1 RAISERROR('NO MORE SEATS',16,1)"); // This will fail if no seats left
                int trancount = (int)Helper.RunSql("SELECT @@TRANCOUNT");
                //Console.WriteLine("SectionR: Trancount (should be > 0): {0}", trancount);
                ContextUtil.SetComplete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("SectionR: Caught exception: {0}", ex.Message);
                ContextUtil.SetAbort();
                throw;
            }
        }
    }
}
