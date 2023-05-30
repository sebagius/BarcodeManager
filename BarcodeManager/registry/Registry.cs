using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.registry
{
    public abstract class Registry<K>
    {
        private Dictionary<String, K> _registry;
        private String _fileName;
        private StreamReader? _readStream;
        private StreamWriter? _writeStream;

        public Dictionary<String, K> InternalRegistry { get { return _registry; } }

        public List<K> AllValues { get { return _registry.Values.ToList(); } }

        public String FileName { get { return _fileName; } }

        public Registry(String fileName)
        {
            this._registry = new Dictionary<String, K>();
            this._fileName = fileName;
        }

        public abstract void save();
        public abstract void load();

        public void Add(String key, K value) => _registry.Add(key, value);
        public void Remove(String key) => _registry.Remove(key);

        public bool Has(String key) => _registry.ContainsKey(key);

    }
}
