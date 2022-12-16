using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_iss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StocksController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public StocksController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        
    }
}
