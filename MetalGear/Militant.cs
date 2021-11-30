using System;
namespace MetalGear
{
    public class Militant
    {
        private static Militant _instance;
        private bool _containsMasterKey = false;

        public bool ContainsMasterKey
        {
            get { return _containsMasterKey; }
            set { _containsMasterKey = value; }
        }

        public static Militant Instance  //singleton pattern
        {
            get
            {
                if (_instance == null) // if no instance create gamworld instance
                {
                    _instance = new Militant(); // create gameworld
                }
                return _instance;
            }
        }

        public void SpeakDeny()
        {
            Console.WriteLine("Militant: Bring Me the MasterKey.  You should know how to get it... go check the other four rooms.");
        }

        public void SpeakGive()
        {
            Console.WriteLine("Militant: Looks like you made the masterKey.  Now GIVE me the masterKey. ");
        }

        public void SpeakChest()
        {
            Console.WriteLine("Militant: You gave me the key.  I have unlocked the chest for you.  INSPECT the chest, PICKUP the key, and UNLOCK the door north to proceed, and DROP the keys back in the chest.  That key is too heavy for you to be carrying around.");
        }

        public void SpeakBigBoss()
        {
            Console.WriteLine("Big Boss: Snake! You weren't supposed to make it here!  Whatever you do, don't INSPECT that chest and PICKUP whatever is inside!");
        }

    }
}