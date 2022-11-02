using System.Collections.Generic;
using System.Threading.Tasks;
using EFCorePractice.Dtos;
using EFCorePractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCorePractice.Controllers
{
    [ApiController]
    [Route("companies")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyDbContext companyDbContext;

        public CompanyController(CompanyDbContext companyDbContext)
        {
            this.companyDbContext = companyDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> List()
        {
            var companies = await this.companyDbContext.Companies.ToListAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> Get(int id)
        {
            var foundCompany = await this.companyDbContext.Companies.FirstOrDefaultAsync(company => company.Id == id);
            var companyDto = new CompanyDto()
            {
                Name = foundCompany?.Name
            };

            return Ok(foundCompany);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> Add(CompanyDto companyDto)
        {
            var company = new Company()
            {
                Name = companyDto.Name
            };

            await this.companyDbContext.Companies.AddAsync(company);
            await this.companyDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = company.Id }, companyDto);
        }
    }
}