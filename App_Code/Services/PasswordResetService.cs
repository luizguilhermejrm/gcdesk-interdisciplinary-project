using System;
using System.Collections.Generic;

public class PasswordResetService
{
    public int CreateResetToken(int userId, string token, string expiresAt)
    {
        return PasswordResetRepository.Insert(userId, token, expiresAt);
    }

    public List<PasswordResetRow> GetValidToken(string token)
    {
        return PasswordResetRepository.GetValidToken(token);
    }

    public int ResetPassword(int userId, string newPassword)
    {
        string hashed = Function.HashText(newPassword);
        return PasswordResetRepository.UpdatePassword(userId, hashed);
    }

    public int MarkTokenUsed(int resetId)
    {
        return PasswordResetRepository.MarkUsed(resetId);
    }
}
