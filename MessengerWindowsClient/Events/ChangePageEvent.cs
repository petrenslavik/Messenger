using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MessengerWindowsClient.Events
{
    public delegate void ChangePageEvent(object sender, ChangePageEventArgs e);

    public class ChangePageEventArgs:EventArgs
    {
        public Control NewPage { get; set; }

        public Control OldPage { get; set; }

        public ChangePageDirection Direction { get; set; }

        public ChangePageEventArgs(Control newPage, Control oldPage, ChangePageDirection direction)
        {
            NewPage = newPage;
            OldPage = oldPage;
            Direction = direction;
        }
    }

    public enum ChangePageDirection
    {
        Forward,
        Backward
    }
}
