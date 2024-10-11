using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs;
using FaceWelcome.Service.DTOs.Request.Guest;
using FaceWelcome.Service.DTOs.Request.WelcomeTemplate;
using FaceWelcome.Service.DTOs.Response.WelcomeTemplate;
using FaceWelcome.Service.Services.Implementations;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FaceWelcome.API.Controllers
{
    [ApiController]
    [Route("api/v1/WelcomeTemplates")]
    public class WelcomeTemplatesController : Controller
    {
        private readonly IWelcomeTemplateService _welcomeTemplateService;

        public WelcomeTemplatesController(IWelcomeTemplateService welcomeTemplateService)
        {
            _welcomeTemplateService = welcomeTemplateService;
        }

        #region Get Template by Guest Id
        [ProducesResponseType(typeof(GetTemplateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.WelcomeTemplate.GuestEndpoint)]
        public async Task<IActionResult> GetTemplateByGuestIdAsync([FromRoute] GuestRequest guestRequest)
        {
            try
            {
                var data = await _welcomeTemplateService.GetTemplateByGuestIdAsync(guestRequest.Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get Template by Id
        [ProducesResponseType(typeof(GetTemplateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.WelcomeTemplate.WelcomeTemplateEndpoint)]
        public async Task<IActionResult> GetTemplateByIdAsync([FromRoute] TemplateIdRequest templateIdRequest)
        {
            try
            {
                var data = await _welcomeTemplateService.GetTemplateByIdAsync(templateIdRequest.Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Get All Templates
        [ProducesResponseType(typeof(GetTemplateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.WelcomeTemplate.WelcomeTemplatesEndpoint)]
        public async Task<IActionResult> GetAllTemplatesAsync([FromQuery] GetTemplatesRequest getTemplatesRequest)
        {
            try
            {
                var data = await _welcomeTemplateService.GetAllTemplatesAsync(getTemplatesRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
