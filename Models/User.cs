﻿namespace petchat.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Message?> Messages { get; set; }


    }
}
