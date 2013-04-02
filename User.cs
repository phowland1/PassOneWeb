//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PassOne
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserEntity
    {
        public UserEntity()
        {
            this.Credentials = new HashSet<CredentialsEntity>();
        }

        public UserEntity(int id, string fn, string ln, string un, string pw)
        {
            Id = id;
            FirstName = fn;
            LastName = ln;
            Username = un;
            Password = pw;
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<CredentialsEntity> Credentials { get; set; }
    }
}
