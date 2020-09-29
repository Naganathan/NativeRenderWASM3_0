using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Drawing;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace NativeRenderWASM3_0
{
    [DataContract]
    public partial class Node: ComponentBase
    {
        private string JSNamespace = "node";
        private string JSProperty = string.Empty;
        private bool _initialized;

        [CascadingParameter]
        internal NativeRenderWASM3_0.Pages.Canvas BaseParent { get; set; }

        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }

        [DataMember]
        [Parameter]
        [JsonPropertyName("annotations")]
        public List<Annotation> Annotations
        {
            get
            {
                return _annotations;
            }
            set
            {
                if (CompareValues(this._annotations, value))
                {
                    _annotations = value;
                    //WiredObservableCollection("annotations", Annotations);
                }
            }
        }
        private List<Annotation> _annotations = new List<Annotation>();

        [DataMember]
        [Parameter]
        [JsonPropertyName("shapes")]
        [DefaultValue(Shapes.Basic)]
        public Shapes Shapes
        {
            get
            {
                return _shapes;
            }
            set
            {
                _shapes = value;
                if (this.BaseParent != null)
                    StateHasChanged();
            }
        }
        private Shapes _shapes = Shapes.Basic;

        [DataMember]
        [Parameter]
        [JsonPropertyName("width")]
        [DefaultValue(50)]
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
                    if (this.BaseParent != null)
                        StateHasChanged();
                }
            }
        }
        private float _width = 50;
        [DataMember]
        [Parameter]
        [JsonPropertyName("id")]
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    if (this.BaseParent != null)
                        StateHasChanged();
                }
            }
        }
        private string _id;
        [DataMember]
        [Parameter]
        [JsonPropertyName("height")]
        [DefaultValue(50)]
        public float Height
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    if (this.BaseParent != null)
                        StateHasChanged();
                }
            }
        }
        private float _height = 50;

        [DataMember]
        [Parameter]
        [JsonPropertyName("x")]
        public float X
        {
            get { return _x; }
            set
            {
                if (value != _x)
                {
                    _x = value;
                    if (this.BaseParent != null)
                        StateHasChanged();
                }
            }
        }
        private float _x;

        [DataMember]
        [Parameter]
        [JsonPropertyName("y")]
        public float Y
        {
            get { return _y; }
            set
            {
                if (value != _y)
                {
                    _y = value;
                    if (this.BaseParent != null)
                        StateHasChanged();
                }
            }
        }
        private float _y;

        public bool CompareValues(object oldValue, object newValue)
        {
            string NewValue = System.Text.Json.JsonSerializer.Serialize(newValue);
            string OldValue = System.Text.Json.JsonSerializer.Serialize(oldValue);
            return NewValue != OldValue;
        }

        SizeF sizeOffset = SizeF.Empty;
        public void MouseDown(MouseEventArgs args, PointF mousePt)
        {
            sizeOffset = new SizeF(this._x + _width / 2 - mousePt.X, this._y + _height / 2 - mousePt.Y);
        }
        public void MouseMove(MouseEventArgs args, PointF mousePt)
        {
            this.X -= (float)(sizeOffset.Width - _width / 2);
            this.Y -= (float)(sizeOffset.Height - _height / 2);
            sizeOffset = new SizeF(this._x + _width / 2 - mousePt.X, this._y + _height / 2 - mousePt.Y);
        }
        public void MouseUp(MouseEventArgs args)
        {

        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
        public override Task SetParametersAsync(ParameterView parameters)
        {
            return base.SetParametersAsync(parameters);
        }

        protected override void OnInitialized()
        {
            if (BaseParent != null)
            {
                (this.BaseParent.Nodes as List<Node>).Add(this);
            }
            StateHasChanged();
        }

        internal void observableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (Annotation item in e.NewItems)
            {
                item.BaseParent = this;
            }
        }
        internal void WiredObservableCollection<T>(string propertyName, T collection)
        {
            if (collection != null && collection.GetType().IsGenericType && propertyName != "AddInfo" && collection.GetType().BaseType.Name != "Object")
            {
                if ((collection is INotifyCollectionChanged))
                {
                    ((INotifyCollectionChanged)collection).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.observableCollectionChanged);
                    ((INotifyCollectionChanged)collection).CollectionChanged += new NotifyCollectionChangedEventHandler(this.observableCollectionChanged);
                }
                SetParentForObservableCollection(collection as ObservableCollection<Annotation>);
            }
        }
        internal void SetParentForObservableCollection(ObservableCollection<Annotation> collection)
        {
            foreach (Annotation item in collection)
            {
                item.BaseParent = this;
            }
        }
    }
}
