using System.Diagnostics.CodeAnalysis;

namespace graduation_project.Models
{
    public class UserConnection : IEquatable<UserConnection>
    {
        public UserConnection()
        {

        }

        public UserConnection(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
        }


        public string Username { get; set; }

        public string ConnectionId { get; set; }


        public bool Equals(UserConnection other)
        {
            if(other is null) return false;
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.ConnectionId == ConnectionId && other.Username == Username) return true;
            return false;

        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UserConnection);
        }
        public override int GetHashCode()
        {
            return this.ConnectionId.GetHashCode();
        }
    }
}
