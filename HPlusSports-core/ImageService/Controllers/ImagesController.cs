using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImageService;
using ImageService.Data;
using Microsoft.AspNetCore.Hosting;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageDbContext _context;

        private IHostingEnvironment _env;

        public ImagesController(ImageDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        
        [HttpGet("catagory/{sku}")]
        public async Task<ActionResult<IEnumerable<Image>>> GetImageByCategory(string sku)
        {
            var imageId =
                _context.Categories
                    .Where(x => x.Key == sku)
                    .Select(x => x.ImageId)
                    .FirstOrDefault();

            return Image(imageId);
        }

        [HttpGet("product/{sku}")]
        public ActionResult Product(string sku)
        {
            var imageId =
                _context.Products
                    .Where(x => x.SKU == sku)
                    .SelectMany(x => x.Images.Select(img => img.Id))
                    .FirstOrDefault();

            return Image(imageId);
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImage()
        {
            return await _context.Images.ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(long id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(long id, Image image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Images
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Image>> DeleteImage(long id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        private bool ImageExists(long id)
        {
            return _context.Images.Any(e => e.Id == id);
        }

        public ActionResult Image(long? id)
        {
            var image = _context.Images.FirstOrDefault(x => x.Id == id);

            if (!string.IsNullOrWhiteSpace(image?.Url))
            {
                return RedirectPermanent(image.Url);
            }

            if (image?.Content == null)
            {
                image = GetPlaceholderImage();
            }

            return File(image.Content, image.ContentType);
        }

        internal static volatile byte[] PlaceholderImageContent;

        private Image GetPlaceholderImage()
        {
            if (PlaceholderImageContent == null)
            {
                var path = System.IO.Path.Combine(_env.WebRootPath, "images/placeholder.jpg");
                PlaceholderImageContent = System.IO.File.ReadAllBytes(path);
            }

            return new Image
            {
                Content = PlaceholderImageContent,
                ContentType = "image/jpeg"
            };
        }
    }
}
