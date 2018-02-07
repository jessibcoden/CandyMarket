using System;
using System.Collections.Generic;
using System.Text;

namespace CandyMarket
{
    internal class User
    {
        readonly DatabaseContext _db;

        public string Name { get; set; }

        public User(string name, DatabaseContext db)
        {
            Name = name;
            _db = db;
        }
        
        public void AddCandy(CandyType typeofCandy, int howMany)
        {
            _db.SaveNewCandy(Name, typeofCandy, howMany);
        }

        public void GiveCandy(CandyType type, string receiver)
        {
            _db.RemoveCandy(Name, type);
            _db.SaveNewCandy(receiver, type, 1);
        }
    }
}
