#include <iostream>
#include <fstream>
#include <algorithm>
#include <string>
#include <sstream>
#include <map>
#include <vector>

bool sortByValue(std::pair<std::string, int> a, std::pair<std::string, int> b)
{
	return (a.second < b.second);
};

int main()
{
	int wordCount = 0;
	std::string line;
	std::ifstream myfile("WarAndPeace.txt");

	if (!myfile.is_open())
	{
		std::cout << "Unable to open WarAndPeace.txt";
		return 0;
	}

	std::map<std::string, int> textDoc;

	while (myfile >> line)
	{
		for (int i = 0; i < line.size(); i++)
		{
			unsigned char temp = static_cast<unsigned char>(line[i]);

			if (ispunct(temp) || line[i] <= -1 || line[i] > 255 || isdigit(temp))
			{
				line.erase(i, 1);
				i--;
			}
		}

		if (line == "")
		{
			continue;
		}

		std::map<std::string, int>::iterator itr = textDoc.find(line);

		if (itr != textDoc.end())
		{
			textDoc[line]++;
		}

		else
		{
			textDoc[line] = 1;
		}

		wordCount++;
	}

	myfile.close();

	std::vector<std::pair<std::string, int>> vectorToSort;

	for (auto itr = textDoc.begin(); itr != textDoc.end(); itr++)
	{
		vectorToSort.push_back(make_pair(itr->first, itr->second));
	}

	std::sort(vectorToSort.begin(), vectorToSort.end(), sortByValue);

	std::cout << vectorToSort[(vectorToSort.size() / 2) - 0.5].first << " appears " << vectorToSort[(vectorToSort.size() / 2) - 0.5].second << " times";

	return 0;
}