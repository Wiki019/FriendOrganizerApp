using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FriendOrganizer.UI.View.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK ?
                MessageDialogResult.Ok : MessageDialogResult.Cancel;
        }

        public MessageDialogResult ShowYesNoDialog(string text, string title)
        {
            var result = MessageBox.Show(text, title, MessageBoxButton.YesNo);
            return result == MessageBoxResult.Yes ? MessageDialogResult.Yes : MessageDialogResult.No;
        }
    }

    public enum MessageDialogResult
    {
        Ok,
        Cancel,
        Yes,
        No
    }
}
