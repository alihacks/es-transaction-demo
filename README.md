# es-transaction-demo
Demonstration of using System.EnterpriseServices to take care of transactions against SQL Server without use of COM+/MTS

## Running
To override connection string: SIS.TransactionDemo.exe "Data Source=MYSQLSERVERNAME;initial catalog=tempdb;integrated security=sspi"

## Sample Output
```
Setting up clean tables
Expect to have 3 open seats in our section and have: 3
Registering One student
   CourseRegistrationR: Inserted enrollment, we now have 1 total (in our transaction)
      SectionR: removed a seat, now have: 2 open seat left (in our transaction)
Transaction result Commited. Enrollments: 1, Open Seats: 2
After registration, we expect 2 remaining seats and have: 2
Registering 2 more students, section should be full after these
   CourseRegistrationR: Inserted enrollment, we now have 2 total (in our transaction)
      SectionR: removed a seat, now have: 1 open seat left (in our transaction)
Transaction result Commited. Enrollments: 2, Open Seats: 1
   CourseRegistrationR: Inserted enrollment, we now have 3 total (in our transaction)
      SectionR: removed a seat, now have: 0 open seat left (in our transaction)
Transaction result Commited. Enrollments: 3, Open Seats: 0

Everything should be good so far, we have 3 enrollments and 0 open seats
Now we will try to enroll in a full class and will expect the transaction to rollback since SQL will throw error
   CourseRegistrationR: Inserted enrollment, we now have 4 total (in our transaction)
      SectionR: removed a seat, now have: -1 open seat left (in our transaction)
      SectionR: Caught exception, will throw: Operation resulted in negative open seats
   CourseRegistrationR: Caught exception, will throw: Operation resulted in negative open seats, Aborting!
Caught Exception: Operation resulted in negative open seats
Transaction result Aborted
After failed registration, we have 3 enrollments and 0 open seats. (Transactional Integrity is preserved)
```