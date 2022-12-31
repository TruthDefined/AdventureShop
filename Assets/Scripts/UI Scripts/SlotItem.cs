public class SlotItem
{
    public DataEntity data {get; protected set;}
    public int stackSize {get; protected set;}

    public SlotItem(DataEntity source){
        data = source;
    }
}