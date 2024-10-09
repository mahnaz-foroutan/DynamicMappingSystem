using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Config;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IDynamicMapper _dynamicMapper;
        private readonly Dictionary<string, string> _mappingRules;

        public ReservationController(IDynamicMapper dynamicMapper)
        {
            _dynamicMapper = dynamicMapper;
            _mappingRules = LoadMappingRules("mapping.json");
        }

        [HttpPost("to-google")]
        public ActionResult<GoogleReservationModel> MapToGoogle([FromBody] Reservation reservation)
        {
            if (reservation == null) return BadRequest("Reservation cannot be null.");

            var googleModel = _dynamicMapper.Map<Reservation, GoogleReservationModel>(reservation, _mappingRules);
            return Ok(googleModel);

        }

        [HttpPost("from-google")]
        public ActionResult<Reservation> MapFromGoogle([FromBody] GoogleReservationModel googleModel)
        {
            if (googleModel == null) return BadRequest("Google model cannot be null.");

            var reservation = _dynamicMapper.MapBack<Reservation, GoogleReservationModel>(googleModel, _mappingRules);
            return Ok(reservation);
        }

        private Dictionary<string, string> LoadMappingRules(string filePath)
        {
            var json = System.IO.File.ReadAllText(filePath);
            var mappingConfig = JsonConvert.DeserializeObject<MappingConfig>(json);
            return mappingConfig.Mappings;
        }
    }

}
