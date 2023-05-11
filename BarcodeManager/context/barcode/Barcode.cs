using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeManager.context.barcode
{
    public class Barcode
    {
        private String _fullBarcode;
        private String? _itemName, _itemDescription;
        private DateTime _dateAdded;
        private BarcodeType? _type;

        private Barcode(String fullBarcode) : this(fullBarcode, new DateTime())
        {

        }

        private Barcode(String fullBarcode, DateTime dateAdded)
        {
            this._fullBarcode = fullBarcode;
            this._dateAdded = dateAdded;
        }

        public String BarcodeNumber { get { return _fullBarcode; }  }

        public String ItemName { get { return _itemName == null ? "" : _itemName; } }

        public String ItemDescription { get { return _itemDescription == null ? "" : _itemDescription; } }

        public DateTime DateAdded { get { return _dateAdded; } }

        public BarcodeType BarcodeType { get { return (BarcodeType) (_type == null ? BarcodeType.Invalid : _type); } }

        public class Builder
        {
            private Barcode _barcode;
            public Builder(String fullBarcode)
            {
                _barcode = new Barcode(fullBarcode);
            }

            public Builder ItemName(String name)
            {
                _barcode._itemName = name;
                return this;
            }

            public Builder ItemDescription(String description)
            {
                _barcode._itemDescription = description;
                return this;
            }

            public Builder DateAdded(DateTime date)
            {
                _barcode._dateAdded = date;
                return this;
            }

            public Builder BarcodeType(BarcodeType type)
            {
                _barcode._type = type;
                return this;
            }

            public Barcode Build()
            {
                if (_barcode._type == null)
                    throw new InvalidBarcodeException();

                return _barcode;
            }
        }

    }
}
