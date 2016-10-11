# es-transaction-demo
Demonstration of using System.EnterpriseServices to take care of transactions against SQL Server without use of COM+/MTS

To override connection string: SIS.TransactionDemo.exe "Data Source=MYDSQLSERVERNAME;initial catalog=tempdb;integrated security=sspi"