using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayitick.Models;
public class Recept: Model
{

    public string Name
    {
        get; set;
    }

    public double Cost
    {
        get; set;
    }

    public virtual ICollection<ResourceCountForRecept> CountForRecepts
    {
        get; set;
    }
}
