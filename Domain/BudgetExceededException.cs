using System;

namespace Coursework.Domain;

public class BudgetExceededException : Exception
{
    public BudgetExceededException(string message) : base(message) { }
}