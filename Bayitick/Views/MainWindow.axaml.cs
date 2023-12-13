using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.ReactiveUI;
using Bayitick.Models;
using Bayitick.ViewModels;
using DynamicData;
using Microsoft.EntityFrameworkCore;

namespace Bayitick.Views;
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {   
        ViewModel = new MainWindowViewModel();
        DataContext = ViewModel;
        InitializeComponent();
    }

    protected override async void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        await UpdateDb();
    }

    private async void NumericUpDown_ValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        ReceptVm.ResourcesUsed.OnNext(null);

        if (sender is not NumericUpDown numeric)
        {
            return;
        }

        if(numeric.DataContext is ResourceVm resourceVm)
        {
            await using (var db = new AppDbContext())
            {
                db.Update(resourceVm.Resource);
                await db.SaveChangesAsync();
            }
            await UpdateDb();
        }
    }

    private async Task UpdateDb()
    {
        await UpdateDatabaseResources();
        await UpdateDatabaseRecipes();
    }

    private async Task UpdateDatabaseResources()
    {
        await using var db = new AppDbContext();

        if (!db.Resources.Any())
        {
            await db.Resources.AddRangeAsync(new()
            {
                Name = "ÍÅÃÐÛ",
                Count = 100
            },
            new()
            {
                Name = "ÏÈÄÎÐÀÑÛ ",
                Count = 100
            },
            new()
            {
                Name = "ÊÓÊÎËÄÛ ",
                Count = 100
            },
            new()
            {
                Name = "SIMP'Û",
                Count = 100
            },
            new()
            {
                Name = "ÏÅÒÓØÀÐÛ",
                Count = 100
            },
            new()
            {
                Name = "×ÅÐÍÛÅ",
                Count = 100
            },
            new()
            {
                Name = "ÍÀÒÓÐÀËÛ",
                Count = 100
            });

            await db.SaveChangesAsync();
        }

        var vms = await db.Resources.ToListAsync();

        ViewModel.Resources.Clear();
        ViewModel.Resources.AddRange(vms.Select(x => new ResourceVm(x)));
    }

    private async Task UpdateDatabaseRecipes()
    {
        await using var db = new AppDbContext();
        if (!db.Recepts.Any())
        {
            var latte = new Recept
            {
                Name = "Ëàòòå 0.5",
                Cost = 240
            };

            await db.Recepts.AddAsync(latte);

            await db.SaveChangesAsync();

            var recept = await db.Recepts.FirstAsync();
            var resource = await db.Resources.FirstAsync();

            var resourceCount = new ResourceCountForRecept()
            {
                ResourceId = resource.Id,
                Resource = resource,
                Recept = recept,
                ReceptId = recept.Id,
                Count = 50
            };

            await db.CountForRecepts.AddAsync(resourceCount);

            await db.SaveChangesAsync();
        }
        await using var db2 = new AppDbContext();

        var recepts = await db2.Recepts.Include(r => r.CountForRecepts).ThenInclude(c => c.Resource).ToArrayAsync();

        ViewModel!.Recepts.Clear();
        ViewModel.Recepts.AddRange(recepts.Select(x => new ReceptVm(ViewModel!, x)));
        
    }

    private async void HiChanged(object? sender, Avalonia.Controls.NumericUpDownValueChangedEventArgs e)
    {
        ReceptVm.ResourcesUsed.OnNext(null);
        if (sender is not NumericUpDown numeric)
        {
            return;
        }

        if (numeric.DataContext is ResourceCountForRecept resourceVm)
        {
            await using (var db = new AppDbContext())
            {
                db.Update(resourceVm);
                await db.SaveChangesAsync();
            }
            await UpdateDb();
        }
    }
}