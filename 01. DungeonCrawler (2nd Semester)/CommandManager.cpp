#include <iostream>
#include <string>
#include "CommandManager.h"

CommandManager::CommandManager()
{
}

//Handles all player input to console, returns enum associated with what player types in, and handles exceptions related to invalid input.
CommandManager::Action CommandManager::GetInput(std::list<WorldObject*> itemsInRoom, Player& p)
{
	std::string value;
	std::getline(std::cin, value);
	
	//Converts all characters in player's input string to lower case
	for (int i = 0; i < value.size(); i++)
	{
		char c = value[i];
		c = std::tolower(c);
		value[i] = c;
	}

	std::cout << std::endl;

	//Prints the name of the room the player is in to the console
	if (value == "look" || value == "look around")
	{
		std::cout << "You look around at your surroundings." << "\n" << std::endl;
		return look;
	}

	//Prints the items that are in the room the player is in (if any) to the console
	if (value == "examine" || value == "inspect" || value == "Examine" || value == "Inspect")
	{
		std::cout << "You examine the area." << "\n" << std::endl;
		return examine;
	}

	//Prints the player's bag to the console
	if (value == "inventory" || value == "view inventory" || value == "look in inventory" || value == "bag" || value == "view bag" || value == "look in bag")
	{
		std::cout << "Inventory: " << "\n" << std::endl;
		return inventory;
	}

	//Moves player in specified direction, should that direction exist in the room the player is in
	if (value == "north" || value == "n" || value == "go north" || value == "go n")
	{
		std::cout << "You head to the North." << "\n" << std::endl;
		return north;
	}

	if (value == "east" || value == "e" || value == "go east" || value == "go e")
	{
		std::cout << "You head to the East." << "\n" << std::endl;
		return east;
	}

	if (value == "south" || value == "s" || value == "go south" || value == "go s")
	{
		std::cout << "You head to the South." << "\n" << std::endl;
		return south;
	}

	if (value == "west" || value == "w" || value == "go west" || value == "go w")
	{
		std::cout << "You head to the West." << "\n" << std::endl;
		return west;
	}

	if (value == "ascend" || value == "go up")
	{
		std::cout << "You go up." << "\n" << std::endl;
		return ascend;
	}

	if (value == "descend" || value == "go down")
	{
		std::cout << "You go down." << "\n" << std::endl;
		return descend;
	}

	//Grabs the specified item from the room the player is in, removes it from the room, and adds it to the player's inventory
	if (value == "get" || value == "take" || value == "grab")
	{
		std::cout << "Get what?" << "\n" << std::endl;
		return get;
	}

	//Removes the specified item from the player's inventory, and adds it to the list within the current room
	if (value == "drop" || value == "leave" || value == "toss")
	{
		std::cout << "Drop what?" << "\n" << std::endl;
		return drop;
	}

	//Gets every item in a room and adds it to the player's inventory, as well as removes them all from the room's list
	if (value == "get all" || value == "take all" || value == "grab all")
	{
		std::cout << "You take everything in the area." << "\n" << std::endl;
		return getall;
	}

	//Consumes ANY specified item, removing it from the player's inventory
	if (value == "eat" || value == "consume")
	{
		std::cout << "What do you wish to consume?" << "\n" << std::endl;
		return eat;
	}

	//The player shouts, but no one is there to heed their call
	if (value == "shout" || value == "yell")
	{
		std::cout << "You shout into the air, but there is no response." << "\n" << std::endl;
		return shout;
	}

	//Prints all available commands to player
	if (value == "command" || value == "commands")
	{
		std::cout << "Command List:" << "\n" <<
			"Look, Look Around - Prints current room" << "\n" <<
			"Examine, Inspect - Prints current room's item list" << "\n" <<
			"Inventory, Bag, View inventory, View bag, Look in inventory, look in bag - Shows player's inventory" << "\n" <<
			"North (N), East (E), South (S), West (W), Ascend, Descend, Go 'direction', Go up, Go down - Travels to room in specified direction, if it exists" << "\n" <<
			"Get, Take, Grab - Takes specified item from current room, and puts in it player's inventory" << "\n" <<
			"Drop, Leave, Toss - Drops specified item from player's inventory, and puts it in the current room" << "\n" <<
			"Get all, Take all, Grab all, - Takes all items from current room, and places them in player's inventory" << "\n" <<
			"Eat, Consume - Consumes specified item (even those that are inedible)" << "\n" <<
			"Shout, Yell - Player shouts into the void" << "\n" <<
			"Quit, Exit - Exits the game" << "\n" << std::endl;
		return command;
	}

	//Exits the game
	if (value == "quit" || value == "exit")
	{
		std::cout << "Goodbye." << std::endl;
		return quit;
	}

	//Handles invalid player input
	else
	{
		std::cout << "I did not understand your input." << "\n" << std::endl;
		return again;
	}
}

CommandManager::~CommandManager()
{
}