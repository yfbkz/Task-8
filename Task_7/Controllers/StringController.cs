
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.OpenApi.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Task_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringController : ControllerBase
    {
        IConfiguration _configuration;
        public StringController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<Object> S4(string str, char sorting_method_Q_T)
        {
            string[] black = _configuration.GetSection("Settings:BlackList").Get<string[]>();
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string incor = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (!letters.Contains(Convert.ToString(str[i])))
                {
                    incor += $"{str[i]} ";
                }
            }
            if (incor != "")
            {
                 return StatusCode(StatusCodes.Status400BadRequest, $"Bad request/Incorrect input: {incor}"); 

            }
            if (black.Contains(str))
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Bad request/String {str} in blacklist");

            }
            string API = _configuration.GetSection("RandomAPI").Get<string>();
            return T7.NewString(str, sorting_method_Q_T, API); 
            
            
        }

        
    }
}
