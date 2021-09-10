using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Services;
using System;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        IBonusPoolService _bonusPoolService;

        public BonusPoolController(IBonusPoolService bonusPoolService)
        {
            _bonusPoolService = bonusPoolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            return Ok(await _bonusPoolService.GetEmployeesAsync());
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        { 
            try
            {
                return Ok(await _bonusPoolService.CalculateAsync(
                    request.TotalBonusPoolAmount,
                    request.SelectedEmployeeId));
            }
            catch (Exception ex)
            {
                return BadRequest($"The request failed with the following exception: {ex.Message}");
            }
        }
    }
}
