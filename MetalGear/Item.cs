using System;
using System.Collections.Generic;
namespace MetalGear
{

    public class Item : IItem
    {

        public String name { get; set; }
        public float weight { get; set; }
        private String _description { get; set; }
        public bool grabbable { get; set; }
        public String Description { get { return _description; } }
        private IItem _decorator;
        public bool isContainer { get { return false; } }
        private int _value;
        public int value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Item() : this("Nameless") { }
        public Item(String name) : this(name, 1f) { }
        public Item(String name, float weight) : this(name, weight, true) { }

        public Item(string name, float weight, bool grab) : this(name, weight, grab, 0) { }

        public Item(string name, float weight,bool grab, int value)
        {
            this.name = name;
            this.weight = weight;
            grabbable = grab;
            this.value = value;
            _description = "Item Description: name: " + name + "," + "weight: " + weight + "," + "value: " + value;
        }

        public void addDecorator(IItem decorator)
        {
            if(_decorator == null)
            {
                _decorator = decorator;

            }
            else
            {
                _decorator.addDecorator(decorator);
            }
        }

        public void AddItem(IItem item)
        {

        }
        
        public IItem RemoveItem(String itemName)
        {
            return null;
        }
    }
}
