using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace NativeRenderWASM3_0
{

    [Serializable]
    public class EjsNode : ComponentBase, ISerializable
    {
        private string JSNamespace = "node";
        private string JSProperty = string.Empty;

        [CascadingParameter]
        internal NativeRenderWASM3_0.Pages.Canvas BaseParent { get; set; }

        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }
       
        [Parameter]
        public float Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value != _width)
                {
                    _width = value;
                }
            }
        }
        public float _width;

        [Parameter]
        public float Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                }
            }
        }
        private float _height;

        [Parameter]
        public float X
        {
            get { return _x; }
            set
            {
                if (value != _x)
                {
                    _x = value;
                }
            }
        }
        private float _x;

        [Parameter]
        public float Y
        {
            get { return _y; }
            set
            {
                if (value != _y)
                {
                    _y = value;
                    //if (this.BaseParent != null)
                    //    this.BaseParent.UpdateBindableProperties("y", value);
                    //StateHasChanged();
                }
            }
        }
        private float _y;

        SizeF sizeOffset = SizeF.Empty;
        protected override void OnInitialized()
        {
            //if (this.BaseParent != null)
            //    (this.BaseParent.Nodes as List<EjsNode>).Add(this);
        }
        public void MouseDown(MouseEventArgs args , PointF mousePt)
        {
            sizeOffset = new SizeF(this._x + _width / 2 - mousePt.X, this._y + _height / 2 - mousePt.Y);
        }
        public void MouseMove(MouseEventArgs args, PointF mousePt)
        {
            //sizeOffset = new SizeF(sizeOffset.Width - mousePt.X, sizeOffset.Height - mousePt.Y);
            this._x -= (float)(sizeOffset.Width - _width / 2);
            this._y -= (float)(sizeOffset.Height - _height / 2);
            sizeOffset = new SizeF(this._x + _width / 2 - mousePt.X, this._y + _height / 2 - mousePt.Y);
        }
        public void MouseUp(MouseEventArgs args)
        {
            StateHasChanged();
        }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddMarkupContent(0, "\r\n                    ");
            builder.OpenElement(1, "g");
            builder.AddAttribute(2, "id", "node" + "_element");
            builder.AddAttribute(3, "xmlns", "http://www.w3.org/2000/svg");
            builder.AddMarkupContent(4, "\r\n                    ");
            builder.OpenElement(5, "rect");
            builder.AddAttribute(6, "xmlns", "http://www.w3.org/2000/svg");
            builder.AddAttribute(7, "id", "node");
            builder.AddAttribute(8, "x", _x);
            builder.AddAttribute(9, "y", _y);
            builder.AddAttribute(10, "width", _width);
            builder.AddAttribute(11, "height", _height);
            string style = "fill:yellow; stroke-width:5";
            builder.AddAttribute(12, "style", style);
            builder.AddMarkupContent(13, "\r\n                    ");
            builder.CloseElement();
            builder.AddMarkupContent(14, "\r\n                    ");
            builder.CloseElement();
            builder.AddMarkupContent(15, "\r\n                    ");


            base.BuildRenderTree(builder);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("x", _x);
            info.AddValue("y", _y);
            info.AddValue("width", _width);
            info.AddValue("height", _height);
        }
        public EjsNode()
        {

        }
        public EjsNode(SerializationInfo info, StreamingContext context)
        {
            foreach (SerializationEntry entry in info)
            {
                switch (entry.Name)
                {
                    case "x":
                        _x = float.Parse(entry.Value.ToString());
                        break;
                    case "y":
                        _y = float.Parse(entry.Value.ToString());
                        break;
                    case "width":
                        _width = float.Parse(entry.Value.ToString());
                        break;
                    case "height":
                        _height = float.Parse(entry.Value.ToString());
                        break;
                }
            }
        }
    }    
}
