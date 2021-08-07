#pragma once
#include "Player.h"
#include "Room.h"

class CommandManager
{
public:
	enum Action {look, examine, inventory, north, east, south, west, ascend, descend, get, take, getall, takeall, drop, eat, shout, command, quit, again};

	CommandManager();

	static Action GetInput(std::list<WorldObject*> itemsInRoom, Player& p);

	~CommandManager();
private:
};