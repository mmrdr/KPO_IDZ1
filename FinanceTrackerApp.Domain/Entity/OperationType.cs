using System.ComponentModel;
using System.Reflection;

namespace FinanceTrackerApp.Domain.Entities;

public enum OperationType
{
    [Description("Income")]
    Income,

    [Description("Expense")]
    Expense
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}