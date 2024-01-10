namespace TextRPG1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// 플레이어 초기화
			int playerLevel = 1;
			string playerName = "Chad";
			string playerClass = "전사";
			int playerAttack = 10;
			int playerDefense = 5;
			int playerHealth = 100;
			int playerGold = 1500;

			List<int> shopItemsPurchased = new List<int>();

			// 상점 초기화

			List<string> shopItems = new List<string>
			{
			"수련자 갑옷|1000|0|5|수련에 도움을 주는 갑옷입니다.",
			"무쇠갑옷|2000|0|9|무쇠로 만들어져 튼튼한 갑옷입니다.",
			"스파르타의 갑옷|3500|0|15|스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
			"낡은 검|600|2|0|쉽게 볼 수 있는 낡은 검입니다.",
			"청동 도끼|1500|5|0|어디선가 사용됐던 거 같은 도끼입니다.",
			"스파르타의 창|2500|7|0|스파르타의 전사들이 사용했다는 전설의 창입니다."
			};

			// 플레이어 인벤토리 초기화
			List<string> playerInventory = new List<string>();
			int equippedItemIndex = -1;

			Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
			Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

			while (true)
			{
				Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점\n0. 종료");
				Console.Write("원하시는 행동을 입력해주세요: ");
				string input = Console.ReadLine();

				if (input == "0")
				{
					Console.WriteLine("게임을 종료합니다. 다음에 또 오세요!");
					break;
				}

				ProcessInput(input);
			}

			void ProcessInput(string input)
			{
				switch (input)
				{
					case "1":
						ShowPlayerStatus();
						break;
					case "2":
						ShowInventory();
						break;
					case "3":
						ShowShop();
						break;
					default:
						Console.WriteLine("잘못된 입력입니다.");
						break;
				}
			}

			void ShowPlayerStatus()
			{
				Console.WriteLine("\n상태 보기");
				Console.WriteLine($"Lv. {playerLevel}\n{playerName} ( {playerClass} )");

				int modifiedAttack = playerAttack;
				int modifiedDefense = playerDefense;

				foreach (string equippedItem in playerInventory)
				{
					string[] itemDetails = equippedItem.Split('|');
					modifiedAttack += int.Parse(itemDetails[2]);
					modifiedDefense += int.Parse(itemDetails[3]);
				}

				Console.WriteLine($"공격력 : {modifiedAttack} (+{playerAttack})");
				Console.WriteLine($"방어력 : {modifiedDefense} (+{playerDefense})");
				Console.WriteLine($"체 력 : {playerHealth}");
				Console.WriteLine($"Gold : {playerGold} G");
				Console.WriteLine("0. 나가기");

				Console.Write("\n원하시는 행동을 입력해주세요: ");
				string userInput = Console.ReadLine();

				if (userInput == "0")
				{
					Console.WriteLine("나가기");
					return;  // 메뉴로 돌아가기
				}
				else
				{
					Console.WriteLine("잘못된 입력입니다.");
				}
			}

			void ShowInventory()
			{
				Console.WriteLine("\n인벤토리");
				Console.WriteLine("[아이템 목록]");

				for (int i = 0; i < playerInventory.Count; i++)
				{
					string equippedIndicator = (i == equippedItemIndex) ? "[E]" : "";
					string[] itemDetails = playerInventory[i].Split('|');
					Console.WriteLine($"{i + 1}. {equippedIndicator}{itemDetails[0]} | {itemDetails[4]}");
				}

				Console.WriteLine("0. 나가기");

				Console.Write("\n원하시는 행동을 입력해주세요: ");
				string userInput = Console.ReadLine();

				if (userInput == "0")
				{
					Console.WriteLine("나가기");
				}
				else if (int.TryParse(userInput, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= playerInventory.Count)
				{
					ManageEquipment(selectedIndex - 1);
				}
				else
				{
					Console.WriteLine("잘못된 입력입니다.");
				}
			}

			void ManageEquipment(int selectedIndex)
			{
				string selectedItem = playerInventory[selectedIndex];

				if (equippedItemIndex == selectedIndex)
				{
					equippedItemIndex = -1;
					Console.WriteLine($"{selectedItem.Split('|')[0]}을(를) 해제했습니다.");
				}
				else
				{
					equippedItemIndex = selectedIndex;
					Console.WriteLine($"{selectedItem.Split('|')[0]}을(를) 장착했습니다.");
				}
			}

			void ShowShop()
			{
				Console.WriteLine("\n상점");
				Console.WriteLine($"[보유 골드]\n플레이어: {playerGold} G");
				Console.WriteLine("[아이템 목록]");

				for (int i = 0; i < shopItems.Count; i++)
				{
					string[] itemDetails = shopItems[i].Split('|');
					string purchaseStatus = (shopItemsPurchased.Contains(i)) ? "구매완료" : $"{itemDetails[1]} G";
					Console.WriteLine($"- {i + 1} {itemDetails[0]} | {itemDetails[4]} | {purchaseStatus}");
				}

				Console.WriteLine("1. 아이템 구매\n0. 나가기");

				Console.Write("\n원하시는 행동을 입력해주세요: ");
				string userInput = Console.ReadLine();

				switch (userInput)
				{
					case "0":
						Console.WriteLine("나가기");
						break;
					case "1":
						BuyItem();
						break;
					default:
						Console.WriteLine("잘못된 입력입니다.");
						break;
				}
			}

			void BuyItem()
			{
				Console.WriteLine("\n아이템 구매");

				Console.WriteLine("[아이템 목록]");

				for (int i = 0; i < shopItems.Count; i++)
				{
					string[] itemDetails = shopItems[i].Split('|');
					string purchaseStatus = (shopItemsPurchased.Contains(i)) ? "구매완료" : $"{itemDetails[1]} G";
					Console.WriteLine($"{i + 1} {itemDetails[0]} | {itemDetails[4]} | {purchaseStatus}");
				}

				Console.WriteLine("0. 나가기");

				Console.Write("\n구매할 아이템 번호를 입력해주세요: ");
				string userInput = Console.ReadLine();

				if (userInput == "0")
				{
					Console.WriteLine("나가기");
				}
				else if (int.TryParse(userInput, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= shopItems.Count)
				{
					ProcessPurchase(selectedIndex - 1);
				}
				else
				{
					Console.WriteLine("잘못된 입력입니다.");
				}
			}

			void ProcessPurchase(int selectedIndex)
			{
				string[] selectedShopItemDetails = shopItems[selectedIndex].Split('|');

				if (shopItemsPurchased.Contains(selectedIndex))
				{
					Console.WriteLine("이미 구매한 아이템입니다.");
				}
				else
				{
					int itemPrice = int.Parse(selectedShopItemDetails[1]);

					if (playerGold >= itemPrice)
					{
						playerGold -= itemPrice;
						playerInventory.Add(shopItems[selectedIndex]);
						shopItemsPurchased.Add(selectedIndex);
						Console.WriteLine($"{selectedShopItemDetails[0]}을(를) 구매했습니다.");
						UpdatePlayerStats();
					}
					else
					{
						Console.WriteLine("Gold가 부족합니다.");
					}
				}
			}

			void UpdatePlayerStats()
			{
				// 장착된 아이템에 따라 플레이어 스탯을 업데이트
				playerAttack = 10;
				playerDefense = 5;

				foreach (string equippedItem in playerInventory)
				{
					string[] itemDetails = equippedItem.Split('|');
					playerAttack += int.Parse(itemDetails[2]);
					playerDefense += int.Parse(itemDetails[3]);
				}
			}
		}

		class Item
		{
			public string Name { get; set; }
			public int Price { get; set; }
			public int AttackBonus { get; set; }
			public int DefenseBonus { get; set; }
			public string Description { get; set; }

			public Item(string name, int price, int attackBonus, int defenseBonus, string description)
			{
				Name = name;
				Price = price;
				AttackBonus = attackBonus;
				DefenseBonus = defenseBonus;
				Description = description;
			}
		}

	}
}