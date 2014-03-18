using System;

namespace Starmade_Remote_Starter
{
    [Serializable()]
    public class ServerObject
    {
        public String Name { get; set; }
        public String Address { get; set; }

        public ServerObject(String name, String address)
        {
            Name = name;
            Address = address;
        }
        public override string ToString()
        {
            return Name + " " + Address;
        }
    }
}
