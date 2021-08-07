#pragma once
#include <string>

class Dictionary
{
public:

	Dictionary();
	~Dictionary();

	void Set(int key, std::string value);
	std::string Get(int key);

private:

	struct DNode
	{
		int key;
		std::string value;

		DNode* left;
		DNode* right;
	};

	DNode* root;

	void ClearDictionary(DNode* dictionaryNode);
};