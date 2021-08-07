#include <iostream>
#include "Dictionary.h"

int main(int argc, char** argv)
{
	Dictionary book;

	book.Set(2, "2");
	book.Set(3, "3");
	book.Set(4, "4");
	book.Set(5, "5");
	book.Set(1, "1");

	std::cout << book.Get(1) << " " << book.Get(2) << " " << book.Get(3) << " " << book.Get(4) << " " << book.Get(5) << " ";

	return 0;
}