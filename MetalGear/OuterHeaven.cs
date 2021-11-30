using System;
using System.Collections.Generic;
namespace MetalGear
{
    public class OuterHeaven
    {
        private static OuterHeaven _instance;
        public static OuterHeaven Instance  //singleton pattern
        {
            get
            {

                if (_instance == null) // if no instance create gamworld instance
                {
                    _instance = new OuterHeaven(); // create gameworld
                }
                return _instance;
            }
        }
        
        private int _trapTimeOut; //keeps count of trap time
        public  int TrapTimeOut
        {
            get { return _trapTimeOut; }
            private set { _trapTimeOut = value; }
        }
        
        private Room _entrance;
        public Room Entrance //set entrance
        {
            get
            {
                return _entrance;
            }

            private set { _entrance = value; }
        }

        private Room _trap; // trap room
        public Room Trap
        {
            get { return _trap; }
            private set { _trap = value; }
        }

        private Room _storedTrap; // stored trap room
        public Room storedTrap
        {
            get { return _storedTrap; }
            private set { _storedTrap = value; }
        }

        private Room Previous;
        public Room _previousRoom
        {
            get { return Previous; }
            private set { Previous = value; }
        }

        private int _token; //keeps track if trap is off or on
        public int Token
        {
            get { return _token; }
            set { _token = value; }
        }

        private Room _transporter; // transporter room
        public Room Transporter
        {
            get { return _transporter; }
            set { _transporter = value; }
        }

        private Room _tradeRoom; // trade room
        public Room tradeRoom
        {
            get { return _tradeRoom; }
            set { _tradeRoom = value; }
        }
     
        private Room _dungeon; // dungeon room variable
        public Room dungeonRoom
        {
            get { return _dungeon; }
            set { _dungeon = value; }
        }

        private Room _bigBossRoom;
        public Room BigBossRoom
        {
            get { return _bigBossRoom; }
            private set { _bigBossRoom = value; }
        }
        
        private List<Room> roomList = new List<Room>(); // room list to get random room


        private OuterHeaven()
        {
            CreateWorld(); // creates world

            // subscribe to notification
            NotificationCenter.Instance.AddObserver("snakeEnteredRoom", enterRoom); // subscribe to notification
            NotificationCenter.Instance.AddObserver("snakePickedUpItem", pickUpItem);
            NotificationCenter.Instance.AddObserver("snakeDroppedItem", dropItem);
            NotificationCenter.Instance.AddObserver("snakeWentBack", back);
            NotificationCenter.Instance.AddObserver("snakeLeavingRoom", snakeLeavingRoom);
            // NotificationCenter.Instance.AddObserver("snakeBrokeIce",snakeBrokeIce);
            NotificationCenter.Instance.AddObserver("snakeUnlockedDoor", unlockDoor);
            NotificationCenter.Instance.AddObserver("snakeGaveMasterKey", gaveMasterKey);

        }

        public void gaveMasterKey(Notification notification)
        {
            Snake snake = (Snake)notification.Object;
            Militant.Instance.ContainsMasterKey = true; //monster has blueFlame
            snake.CurrentRoom.chest.unlock();
            Console.WriteLine("snake gave the master key to the militant");
        }
        
        
        // public void snakeBrokeIce(Notification notification) //snake breaks ice to leave room
        // {
        //     Console.WriteLine("You have broken the ice");
        //     if(Previous == _trap)
        //     {
        //        Trap = null;
        //        NotificationCenter.Instance.RemoveObserver("GameClockTick", TimedTrap);
        //        TrapTimeOut = 1;
        //     }
        // }


        public void snakeLeavingRoom(Notification notification) //snake leaving room notification
        {
            Snake snake = (Snake)notification.Object;
            Previous = snake.CurrentRoom;
            // if(Previous == _storedTrap && TrapTimeOut > 0 )
            // {
            //     Token = 0;
            //     NotificationCenter.Instance.RemoveObserver("GameClockTick",TimedTrap); // remove observer form timetrap after snake leaves room
            // }
           
        }

        public void back(Notification notification) // snake went back notification 
        {
            Snake snake = (Snake)notification.Object;
            if (snake.CurrentRoom == Trap)
            {
                snake.CurrentRoom = _storedTrap;
            }
            Console.WriteLine("snake went back to previous room");
        }


