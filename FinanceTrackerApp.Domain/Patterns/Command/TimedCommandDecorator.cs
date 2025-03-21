using System.Diagnostics;
using FinanceTrackerApp.Domain.Abstractions.Command;
namespace FinanceTrackerApp.Domain.Patterns.Command;

public class TimedCommandDecorator: ICommand
{
    private readonly ICommand _command;
    public TimedCommandDecorator(ICommand command) => _command = command;
    
    public void Execute()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        _command.Execute();
        stopwatch.Stop();
        Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds}ms");
    }
}   