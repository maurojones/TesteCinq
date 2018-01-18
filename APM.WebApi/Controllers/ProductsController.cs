using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using APM.WebApi.Models;
using APM.WebAPI.Models;
using System.Web.Http.Description;
using System;
using APM.WebApi.Models.Interfaces;

namespace APM.WebApi.Controllers
{
    [EnableCors("http://localhost:7972", "*", "*")]
    public class ProductsController : ApiController
    {
		private readonly IProductRepository _productRepository;

		public ProductsController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		// GET: api/Products
		[EnableQuery()]
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_productRepository.Retrieve().AsQueryable());
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [ResponseType(typeof(Product))]
        // GET: api/Products/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                Product product;
                if (id > 0)
                {
                    var products = _productRepository.Retrieve();
                    product = products.FirstOrDefault(p => p.ProductId == id);

                    if (product == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    product = _productRepository.Create();
                }

                return Ok(product);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // POST: api/Products
        public IHttpActionResult Post([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newProduct = _productRepository.Save(product);

                if (newProduct == null)
                {
                    return Conflict();
                }

                return Created<Product>(Request.RequestUri + newProduct.ProductId.ToString(), newProduct);

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // PUT: api/Products/5
        public IHttpActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Product cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedProduct = _productRepository.Save(id, product);

                if (updatedProduct == null)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
