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
            Helper.SetupDb();

            Console.WriteLine("Starting with open seats (expect 3): {0}", Helper.GetOpenSeats());
            Console.WriteLine("Registering Once");
            ClickEnroll();

            Console.WriteLine("After reg, (expect 2): {0}", Helper.GetOpenSeats());


            Console.WriteLine("Registering 2 more students, section should be full now with no open seats");
            ClickEnroll();
            ClickEnroll();
            Console.WriteLine("Open seats: {0}", Helper.GetOpenSeats());

            Console.WriteLine("Everything should be good so far, we have {0} enrollments and {1} open seats", Helper.GetTotalEnrollments(), Helper.GetOpenSeats());
            Console.WriteLine("Now we will try to enroll in a full class and will expect the transaction to rollback since SQL will throw error");
            ClickEnroll();

            Console.WriteLine("After failed registration, we have {0} enrollments and {1} open seats. (Transactional Integrity is preserved)", Helper.GetTotalEnrollments(), Helper.GetOpenSeats());

        }

        private static void ClickEnroll() // This simulates input from a web form for enrollment
        {
            // Setup context
            ServiceConfig context = new ServiceConfig();
            context.Transaction = TransactionOption.Required;
            ServiceDomain.Enter(context); // enter the transactional context (not using MTS)

            // Business Logic
            var courseReg = new CourseRegistrationR();
            courseReg.RegisterStudent();

            // Check success
            TransactionStatus status = ServiceDomain.Leave();
            Console.WriteLine("Transaction result {0}", status);
            // TODO: Handle result accordingly

        }
    }
}
