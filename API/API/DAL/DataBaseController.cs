using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DAL
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataBaseController : ControllerBase
    {
        private readonly DataContext dataContext;

        public DataBaseController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        public ActionResult RecreateDatabase()
        {
            dataContext.RecreateDatabase();

            return NoContent();
        }
    }
}
