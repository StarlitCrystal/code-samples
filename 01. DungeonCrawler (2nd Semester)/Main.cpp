#include <iostream>
#include <string>
#include "Inventory.h"
#include "Item.h"
#include "ItemDefinition.h"
#include "Player.h"
#include "Room.h"
#include "WorldObject.h"
#include "CommandManager.h"

int main(int argc, char** argv)
{
	//Adding all rooms in the map
	Room start("Dense Forest");
	start.AddConnection(Room::Connection::North, new Room("Steep Cliff"));
	start.AddConnection(Room::Connection::South, new Room("Abandoned Ruins"));
	start.AddConnection(Room::Connection::East, new Room("Flowing River"));
	start.GetConnection(Room::Connection::East)->AddConnection(Room::Connection::East, new Room("Lake Shore"));
	
	//Adds a sword to the starting room
	ItemDefinition* sword = new ItemDefinition(ItemDefinition::Weapon, "Longsword", "A long, sharpened blade with a dull, unadorned hilt.", 1, 1);
	Item* longsword = new Item(sword, 1);
	start.AddObject(longsword);

	//Adds a player to the start room, with a bag for an inventory
	Inventory* bag = new Inventory;
	Player p("Kyle", 1, bag);
	start.AddObject(&p);

	//Adds a potion to the player's bag
	ItemDefinition* potion = new ItemDefinition(ItemDefinition::Consumable, "Health Potion", "A red potion that restores health when consumed.", 1, 10);
	Item* healthPotion = new Item(potion, 1);
	p.GetInventory()->AddItem(*healthPotion);

	//Start text
	std::cout << "You are in a dense forest. You are holding a bag with which you can carry items. " << "\n"
		<< "You see several paths laid out before you: " << "\n"
		<< "a river to the East, a clearing to the North, and road to the South." << "\n"
		<< "(Type 'Commands' for a list of commands)" << std::endl;
	std::cout << std::endl;

	//Main Game Loop
	while (true)
	{
		//Retrieves room player is currently in and prints it to the console
		Room* currentRoom = p.GetRoom();
		std::cout << "- ";
		currentRoom->PrintRoom();
		std::cout << " -" << "\n" << std::endl;

		//Retrieves the item list in the room the player is in
		std::list<WorldObject*> itemsInRoom = currentRoom->GetItems();
		//Handles the user's input
		CommandManager::Action playerAction = CommandManager::GetInput(itemsInRoom, p);

		//All cases for the enum within CommandManager
		switch (playerAction)
		{
			int value;

			//Prints the name of the room the player is in to the console
		case::CommandManager::look:
			std::cout << "You are in the ";
			currentRoom->PrintRoom();
			std::cout << "." << "\n" << "\n" << std::endl;
			break;

			//Prints the items that are in the room the player is in (if any) to the console
		case::CommandManager::examine: 

			//Checks if there are more items in the room besides the player object
			if (itemsInRoom.size() == 1)
			{
				std::cout << "There are no items in the area." << std::endl;
				std::cout << "\n" << std::endl;
				break;
			}

			for (auto itr = itemsInRoom.begin(); itr != itemsInRoom.end(); itr++)
			{
				//Ignores the player as an item
				if ((*itr) == &p)
				{
					continue;
				}

				//Iterates through the items in the room and prints their name and stack size
				else if ((*itr) == (*itr)->GetItem())
				{
					if ((*itr)->GetItem()->stack == 0)
					{
						std::cout << "There is " << 1 << " " << (*itr)->GetItem()->itemDef->itemName << " on the ground." << std::endl;
					}

					else
					{
						std::cout << "There is/are " << (*itr)->GetItem()->stack << " " << (*itr)->GetItem()->itemDef->itemName << "(s) on the ground." << std::endl;
					}
				}
			}
			std::cout << "\n" << std::endl;
			break;

			//Prints the player's bag to the console
		case::CommandManager::inventory:

			if (p.GetInventory()->inventorySpace.size() == 0)
			{
				std::cout << "Your bag is empty." << "\n" << std::endl;
			}

			//Loops through the items within the player's inventory to print their info
			for (int i = 0; i < p.GetInventory()->inventorySpace.size(); i++)
			{
				p.GetInventory()->Print(*p.GetInventory()->inventorySpace[i]);
			}
			std::cout << std::endl;
			break;

			//Moves player in specified direction, should that direction exist in the room the player is in
			//Moves North
		case::CommandManager::north:

			p.aRoom->MoveObject(Room::Connection::North, &p);
			std::cout << std::endl;
			break;

			//Moves East
		case::CommandManager::east:

			p.aRoom->MoveObject(Room::Connection::East, &p);
			std::cout << std::endl;
			break;

			//Moves South
		case::CommandManager::south:

			p.aRoom->MoveObject(Room::Connection::South, &p);
			std::cout << std::endl;
			break;

			//Moves West
		case::CommandManager::west:

			p.aRoom->MoveObject(Room::Connection::West, &p);
			std::cout << std::endl;
			break;

			//Moves upward
		case::CommandManager::ascend:

			p.aRoom->MoveObject(Room::Connection::Ascend, &p);
			std::cout << std::endl;
			break;

			//Moves downwards
		case::CommandManager::descend:

			p.aRoom->MoveObject(Room::Connection::Descend, &p);
			std::cout << std::endl;
			break;

			//Grabs the specified item from the room the player is in, removes it from the room, and adds it to the player's inventory
		case::CommandManager::get:

			//Checks if there are more items in the room besides the player object
			if (itemsInRoom.size() == 1)
			{
				std::cout << "There is nothing here to take." << std::endl;
				std::cout << "\n" << std::endl;
				break;
			}

			for (auto itr = itemsInRoom.begin(); itr != itemsInRoom.end(); itr++)
			{
				int index = std::distance(itemsInRoom.begin(), itr);

				//Ignores the player as an item
				if ((*itr) == &p)
				{
					continue;
				}

				//Prints the items in the room
				else
				{
					std::cout << index << ". " << (*itr)->GetItem()->itemDef->itemName << "." << "\n" << std::endl;
				}
			}

			//Number input from player is required to pick up an item
			std::cin >> value;
			std::cout << std::endl;

			for (auto itr = itemsInRoom.begin(); itr != itemsInRoom.end(); itr++)
			{
				int index = std::distance(itemsInRoom.begin(), itr);

				//Ignores the player as an item
				if ((*itr) == &p)
				{
					continue;
				}

				//If the value the player types is the same as the index number of the item
				else if (value == index)
				{
					//Adds the item to the player's inventory, and prints out what was picked up
					p.GetInventory()->AddItem((*itr)->GetItem()->itemDef);
					std::cout << "You picked up the " << (*itr)->GetItem()->itemDef->itemName << "." << std::endl;
					
					//Removes the object from the room the player is in
					currentRoom->RemoveObject((*itr)->GetItem());
					std::cout << "\n" << std::endl;
					delete (*itr);
					std::cin.ignore();
					break;
				}

				//If the input from the player is not the same as the index, continues to the next item
				else
				{
					continue;
				}
			}
			std::cin.clear();
			break;

			//Removes the specified item from the player's inventory, and adds it to the list within the current room
		case::CommandManager::drop:

			//If player's bag is empty, prints that out to the player
			if (p.GetInventory()->inventorySpace.size() == 0)
			{
				std::cout << "You have no items." << std::endl;
				std::cout << "\n" << std::endl;
			}

			//Prints out the player's inventory
			for (int i = 0; i < p.GetInventory()->inventorySpace.size(); i++)
			{
				std::cout << i + 1 << ". ";
				p.GetInventory()->Print(*p.GetInventory()->inventorySpace[i]);

			}

			//Player input to select an item to drop
			std::cin >> value;
			std::cout << std::endl;

			for (int i = 0; i < p.GetInventory()->inventorySpace.size(); i++)
			{
				if ((value - 1) == i)
				{
					//Adds the specified object to the room the player is in, prints out what was dropped, and the room name
					p.aRoom->AddObject(p.GetInventory()->inventorySpace[i]);
					std::cout << p.GetInventory()->inventorySpace[i]->itemDef->itemName << " dropped in the ";
					currentRoom->PrintRoom();
					
					//Removes the specified item from the player's inventory
					p.GetInventory()->RemoveItem(*p.GetInventory()->inventorySpace[i]);
					std::cout << "\n" << "\n" << std::endl;
					std::cin.ignore();
					break;
				}

				//If the value (minus 1) the player types is less than i, and i does not equal the inventory vector's size, continues to the next item
				else if ((value - 1) > i && i != p.GetInventory()->inventorySpace.size())
				{
					continue;
				}
			}
			std::cin.clear();
			break;

			//Gets every item in a room and adds it to the player's inventory, as well as removes them all from the room's list
		case::CommandManager::getall:

			//If there is only the player in the room, nothing is picked up
			if (itemsInRoom.size() == 1)
			{
				std::cout << "There is nothing here to take." << std::endl;
				std::cout << "\n" << std::endl;
				break;
			}

			//Iterates through the items within the room
			for (auto itr = itemsInRoom.begin(); itr != itemsInRoom.end(); itr++)
			{
				//Ignores the player as an item
				if ((*itr) == &p)
				{
					continue;
				}

				//Adds an item to the player's inventory, removes it from the room the player is in, and continues till all items are gone from the room
				else
				{
					p.GetInventory()->AddItem((*itr)->GetItem()->itemDef);
					currentRoom->RemoveObject((*itr)->GetItem());
					delete (*itr);
				}
			}
			std::cout << std::endl;
			break;

			//Consumes ANY specified item, removing it from the player's inventory
		case::CommandManager::eat:

			//If the bag is empty, prints out as much
			if (p.GetInventory()->inventorySpace.size() == 0)
			{
				std::cout << "Your bag is empty." << "\n" << std::endl;
				break;
			}

			//Prints out player's inventory
			for (int i = 0; i < p.GetInventory()->inventorySpace.size(); i++)
			{
				std::cout << i + 1 << ". ";
				p.GetInventory()->Print(*p.GetInventory()->inventorySpace[i]);
			}

			//Player's input to select an item
			std::cin >> value;
			std::cout << std::endl;

			for (int i = 0; i < p.GetInventory()->inventorySpace.size(); i++)
			{
				//"Consumes" the item from the player's inventory, printing out what the player ate, and removes it from their bag
				if ((value - 1) == i)
				{
					std::cout << "You consume the " << p.GetInventory()->inventorySpace[i]->itemDef->itemName << std::endl;
					p.GetInventory()->RemoveItem(*p.GetInventory()->inventorySpace[i]);
					std::cin.ignore();
					break;
				}

				//If the value (minus 1) the player types is less than i, and i does not equal the inventory vector's size, continues to the next item
				else if ((value - 1) > i && i != p.GetInventory()->inventorySpace.size())
				{
					continue;
				}
			}
			std::cin.clear();
			std::cout << "\n" << std::endl;
			break;

			//The player shouts, but no one is there to heed their call
		case::CommandManager::shout:

			std::cout << std::endl;
			break;

			//Prints all available commands to player
		case::CommandManager::command:

			std::cout << std::endl;
			break;

			//Exits the game
		case::CommandManager::quit:

			return 0;

			//Handles invalid player input
		case::CommandManager::again:

			std::cout << std::endl;
			break;
		}
	}
}