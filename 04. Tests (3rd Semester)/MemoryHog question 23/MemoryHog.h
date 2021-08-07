#pragma once
#include <vector>
#include "Player.h"

class MemoryHog
{
public:
	MemoryHog();
	~MemoryHog();

private:
	Player players[5];
	std::vector<Player*> b;
};