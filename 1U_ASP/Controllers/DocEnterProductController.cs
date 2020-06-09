using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1U_ASP.DTO;
using _1U_ASP.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1U_ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocEnterProductController : ControllerBase
    {
        private readonly IDocEnterProductService _docEnterProductService;

        public DocEnterProductController(IDocEnterProductService docEnterProductService)
        {
            _docEnterProductService = docEnterProductService;
        }

        [HttpGet]
        public async Task<List<DocEnterProductDetailDto>> GetAllEnterProductDetails()
        {
            return await _docEnterProductService.GetAllEnterProductDetails();
        }


    }
}