using System;
using System.Collections.Generic;

namespace MetalGear
{
    //Hierarchy/Container Design Pattern
    public class ItemContainer : IItem 
    {
        private Dictionary<String, IItem> _chest;
        public String name { get; set; }
        private float _weight;
        public float weight
        {
            get
            {
                float containedWeight = 0;
                //Adding all weights together
                foreach (IItem item in _chest.Values) 
                {
                    containedWeight += item.weight;
                }
                return _weight + containedWeight;
            }
            set { _weight = value; }
        }
        private String _description { get; set; }
        public bool grabbable { get; set; }
        public String Description {
            get {
                if (_isLocked)
                {
                    return "Chest is locked";
                }
                else
                {
                    string itemList = "Items: ";
                    foreach (IItem item in _chest.Values)
                    {
                        itemList += "\n " + item.Description;
                    }
                    return "Name: " + name + ", Weight: " + weight + "," + _description + "\n" + itemList;
                }
            }

        }
        private IItem _decorator;
        public bool isContainer { get { return true; } }
        private int _value;
        public int value
        {
            get { return _value; }
            set { _value = value; }
        }
        private bool _isLocked;
        public bool isLocked
        {
           get{ return _isLocked; }
            set { _isLocked = value; }

        }

        public ItemContainer() : this("Chest") { }
        public ItemContainer(String name) : this(name, 1f) { }
        public ItemContainer(String name, float weight) : this(name, weight, true) { }

        public ItemContainer(string name, float weight, bool grab) : this(name, weight, grab, 0) { }

        public ItemContainer(string name, float weight, bool grab, int value)
        {
            this.name = name;
            _chest = new Dictionary<string, IItem>();
            this.weight = weight;
            grabbable = grab;
            this.value = value;
            _description = "Item Description: name: " + name + "," + "weight: " + weight + "," + "value: " + value;
        }

        public void addDecorator(IItem decorator) //decorate object
        {
            if (_decorator == null)
            {
                _decorator = decorator;

            }
            else
            {
                _decorator.addDecorator(decorator);
            }
        }

        public void AddItem(IItem item) // add item in container object
        {
            _chest[item.name] = item;
        }
        public IItem RemoveItem(String itemName) //remove item from container object
        {
            IItem item = null;
            _chest.Remove(itemName, out item);
            return item;
        }

        public void Lock() //locks chest
        {
            _isLocked = true;

        }

        public void unlock() //unlocks chest
        {
            _isLocked = false;
        }

            
    }
}