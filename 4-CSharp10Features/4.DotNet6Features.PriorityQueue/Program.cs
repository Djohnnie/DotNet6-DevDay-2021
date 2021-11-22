var queue = new PriorityQueue<string, byte>();

queue.Enqueue("Apple", 1);
queue.Enqueue("Banana", 2);
queue.Enqueue("Lemon", 4);
queue.Enqueue("Peach", 4);
queue.Enqueue("Kiwi", 3);

while (queue.Count > 0)
{
    Console.WriteLine(queue.Dequeue());
}