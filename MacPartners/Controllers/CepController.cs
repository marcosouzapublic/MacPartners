using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViaCep;

namespace MacPartners.Controllers
{
    public class CepController : Controller
    {
        [HttpGet]
        public JsonResult AddressByCep(string cep)
        {
            var viaCep = new ViaCepClient();

            cep = cep.Replace("-", "");

            var result = viaCep.Search(cep);

            if (result != null)
            {
                var resultado = new
                {
                    state = result.StateInitials,
                    city = result.City,
                    district = result.Neighborhood,
                    street = result.Street,
                    complement = result.Complement,
                };

                return Json(resultado);

            }
            else
                return Json(new { message = "error" });
        }
    }
}
