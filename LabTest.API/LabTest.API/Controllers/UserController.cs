using LabTest.Application.IBusinessService;
using LabTest.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabTest.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Global Variables
        /// <summary>
        /// Private read only variable IUserBusinessService
        /// </summary>
        private readonly IUserBusinessService loginUserBusinessService; 
        #endregion

        #region Contructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="loginUserBusinessService">IUserBusinessService</param>
        public UserController(IUserBusinessService loginUserBusinessService)
        {
            this.loginUserBusinessService = loginUserBusinessService;
        }
        #endregion

        #region APIs
        /// <summary>
        /// Authentication API
        /// </summary>
        /// <param name="loginUser">Model to check the authentication</param>
        /// <returns>string token</returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User loginUser)
        {
            var token = loginUserBusinessService.Login(loginUser);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        } 
        #endregion


    }
}
