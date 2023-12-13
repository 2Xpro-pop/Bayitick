using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Bayitick.Models;

namespace Bayitick.ViewModels;
public class ResourceVm : ViewModelBase
{
    public Resource Resource
    {
        get; set;
    }

    public ICommand EditCommand
    {
        get; set;
    }

    public ResourceVm(Resource resource)
    {
        Resource = resource;


    }
}
