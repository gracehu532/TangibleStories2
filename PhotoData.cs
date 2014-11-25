using System;

namespace Drag_and_Drop
{
    public class PhotoData
    {

        public string Source { get; private set; }
        public string Caption { get; private set; }

        public PhotoData(string source, string caption)
        {
            this.Source = source;
            this.Caption = caption;
        }
    }
}