using AutoServis.Core;
using System.Threading.Tasks;

namespace AutoServis.ClientApp.ViewModels
{
    public class MainMenuViewModel
    {
        private readonly AppDbContext _context;

        public MainMenuViewModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveUserThemeAsync(int klijentId, string themeName)
        {
            var klijent = await _context.Klijenti.FindAsync(klijentId);
            if (klijent != null)
            {
                klijent.SelectedTheme = themeName;
                await _context.SaveChangesAsync();
            }
        }
    }
}