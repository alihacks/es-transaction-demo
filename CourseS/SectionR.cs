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

        [System.EnterpriseServices.AutoComplete]
        public void RemoveSeat()
        {
            try
            {
                Helper.RunSql("UPDATE transaction_demo_sections SET open_seats = open_seats - 1"); 
                Console.WriteLine("      SectionR: removed a seat, now have: {0} open seat left (in our transaction)", Helper.GetOpenSeats());

                Helper.RunSql("IF EXISTS(SELECT 1 FROM transaction_demo_sections WHERE open_seats < 0) RAISERROR('Operation resulted in negative open seats',16,1)"); // This will fail if no seats left

                //ContextUtil.SetComplete(); // Not needed since we use autocomplete
            }
            catch (Exception ex)
            {
                Console.WriteLine("      SectionR: Caught exception, will throw: {0}", ex.Message);
                //ContextUtil.SetAbort(); // Not needed since we use autocomplete
                throw;
            }
        }
    }
}
