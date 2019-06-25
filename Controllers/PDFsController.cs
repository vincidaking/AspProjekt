
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;


namespace Apka2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFsController : ControllerBase
    {

        //private readonly IPDFService _pdfService;

        //public PDFsController(IPDFService pdfService)
        //{
        //    _pdfService = pdfService;
        //}

        //[HttpGet("Create")]
        //public async Task<IActionResult> CreatePdf()
        //{
        //    var file = await _pdfService.Create();
        //    return File(file, "application/pdf"); ;
        //}



    }
}
