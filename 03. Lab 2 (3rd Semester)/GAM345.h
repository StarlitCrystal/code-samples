#pragma once

namespace GAM345
{
	template <class T>

	class LinkedListNode
	{
	public:
		T data;
		LinkedListNode* nextNode;

		LinkedListNode() :
			nextNode(nullptr)
		{
		}
	};

	template <class T>

	class LinkedList
	{
	public:

		//Initializes a linked list with a pointer to the first node, and a size of 0
		LinkedList() :
			firstNode(nullptr),
			Size(0)
		{
		}

		//Returns size for purpose of seeing how many elements are within the linked list
		int size()
		{
			return Size;
		}

		//Standard operator overload function
		T& operator[](int position)
		{
			LinkedListNode<T>* temp = firstNode;

			for (int i = 0; i < position; i++)
			{
				temp = temp->nextNode;
			}

			return temp->data;
		}

		//Inserts a new element into the specified position
		void insert(T element, int position)
		{
			//Increments size by one
			Size++;

			//Creates a temp LinkedListNode pointer set to a new pointer, sets the LinkedListNode's data to the element
			LinkedListNode<T>* newNode = reinterpret_cast<LinkedListNode<T>*>(new char[sizeof(LinkedListNode<T>)]);
			newNode->data = element;
			newNode->nextNode = nullptr;

			//If the first node in LinkedList is null, sets the first node equal to the new node and sets temp equal to a null pointer
			if (firstNode == nullptr)
			{
				firstNode = newNode;
			}

			//If the first node pointer is not null:
			else
			{
				//Creates a temp node equal to the first node
				LinkedListNode<T>* temp = firstNode;

				//Loops through the list
				for (int i = 0; i < position; i++)
				{
					//If next node is a null pointer, sets the next node equal to the new node and breaks
					if (temp->nextNode == nullptr)
					{
						temp->nextNode = newNode;
						break;
					}

					//If the next node is not a null pointer and i does not equal the position minus 1, sets temp equal to the next node and continues
					else if (temp->nextNode != nullptr && i != (position - 1))
					{
						temp = temp->nextNode;
					}

					//If i is equal to the position minus 1:
					else if (i == (position - 1))
					{
						//If the next node is not a null pointer, sets the new node's next node pointer to temp's next node, and sets temp's next node to the new node
						if (temp->nextNode != nullptr)
						{
							newNode->nextNode = temp->nextNode;
							temp->nextNode = newNode;
						}

						//If the next node is a null pointer, sets the next node equal to the new node and breaks
						else
						{
							temp->nextNode = newNode;
						}
					}
				}
			}
		}

		//Erases an element at the specified position within the linked list
		void erase(int position)
		{
			//Creates a temp LinkedListNode equal to the first node
			LinkedListNode<T>* temp = firstNode;

			//If the position erased is the first position, sets the first node to equal the next node
			if (position == 0)
			{
				firstNode->data = temp->nextNode->data;
				firstNode = temp->nextNode;

				delete temp;
				temp = nullptr;

				Size--;
			}

			for (int i = 0; i < position; i++)
			{
				if (temp->nextNode == nullptr)
				{
					//Does nothing
					break;
				}

				//If the next node is not a null pointer and i does not equal the position minus 1, sets temp equal to the next node and continues
				else if (temp->nextNode != nullptr && i != (position - 1))
				{
					temp = temp->nextNode;
				}

				//If i equals position minus 1:
				else if (i == (position - 1))
				{

					//Ensures temp and temp's next pointer does not equal a null pointer first
					if (temp != nullptr && temp->nextNode != nullptr && temp->nextNode->nextNode != nullptr)
					{
						//Creates a temporary holder for temp's next node
						LinkedListNode<T>* tempNext = temp->nextNode->nextNode;

						//Deletes temp's next node and sets it to a null pointer
						delete temp->nextNode;
						temp->nextNode = nullptr;

						//Sets temp's next node equal to what it's next node's next node was pointing to
						temp->nextNode = tempNext;

						//Deallocates tempNext
						tempNext = nullptr;
						delete tempNext;

						Size--;
					}

					//If the node past the one marked for deletion is null, deletes just what is marked for erasure without setting any other pointers
					else if (temp->nextNode->nextNode == nullptr)
					{
						delete temp->nextNode;
						temp->nextNode = nullptr;

						Size--;
					}
				}
			}
		}

		//Clears the linked list of all elements, and sets all pointers to null before deleting them
		void clear()
		{
			//Creates a temp LinkedListNode equal to the first node
			LinkedListNode<T>* temp = firstNode;

			for (int i = 0; i < Size; i++)
			{
				//Deletes the first node and sets it to a null pointer
				firstNode = nullptr;
				delete firstNode;

				//If temp's next node does not equal a null pointer
				if (temp->nextNode != nullptr)
				{
					//Sets temp equal to the next node in the set, and sets the new first node to the next node
					temp = temp->nextNode;
					firstNode = temp->nextNode;
				}

				else
				{
					//Deallocates temp
					delete temp;
					temp = nullptr;
				}
			}

			//Sets size to 0, and deallocates temp
			Size = 0;
			delete temp;
			temp = nullptr;
		}
	
		//Simply calls clear function
		~LinkedList()
		{
			if (firstNode != nullptr)
			{
				clear();
			}
		}

	private:
		LinkedListNode<T>* firstNode;
		int Size;
	};
}