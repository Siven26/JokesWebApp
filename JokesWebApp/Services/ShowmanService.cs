using JokesWebApp.Data.DataModels;
using JokesWebApp.Data;
using JokesWebApp.Services.Interfaces;
using JokesWebApp.Services.ViewModels;

namespace JokesWebApp.Services
{
    public class ShowmanService : IShowmanService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShowmanService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<ShowmanViewModel> GetAllShowmans()
        {
            return _context.Showmans
                .Select(showman => new ShowmanViewModel
                {
                    ShowmanID = showman.ShowmanID,
                    ShowmanName = showman.ShowmanName,
                    ShowmanImage = showman.ShowmanImage,
                    ShowmanDescription = showman.ShowmanDescription
                })
                .ToList();
        }

        public async Task CreateShowmanAsync(ShowmanViewModel model)
        {
            Showman showman = new Showman
            {
                ShowmanID = Guid.NewGuid().ToString(),
                ShowmanName = model.ShowmanName,
                ShowmanImage = model.ShowmanImage,
                ShowmanDescription = model.ShowmanDescription
            };

            await _context.Showmans.AddAsync(showman);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShowman(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Error!");
                return;
            }

            var showmanDb = _context.Showmans
                .FirstOrDefault(x => x.ShowmanID == id);

            if (showmanDb == null)
            {
                Console.WriteLine("Showman not found!");
                return;
            }

            _context.Showmans.Remove(showmanDb);

            await _context.SaveChangesAsync();
        }

        public ShowmanViewModel GetShowmanDetailsById(string id)
        {
            var showman = _context.Showmans
                .SingleOrDefault(j => j.ShowmanID == id);

            if (showman == null)
            {
                return null;
            }

            var showmanViewModel = new ShowmanViewModel
            {
                ShowmanID = showman.ShowmanID,
                ShowmanName = showman.ShowmanName,
                ShowmanImage = showman.ShowmanImage,
                ShowmanDescription = showman.ShowmanDescription,
            };

            return showmanViewModel;
        }

        public ShowmanViewModel UpdateShowmanById(string id)
        {
            ShowmanViewModel showman = _context.Showmans
                .Select(showman => new ShowmanViewModel
                {
                    ShowmanID = showman.ShowmanID,
                    ShowmanName = showman.ShowmanName,
                    ShowmanImage = showman.ShowmanImage,
                    ShowmanDescription = showman.ShowmanDescription,
                }).SingleOrDefault(showman => showman.ShowmanID == id);

            return showman;
        }

        public void UpdateShowman(ShowmanViewModel model)
        {
            Showman showman = _context.Showmans.Find(model.ShowmanID);

            bool isShowmanNull = showman == null;
            if (isShowmanNull)
            {
                return;
            }

            showman.ShowmanName = model.ShowmanName;
            showman.ShowmanImage = model.ShowmanImage;
            showman.ShowmanDescription = model.ShowmanDescription;

            _context.Showmans.Update(showman);
            _context.SaveChanges();
        }

        public async Task SetImage(ShowmanViewModel showman, IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            showman.ShowmanImage = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath, @"images\showman", showman.ShowmanImage);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
