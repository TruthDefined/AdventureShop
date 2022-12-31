/// <summary>
/// A container for an inventory item, the number of that item, and functions to add or remove from that stack.
/// </summary>
public class InventoryItem : SlotItem
{
    // public DataEntity data {get; private set;}
    // public int stackSize {get; private set;}

    public InventoryItem(DataEntity source) : base(source)
    {
        data = source;
        AddToStack();
    }
    public void AddToStack(){
        stackSize++;
    }
    public void AddToStack(int num){
        stackSize += num;
    }
    public void RemoveFromStack(){
        stackSize--;
    }
    public void RemoveFromStack(int num){
        stackSize -= num;
    }
}
