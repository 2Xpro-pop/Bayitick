using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bayitick.Models;
using ReactiveUI.Fody.Helpers;

namespace Bayitick.ViewModels;
public class ResourceCountForReceptVm : ViewModelBase
{
    [Reactive]
    public ResourceCountForRecept ResourceCountForRecept
    {
        get; set;
    }

    public ResourceCountForReceptVm(ResourceCountForRecept resourceCountForRecept)
    {
        ResourceCountForRecept = resourceCountForRecept;


    }
}
