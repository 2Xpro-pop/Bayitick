using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayitick.Models;
public class ResourceCountForRecept: Model
{
    public Guid ResourceId
    {
        get; set;
    }

    public Resource Resource
    {
        get; set;
    } = null!;

    public Guid ReceptId
    {
        get; set;
    }

    public Recept Recept
    {
        get; set;
    } = null!;

    public double Count
    {
        get; set;
    }
}
