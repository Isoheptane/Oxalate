using System;
using JsonSharp;

namespace Oxalate.Standard
{
    public class User
    {
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }

        public int PermissionLevel { get; set; }

        public DateTime RegisterTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime BanTime { get; set; }
        public JsonObject Data { get; set; }

        public User() { }

        public User(string name, string password)
        {
            Username = name;
            Nickname = name;
            Password = password ;
            RegisterTime = DateTime.Now;
            LastLoginTime = DateTime.MinValue;
            BanTime = DateTime.MinValue;
            Data = new JsonObject();
        }

        /// <summary>
        /// Create a User instance with indicated JSON profile.
        /// </summary>
        /// <param name="jsonFile">JSON Profile</param>
        /// <returns>User instance</returns>
        public static User CreateFromProfile(JsonObject jsonFile)
        {
            User user = new User();
            user.Username           = jsonFile["username"];
            user.Nickname           = jsonFile["nickname"];
            user.Password           = jsonFile["password"];
            user.PermissionLevel    = jsonFile["permissionLevel"];
            user.RegisterTime       = jsonFile["registerTime"];
            user.LastLoginTime      = jsonFile["lastLoginTime"];
            user.BanTime            = jsonFile["banTime"];
            user.Data               = jsonFile["data"];
            return user;
        }

        /// <summary>
        /// Convert the User instance to a JSON profile.
        /// </summary>
        /// <returns>JSON profile</returns>
        public JsonObject ToJsonObject()
        {
            JsonObject json = new JsonObject();
            json["username"]        = Username;
            json["nickname"]        = Nickname;
            json["password"]        = Password;
            json["permissionLevel"] = PermissionLevel;
            json["registerTime"]    = RegisterTime;
            json["lastLoginTime"]   = LastLoginTime;
            json["banTime"]         = BanTime;
            json["data"]            = Data;
            return json;
        }
        
    }
}
