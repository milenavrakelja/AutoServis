using AutoServis.Core;
using AutoServis.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AutoServis.WorkerApp.ViewModels;

public class DashboardViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _context;
    private Radnik _radnik;

    public DashboardViewModel(AppDbContext context)
    {
        _context = context;
    }

    
    public void SetCurrentUser(Radnik radnik)
    {
        _radnik = radnik;
        OnPropertyChanged();
    }

    public string Uloga => _radnik?.Uloga ?? string.Empty;
    public bool IsAdmin => _radnik?.Uloga == "Administrator";

    
    public async Task SaveUserThemeAsync(string themeName)
    {
        if (_radnik == null) return;

        var radnikFromDb = await _context.Radnici.FindAsync(_radnik.RadnikId);
        if (radnikFromDb != null)
        {
            radnikFromDb.SelectedTheme = themeName;
            await _context.SaveChangesAsync();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}