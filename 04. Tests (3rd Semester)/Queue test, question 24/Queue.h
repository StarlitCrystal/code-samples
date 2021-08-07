#pragma once
#include <list>
#include "Node.h"

class Queue
{
public:
	
	Queue();
	~Queue();

	void Enqueue(char letter);
	char Dequeue();

private:

	Node* front;
	Node* back;
};