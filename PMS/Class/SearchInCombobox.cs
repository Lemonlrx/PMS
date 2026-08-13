using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMS.Class
{
    internal class SearchInCombobox
    {
        /// <summary>
        /// <para> Event KeyCode (Enter) and Leave </para>
        /// </summary>
        public static void TextLikevalues(ComboBox cb)
        {
            if (cb.Text != "")
            {
                String IndexName = "";
                if (cb.SelectedIndex != -1)
                    IndexName = cb.Items[cb.SelectedIndex].ToString();
                String Text = cb.Text;
                if (Text.Contains(IndexName))
                {
                    for (int x = 0; x < cb.Items.Count; x++)
                    {
                        if (cb.Items[x].ToString().Contains(Text))
                        {
                            cb.SelectedIndex = x;
                            break;
                        }
                        else if (x == cb.Items.Count - 1)
                            cb.SelectedIndex = -1;
                    }
                }
            }
            else
                cb.SelectedIndex = -1;
            cb.SelectionStart = cb.Text.Length;
        }
        /// <summary>
        /// <para> Event TextChange </para>
        /// </summary>
        public static void CheckTextChangeCB(ComboBox cb, List<String[,]> ListCBItem)
        {
            if (cb.SelectedIndex == -1)
            {
                cb.Items.Clear();
                for (int x = 0; x < ListCBItem.Count; x++)
                    if (ListCBItem[x][0, 0].Contains(cb.Text))
                        cb.Items.Add(new PMS.Class.ComboboxInfo(ListCBItem[x][0, 0], ListCBItem[x][0, 1]));
                if (!cb.DroppedDown)
                {
                    if (cb.Enabled)
                    {
                        cb.DroppedDown = true;
                        cb.SelectedText = null;
                    }
                    else
                        cb.DroppedDown = false;
                }
                cb.SelectionStart = cb.Text.Length;
            }
        }
        /// <summary>
        /// <para> Event KeyPress </para>
        /// </summary>
        public static void CheckKeypressCombobox(ComboBox cb, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar))
            {

                bool CheckBreak = false;
                int Nextchar = cb.Text.Length;
                if (cb.SelectedIndex != -1)
                {
                    Nextchar = 0;
                }
                cb.SelectedIndex = -1;

                for (int a = 0; a < cb.Items.Count; a++)
                {
                    String ItemChar = cb.Items[a].ToString();

                    if (Nextchar + 1 < ItemChar.Length)
                        ItemChar = ItemChar.Remove(Nextchar + 1);

                    ItemChar = ItemChar.Remove(0, Nextchar);
                    if (Char.TryParse(ItemChar, out char value) && e.KeyChar == value)
                    {
                        CheckBreak = true;
                        break;
                    }
                }
                if (!CheckBreak)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
