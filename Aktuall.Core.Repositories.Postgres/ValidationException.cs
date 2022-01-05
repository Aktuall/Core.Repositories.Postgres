using System;

namespace Aktuall.Core.Repositories.Postgres;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    { }
}