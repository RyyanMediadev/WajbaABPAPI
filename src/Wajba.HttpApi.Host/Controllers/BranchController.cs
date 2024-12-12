﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Wajba.BranchContract;

namespace Wajba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : AbpController
    {
        private readonly IBranchAppService _branchAppService;

        public BranchController(IBranchAppService branchAppService)
        {
            _branchAppService = branchAppService;
        }

        [HttpGet("{id}")]
        public async Task<BranchDto> GetAsync(int id)
        {
            return await _branchAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<BranchDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            return await _branchAppService.GetListAsync(input);
        }

        [HttpPost]
        public async Task<BranchDto> CreateAsync(CreateUpdateBranchDto input)
        {
            return await _branchAppService.CreateAsync(input);
        }

        [HttpPut("{id}")]
        public async Task<BranchDto> UpdateAsync(int id, CreateUpdateBranchDto input)
        {
            return await _branchAppService.UpdateAsync(id, input);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await _branchAppService.DeleteAsync(id);
        }
    }
}