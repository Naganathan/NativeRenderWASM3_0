using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NativeRenderWASM3_0
{
    public class CustomNode: Node
    {
        public CustomNode() : base()
        {

        }

        public float CustomWidth
        {
            get
            {
                return _cwidth;
            }
            set
            {
                _cwidth = value;
            }
        }
        private float _cwidth;

    }
}
