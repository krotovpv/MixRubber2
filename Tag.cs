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
        public Tag(string name, string path, string alternativePath = "")
        {
            Name = name;
            Path = path;
            AlternativePath = alternativePath;
            ClientHandle = indexe++;
        }
        public string Path { get; }
        public string AlternativePath { get; }
        public string Name { get; }
        public int ClientHandle { get; }
        public int ServerHandle { get; set; }
        private object _value = null;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    ValueChanged?.Invoke(value);
                }
            }
        }
        public OPCQuality Qualitie { get; set; }
        public DateTime TimeStamp { get; set; }

        public event Action<object> ValueChanged;
    }
}
