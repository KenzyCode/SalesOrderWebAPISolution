using Microsoft.AspNetCore.Mvc;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.DTO.UserResponseDto;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesOrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _product;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductRepository product, IWebHostEnvironment environment)
        {
            _product = product;
            _environment = environment;
        }

        [HttpGet("GetAll")]
        public async Task<List<ProductDto>> GetAll()
        {
            var productlist = await _product.Getall();
            if (productlist != null && productlist.Count > 0)
            {
                productlist.ForEach(item =>
                {
                    item.productImage = GetImagebyProduct(item.Code);
                });
            }
            else
            {
                return new List<ProductDto>();
            }
            return productlist;

        }
        [HttpGet("GetByCode")]
        public async Task<ProductDto> GetByCode(string Code)
        {
            return await _product.Getbycode(Code);

        }

        [HttpGet("Getbycategory")]
        public async Task<List<ProductDto>> Getbycategory(int Code)
        {
            return await _product.Getbycategory(Code);

        }

        [HttpPost("UploadImage")]
        public async Task<ActionResult> UploadImage()
        {
            bool Results = false;
            try
            {
                var _uploadedfiles = Request.Form.Files;
                foreach (IFormFile source in _uploadedfiles)
                {
                    string Filename = source.FileName;
                    string Filepath = GetFilePath(Filename);

                    if (!System.IO.Directory.Exists(Filepath))
                    {
                        System.IO.Directory.CreateDirectory(Filepath);
                    }

                    string imagepath = Filepath + "\\image.png";

                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await source.CopyToAsync(stream);
                        Results = true;
                    }


                }
            }
            catch (Exception ex)
            {

            }
            return Ok(Results);
        }

        [HttpGet("RemoveImage/{code}")]
        public ResponseType RemoveImage(string code)
        {
            string Filepath = GetFilePath(code);
            string Imagepath = Filepath + "\\image.png";
            try
            {
                if (System.IO.File.Exists(Imagepath))
                {
                    System.IO.File.Delete(Imagepath);
                }
                return new ResponseType { Result = "pass", KyValue = code };
            }
            catch (Exception ext)
            {
                throw ext;
            }
        }

        [HttpPost("SaveProduct")]
        public async Task<ResponseType> SaveProduct([FromBody] ProductDto _product)
        {
            return await _product.SaveProduct(_product);
        }

     

        [NonAction]
        private string GetFilePath(string ProductCode)
        {
            return this._environment.WebRootPath + "\\Uploads\\Product\\" + ProductCode;
        }
        [NonAction]
        private string GetImagebyProduct(string productcode)
        {
            string ImageUrl = string.Empty;
            string HostUrl = "https://localhost:7118/";
            string Filepath = GetFilePath(productcode);
            string Imagepath = Filepath + "\\image.png";
            if (!System.IO.File.Exists(Imagepath))
            {
                ImageUrl = HostUrl + "/uploads/common/noimage.png";
            }
            else
            {
                ImageUrl = HostUrl + "/uploads/Product/" + productcode + "/image.png";
            }
            return ImageUrl;

        }
    }
}


