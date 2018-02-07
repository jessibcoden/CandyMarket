﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
	class Program
	{
        static User _currentUser;
        static List<User>  _allUsers;

        static void Main(string[] args)
		{
			// wanna be a l33t h@x0r? skip all this console menu nonsense and go with straight command line arguments. something like `candy-market add taffy "blueberry cheesecake" yesterday`
			var db = SetupNewApp();
            _allUsers = db.GetAllUsers();

            _currentUser = CurrentUserMenu(db);

            var run = true;
			while (run)
			{
                ConsoleKeyInfo userInput = MainMenu(db);
                CandyType candyType;


                switch (userInput.KeyChar)
				{
					case '0':
						run = false;
						break;
					case '1': // add candy to your bag

						// select a candy type, returns SelectedCandyType:
						var selectedCandyType = AddNewCandyType(db);

                        /** MORE DIFFICULT DATA MODEL
						 * show a new menu to enter candy details
						 * it would be convenient to show the menu in stages e.g. press enter to go to next detail stage, but write the whole screen again with responses populated so far.
						 */

                        // if(moreDifficultDataModel) bug - this is passing candy type right now (which just increments in our DatabaseContext), but should also be passing candy details

                        candyType = (CandyType)int.Parse(selectedCandyType.KeyChar.ToString());
                        _currentUser.AddCandy(candyType, 1);
						break;
					case '2':
                        /** eat candy
						 * select a candy type
						 * 
						 * select specific candy details to eat from list filtered to selected candy type
						 * 
						 * enjoy candy
						 */
                        candyType = (CandyType)int.Parse(selectedCandyType.KeyChar.ToString());
                        selectedCandyType = EatCandy(db);
                        db.RemoveCandy(_currentUser.Name, candyType);
                        break;
					case '3':
                        /** throw away candy
						 * select a candy type
						 * if(moreDifficultDataModel) enhancement - give user the option to throw away old candy in one action. this would require capturing the detail of when the candy was new.
						 * 
						 * select specific candy details to throw away from list filtered to selected candy type
						 * 
						 * cry for lost candy
						 */
                        candyType = (CandyType)int.Parse(selectedCandyType.KeyChar.ToString());
                        selectedCandyType = TossCandy(db);
                        db.RemoveCandy(_currentUser.Name, candyType);
                        break;
					case '4':
						/** give candy
						 * feel free to hardcode your users. no need to create a whole UI to register users.
						 * no one is impressed by user registration unless it's just amazingly fast & simple
						 * 
						 * select candy in any manner you prefer.
						 * it may be easiest to reuse some code for throwing away candy since that's basically what you're doing. except instead of throwing it away, you're giving it away to another user.
						 * you'll need a way to select what user you're giving candy to.
						 * one design suggestion would be to put candy "on the table" and then "give the candy on the table" to another user once you've selected all the candy to give away
						 */
						break;
					case '5':
						/** trade candy
						 * this is the next logical step. who wants to just give away candy forever?
						 */
						break;
					default: // what about requesting candy? like a wishlist. that would be cool.
						break;
				}
			}
		}

		static DatabaseContext SetupNewApp()
		{
			Console.Title = "Cross Confectioneries Incorporated";

			var cSharp = 554;
			var db = new DatabaseContext(tone: cSharp);

			Console.SetWindowSize(60, 30);
			Console.SetBufferSize(60, 30);
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			return db;
		}

        static User CurrentUserMenu(DatabaseContext db)
        {
            var allUsers = db.GetAllUsers();
            var userNames = allUsers.Select(user => user.Name).ToList(); //ToList to convert IEnum string to a list of strings
            var userMenu = new View()
                    .AddMenuText("Who are you?")
                    .AddMenuOptions(userNames);

            Console.Write(userMenu.GetFullMenu());

            ConsoleKeyInfo currentUser = Console.ReadKey();

            var userIndex = int.Parse(currentUser.KeyChar.ToString()); // converts key to userIndex as a string

            
            return allUsers[userIndex -1];
        }

        static ConsoleKeyInfo MainMenu(DatabaseContext db)
        {
            db.CalculateAllUsersCandy();
            
            View mainMenu = new View()
                    .AddMenuText("Your Candy Collection..");
                    foreach (var item in db.CandyTypeCounts)
                    {
                        var userName = item.Key.ToString();
                        if (userName == _currentUser.Name)
                        {
                            mainMenu.AddMenuText($"{item.Value.CandyType}: {item.Value.TypeCount}");

                        }
                    }
            mainMenu.AddMenuOption("Did you just get some new candy? Add it here.")
					.AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuOption("Do you want to throw away some candy? Toss it here.")
                    .AddMenuText("Press 0 to exit.");

            Console.Write(mainMenu.GetFullMenu());
			ConsoleKeyInfo userOption = Console.ReadKey();
			return userOption;
		}

		static ConsoleKeyInfo AddNewCandyType(DatabaseContext db)
		{
			var candyTypes = db.GetCandyTypes();

			var newCandyMenu = new View()
					.AddMenuText("What type of candy did you get?")
					.AddMenuOptions(candyTypes);

			Console.Write(newCandyMenu.GetFullMenu());

			ConsoleKeyInfo selectedCandyType = Console.ReadKey();
			return selectedCandyType;
		}

        static ConsoleKeyInfo EatCandy(DatabaseContext db)
        {
            var candyTypes = db.GetCandyTypes();

            var newCandyMenu = new View()
                    .AddMenuText("What type of candy did you eat?")
                    .AddMenuOptions(candyTypes);

            Console.Write(newCandyMenu.GetFullMenu());

            ConsoleKeyInfo selectedCandyType = Console.ReadKey();
            return selectedCandyType;
        }

        static ConsoleKeyInfo TossCandy(DatabaseContext db)
        {
            var candyTypes = db.GetCandyTypes();

            var newCandyMenu = new View()
                    .AddMenuText("What type of candy do you want to toss?")
                    .AddMenuOptions(candyTypes);

            Console.Write(newCandyMenu.GetFullMenu());

            ConsoleKeyInfo selectedCandyType = Console.ReadKey();
            return selectedCandyType;
        }

        static ConsoleKeyInfo GiveCandy(DatabaseContext db)
        {
            var candyTypes = db.GetCandyTypes();

            var newCandyMenu = new View()
                    .AddMenuText("What type of candy do you want to toss?")
                    .AddMenuOptions(candyTypes);

            Console.Write(newCandyMenu.GetFullMenu());

            ConsoleKeyInfo selectedCandyType = Console.ReadKey();
            return selectedCandyType;
        }
    }
}
