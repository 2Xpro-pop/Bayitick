using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media;
using Bayitick.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Bayitick.ViewModels;
public class ReceptVm : ViewModelBase
{
    public static readonly Subject<Recept?> ResourcesUsed = new();
    [Reactive]
    public Recept Recept
    {
        get; set;
    }

    [Reactive]
    public Brush Background
    {
        get;
        set;
    }

    [Reactive]
    public MainWindowViewModel MainWindowViewModel
    {
        get; set;
    }

    public ICommand MakeOrderCommand
    {
        get;
    }

    private readonly SolidColorBrush _solid = new();

    public ReceptVm(MainWindowViewModel viewModel, Recept recept)
    {
        Recept = recept;
        MainWindowViewModel = viewModel;
        Background = _solid;

        MakeOrderCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            MainWindowViewModel.Orders.Add(Recept);
            await using var db = new AppDbContext();

            UseResourcesForRecipe(Recept, db);

            ResourcesUsed.OnNext(recept);
        }, this.WhenAnyValue(x => x._solid.Color).Select(x => x != Colors.Red));
        _solid.Color = AreRequiredResourcesAvailable(Recept) ? Colors.Gray : Colors.Red;

        ResourcesUsed.Subscribe(x =>
        {
            _solid.Color = AreRequiredResourcesAvailable(Recept) ? Colors.Gray : Colors.Red;
        });
    }

    public static bool AreRequiredResourcesAvailable(Recept? recipe)
    {
        if (recipe is null)
        {
            return false;
        }

        if (recipe.CountForRecepts is null)
        {
            return true;
        }

        foreach (var resourceCount in recipe.CountForRecepts)
        {
            if (resourceCount.Resource.Count < resourceCount.Count)
            {
                // Ресурсов недостаточно
                return false;
            }
        }
        // Все ресурсы доступны в достаточном количестве
        return true;
    }

    public static void UseResourcesForRecipe(Recept recipe, AppDbContext dbContext)
    {
        foreach (var resourceCount in recipe.CountForRecepts)
        {

            // Уменьшаем количество ресурса
            resourceCount.Resource.Count -= resourceCount.Count;
            dbContext.Update(resourceCount.Resource);
        }

        dbContext.SaveChanges();

    }
}
