using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2.DAL
{
    public enum ParameterDirection
    {
        In = 0,
        Out = 1,
        InOut = 2
    }

    public enum DataType
    {
        TinyInt,
        SmallInt,
        Int,
        String,
        Char,
        Date,
        DateTime,
        Bit,
        Byte,
        Decimal,
        Money,
        MaxString,
        IPAddress,
        NString,
		PhoneNo
    }

    public class DatabaseParameter
    {

        public DatabaseParameter(string name, ParameterDirection direction, DataType dataType) : this(name, direction, dataType, null, -1)
        {
        }

        public DatabaseParameter(string name, ParameterDirection direction, DataType dataType, object value) : this(name, direction, dataType, value, -1)
        {
        }
        public DatabaseParameter(string name, ParameterDirection direction, DataType dataType, object value, int size)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Parameter name should not be empty/null");
            }
            if ((DataType == DataType.Char || DataType == DataType.String || DataType == DataType.Byte) && size <= 0)
            {
                throw new ArgumentException("Size should be more then 0","size");
            }
            this.Name = name;
            this.Direction = direction;
            this.DataType = dataType;
            this.Value = value;
            this.Size = size;
        }

        public string Name { get; set; }
        public ParameterDirection Direction { get; set; }
        public DataType DataType { get; set; }
        public int Size { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return string.Format(@"Name: {0}, Direction: {1}, DataType: {2}, Value: {3}, Size: {4}",
                                 Name, Direction.ToString(), DataType.ToString(),
                                 (Value != null ? Value.ToString() : string.Empty), Size.ToString());
        }
    }
}
