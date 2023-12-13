using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Linq;
using Bayitick.Models;
using DynamicData;
using ReactiveUI.Fody.Helpers;

namespace Bayitick.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<ResourceVm> Resources
    {
        get; set;
    }

    public ObservableCollection<ReceptVm> Recepts
    {
        get;
    }
    public ObservableCollection<Recept> Orders
    {
        get;
    }

    [Reactive]
    public double TotalCost
    {
        get; set;
    }

    public MainWindowViewModel()
    {
        Resources = [];
        Recepts = [];
        Orders = [];

        Orders.CollectionChanged += (_, _) =>
        {
            TotalCost = Orders.Select(x => x.Cost).Sum();
        };
        
    }
}
