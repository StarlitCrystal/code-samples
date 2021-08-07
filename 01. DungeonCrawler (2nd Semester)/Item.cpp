#include <iostream>
#include "Item.h"

Item::Item(ItemDefinition* itemDef, int stack) :
	itemDef(itemDef),
	stack(stack)
{
}

//Calls the 'Print' function in the 'ItemDefinition' class, and assigns the 'Stack' integer to the 'Item' stack number.
void Item::Print(ItemDefinition* itemDef, int stack)
{
	itemDef->Print(itemDef->itemName, itemDef->itemDesc, itemDef->weight, itemDef->maxStack, stack);
}

//Returns appropriate 'Item' if called upon.
Item* Item::GetItem()
{
	return this;
}

Item::~Item()
{
}