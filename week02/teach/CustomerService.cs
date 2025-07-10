
/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: We expect only 1 customer to be added based on the limit set up
        // Expected Result: Only one Customer added, for the rest should display a message of maximun customers reached.
        // Console.WriteLine("Test 1");
        // var cs = new CustomerService(1);
        // cs.AddNewCustomer();
        // cs.AddNewCustomer();
        // cs.AddNewCustomer();
        // Defect(s) Found: It is adding an extra customer over the limit (Index error)

        Console.WriteLine("=================");

        // Test 2
        // Scenario: We will add 2 customers, then serve one and then add 1 and then serve 3.
        // Expected Result: We should have the fist customer served first and then in order the rest of them
        // Console.WriteLine("Test 2");
        // var css = new CustomerService(3);
        // css.AddNewCustomer();
        // css.AddNewCustomer();
        // css.ServeCustomer();
        // css.AddNewCustomer();
        // css.ServeCustomer();
        // css.ServeCustomer();
        
        // Defect(s) Found: When serving it is showing the second customer and not the fist. We also got an indexOutOfRange exeption.

        Console.WriteLine("=================");

        // Test 3
        // Scenario: We will try to serve a customer before adding one.
        // Expected Result: A message should be displaying saying the queue is empty
        Console.WriteLine("Test 3");
        var test = new CustomerService(3);
        test.ServeCustomer();
        
        // Defect(s) Found: An exeption is raised but no error message for the user
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize-1;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        if (_queue.Count > _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        if (_queue.Count <= 0)
        {
            Console.WriteLine("The queue is empty, no customer can be served.");

        }
        else
        {
            var customer = _queue[0];
            Console.WriteLine(customer);
            _queue.RemoveAt(0);
        }

    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}