#include <iostream>
#include "Queue.h"


int main(int argc, char** argv)
{
	Queue queue;

	queue.Enqueue('1');
	queue.Enqueue('2');
	queue.Enqueue('3');
	queue.Dequeue();
	queue.Enqueue('4');
	queue.Enqueue('5');
	queue.Enqueue('6');
	queue.Dequeue();

	return 0;
}