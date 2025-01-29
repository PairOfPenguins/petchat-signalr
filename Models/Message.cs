namespace petchat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content {get; set;}
        public DateTime CreatedDate { get; set;} = DateTime.Now;
        public int AssignedUserId {get; set;}
        public User AssignedUser {get; set;}
        public string AssignedUserName {get; set;}



    }
}
