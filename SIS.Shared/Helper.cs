using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Shared
{
    public class Helper
    {
        // This is hardcoded for demo, normally it needs to come from config
        public static string GetConnectionString()
        {
            return "Data Source=ISISDEVSQL;Initial Catalog=tempdb;Integrated Security=SSPI";
        }

        public static object RunSql(string sqlCommand)
        {

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlCommand, conn);
                return cmd.ExecuteScalar();
            }
        }

        public static int GetOpenSeats()
        {
            return (int)RunSql("SELECT open_seats from transaction_demo_sections where id=1");
        }

        public static int GetTotalEnrollments()
        {
            return (int)RunSql("SELECT count(*) from transaction_demo_enrollments");
        }

        public static void SetupDb()
        {
            string setupSql =
                @"if object_id('transaction_demo_enrollments') is not null drop table transaction_demo_enrollments
create table transaction_demo_enrollments(id int identity(1,1) primary key, section_id int, person_id int)

if object_id('transaction_demo_sections') is not null drop table transaction_demo_sections
create table transaction_demo_sections(id int primary key, open_seats int)

insert transaction_demo_sections(id,open_seats) values(1,3)";
            Console.WriteLine("Setting up clean tables");
            RunSql(setupSql);

        }
    }
}
