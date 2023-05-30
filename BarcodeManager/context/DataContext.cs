using BarcodeManager.registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.context
{
    public interface DataContext<T>
    {
        Registry<T> Registry { get; }
    }
}
