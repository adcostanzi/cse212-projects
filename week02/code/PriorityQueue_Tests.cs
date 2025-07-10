using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add 3 priority items, second with higher priority, dequeue the 3
    // Expected Result: order should be: second item, then first and then third
    // Defect(s) Found: The dequeued items did not match the expected priority order. 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Homework", 2);
        priorityQueue.Enqueue("Work", 3);
        priorityQueue.Enqueue("Play videogames", 1);
        List<string> items = new List<string>();
        items.Add(priorityQueue.Dequeue());
        items.Add(priorityQueue.Dequeue());
        items.Add(priorityQueue.Dequeue());
        Console.WriteLine(items);
        List<string> toCompare = new List<string> { "Work", "Homework", "Play videogames" };
        Console.WriteLine(toCompare);
        CollectionAssert.AreEqual(items, toCompare, "The dequeued items did not match the expected priority order.");
    }

    [TestMethod]
    // Scenario: 4 Items added, 3 out of 4 have the same priority
    // Expected Result: Items with the same priority should be dequeue in the order they were added
    // Defect(s) Found: The order is inverse when priority is the same (first dequeue is the last that was added)
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Homework", 2);
        priorityQueue.Enqueue("Work", 2);
        priorityQueue.Enqueue("Play videogames", 1);
        priorityQueue.Enqueue("FHE", 2);
        List<string> items = new List<string>();
        items.Add(priorityQueue.Dequeue());
        items.Add(priorityQueue.Dequeue());
        items.Add(priorityQueue.Dequeue());
        items.Add(priorityQueue.Dequeue());
        List<string> toCompare = new List<string> { "Homework", "Work", "FHE", "Play videogames" };
        CollectionAssert.AreEqual(items, toCompare, "The dequeued items did not match the expected priority order.");
    }

    [TestMethod]
    // Scenario: An item is dequeue when queue is empty
    // Expected Result: An exemption should be raised
    // Defect(s) Found: No issues
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue(), "Dequeueing an empty queue should raise an exemption");
    }
    
    // Add more test cases as needed below.
}