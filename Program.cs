// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using SQL2EXCEL;
using System.Xml.Linq;

DatatableToCSV toCSV = new DatatableToCSV();

// load the configuration file.
var configBuilder = new ConfigurationBuilder().
   AddJsonFile("appsettings.json").Build();

IConfigurationRoot _config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

// get the section to read  
var appSettings = configBuilder.GetSection("AppSettings");
var fileLocation = appSettings["FileLocation"] ?? "c:\\";



var sql = configBuilder.GetSection("AppSettings:query").Get<List<string>>();

//sql.Join(",");
var sqlString = String.Join("\n", sql);

var connectionStrings = configBuilder.GetSection("ConnectionStrings");
var defaultConnection = connectionStrings["DefaultConnection"] ?? null;

SqlConnection connection = new SqlConnection();

connection.ConnectionString = defaultConnection;
SqlCommand command = new SqlCommand();
command.CommandText = sqlString;

using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command.CommandText, connection))
{
    DataTable t = new DataTable();
    dataAdapter.Fill(t);
    toCSV.write(t, fileLocation);
}
