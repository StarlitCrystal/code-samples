#include "MemoryHog.h"

MemoryHog::MemoryHog()
{
	for (int i = 0; i < 5; i++)
	{
		players[i] = *(new Player());
	}

	b.push_back(new Player());
	b.push_back(new Player());
}

MemoryHog::~MemoryHog()
{
	for (int i = 0; i < 5; i++)
	{
		delete &players[i];
	}

	delete[] players;

	b.clear();

	b.shrink_to_fit();
}