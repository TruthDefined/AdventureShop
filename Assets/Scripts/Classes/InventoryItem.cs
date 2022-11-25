public class InventoryItem
{
    public DataEntity data {get; private set;}
    public int stackSize {get; private set;}

    public InventoryItem(DataEntity source){
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
