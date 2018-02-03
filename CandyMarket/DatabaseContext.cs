using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CandyMarket
{
    internal class DatabaseContext
    {
        private int _countOfTaffy;
        private int _countOfCandyCoated;
        private int _countOfChocolateBar;
        private int _countOfZagnut;

        /**
		 * this is just an example.
		 * feel free to modify the definition of this collection "BagOfCandy" if you choose to implement the more difficult data model.
		 * Dictionary<CandyType, List<Candy>> BagOfCandy { get; set; }
		 */

        public DatabaseContext(int tone) => Console.Beep(tone, 2500);

        internal List<string> GetAllUsers()
        {
            return Enum
                .GetNames(typeof(User))
                .Select(allUsers =>
                    allUsers.Humanize(LetterCasing.Title))
                .ToList();
        }

        internal List<string> GetCandyTypes()
		{
			return Enum
				.GetNames(typeof(CandyType))
				.Select(candyType =>
					candyType.Humanize(LetterCasing.Title))
				.ToList();
		}

        internal string ShowTaffyCount()
        {
            return $"Taffy:{_countOfTaffy}";
        }

        internal string ShowCandyCoatedCount()
        {
            return $"Candy Coated:{_countOfCandyCoated}";
        }

        internal string ShowChocolateBarCount()
        {
            return $"Chocolate Bar(s):{_countOfChocolateBar}";
        }

        internal string ShowZagnutCount()
        {
            return $"Zagnut(s):{_countOfZagnut}";
        }

        internal void SaveNewCandy(char selectedCandyMenuOption)
        {
            var candyOption = int.Parse(selectedCandyMenuOption.ToString());

            var maybeCandyMaybeNot = (CandyType)selectedCandyMenuOption;
            var forRealTheCandyThisTime = (CandyType)candyOption;

            switch (forRealTheCandyThisTime)
            {
                case CandyType.TaffyNotLaffy:
                    ++_countOfTaffy;
                    break;
                case CandyType.CandyCoated:
                    ++_countOfCandyCoated;
                    break;
                case CandyType.CompressedSugar:
                    ++_countOfChocolateBar;
                    break;
                case CandyType.ZagnutStyle:
                    ++_countOfZagnut;
                    break;
                default:
                    break;
            }
        }

            internal void RemoveCandy(char selectedCandyMenuOption)
            {
                var candyOption = int.Parse(selectedCandyMenuOption.ToString());

                var maybeCandyMaybeNot = (CandyType)selectedCandyMenuOption;
                var forRealTheCandyThisTime = (CandyType)candyOption;

                switch (forRealTheCandyThisTime)
                {
                    case CandyType.TaffyNotLaffy:
                        --_countOfTaffy;
                        break;
                    case CandyType.CandyCoated:
                        --_countOfCandyCoated;
                        break;
                    case CandyType.CompressedSugar:
                        --_countOfChocolateBar;
                        break;
                    case CandyType.ZagnutStyle:
                        --_countOfZagnut;
                        break;
                    default:
                        break;
                }
            }
	}


}