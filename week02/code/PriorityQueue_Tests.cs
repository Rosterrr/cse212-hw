using Microsoft.VisualStudio.TestTools.UnitTesting;

// Problem 2 - Test cases, observed defects, and results before and after fixes.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three priorities, with the highest priority at the back.
    // Expected Result: Enqueue preserves insertion order; Dequeue returns High.
    // Defect(s) Found: Dequeue stopped its search before the last item in the queue.
    // Actual Result (before priority fixes): FAILED; returned Middle instead of High.
    // Actual Result (after fixes): PASSED; enqueue order was preserved and High was returned.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Middle", 5);
        priorityQueue.Enqueue("High", 10);

        Assert.AreEqual("[Low (Pri:1), Middle (Pri:5), High (Pri:10)]", priorityQueue.ToString());
        Assert.AreEqual("High", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue tied highest priorities with a lower-priority item between them.
    // Expected Result: First, Second, Third, then Low; tied priorities follow FIFO.
    // Defect(s) Found: The >= comparison replaced an earlier tied item with a later one.
    // Actual Result (before priority fixes): FAILED; returned Second instead of First.
    // Actual Result (after fixes): PASSED; returned First, Second, Third, Low, then was empty.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 9);
        priorityQueue.Enqueue("Second", 9);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Third", 9);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Remove a highest-priority item that is already at the front.
    // Expected Result: High is removed, leaving Middle and Low, which are then removed.
    // Defect(s) Found: Dequeue returned the selected value without removing its item.
    // Actual Result (before priority fixes): FAILED; High remained in the queue after Dequeue.
    // Actual Result (after fixes): PASSED; each item was removed and the queue ended empty.
    public void TestPriorityQueue_RemovesDequeuedItems()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Middle", 5);
        priorityQueue.Enqueue("Low", 1);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("[Middle (Pri:5), Low (Pri:1)]", priorityQueue.ToString());
        Assert.AreEqual("Middle", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Dequeue from a newly created empty queue.
    // Expected Result: InvalidOperationException with the exact message The queue is empty.
    // Defect(s) Found: None; the empty-queue exception already met the requirement.
    // Actual Result (before priority fixes and after fixes): PASSED; the exception type
    // and message were InvalidOperationException and "The queue is empty."
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var error = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", error.Message);
    }

    [TestMethod]
    // Scenario: Enqueue one item, remove it, and attempt another Dequeue.
    // Expected Result: Only is returned once; the second call throws the required exception.
    // Defect(s) Found: The single item remained in the queue after it was returned.
    // Actual Result (before priority fixes): FAILED; the second Dequeue did not throw.
    // Actual Result (after fixes): PASSED; Only was returned once, then the required
    // InvalidOperationException with message "The queue is empty." was thrown.
    public void TestPriorityQueue_SingleItemThenEmpty()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Only", 4);

        Assert.AreEqual("Only", priorityQueue.Dequeue());
        var error = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", error.Message);
    }

    [TestMethod]
    // Scenario: Enqueue items whose priorities are all negative, with the highest last.
    // Expected Result: MinusOne, MinusThree, then MinusTen because higher numbers win.
    // Defect(s) Found: The search skipped the final item, which had the highest priority.
    // Actual Result (before priority fixes): FAILED; returned MinusThree instead of MinusOne.
    // Actual Result (after fixes): PASSED; returned MinusOne, MinusThree, then MinusTen.
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("MinusTen", -10);
        priorityQueue.Enqueue("MinusThree", -3);
        priorityQueue.Enqueue("MinusOne", -1);

        Assert.AreEqual("MinusOne", priorityQueue.Dequeue());
        Assert.AreEqual("MinusThree", priorityQueue.Dequeue());
        Assert.AreEqual("MinusTen", priorityQueue.Dequeue());
    }
}
