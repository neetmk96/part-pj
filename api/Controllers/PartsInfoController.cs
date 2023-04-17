using Microsoft.AspNetCore.Mvc;
using PartsInfo.Dto;
using PartsInfo.Repo.Interface;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PartsInfo.Controllers
{

    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class PartsInfoController : ControllerBase
    {
        private readonly IPartsInfoRepository _parts;
        public PartsInfoController(IPartsInfoRepository partsInfoRepository)
        {
            _parts = partsInfoRepository;
        }
        [HttpGet]
        public IActionResult GetAllPart(int? page ,int? pageSize , string? search)
        {
            try
            {
                var query = new QueryParam
                {
                    page = page ?? 1,
                    pageSize = pageSize ?? 25,
                    search = search
                };
                var getParts = _parts.GetAllParts(query);
                return Ok(getParts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult GetPartById(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("id phải lớn hơn 0");
                var data = _parts.GetPartById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateNewPart(PartsInfoDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _parts.CreateNewPart(model);
                    return Ok(data);
                }
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
 

            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult GetTreePart(int? id)
        {
            try
            {
                var data = _parts.GetTreePart(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"{ex.ToString()}");
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        public IActionResult UpdatePart(PartsInfoDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _parts.EditPart(dto);
                    return Ok(data);
                } return BadRequest("Có trường dữ liệu không hợp lệ");
             
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult DeletePart(int id)
        {
            try
            {

                var data =_parts.DeletePart(id);
                return Ok(data);
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex.ToString()); return BadRequest(ex.Message);
            }
        }


    }
}
