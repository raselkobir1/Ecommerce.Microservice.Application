using Catelog.API.Interfaces.Manager;
using Catelog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult CreateProduct([FromBody]Product product)
        {
            try
            {
                product.Id = ObjectId.GenerateNewId().ToString();  
                bool isSaved = _productManager.Add(product);
                if (isSaved)
                {
                    return CustomResult("Product has been save successfully.", product, HttpStatusCode.Created);
                }
                else
                {
                    return CustomResult("Product saved faild", product, HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.Id))
                {
                    return CustomResult("Data not found",HttpStatusCode.NotFound);
                }
                bool isUpdated = _productManager.Update(product.Id,product);
                if (isUpdated)
                {
                    return CustomResult("Product has been Updated successfully.", product, HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Product Updated faild", product, HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                bool isDeleted= _productManager.Delete(id);
                if (isDeleted)
                {
                    return CustomResult("Product has been Deleted successfully.", HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Product Deleted faild", HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public IActionResult GetById(string id)  
        {
            try
            {
                var product = _productManager.GetById(id);
                return CustomResult("Data load successfully",product);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 10)]
        public IActionResult GetByCatatory(string catatory) 
        {
            try
            {
                var products = _productManager.GetByCatagory(catatory);
                return CustomResult("Data load successfully",products);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
