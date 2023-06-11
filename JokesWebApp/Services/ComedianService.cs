using JokesWebApp.Data.DataModels;
using JokesWebApp.Data;
using JokesWebApp.Services.ViewModels;
using JokesWebApp.Services.Interfaces;

namespace JokesWebApp.Services
{
    public class ComedianService : IComedianService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ComedianService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<ComedianViewModel> GetAllComedians()
        {
            return _context.Comedians
                .Select(comedian => new ComedianViewModel
                {
                    ComedianID = comedian.ComedianID,
                    ComedianName = comedian.ComedianName,
                    ComedianImage = comedian.ComedianImage,
                    ComedianDescription = comedian.ComedianDescription
                })
                .ToList();
        }

        public async Task CreateComedianAsync(ComedianViewModel model)
        {
            Comedian comedian = new Comedian
            {
                ComedianID = Guid.NewGuid().ToString(),
                ComedianName = model.ComedianName,
                ComedianImage = model.ComedianImage,
                ComedianDescription = model.ComedianDescription
            };

            await _context.Comedians.AddAsync(comedian);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComedian(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Error!");
                return;
            }

            var comedianDb = _context.Comedians
                .FirstOrDefault(x => x.ComedianID == id);

            if (comedianDb == null)
            {
                Console.WriteLine("Comedian not found!");
                return;
            }

            _context.Comedians.Remove(comedianDb);

            await _context.SaveChangesAsync();
        }

        public ComedianViewModel GetComedianDetailsById(string id)
        {
            var comedian = _context.Comedians
                .SingleOrDefault(j => j.ComedianID == id);

            if (comedian == null)
            {
                return null;
            }

            var comedianViewModel = new ComedianViewModel
            {
                ComedianID = comedian.ComedianID,
                ComedianName = comedian.ComedianName,
                ComedianImage = comedian.ComedianImage,
                ComedianDescription = comedian.ComedianDescription,
            };

            return comedianViewModel;
        }

        public ComedianViewModel UpdateComedianById(string id)
        {
            ComedianViewModel comedian = _context.Comedians
                .Select(comedian => new ComedianViewModel
                {
                    ComedianID = comedian.ComedianID,
                    ComedianName = comedian.ComedianName,
                    ComedianImage = comedian.ComedianImage,
                    ComedianDescription = comedian.ComedianDescription,
                }).SingleOrDefault(joke => joke.ComedianID == id);

            return comedian;
        }

        public void UpdateComedian(ComedianViewModel model)
        {
            Comedian comedian = _context.Comedians.Find(model.ComedianID);

            bool isComedianNull = comedian == null;
            if (isComedianNull)
            {
                return;
            }

            comedian.ComedianName = model.ComedianName;
            comedian.ComedianImage = model.ComedianImage;
            comedian.ComedianDescription = model.ComedianDescription;

            _context.Comedians.Update(comedian);
            _context.SaveChanges();
        }

        public async Task SetImage(ComedianViewModel comedian, IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            comedian.ComedianImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath, @"images\comedian", comedian.ComedianImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
