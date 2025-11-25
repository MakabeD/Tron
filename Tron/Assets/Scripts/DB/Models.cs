using System;
using System.Collections.Generic;

public class UserDto
{
    public int UserId { get; set; }      // user_id
    public string Username { get; set; } // username
    public int RoleId { get; set; }      // role_id
    public DateTime CreatedAt { get; set; }
}

public class MatchDto
{
    public int MatchId { get; set; }     // match_id
    public int? UserId { get; set; }
    public string Username { get; set; }
    public DateTime Time { get; set; }
    public int? Score { get; set; }
    public string Result { get; set; }
}

public class FeedbackDto
{
    public int FeedbackId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    // feedback is a nested table in Oracle; here we represent as list of strings
    public List<string> FeedbackItems { get; set; } = new List<string>();
}
