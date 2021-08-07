#include <iostream>
#include "GAM345.h"

// Main function
int main(int argc, char** argv)
{
	GAM345::LinkedList<int> testLinkedList;

	for (int i = 0; i < 5; i++)
	{
		testLinkedList.insert(i, i);
	}

	for (int i = 0; i < 5; i++)
	{
		testLinkedList.insert(i, i);
	}

	testLinkedList.insert(22, 2);

	for (int i = 0; i < testLinkedList.size(); i++)
	{
		std::cout << testLinkedList[i] << " ";
	}

	std::cout << std::endl;

	testLinkedList.erase(0);

	for (int i = 0; i < testLinkedList.size(); i++)
	{
		std::cout << testLinkedList[i] << " ";
	}

	std::cout << std::endl;

	testLinkedList.erase(2);

	for (int i = 0; i < testLinkedList.size(); i++)
	{
		std::cout << testLinkedList[i] << " ";
	}

	testLinkedList.clear();

	return 0;
}