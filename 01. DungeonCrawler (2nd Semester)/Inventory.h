#pragma once
#include <vector>
#include "Item.h"
#include "ItemDefinition.h"

class Inventory
{
public:
	std::vector<Item*> inventorySpace;

	Inventory();

	void AddItem(ItemDefinition* itemDef);
	void AddItem(Item aItem);
	void AddItem(ItemDefinition* itemDef, int quantity);
	void RemoveItem(Item aItem);
	void Print(Item aItem);

	~Inventory();
private:
};