namespace FriendOrganizer.UI.View.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string text, string title);

        MessageDialogResult ShowYesNoDialog(string text, string title);
    }
}