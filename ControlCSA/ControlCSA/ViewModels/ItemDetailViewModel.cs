using System;

using ControlCSA.Models;
using ZXing.Net.Mobile.Forms;

namespace ControlCSA.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private ZXingScannerPage scannerPage;

        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }

        public ItemDetailViewModel(ZXingScannerPage scannerPage)
        {
            this.scannerPage = scannerPage;
        }
    }
}
