using APBD25_CW11.DTO;
using APBD25_CW11.Exceptions;
using APBD25_CW11.Models;
using APBD25_CW11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD25_CW11.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IDbService _dbService;
        public PrescriptionController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("new/{doctorId}")]
        public async Task<IActionResult> Post(int doctorId,[FromBody] PrescriptionDto prescription, CancellationToken cancellationToken)
        {

            try
            {

                var resp = await _dbService.InsertPrescription(doctorId, prescription, cancellationToken);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem("Unexpected error occured", statusCode: 500);
            }
            return Ok();
        }
        
    }
}
