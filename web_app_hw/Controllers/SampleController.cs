using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_app_hw.Models;
using web_app_hw.Services;

namespace web_app_hw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        #region Services
        private readonly SampleObjectPool _pool;
        #endregion

        public SampleController(SampleObjectPool pool)
        {
            _pool = pool;
        }

        [HttpPost("create")]
        public ActionResult<bool> Create(int id)
        {
            return Ok(_pool.Add(id));
        }

        [HttpGet("getAll")]
        public ActionResult<List<SampleObject>> GetAll()
        {
            return Ok(_pool.GetAll());
        }
    }
}
