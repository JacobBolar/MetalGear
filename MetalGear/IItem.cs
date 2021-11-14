using System;

namespace MetalGear
{
    public interface IItem
    {
        String name { get; set; }
        float weight { get; set; }
        String Description { get; }
        void addDecorator(IItem decorator);
        int value { get; set; }
        bool isContainer { get; }
        void AddItem(IItem item);
        IItem RemoveItem(String itemName);
    }
}