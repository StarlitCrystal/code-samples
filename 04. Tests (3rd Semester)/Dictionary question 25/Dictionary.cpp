#include "Dictionary.h"

Dictionary::Dictionary() :
	root(nullptr)
{
}

Dictionary::~Dictionary()
{
	ClearDictionary(root);

	root->left = nullptr;
	root->right = nullptr;
	root = nullptr;
}

void Dictionary::ClearDictionary(DNode* dictionaryNode)
{
	if (dictionaryNode == nullptr)
	{
		return;
	}

	ClearDictionary(dictionaryNode->left);

	ClearDictionary(dictionaryNode->right);

	delete dictionaryNode;
	dictionaryNode = nullptr;
}

void Dictionary::Set(int key, std::string value)
{
	if (root == nullptr)
	{
		root = new DNode;
		root->key = key;
		root->value = value;

		root->left = nullptr;
		root->right = nullptr;
	}

	else
	{
		DNode* temp = root;

		while (temp != nullptr)
		{
			if (key < temp->key)
			{
				if (temp->left == nullptr)
				{
					temp->left = new DNode;
					temp->left->key = key;
					temp->left->value = value;

					temp->left->left = nullptr;
					temp->left->right = nullptr;

					return;
				}

				else
				{
					temp = temp->left;
					continue;
				}
			}

			else
			{
				if (temp->right == nullptr)
				{
					temp->right = new DNode;
					temp->right->key = key;
					temp->right->value = value;

					temp->right->left = nullptr;
					temp->right->right = nullptr;

					return;
				}

				else
				{
					temp = temp->right;
					continue;
				}
			}
		}
	}
}

std::string Dictionary::Get(int key)
{
	DNode* temp = root;

	while (temp != nullptr)
	{
		if (temp->key == key)
		{
			return temp->value;
		}

		else if (temp->key < key)
		{
			temp = temp->right;
		}

		else
		{
			temp = temp->left;
		}
	}

	return "Not in dictionary or dictionary is empty";
}