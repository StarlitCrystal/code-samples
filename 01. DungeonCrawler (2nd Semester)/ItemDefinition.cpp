#include <iostream>
#include "ItemDefinition.h"

ItemDefinition::ItemDefinition(ItemType itemClassification, std::string itemName, std::string itemDesc, int weight, int maxStack) :
	itemName(itemName),
	itemDesc(itemDesc),
	weight(weight),
	maxStack(maxStack)
{
}

//Prints details of all values in an 'ItemDefinition'.
void ItemDefinition::Print(std::string itemName, std::string itemDesc, int weight, int maxStack, int stack)
{
	std::cout << itemName << " - " << itemDesc << "\n" <<
		         "You carry: " << stack << "/" << maxStack << "\n" <<
		         "Total weight: " << weight * stack << " kilogram(s)" << "\n" << std::endl;
}

ItemDefinition::~ItemDefinition()
{
}