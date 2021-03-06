using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using Brushes = System.Windows.Media.Brushes;

namespace XSemmel.Schema.Parser {

    public class XsdImportInclude : XsdNode
    {

        public XsdImportInclude(XmlNode node, string name) : base(node)
        {
            Name = name;
            {
                string attr = VisualizerHelper.GetAttr(node, "namespace");
                if (attr != null)
                {
                    Namespace = attr;
                }
            }
            {
                string attr = VisualizerHelper.GetAttr(node, "schemaLocation");
                if (attr != null)
                {
                    SchemaLocation = attr;
                }
            }
        }

        public string Namespace { get; set; }
        public string SchemaLocation { get; set; }

        public override string ToString()
        {
            return Name;
        }


        public override UIElement GetPaintComponent(XsdVisualizer.PaintOptions po, int fontSize)
        {
            if (fontSize <= 0) return null;

            StackPanel pnl = new StackPanel();

            pnl.Children.Add(GetPaintTitle(po, fontSize));

            string s = SchemaLocation;
            if (s.Contains("/"))
            {
                s = "..." + s.Substring(s.LastIndexOf('/'));
            }  
            pnl.Children.Add(new TextBlock
            {
                Text = s,
                Foreground = Brushes.Gray,
                FontSize = fontSize-1
            }); 

            if (po.ExpandIncludes)
            {
                pnl.Children.Add(GetPaintChildren(po, fontSize - 1));
            }

            addMouseEvents(po, pnl, this);

            return new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(1), Child = pnl, Margin = new Thickness(5) };
        }

        protected override UIElement GetPaintTitle(XsdVisualizer.PaintOptions po, int fontSize)
        {
            return new TextBlock
            {
                Text = ToString(),
                Background = new LinearGradientBrush(Colors.SeaGreen, Colors.Transparent, 90),
                FontSize = fontSize,
                FontWeight = FontWeights.DemiBold
            };
        }

    }
}