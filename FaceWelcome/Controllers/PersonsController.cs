using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Response.Person;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace FaceWelcome.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PersonsController : Controller
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        #region Get Person By Id
        [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Person.PersonEndpoint)]
        public async Task<IActionResult> GetPersonByIdAsync([FromRoute]PersonIdRepuest personIdRepuest)
        {
            try
            {
                var data = await _personService.GetPersonByIdAsync(personIdRepuest.Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion
    }
}
