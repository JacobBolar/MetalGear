using System;
namespace MetalGear
{
    public class Militant
    {
        private static Militant _instance;
        private bool _containsFlame = false;
        public bool containFlame
        {
            get { return _containsFlame; }
            set { _containsFlame = value; }
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

        public void speak()
        {
            Console.WriteLine("Monster: Bring me the sacred blue flame in exchange for the key!");
        }


    }
}