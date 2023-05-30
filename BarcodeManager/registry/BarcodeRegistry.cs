using BarcodeManager.context.barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.registry
{
    /// <summary>
    /// Simple File Structure
    /// 
    /// Barcode 1 (Number)\n
    /// Barcode 1 (Type)\n
    /// Barcode 1 (Name)\n
    /// Barcode 1 (Description)\n
    /// Barcode 1 (Date Added)\n
    /// Barcode 2 (Number)\n
    /// etc
    /// etc
    /// etc
    /// etc
    /// </summary>
    public class BarcodeRegistry : Registry<Barcode>
    {
        public BarcodeRegistry() : base("barcode-registry.bm")
        {

        }

        public override void Load()
        {
            base.Load();
            StreamReader? reader = this.InitialiseReader();
            if (reader == null)
                return;
            String? line;

            Barcode.Builder? builder = null;
            int step = 0;
            while((line = reader.ReadLine()) != null) 
            { 
                switch(step)
                {
                    case 0:
                        builder = new Barcode.Builder(line);
                        step++;
                        continue;
                    case 1:
                        BarcodeType type;
                        bool success = Enum.TryParse<BarcodeType>(line, true, out type);
                        if (!success) 
                        {
                            throw new Exception("Failed to read data");
                        }
                        builder!.BarcodeType(type);
                        step++;
                        continue;
                    case 2:
                        builder!.ItemName(line);
                        step++;
                        continue;
                    case 3:
                        builder!.ItemDescription(line);
                        step++;
                        continue;
                    case 4:
                        builder!.DateAdded(DateTime.Parse(line));
                        step = 0;
                        Barcode b = builder.Build();
                        Add(b.BarcodeNumber, b);
                        continue;
                }
            }
            CloseReader();
        }

        public override void Save()
        {
            StreamWriter writer = this.InitialiseWriter();
            foreach(Barcode b in this.AllValues)
            {
                writer.WriteLine(b.BarcodeNumber);
                writer.WriteLine(b.BarcodeType.ToString());
                writer.WriteLine(b.ItemName);
                writer.WriteLine(b.ItemDescription);
                writer.WriteLine(b.DateAdded);
            }

            this.CloseWriter();
        }
    }
}
