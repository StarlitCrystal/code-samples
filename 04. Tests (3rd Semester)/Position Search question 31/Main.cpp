#include <vector>
#include <list>
#include <iostream>

struct Vector3
{
	float x;
	float y;
	float z;
};

struct GameLocation
{
	Vector3 position;
	std::vector<GameLocation*> connections;
};

std::vector<GameLocation> locations;

//To compare a Vector3 with another Vector3
bool operator==(const Vector3& other1, const Vector3& other2)
{
	return (other1.x == other2.x && other1.y == other2.y && other1.z == other2.z);
}

bool operator!=(const Vector3& other1, const Vector3& other2)
{
	if (other1.x != other2.x || other1.y != other2.y || other1.z != other2.z)
	{
		return true;
	}

	return false;
}

bool canMoveBetween(GameLocation* start, GameLocation* end)
{
	std::vector<GameLocation*> frontier;
	std::vector<GameLocation*> visited;
	frontier.push_back(start);
	visited.push_back(start);

	for (int i = 0; i < frontier.size(); i++)
	{
		if (frontier[i]->position == end->position)
		{
			return true;
		}

		for (int j = 0; j < visited.size(); j++)
		{
			if (frontier[i]->position == visited[j]->position)
			{
				break;
			}

			else if (frontier[i]->position != visited[j]->position && (j + 1) == visited.size())
			{
				visited.push_back(frontier[i]);
				break;
			}
		}

		for (int p = 0; p < frontier[i]->connections.size(); p++)
		{
			for (int k = 0; k < visited.size(); k++)
			{
				if (visited[k]->position == frontier[i]->connections[p]->position)
				{
					break;
				}

				else if (visited[k]->position != frontier[i]->connections[p]->position && (k + 1) == visited.size())
				{
					frontier.push_back(frontier[i]->connections[p]);
					break;
				}
			}
		}
	}

	return false;
}

std::list<GameLocation> FlyToLocation(GameLocation* start, GameLocation* goal)
{
	std::vector<GameLocation*> frontier;
	std::vector<GameLocation*> visited;
	frontier.push_back(start);
	visited.push_back(start);

	for (int i = 0; i < frontier.size(); i++)
	{
		if (frontier[i]->position == goal->position)
		{
			break;
		}

		for (int j = 0; j < visited.size(); j++)
		{
			if (frontier[i]->position == visited[j]->position)
			{
				break;
			}

			else if (frontier[i]->position != visited[j]->position && (j + 1) == visited.size())
			{
				visited.push_back(frontier[i]);
				break;
			}
		}

		for (int p = 0; p < frontier[i]->connections.size(); p++)
		{
			for (int k = 0; k < visited.size(); k++)
			{
				if (visited[k]->position == frontier[i]->connections[p]->position)
				{
					break;
				}

				else if (visited[k]->position != frontier[i]->connections[p]->position && (k + 1) == visited.size())
				{
					frontier.push_back(frontier[i]->connections[p]);
					break;
				}
			}
		}

		if (frontier.size() == visited.size())
		{
			break;
		}
	}

	std::list<GameLocation> path;
	int i = 0;

	for (auto itr = visited.begin(); itr != visited.end(); itr++)
	{
		i++;
		path.push_back(*(*itr));

		if (i == 3)
		{
			return path;
		}
	}
}

int main(int argc, char** argv)
{
	GameLocation* A = new GameLocation;
	A->position.x = 1;
	A->position.y = 1;
	A->position.z = 1;
	
	GameLocation* B = new GameLocation;
	B->position.x = 2;
	B->position.y = 2;
	B->position.z = 2;

	GameLocation* C = new GameLocation;
	C->position.x = 3;
	C->position.y = 3;
	C->position.z = 3;

	GameLocation* D = new GameLocation;
	D->position.x = 4;
	D->position.y = 4;
	D->position.z = 4;

	GameLocation* E = new GameLocation;
	E->position.x = 5;
	E->position.y = 5;
	E->position.z = 5;

	GameLocation* F = new GameLocation;
	F->position.x = 6;
	F->position.y = 6;
	F->position.z = 6;

	GameLocation* G = new GameLocation;
	G->position.x = 7;
	G->position.y = 7;
	G->position.z = 7;

	A->connections.push_back(B);

	B->connections.push_back(A);
	B->connections.push_back(C);
	B->connections.push_back(E);

	C->connections.push_back(B);
	C->connections.push_back(D);

	D->connections.push_back(C);
	D->connections.push_back(F);

	E->connections.push_back(B);

	F->connections.push_back(D);

	locations.push_back(*A);
	locations.push_back(*B);
	locations.push_back(*C);
	locations.push_back(*D);
	locations.push_back(*E);
	locations.push_back(*F);
	locations.push_back(*G);

	std::cout << canMoveBetween(A, C) << std::endl;
	std::cout << canMoveBetween(A, E) << std::endl;
	std::cout << canMoveBetween(A, F) << std::endl;
	std::cout << canMoveBetween(A, G) << std::endl;

	FlyToLocation(A, F);

	return 0;
}