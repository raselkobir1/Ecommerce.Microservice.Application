﻿using Catelog.API.Interfaces.Manager;
using Catelog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catelog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        IProductManager _productManager;
        public CatalogController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 10)]
        public IActionResult GetProducts()
        {
            try
            {
                //Controller call to->Manager -> Manager call to -> repositoy
                var products = _productManager.GetAll();
                return CustomResult(products);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.NotFound);
            }
        }
    }
}
