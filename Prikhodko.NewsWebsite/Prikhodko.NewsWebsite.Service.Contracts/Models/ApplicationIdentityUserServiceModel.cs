using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class ApplicationIdentityUserServiceModel
    {
        public UserServiceModel User { get; set; }

        /// <summary>Email</summary>
        public virtual string Email { get; set; }

        /// <summary>True if the email is confirmed, default is false</summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>The salted/hashed form of the user password</summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>PhoneNumber for the user</summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///     True if the phone number is confirmed, default is false
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>Is two factor enabled for the user</summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        ///     DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>Is lockout enabled for this user</summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        ///     Used to record failures for the purposes of lockout
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>Navigation property for user roles</summary>
        public virtual IList<string> Roles { get; set; }

        /// <summary>Navigation property for user claims</summary>
        public virtual ICollection<ClaimServiceModel> Claims { get; private set; }

        /// <summary>Navigation property for user logins</summary>
        public virtual ICollection<LoginViewModel> Logins { get; private set; }

        /// <summary>User ID (Primary Key)</summary>
        public virtual string Id { get; set; }

        /// <summary>User name</summary>
        public virtual string UserName { get; set; }
    }
}