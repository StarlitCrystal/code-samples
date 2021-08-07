#pragma once
#include <string>

class ItemDefinition
{
public:
	enum ItemType { Consumable, Weapon, Tool, Key };
	std::string itemName;
	std::string itemDesc;
	int weight;
	int maxStack;

	ItemDefinition(ItemType itemClassification, std::string itemName, std::string itemDesc, int weight, int maxStack);

	void Print(std::string itemName, std::string itemDesc, int weight, int maxStack, int stack);

	~ItemDefinition();
private:
};