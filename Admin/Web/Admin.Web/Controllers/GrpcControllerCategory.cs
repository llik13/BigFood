using Admin.Web.Protos;
using Microsoft.AspNetCore.Mvc;
using Google.Protobuf.WellKnownTypes;
using Aplication.Category;


namespace Admin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrpcControllerCategory : ControllerBase
    {
        private readonly Categories.CategoriesClient _grpcClient;

        public GrpcControllerCategory(Categories.CategoriesClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryList>> GetCategories()
        {
            var emptyRequest = new Empty();
            var categories = _grpcClient.GetCategory(emptyRequest);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryDto request)
        {
            var category = new CategoryDTOs
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };
            _grpcClient.AddCategory(category);
            return Ok(category);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(CategoryDto request)
        {
            var categoryDTOs = new CategoryDTOs
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl
            };
            _grpcClient.UpdateCategory(categoryDTOs);
            return Ok(categoryDTOs);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(CategoryDto request)
        {
            var CategoryDto = new CategoryDTOs
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl

            };
            _grpcClient.DeleteCategory(CategoryDto);
            return Ok();
        }


    }
}
