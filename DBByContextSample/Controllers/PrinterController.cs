using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace DBByContextSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrinterController : ControllerBase
    {
        private readonly SampleDbContext _dbContext;

        public PrinterController(SampleDbContext sampleDbContext) => _dbContext = sampleDbContext;

        [HttpPost]
        public IActionResult NewPrinter([FromBody] Printer printerToAdd)
        {
            StringValues companyName = Request.Headers["Company"];

            string dbName = "SampleDB";

            if (companyName.Any())
                dbName = companyName.ToString();

            SqlConnectionStringBuilder builder = BuildConnectionString(dbName);

            _dbContext.Database.SetConnectionString(builder.ConnectionString);

            _dbContext.Database.EnsureCreated();

            _dbContext.Printers.Add(printerToAdd);

            _dbContext.SaveChanges();

            return Ok(dbName);
        }

        private static SqlConnectionStringBuilder BuildConnectionString(string dbName)
        {
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = "DESKTOP-4RC3DKK, 1433";
            builder.InitialCatalog = dbName;
            builder.Authentication = SqlAuthenticationMethod.SqlPassword;
            builder.UserID = "sa";
            builder.Password = "P@ssw0rd@123";

            return builder;
        }
    }
}
