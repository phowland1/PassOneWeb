﻿using System;
using System.Linq;

namespace PassOne.Domain
{
    [Serializable]
    public class Credentials
    {
        public int Id { get; set; }
        public string Website { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }

        public byte[] EncryptedWebsite { get; set; }
        public byte[] EncryptedUrl { get; set; }
        public byte[] EncryptedUsername { get; set; }
        public byte[] EncryptedPassword { get; set; }
        public byte[] EncryptedEmail { get; set; }

        //Constructors
        public Credentials()
        {
        }

        public Credentials(int id, string title)
        {
            Id = id;
            Website = title;
        }

        public Credentials(string title, string url, string username, string password, string email, int id = 0 )
        {
            CheckForMissingInformation(title, username, password, email);
            Id = id;
            Website = title;
            Url = url;
            Username = username;
            Password = password;
            EmailAddress = email;
        }

        public Credentials(int id, byte[] title, byte[] url, byte[] username, byte[] password, byte[] email)
        {
            Id = id;
            Website = string.Empty;
            Url = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            EmailAddress = string.Empty;
            EncryptedWebsite = title;
            EncryptedUrl = url;
            EncryptedUsername = username;
            EncryptedPassword = password;
            EncryptedEmail = email;
        }

        /// <summary>
        /// Checks the submitted credentials set for any missing information and throws an exception if it does.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        private void CheckForMissingInformation(string title, string username, string password, string email)
        {
            if (title == string.Empty)
                throw new MissingInformationException("a website name");
            if (username == string.Empty && email == string.Empty)
                throw new MissingInformationException("a username or email address");
            if (password == string.Empty)
                throw new MissingInformationException("a password");
        }
        /// <summary>
        /// Method to encrypt the contents of this credentials object, clears all string data when complete.
        /// </summary>
        /// <param name="encrypt">Encryption object containing key and vector values</param>
        public void Encrypt(Encryption encrypt)
        {
            EncryptedWebsite = encrypt.Encrypt(Website);
            Website = string.Empty;
            EncryptedUrl = encrypt.Encrypt(Url);
            Url = string.Empty;
            EncryptedUsername = encrypt.Encrypt(Username);
            Username = string.Empty;
            EncryptedPassword = encrypt.Encrypt(Password);
            Password = string.Empty;
            EncryptedEmail = encrypt.Encrypt(EmailAddress);
            EmailAddress = string.Empty;
        }

        /// <summary>
        /// Method to decrypt the contents of this Credentials object, clears all byte[] data when complete.
        /// </summary>
        /// <param name="encrypt">Encryption object containing key and vectory values</param>
        public void Decrypt(Encryption encrypt)
        {
            Website = encrypt.Decrypt(EncryptedWebsite);
            EncryptedWebsite = null;
            Url = encrypt.Decrypt(EncryptedUrl);
            EncryptedUrl = null;
            Username = encrypt.Decrypt(EncryptedUsername);
            EncryptedUsername = null;
            Password = encrypt.Decrypt(EncryptedPassword);
            EncryptedPassword = null;
            EmailAddress = encrypt.Decrypt(EncryptedEmail);
            EncryptedEmail = null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Credentials) obj);
        }

        protected bool Equals(Credentials other)
        {
            return Id == other.Id && string.Equals(Website, other.Website) && string.Equals(Url, other.Url) &&
                   string.Equals(Username, other.Username) && string.Equals(Password, other.Password) &&
                   string.Equals(EmailAddress, other.EmailAddress) && EncryptedEquals(EncryptedEmail, other.EncryptedEmail) &&
                   EncryptedEquals(EncryptedWebsite, other.EncryptedWebsite) && EncryptedEquals(EncryptedUrl, other.EncryptedUrl) &&
                   EncryptedEquals(EncryptedUsername, other.EncryptedUsername) &&
                   EncryptedEquals(EncryptedPassword, other.EncryptedPassword);
        }

        protected bool EncryptedEquals(byte[] thisByte, byte[] other)
        {
            if (thisByte == null || other == null)
                return thisByte == other;
            return !other.Where((t, i) => t != thisByte[i]).Any();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Id;
                hashCode = (hashCode * 397) ^ (Website != null ? Website.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Url != null ? Url.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Username != null ? Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EmailAddress != null ? EmailAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EncryptedEmail != null ? EncryptedEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EncryptedWebsite != null ? EncryptedWebsite.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EncryptedUrl != null ? EncryptedUrl.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EncryptedUsername != null ? EncryptedUsername.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EncryptedPassword != null ? EncryptedPassword.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return "ID: " + Id +
                   "/nWebsite: " + Website +
                   "/nURL: " + Url +
                   "/nUsername: " + Username +
                   "/nPassword: " + Password +
                   "/nEmail Address: " + EmailAddress;
        }

        /// <summary>
        /// Creates a copy of this credentials object
        /// </summary>
        /// <returns>The copied credentials</returns>
        public Credentials Copy()
        {
            return new Credentials(Website, Url, Username, Password, EmailAddress, Id);
        }
    }
}
