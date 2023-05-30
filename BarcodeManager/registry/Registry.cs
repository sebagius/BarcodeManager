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

        public abstract void Save();
        public virtual void Load()
        {
            this._registry.Clear();
        }

        public void Add(String key, K value) => _registry.Add(key, value);
        public void Remove(String key) => _registry.Remove(key);

        public bool Has(String key) => _registry.ContainsKey(key);

        public StreamWriter InitialiseWriter()
        {
            _writeStream = new StreamWriter(FileName, false);
            return _writeStream;
        }

        public StreamReader? InitialiseReader()
        {
            if (!File.Exists(FileName))
                return null;
            _readStream = new StreamReader(FileName);
            return _readStream;
        }

        public void CloseWriter()
        {
            if(_writeStream != null)
                _writeStream.Close();
            _writeStream = null;
        }

        public void CloseReader()
        {
            if (_readStream != null)
                _readStream.Close();
            _readStream = null;
        }

    }
}
