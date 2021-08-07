#include "Queue.h"

Queue::Queue() :
	front(nullptr),
	back(nullptr)
{
}

Queue::~Queue()
{
	Node* temp = front;

	while (front != nullptr)
	{
		front = front->next;

		delete temp;
		temp = nullptr;

		temp = front;
	}

	delete temp;
	temp = nullptr;

	front = nullptr;
	back = nullptr;
}

void Queue::Enqueue(char letter)
{
	Node* temp = new Node;

	if (back == nullptr)
	{
		front = temp;
		back = temp;

		front->data = letter;
		back->data = letter;

		return;
	}

	else
	{
		back->next = temp;
		back->next->data = letter;

		back = back->next;
	}
}

char Queue::Dequeue()
{
	if (front == nullptr)
	{
		return '\0';
	}

	else
	{
		Node* temp = front;
		front = front->next;

		if (front == nullptr)
		{
			delete back;
			back = nullptr;
		}

		delete temp;
		temp = nullptr;
	}
}