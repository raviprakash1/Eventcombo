using EventCombo.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCombo.Service
{
  public interface IAccountService
  {
    bool CheckUserName(string userName);
    bool RegisterLoginAttempt(string userId, string ipAddress);
    void RegisterLogout(string userId);
    bool RegisterNewUser(string userId, RegisterUserRequestViewModel user, string ipAddress, bool autoRegister = false);
    string GetNewCode(int length);
    void ProcessNewCode(string userId, string userEmail, string code);
    bool TryUseCode(string userId, string code);
    bool CheckCodePassword(string userId, string Code);
    void SendNewPasswordNotification(string userId, string userEmail);
    bool CheckUserLogin(string userId);
  }
}
