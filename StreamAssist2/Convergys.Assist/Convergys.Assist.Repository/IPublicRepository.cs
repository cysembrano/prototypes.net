using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.BusinessModels;

namespace Convergys.Assist.Repository
{
    public interface IPublicRepository
    {
        /// <summary>
        /// Gets EmployeeView object when authenticated with email address and password
        /// </summary>
        /// <returns>Returns null when authentication fails.</returns>
        EmployeeView LoginEmployee(string EmailAddress, string ClearPassword);

        /// <summary>
        /// Checks validity of email address.  Sends a new password.
        /// </summary>
        /// <param name="htmlTemplate">Html Message Template.  Can be set to empty.</param>
        /// <param name="url">Login url.</param>
        /// <returns></returns>
        bool SendNewPassword(string emailAddress, string htmlTemplate, string url);

    }
}
