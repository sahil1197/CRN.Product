using Asp.Versioning;
using CRN.Product.Application.DTOs.Authentication;
using CRN.Product.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRN.Product.Api.Controllers
{
    /// <summary>
    /// Provides authentication endpoints for user login and token refresh.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">Authentication service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user and returns an access token and refresh token.
        /// </summary>
        /// <param name="request">User login credentials.</param>
        /// <returns>JWT access token and refresh token.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(request);

            return Ok(response);
        }

        /// <summary>
        /// Generates a new access token using a valid refresh token.
        /// </summary>
        /// <param name="request">Refresh token request.</param>
        /// <returns>New JWT access token and refresh token.</returns>
        [AllowAnonymous]
        [HttpPost("refresh")]
        [ProducesResponseType(typeof(RefreshTokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequestDto request)
        {
            var response = await _authService.RefreshTokenAsync(request);

            return Ok(response);
        }
    }
}