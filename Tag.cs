using OPCAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixRubber2
{
    public class Tag
    {
        static int indexe = 0;
        public Tag(string name, string path, string alternativePath = "", int? bitNumber = null)
        {
            Name = name;
            Path = path;
            AlternativePath = alternativePath;
            ClientHandle = indexe++;
            this.bitNumber = bitNumber;
        }
        public string Path { get; }
        public string AlternativePath { get; }
        public string Name { get; }
        public int ClientHandle { get; }
        public int ServerHandle { get; set; }
        private int? bitNumber = null;
        private object _value = null;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (bitNumber != null)
                {
                    bool newValue = ((int)value & (1 << bitNumber)) != 0;
                    bool? oldValue = Convert.ToBoolean(_value);
                    if (oldValue != newValue)
                    {
                        _value = newValue;
                        ValueChanged?.Invoke(_value);
                    }
                }
                else
                {
                    if (_value != value)
                    {
                        _value = value;
                        ValueChanged?.Invoke(_value);
                    }
                }
            }
        }
        public OPCQuality Qualitie { get; set; }
        public DateTime TimeStamp { get; set; }

        public event Action<object> ValueChanged;
    }
}
