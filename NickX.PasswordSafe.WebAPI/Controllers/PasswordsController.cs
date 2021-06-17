using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NickX.PasswordSafe.WebAPI.Domain.Models;
using NickX.PasswordSafe.WebAPI.Domain.Services.Interfaces;
using NickX.PasswordSafe.WebAPI.Resources;
using NickX.PasswordSafe.WebAPI.Utils.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NickX.PasswordSafe.WebAPI.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    public class PasswordsController : Controller
    {
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public PasswordsController(IPasswordService passwordService, IMapper mapper)
        {
            _passwordService = passwordService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PasswordResource>> GetAllAsync()
        {
            var passwords = await _passwordService.ListAsync();
            var retVal = _mapper.Map<IEnumerable<Password>, IEnumerable<PasswordResource>>(passwords);
            return retVal;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePasswordResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var password = _mapper.Map<SavePasswordResource, Password>(resource);
            var result = await _passwordService.SaveAsync(password);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var passwordResource = _mapper.Map<Password, PasswordResource>(result.Password);
            return Ok(passwordResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePasswordResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var password = _mapper.Map<SavePasswordResource, Password>(resource);
            var result = await _passwordService.UpdateAsync(id, password);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var passwordResource = _mapper.Map<Password, PasswordResource>(result.Password);
            return Ok(passwordResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _passwordService.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            var passwordResource = _mapper.Map<Password, PasswordResource>(result.Password);
            return Ok(passwordResource);
        }
    }
}
