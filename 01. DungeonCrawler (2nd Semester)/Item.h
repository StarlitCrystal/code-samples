#pragma once
#include "WorldObject.h"
#include "ItemDefinition.h"

class Item : public WorldObject
{
public:
	ItemDefinition* itemDef;
	int stack;

	Item(ItemDefinition* itemDef, int stack);
	Item* GetItem();

	void Print(ItemDefinition* itemDef, int stack);

	~Item();
private:
};