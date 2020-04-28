using Prism.Events;

namespace FriendOrganizer.UI.Event
{
    public class AfterFriendSaveEvents : PubSubEvent<AfterFriendSavedEventArgs>
    {
    }

    public class AfterFriendSavedEventArgs
    {
        public int ID { get; set; }
        public string DisplayMember { get; set; }
    }
}
