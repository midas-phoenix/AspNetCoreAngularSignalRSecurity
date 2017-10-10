using ApiServer.Model;
using ApiServer.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    [Authorize("dataEventRecords")]
    [Route("api/[controller]")]
    public class DataEventRecordsController : Controller
    {
        private readonly IDataEventRecordRepository _dataEventRecordRepository;

        public DataEventRecordsController(IDataEventRecordRepository dataEventRecordRepository)
        {
            _dataEventRecordRepository = dataEventRecordRepository;
        }

        [Authorize("dataEventRecordsUser")]
        [HttpGet]
        public IActionResult Get()
        {
            var username = HttpContext.User.Identity.Name;
            return Ok(_dataEventRecordRepository.GetAll(username));
        }

        [Authorize("correctUser")]
        [Authorize("dataEventRecordsAdmin")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_dataEventRecordRepository.Get(id));
        }

        [Authorize("dataEventRecordsAdmin")]
        [HttpPost]
        public void Post([FromBody]DataEventRecordDto value)
        {
            var username = HttpContext.User.Identity.Name;
            _dataEventRecordRepository.Post(value, username);
        }

        [Authorize("dataEventRecordsAdmin")]
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]DataEventRecordDto value)
        {
            _dataEventRecordRepository.Put(id, value);
        }

        [Authorize("dataEventRecordsAdmin")]
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _dataEventRecordRepository.Delete(id);
        }
    }
}