using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

            string connectionString = $@"Server=DESKTOP-4RC3DKK, 1433;Database={dbName};User=sa;Password=P@ssw0rd@123;";

            _dbContext.Database.SetConnectionString(connectionString);

            _dbContext.Database.EnsureCreated();

            _dbContext.Printers.Add(printerToAdd);

            _dbContext.SaveChanges();

            return Ok(dbName);
        }
    }
}
