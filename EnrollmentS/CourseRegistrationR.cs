using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.EnterpriseServices;
using SIS.Shared;

namespace SIS.Enrollment
{

    [Transaction(TransactionOption.Required)]
    public class CourseRegistrationR
    {
        public void RegisterStudent()
        {

            try
            {
                Helper.RunSql("INSERT INTO transaction_demo_enrollments(section_id,person_id) VALUES(1,1)");
                Console.WriteLine("   CourseRegistrationR: Inserted enrollment, we now have {0} during our transaction", Helper.GetTotalEnrollments());
                int trancount = (int)Helper.RunSql("SELECT @@TRANCOUNT");
                Console.WriteLine("   CourseRegistrationR: Trancount (should be > 0): {0}", trancount);
                var section = new Course.SectionR();
                section.RemoveSeat();
                ContextUtil.SetComplete();
            }
            catch(Exception ex)
            {
                Console.WriteLine("CourseRegistrationR: Caught exception: {0}, Aborting!", ex.Message);
                ContextUtil.SetAbort();
                //throw; // Dont throw since this is demo, just print it

            }

        }
        
    }
}
