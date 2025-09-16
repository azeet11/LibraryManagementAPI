using LibraryManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DelegateController : ControllerBase
{
    private delegate int MathOperation(int a, int b);
    private readonly ILogger<DelegateController> _logger;
    //private readonly DataContext _context;

    //public DelegateController(DataContext context)
    //{
    //    _context = context;
    //}

    [HttpGet()]
    public string GetData()
    {
        var child = new Child();
        var parent = new Parent();
        return child.GetName();
    }

    [HttpGet("test")]
    public IActionResult Get()
    {
        // 1. Func Example: Add numbers
        var sum = Addfunc(x => x + 5);   // Input 10 => 15

        // 2. Predicate Example: Check if a number is positive
        var isNumPositive = CheckPositive(x => x > 0, -2);  // -2 => false

        // 3. Action Example: Log a message
        LogMessage(msg => Console.WriteLine(msg), "Hello from Action delegate!");

        return Ok(new
        {
            SumResult = sum,
            IsPositiveCheck = isNumPositive,
            Message = "Delegates executed successfully!"
        });
    }

    // Func<T, TResult> demo
    private int Addfunc(Func<int, int> add)
    {
        var result = add(10); // Call lambda with x=10
        return result;
    }

    // Action<T> demo
    private void LogMessage(Action<string> logMessage, string message)
    {
        logMessage(message); // Execute the delegate
    }

    // Predicate<T> demo
    private bool CheckPositive(Predicate<int> isPositive, int number)
    {
        return isPositive(number); // Evaluate the condition
    }

    //[HttpGet]
    //public IActionResult Get()
    //{
    //    Func<int, int, int> add = (a, b) => a + b;
    //    Predicate<int> isPositive = x => x > 0;
    //    Action<string> logMessage = message => Console.WriteLine(message);
    //    var sum = Addfunc(add);
    //    var isNumPositive = CheckPositive(isPositive, 10);
    //    LogMessage(logMessage, "This is a log message.");
    //    var bookList = _context.Books.Where(x => x.Id == 1).ToList();
    //    MathOperation d = Add;
    //    d += Subtract;
    //    d += Multiply;

    //    var result = d(10, 5); // Calls both Add and Subtract

    //    CallBack(Add, 20, 10);

    //    return Ok(result);
    //}

    private int Addfunc(Func<int, int, int> add)
    {
        var result = add(10, 20);
        return result;
    }
    //private void LogMessage(Action<string> logMessage, string message)
    //{
    //    logMessage(message);
    //}
    //private bool CheckPositive(Predicate<int> isPositive, int number)
    //{
    //    return isPositive(number);
    //}
    private void CallBack(MathOperation operation, int a, int b)
    {
        var result = operation(a, b);
        Console.WriteLine($"Result: {result}");
    }
    private int Add(int a, int b)
        {
        Console.WriteLine("Add method called");
        return a + b;
    }
    private int Subtract(int a, int b)
        {
        Console.WriteLine("Subtract method called");
        return a - b;
    }
    private int Multiply(int a, int b)
        {
        Console.WriteLine("Multiply method called");
        return a * b;
    }
}
