using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Pdf;

namespace Apka2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFsController : ControllerBase
    {
        
        // GET: api/PDFs/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "xd";
        }

        
    }
}
