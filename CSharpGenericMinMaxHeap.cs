/*
	This is a C# implementation of Generic Heap (Priority Queue in Java).
*/
public abstract class Heap<T>
{
	/// <summary>
    /// Current array length.
    /// </summary>
    protected int capacity;
    /// <summary>
    /// Current number of elements in Heap.
    /// </summary>
    protected int size;
    /// <summary>
    /// Array of Heap elements.
    /// </summary>
    protected T[] items;

    public Heap()
    {
        this.capacity = 10;
        this.size = 0;
        this.items = new T[capacity];
    }

    public int GetLeftChildIndex(int parentIndex)
    {
        int index = 2 * parentIndex + 1;
        return index;
    }

    public int GetRightChildIndex(int parentIndex)
    {
        int index = 2 * parentIndex + 2;
        return index;
    }

    public int GetParentIndex(int childIndex)
    {
        int index = (childIndex - 1) / 2;
        return index;
    }

    public bool HasLeftChild(int index)
    {
        return GetLeftChildIndex(index) < size;
    }

    public bool HasRightChild(int index)
    {
        return GetRightChildIndex(index) < size;
    }

    public bool HasParent(int index)
    {
        return GetParentIndex(index) >= 0;
    }

    public T LeftChild(int parentIndex)
    {
        int index = GetLeftChildIndex(parentIndex);
        return items[index];
    }

    public T RightChild(int parentIndex)
    {
        int index = GetRightChildIndex(parentIndex);
        return items[index];
    }

    public T Parent(int childIndex)
    {
        int index = GetParentIndex(childIndex);
        return items[index];
    }

    public void Swap(int indexOne, int indexTwo)
    {
        T temp = items[indexOne];
        items[indexOne] = items[indexTwo];
        items[indexTwo] = temp;
    }

    public void EnsureCapacity()
    {
        if (size == capacity)
        {
            capacity = capacity << 1;
            Array.Resize(ref items, capacity);
        }
    }

    public T Peek()
    {
        if(IsEmpty()) return default(T);
		else return items[0];
    }

    public bool IsEmpty()
    {
        if (size == 0)
        {
            return true;
        }
		return false;
    }

    public T Poll()
    {
        if(IsEmpty()) return default(T);
        T item = items[0];
        items[0] = items[size - 1];
        size--;
        HeapifyDown();
        return item;
    }

    public void Add(T item)
    {
        EnsureCapacity();
        items[size] = item;
        size++;
        HeapifyUp();
    }

    public abstract void HeapifyDown();

    public abstract void HeapifyUp();
	
	public abstract void PrintElements(string format = "");
}

public class MaxHeap<T> : Heap<T> where T : IComparable<T>
{
    public override void HeapifyDown()
    {
        int index = 0;
        while (HasLeftChild(index))
        {
            int smallerChildIndex = GetLeftChildIndex(index);

            if (HasRightChild(index) && RightChild(index).CompareTo(LeftChild(index)) > 0)
            {
                smallerChildIndex = GetRightChildIndex(index);
            }
            if (items[index].CompareTo(items[smallerChildIndex]) > 0)
            {
                break;
            }
            else
            {
                Swap(index, smallerChildIndex);
            }
            index = smallerChildIndex;
        }
    }

    public override void HeapifyUp()
    {
        int index = size - 1;
        while (HasParent(index) && Parent(index).CompareTo(items[index]) < 0)
        {
            int parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = parentIndex;
        }
    }
	
	public override void PrintElements(string format = " ")
	{
		while(!this.IsEmpty()){
			Console.Write($"{this.Poll()}{format}");
		}
	}
}

public class MinHeap<T>: Heap<T> where T: IComparable<T>
{
    public override void HeapifyDown()
    {
        int index = 0;
        while (HasLeftChild(index))
        {
            int smallerChildIndex = GetLeftChildIndex(index);
            if(HasRightChild(index))
            {
                T rightChild = RightChild(index);
                T leftChild = LeftChild(index);

                if(rightChild.CompareTo(leftChild) < 0)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }
            }

            if (items[index].CompareTo(items[smallerChildIndex]) < 0)
            {
                break;
            }
            else
            {
                Swap(index, smallerChildIndex);
            }
            index = smallerChildIndex;
        }
    }

    public override void HeapifyUp()
    {
        int index = size - 1;
        while (HasParent(index) && Parent(index).CompareTo(items[index]) > 0)
        {
            int parentIndex = GetParentIndex(index);
            Swap(parentIndex, index);
            index = GetParentIndex(index);
        }
    }
	
	public override void PrintElements(string format = " ")
	{
		while(!this.IsEmpty()){
			Console.Write($"{this.Poll()}{format}");
		}
	}
}

void Main()
{
	IntHeap();
	DoubleHeap();
	StringHeap();
}

public void StringHeap()
{
	Console.WriteLine($"{Environment.NewLine}-------------------String Heap Demo-------------------");
	var minHeap = new MinHeap<string>();
	var maxHeap = new MaxHeap<string>();
	var range = 10;
	var random = new Random();
	for(var i = 0; i < range; i++)
	{
		var value = Path.GetRandomFileName().Replace(".", "");
		minHeap.Add(value);
		maxHeap.Add(value);
		Console.WriteLine($"{value} ");
	}
	
	Console.WriteLine($"{Environment.NewLine} MinHeap Poll Values:\n------------------------");
	minHeap.PrintElements(Environment.NewLine);
	Console.WriteLine();
	Console.WriteLine($"{Environment.NewLine} MaxHeap Poll Values:\n------------------------");
	maxHeap.PrintElements(Environment.NewLine);
	Console.WriteLine();
}

public void DoubleHeap()
{
	Console.WriteLine($"{Environment.NewLine}-------------------Double Heap Demo-------------------");
	var range = 10;
	var minHeap = new MinHeap<double>();
	var maxHeap = new MaxHeap<double>();
	var rnd = new Random();
	
	for (int i = 0; i < range; i++)
    {
        var value = rnd.NextDouble();
        minHeap.Add(value);
		maxHeap.Add(value);
		Console.WriteLine($"{value} ");
    }

	Console.WriteLine($"{Environment.NewLine} MinHeap Poll Values:\n------------------------");
	minHeap.PrintElements(Environment.NewLine);
	Console.WriteLine();
	Console.WriteLine($"{Environment.NewLine} MaxHeap Poll Values:\n------------------------");
	maxHeap.PrintElements(Environment.NewLine);
	Console.WriteLine();
}

public void IntHeap()
{
	Console.WriteLine($"{Environment.NewLine}-------------------Integer Heap Demo-------------------");
	var range = 10;
	var rnd = new Random();
	var minHeap = new MinHeap<int>();
    var maxHeap = new MaxHeap<int>();
	
	for (int i = 0; i < range; i++)
    {
		var value = rnd.Next(1, range * 10);
        minHeap.Add(value);
		maxHeap.Add(value);
		Console.Write($"{value} ");
    }

	Console.WriteLine($"{Environment.NewLine} MinHeap Poll Values:\n------------------------");
	minHeap.PrintElements();
	Console.WriteLine();
	Console.WriteLine($"{Environment.NewLine} MaxHeap Poll Values:\n------------------------");
	maxHeap.PrintElements();
	Console.WriteLine();
}