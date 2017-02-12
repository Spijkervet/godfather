using TheGodfatherGM.Data.Enums;

namespace TheGodfatherGM.Data.MessagingModels
{
    public class CreateGroupMessage
    {
        public string UserName { get; set; }
        public string SocialClub { get; set; }
        public string GroupName { get; set; }
        public GroupType GroupType { get; set; }
        public GroupExtraType GroupExtraType { get; set; }
    }
}
