using LabTest.Application.IBusinessService;
using LabTest.Domain.Interfaces;
using LabTest.Domain.Models;

namespace LabTest.Application.Services
{
    public class UserBusinessService : IUserBusinessService
    {
        #region Global Variables
        /// <summary>
        /// Private read only variable of IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Contructor

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public UserBusinessService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Declaration of Login method
        /// </summary>
        /// <param name="loginUser">Model to check the authentication</param>
        /// <returns>string token</returns>
        public string Login(User loginUser)
        {
            return unitOfWork.User.Login(loginUser);
        } 
        #endregion
    }
}
