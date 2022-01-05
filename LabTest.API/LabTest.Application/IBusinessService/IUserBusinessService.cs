using System;
using System.Collections.Generic;
using System.Text;
using LabTest.Domain.Models;

namespace LabTest.Application.IBusinessService
{
    public interface IUserBusinessService
    {
        /// <summary>
        /// Declaration of Login method
        /// </summary>
        /// <param name="loginUser">Model to check the authentication</param>
        /// <returns>string token</returns>
        string Login(User loginUser);
    }
}
