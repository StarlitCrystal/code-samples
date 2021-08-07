#include <iostream>
#include "Inventory.h"

Inventory::Inventory()
{
	inventorySpace.reserve(99);
}

void Inventory::AddItem(ItemDefinition* itemDef)
{
	//Adds new 'ItemDefinition' to the inventory if the vector is empty
	if (inventorySpace.empty())
	{
		inventorySpace.push_back(new Item(itemDef, 1));
	}

	else
	{
		for (auto itr = inventorySpace.begin(); itr != inventorySpace.end(); itr++)
		{
			/*Adds one to current 'Item' stack if the name of the current 'Item' in the vector and 
			the 'Item' being added match, and if adding it would not exceed maximum allowed stack.*/
			if ((itemDef->itemName == (*itr)->itemDef->itemName && (*itr)->stack < (*itr)->itemDef->maxStack))
			{
				(*itr)->stack++;

				//If the additional stack increase would exceed the maximum stack allowed, sets current 'Item' stack to max, and adds a new 'Item' to the inventory.
				if ((*itr)->stack > (*itr)->itemDef->maxStack)
				{
					(*itr)->stack = (*itr)->itemDef->maxStack;
					inventorySpace.push_back(new Item(itemDef, 1));
					break;
				}
			}

			//If the names match, but adding the 'Item' would exceed the stack amount:
			else if (itemDef->itemName == (*itr)->itemDef->itemName && (*itr)->stack >= (*itr)->itemDef->maxStack)
			{
				//Checks if the vector ends, if it does it continues the loop to check the next 'Item'.
				if (itr + 1 != inventorySpace.end())
				{
					continue;
				}

				//If the vector does end, it adds a new 'Item'.
				else
				{
					inventorySpace.push_back(new Item(itemDef, 1));
					break;
				}
			}

			//If the names do not match:
			else
			{
				//Adds a new item should the vector end on the next iteration.
				if (itr + 1 == inventorySpace.end())
				{
					inventorySpace.push_back(new Item(itemDef, 1));
					break;
				}

				//Continues the loop if the vector isn't ending, but the names still don't match.
				else if (itemDef->itemName != (*itr)->itemDef->itemName)
				{
					continue;
				}
			}
		}
	}
}

//Much the same of the prior function.
void Inventory::AddItem(Item aItem)
{
	/*If the current 'Item' stack that is added to the inventory is larger than the maximum stack,
	runs the function with the 'Item' with the max stack, and recursively calls the function until
	the stack is depleted.*/
	while (aItem.stack > aItem.itemDef->maxStack)
	{
		int quantity = aItem.stack - aItem.itemDef->maxStack;
		aItem.stack = aItem.itemDef->maxStack;
		AddItem(aItem);
		aItem.stack = quantity;
	}

	if (inventorySpace.empty())
	{
		inventorySpace.push_back(new Item(aItem));
	}

	else
	{
		for (auto itr = inventorySpace.begin(); itr != inventorySpace.end(); itr++)
		{
			if ((aItem.itemDef->itemName == (*itr)->itemDef->itemName && (*itr)->stack < (*itr)->itemDef->maxStack))
			{
				(*itr)->stack = (*itr)->stack + aItem.stack;

				/*Sets the added new 'Item' object's stack to the remainder after the appropriate
				amount is added to the current 'Item' in the vector, then adds that new 'Item' to the vector.*/
				if ((*itr)->stack > (*itr)->itemDef->maxStack)
				{
					aItem.stack = (*itr)->stack - (*itr)->itemDef->maxStack;
					(*itr)->stack = (*itr)->itemDef->maxStack;
					inventorySpace.push_back(new Item(aItem));
					break;
				}
			}

			else if (aItem.itemDef->itemName == (*itr)->itemDef->itemName && (*itr)->stack >= (*itr)->itemDef->maxStack)
			{
				if (itr + 1 != inventorySpace.end())
				{
					continue;
				}

				else
				{
					inventorySpace.push_back(new Item(aItem));
					break;
				}
			}

			else
			{
				if (itr + 1 == inventorySpace.end())
				{
					inventorySpace.push_back(new Item(aItem));
					break;
				}

				else if (aItem.itemDef->itemName != (*itr)->itemDef->itemName)
				{
					continue;
				}
			}
		}
	}
}

//Same as the prior two functions.
void Inventory::AddItem(ItemDefinition* itemDef, int quantity)
{
	while (quantity > itemDef->maxStack)
	{
		AddItem(itemDef, itemDef->maxStack);
		quantity -= itemDef->maxStack;
	}

	if (inventorySpace.empty())
	{
		inventorySpace.push_back(new Item(itemDef, 1));
	}

	else
	{
		for (auto itr = inventorySpace.begin(); itr != inventorySpace.end(); itr++)
		{
			if ((itemDef->itemName == (*itr)->itemDef->itemName && (*itr)->stack < (*itr)->itemDef->maxStack))
			{
				(*itr)->stack = (*itr)->stack + quantity;

				if ((*itr)->stack > (*itr)->itemDef->maxStack)
				{
					quantity = (*itr)->itemDef->maxStack - quantity;
					(*itr)->stack = (*itr)->itemDef->maxStack;
					inventorySpace.push_back(new Item(itemDef, quantity));
					break;
				}
			}

			else if (itemDef->itemName == (*itr)->itemDef->itemName && (*itr)->stack >= (*itr)->itemDef->maxStack)
			{
				if (itr + 1 != inventorySpace.end())
				{
					continue;
				}

				else
				{
					inventorySpace.push_back(new Item(itemDef, quantity));
					break;
				}
			}

			else
			{
				if (itr + 1 == inventorySpace.end())
				{
					inventorySpace.push_back(new Item(itemDef, quantity));
					break;
				}

				else if (itemDef->itemName != (*itr)->itemDef->itemName)
				{
					continue;
				}
			}
		}
	}
}

//Basic copy of AddItem(Item aItem) function, but reverses the process to remove one from the stack, or remove the 'Item' from the vector if only one is in the stack.
void Inventory::RemoveItem(Item aItem)
{
	int i = 0;

	if (inventorySpace.empty())
	{
		std::cout << "Your inventory is empty." << std::endl;
	}

	else
	{
		for (auto itr = inventorySpace.begin(); itr != inventorySpace.end(); itr++)
		{
			if ((aItem.itemDef->itemName == (*itr)->itemDef->itemName && (*itr)->stack <= (*itr)->itemDef->maxStack))
			{
				(*itr)->stack--;

				if ((*itr)->stack <= 0)
				{
					inventorySpace.erase(inventorySpace.begin() + i);
					i = 0;
					break;
				}
			}

			else if (aItem.itemDef->itemName == (*itr)->itemDef->itemName && (*itr)->stack >= (*itr)->itemDef->maxStack)
			{
				if (itr + 1 != inventorySpace.end())
				{
					i++;
					continue;
				}

				else
				{
					inventorySpace.erase(inventorySpace.begin() + i);
					i = 0;
					break;
				}
			}

			else
			{
				if (itr + 1 == inventorySpace.end())
				{
					inventorySpace.erase(inventorySpace.begin() + i);
					i = 0;
					break;
				}

				else if (aItem.itemDef->itemName != (*itr)->itemDef->itemName)
				{
					i++;
					continue;
				}
			}
		}
	}
}

//Calls the 'Print' function in 'Item' class.
void Inventory::Print(Item aItem)
{
	aItem.Print(aItem.itemDef, aItem.stack);
}

Inventory::~Inventory()
{
}