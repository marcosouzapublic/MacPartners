using Consulta.CNPJ.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Controllers
{
    public class CnpjController : Controller
    {
        [HttpGet]
        public JsonResult CompanyByCnpj(string cnpj)
        {
            CNPJService service = new CNPJService();

            cnpj = cnpj.Replace(".","").Replace("/", "").Replace("-", "");

            var cliente = cnpj.Length == 14 ? service.ConsultarCPNJ(cnpj) : null;

            if (cliente != null)
                return Json(new
                {
                    message = "Ok",
                    name = cliente.Nome,
                    tradeName = cliente.Fantasia,
                    phone = cliente.Telefone,
                    zipCode = cliente.Cep.Replace(".", ""),
                    street = cliente.Logradouro,
                    number = cliente.Numero,
                    district = cliente.Bairro,
                    complement = cliente.Complemento,
                    city = cliente.Municipio,
                    state = cliente.Uf
                });
            else
                return Json(new {message = "error"});
        }
    }
}
