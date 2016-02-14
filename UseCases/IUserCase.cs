using System;

namespace UseCases
{
    public interface IUserCase
    {
        IFormattable CreateUser(string nickaname, string email);

        void DeleteUser(IFormattable id);
    }
}
