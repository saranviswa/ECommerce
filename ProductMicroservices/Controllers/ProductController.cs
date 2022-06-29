using Microsoft.AspNetCore.Mvc;
using ProductMicroservices.Contants;
using ProductMicroservices.Model;
using ProductMicroservices.Repository;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductMicroservices.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductController));
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _log4net.Info("Logger initiated");
        }
        /// <summary>
        /// This action method returns all the product details from the database
        /// </summary>
        /// <returns></returns>
        // GET api/<ProductController>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetAll()
        {
            _log4net.Info("Loading all available product");
            var product = _productRepository.GetAllProduct();
            if (product == null)
            {
                _log4net.Error(Constant.ProductNotFound);
                return NotFound(Constant.ProductNotFound);
            }

            return new OkObjectResult(product);
        }


        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        // GET api/<ProductController>/5
        [HttpGet("GetById/{id}")]
        public IActionResult Get(int id)
        {
            _log4net.Info("Searching product by productId");
            var product = _productRepository.SearchProductByID(id);
            if (product == null)
            {
                _log4net.Error(Constant.ProductNotFoundById);
                return NotFound(Constant.ProductNotFoundById);
            }

            return new OkObjectResult(product);
        }

        /// <summary>
        /// This Action method takes the name of the product and returns the product details from database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [HttpGet("GetByName/{name}")]
        public IActionResult GetbyName(string name)
        {
            _log4net.Info("Searching product by productName");
            var product = _productRepository.SearchProductByName(name);
            if (product == null)
            {
                _log4net.Error(Constant.ProductNotFoundByName);
                return NotFound(Constant.ProductNotFoundByName);
            }
            return new OkObjectResult(product);
        }

        /// <summary>
        /// This action method adds rating to a particular product
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        // POST api/<ProductController>
        [HttpPost("AddProductRating")]
        public IActionResult PostAddRating(JsonData data)
        {
            var product = _productRepository.SearchProductByID(data.id);
            if (product == null)
            {
                _log4net.Error(Constant.ProductRatingNotAdded);
                return NotFound(Constant.ProductRatingNotAdded);
            }
            else
            {
                _log4net.Info(Constant.ProductRating);
                _productRepository.AddProductRating(data.id, data.rating);
            }
            return Ok("Success");

        }

    }


}

