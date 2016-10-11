using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS.Enrollment;
using SIS.Shared;

//using System.EnterpriseServices;

namespace SIS.TransactionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Helper.SetConnectionString(args[0]);
            }
            Helper.SetupDb();

            Console.WriteLine("Expect to have 3 open seats in our section and have: {0}", Helper.GetOpenSeats());
            Console.WriteLine("Registering One student");
            ClickEnroll();

            Console.WriteLine("After registration, we expect 2 remaining seats and have: {0}", Helper.GetOpenSeats());


            Console.WriteLine("Registering 2 more students, section should be full after these");
            ClickEnroll();
            ClickEnroll();
            Console.WriteLine();
            Console.WriteLine("Everything should be good so far, we have {0} enrollments and {1} open seats", Helper.GetTotalEnrollments(), Helper.GetOpenSeats());
            Console.WriteLine("Now we will try to enroll in a full class and will expect the transaction to rollback since SQL will throw error");
            ClickEnroll();

            Console.WriteLine("After failed registration, we have {0} enrollments and {1} open seats. (Transactional Integrity is preserved)", Helper.GetTotalEnrollments(), Helper.GetOpenSeats());

        }

        private static void ClickEnroll() // This simulates input from a web form for enrollment
        {
            try
            {
                // Setup context
                ServiceConfig context = new ServiceConfig {Transaction = TransactionOption.Required};
                ServiceDomain.Enter(context); // enter the transactional context (not using MTS)

                // Business Logic
                var courseReg = new CourseRegistrationR();
                courseReg.RegisterStudent();

                // Check success
                TransactionStatus status = ServiceDomain.Leave();
                Console.WriteLine("Transaction result {0}. Enrollments: {1}, Open Seats: {2}", status, Helper.GetTotalEnrollments(), Helper.GetOpenSeats());
                // TODO: Handle result accordingly
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught Exception: {0}",ex.Message);
                ContextUtil.SetAbort();
                TransactionStatus status = ServiceDomain.Leave();
                Console.WriteLine("Transaction result {0}", status);
            }
        }
    }
}
