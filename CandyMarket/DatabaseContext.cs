using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
    internal class DatabaseContext
    {
        Dictionary<string, int> _taffy = new Dictionary<string, int>();
        Dictionary<string, int> _candyCoated = new Dictionary<string, int>();
        Dictionary<string, int> _chocolateBar = new Dictionary<string, int>();
        Dictionary<string, int> _zagnut = new Dictionary<string, int>();

        public Dictionary<string, CandyCount> CandyTypeCounts { get; set; } = new Dictionary<string, CandyCount>();



        /**
		 * this is just an example.
		 * feel free to modify the definition of this collection "BagOfCandy" if you choose to implement the more difficult data model.
		 * Dictionary<CandyType, List<Candy>> BagOfCandy { get; set; }
		 */

        public DatabaseContext(int tone)
        {
            Console.Beep(tone, 2500);
            //CandyTypeCounts.Add(CandyType.TaffyNotLaffy, 0);
        }

        public List<User> AllUsers { get; set; } = new List<User>();

        internal List<User> GetAllUsers()
        {
            var Jess = new User("Jess", this); //using "this" because we're currently in DatabaseContext so "this" refers to this instance
            var Sam = new User("Sam", this);
            var Kit = new User("Kit", this);

            if (!AllUsers.Any(thing => thing.Name == "Jess"))
            {
                AllUsers.Add(Jess);
            }
            if (!AllUsers.Any(thing => thing.Name == "Sam"))
            {
                AllUsers.Add(Sam); 
            }
            if (!AllUsers.Any(thing => thing.Name == "Kit"))
            {
                AllUsers.Add(Kit); 
            }

            return AllUsers;
        }

        internal List<string> GetCandyTypes()
		{
			return Enum
				.GetNames(typeof(CandyType))
				.Select(candyType =>
					candyType.Humanize(LetterCasing.Title))
				.ToList();
		}

        internal void CalculateAllUsersCandy()
        {

            foreach (var user in AllUsers)
            {
                if (!_taffy.ContainsKey(user.Name))
                {
                    _taffy.Add(user.Name, 0);
                    _candyCoated.Add(user.Name, 0);
                    _chocolateBar.Add(user.Name, 0);
                    _zagnut.Add(user.Name, 0);
                }

                foreach (var taffy in _taffy)
                {
                    if (taffy.Key == user.Name)
                    {
                        CandyTypeCounts[$"{user.Name}Taffy"] = new CandyCount { CandyOwner = user.Name, CandyType = "Taffy", TypeCount = taffy.Value };
                    }
                }
                foreach (var candy in _candyCoated)
                {
                    if (candy.Key == user.Name)
                    {
                        CandyTypeCounts[$"{user.Name}CandyCoated"] = new CandyCount { CandyOwner = user.Name, CandyType = "Candy Coated", TypeCount = candy.Value };
                    }
                }
                foreach (var bar in _chocolateBar)
                {
                    if (bar.Key == user.Name)
                    {
                        CandyTypeCounts[$"{user.Name}ChocolateBar"] = new CandyCount { CandyOwner = user.Name, CandyType = "Chocolate Bar", TypeCount = bar.Value };
                    }
                }
                foreach (var zag in _zagnut)
                {
                    if (zag.Key == user.Name)
                    {
                        CandyTypeCounts[$"{user.Name}Zagnut"] = new CandyCount { CandyOwner = user.Name, CandyType = "Zagnut", TypeCount = zag.Value };
                    }
                }

            }

            

        }

        internal void SaveNewCandy(string userName, CandyType candyType, int howMany)
        {
            switch (candyType)
            {
                case CandyType.TaffyNotLaffy:
                    _taffy[userName] += howMany;
                    break;
                case CandyType.CandyCoated:
                    _candyCoated[userName] += howMany;
                    break;
                case CandyType.ChocolateBar:
                    _chocolateBar[userName] += howMany;
                    break;
                case CandyType.ZagnutStyle:
                    _zagnut[userName] += howMany;
                    break;
                default:
                    break;
            }
        }

        internal void RemoveCandy(string name, CandyType type)
        {
            switch (type)
            {
                case CandyType.TaffyNotLaffy:
                    if (_taffy[name] > 0)
                    {
                        _taffy[name]--;  
                    }
                    break;
                case CandyType.CandyCoated:
                    if (_candyCoated[name] > 0)
                    {
                        _candyCoated[name]--; 
                    }
                    break;
                case CandyType.ChocolateBar:
                    if (_chocolateBar[name] > 0)
                    {
                        _chocolateBar[name]--; 
                    }
                    break;
                case CandyType.ZagnutStyle:
                    if (_zagnut[name] > 0)
                    {
                        _zagnut[name]--; 
                    }
                    break;
                default:
                    break;
            }
        }


            
	}


}