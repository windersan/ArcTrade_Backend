# ArcTrade_Backend

.NET Core Web API

**Published to "arctrade.azurewebsites.net"

# Configuring SQL
Please go to the file "ADO.cs" and change the connection string.

You should see:

    public static string conn_str = @"Data Source=.;Initial Catalog=dbdb;Integrated Security=True;";
  
Change "conn_str" to match your system.  I use "localhost" and a database called "dbdb" so if you use "localhost", change the "Initial Catalog" to one of your databases.
