using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InstantMessaging
{
    public static class ComboBoxExtensions
    {
        public static void SelectItemByTag(this ComboBox comboBox, object tag)
        {
            var selectedItem = comboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item =>
            {
                var itemTag = item.Tag;
                return itemTag != null && itemTag.Equals(tag.ToString());
            });

            if (selectedItem != null)
            {
                selectedItem.IsSelected = true;
            }
        }
    }
}
