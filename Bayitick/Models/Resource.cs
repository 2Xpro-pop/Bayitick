using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayitick.Models;
public class Resource : Model
{

    public string Name
    {
        get; set;
    }

    public double Count
    {
        get; set;
    }

    public virtual ICollection<ResourceCountForRecept> Recepts
    {
        get; set;
    }
}