        public void dropItem(Notification notification) //drop item notification
        { 

            Console.WriteLine("snake dropped item");

        }
        public void pickUpItem(Notification notification) // pick up notification
        {
            Console.WriteLine("snake picked up item");
            Snake snake = (Snake)notification.Object;
            if (snake.Inventory.ContainsKey("crown"))
            {
                Console.WriteLine("Congrats!! you beat the game!");
            }

        }
        public void enterRoom(Notification notification) // enter room notification 
        {
            Snake snake = (Snake)notification.Object;
            if (Previous == Trap) //check if previous room was trap
            {
                snake.CurrentRoom = Trap;
                Console.WriteLine("The door is frozen");
                
            }
            if (snake.CurrentRoom == _storedTrap) //if snake enters room they have 10 sec to leave
            {
                if(Token == 0) { 
                    NotificationCenter.Instance.AddObserver("GameClockTick", TimedTrap); // gives notification to timetrap
                    
              
                    TrapTimeOut = 2;
                    //freezeTime = 3;
                }
            }
            if(snake.CurrentRoom == Transporter)
            {
                Console.WriteLine("You have entered the transporter room");
                Random random = new Random();
                int r = random.Next(roomList.Count); 
                snake.CurrentRoom = roomList[r]; // select random room from list
            }
            if(snake.CurrentRoom == tradeRoom)
            {
                Console.WriteLine("You may buy and sell items here");
            }
            if(snake.CurrentRoom == dungeonRoom)
            {
                Militant.Instance.SpeakDeny();
            }

            if (snake.CurrentRoom == _bigBossRoom)
            {
                Militant.Instance.SpeakBigBoss();
            }
        }
        
      
        public void unlockDoor(Notification notification) // unlock door notification
        {
            Snake snake = (Snake)notification.Object;
            Console.WriteLine("snake has unlocked the Door");
        }

        public void TimedTrap(Notification notification) //sets trap
        {

            TrapTimeOut--;
            
            if (TrapTimeOut <= 0) //set trap if timeout
            {
                Token++;
                Console.WriteLine("Oh no the door has frozen!");
                Trap = _storedTrap;
                NotificationCenter.Instance.RemoveObserver("GameClockTick", TimedTrap);
            }

        }

        private void CreateWorld() // create rooms , set exits, create items
        {
            Room entrancePlatform = new Room("on the main platform of Outer Heaven"); //outside of the castle
            Room researchRoom = new Room("Research and Development"); // main area of castle
            Room armsRoom = new Room("the arms room.");
            Room barracksRoom = new Room("barracks room");
            Room medicalBay = new Room("medical bay");
            Room bigBossRoom = new Room("Big boss room");

            //add all rooms to list for transporter 
            roomList.Add(entrancePlatform);
            roomList.Add(researchRoom);
            roomList.Add(armsRoom);
            roomList.Add(barracksRoom);
            roomList.Add(medicalBay);
            roomList.Add(bigBossRoom);
            
            //create items
            Item researchKey = new Item("researchKey", 2, true, 50);
            Item armsKey = new Item("armsKey", 2, true,8);
            Item barracksKey = new Item("barracksKey", 3, true,10);
            Item medicalKey = new Item("medicalKey", 1, true,2);
            Item masterKey = new Item("masterKey", 30, true, 100);
            Item bigBossKey = new Item("bigBossKey", 2, true, 500);
            Item selfDestructDevice = new Item("selfDestructDevice", 10, true, 1000);

            //create chests and add items in chest
            ItemContainer researchChest = new ItemContainer("researchChest");
            researchChest.AddItem(researchKey);
            researchRoom.addItem(researchChest);

            ItemContainer armsChest = new ItemContainer("armsChest");
            armsChest.AddItem(armsKey);
            armsRoom.addItem(armsChest);

            ItemContainer barracksChest = new ItemContainer("barracksChest");
            barracksChest.AddItem(barracksKey);
            barracksRoom.addItem(barracksChest);

            ItemContainer medicalChest = new ItemContainer("medicalChest");
            medicalChest.AddItem(medicalKey);
            medicalBay.addItem(medicalChest);

            ItemContainer entranceChest = new ItemContainer("entranceChest");
            entranceChest.AddItem(masterKey);
            entranceChest.AddItem(bigBossKey);
            entranceChest.isLocked = true;
            entrancePlatform.addItem(entranceChest);

            ItemContainer bigBossChest = new ItemContainer("bigBossChest");
            bigBossChest.AddItem(selfDestructDevice);
            bigBossRoom.addItem(bigBossChest);
            
            // create doors and sets exits for each room
            Door door = Door.CreateDoor(entrancePlatform, bigBossRoom, "north", "south",true);
            door = Door.CreateDoor(entrancePlatform, researchRoom, "west", "east",false);
            door = Door.CreateDoor(entrancePlatform, barracksRoom, "east", "west",false);
            door = Door.CreateDoor(researchRoom, medicalBay, "north", "south",false);
            door = Door.CreateDoor(barracksRoom, armsRoom, "north", "south",false);

            Entrance = entrancePlatform;
            BigBossRoom = bigBossRoom;
            // _storedTrap = iceHouse;
            // _trap = null;
            // Token = 0; //takes count of if the trap is still going
            // Transporter = undercroft; //transporter room
            // tradeRoom = TradeRoom; 
            // dungeonRoom = dungeon;

        }

    }

}
