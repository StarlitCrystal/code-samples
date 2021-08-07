#pragma once

namespace GAM345
{
	template <class T>

	class Vector
	{
	public:

		//Initializes a vector with a block of memory the size of T elements multiplied by 5, size of 0, and capacity of 5
		Vector() :
			data(reinterpret_cast<T*>(new char[(sizeof(T) * 5)])),
			Size(0),
			Capacity(5)
		{
		}

		//Standard operator overload function
		T& operator[](int n)
		{
			return *(data + n);
		}

		//Returns size for purpose of seeing how many elements are within the vector
		int size()
		{
			return Size;
		}

		//Returns capacity to show how big the vector is
		int capacity()
		{
			return Capacity;
		}

		//Adds one to size, resizes vector if the new size is greater than capacity, and then adds the element to the end of the vector
		void push_back(T value)
		{
			Size++;

			if (Size > Capacity)
			{
				reserve(Size);
			}

			data[Size - 1] = value;
		}

		//Inserts a new element to the requested index
		void insert(T value, int n)
		{
			//Adds one to size
			Size++;

			//Resizes vector if the new size is greater than capacity
			if (Size > Capacity)
			{
				reserve(Size);
			}

			//Shifts every element past the requested index back
			for (int i = n; i >= Size; i--)
			{
				data[i] = data[i - 1];
			}

			//Adds the new element to the opened space
			data[n] = value;
		}

		//Resizes the vector to the newly requested size
		void resize(int n)
		{
			//Reserves more space if the size is greater than the capacity
			if (Size > Capacity)
			{
				reserve(Size);
			}

			//If the size is less than or equal to the requested size, changes the vector's size to that amount
			if (Size <= n)
			{
				Size = n;

				//Creates a new element until the vector is filled
				for (int i = 0; i < Size; i++)
				{
					new(data + i) T();
				}
			}

			//If the size is greater than the resized amount, removes every element past the new size while keeping the capacity the same
			else
			{
				for (int i = n; i < Size; i++)
				{
					data[i].~T();
				}

				Size = n;
			}
		}

		//Reserves a specified amount of memory for the vector
		void reserve(int n)
		{
			if (Capacity >= n)
			{
				//Do nothing
			}

			//If capacity is less than the requested amount
			else
			{
				//Stores the old elements in a temporary variable
				T* temp = data;

				//Opens more memory to the requested capacity
				data = reinterpret_cast<T*>(new char[(sizeof(T) * n)]);

				//Moves every element over from the temporary variable to the new block of memory
				for (int i = 0; i < n; i++)
				{
					data[i] = temp[i];
				}

				//Deletes temporary variable
				delete[] temp;
				temp = nullptr;
				Capacity = n;
			}
		}

		//Removes the specified element and shifts every element over one, reduces size by one
		void remove(int n)
		{
			Size--;
			data[n].~T();

			if (n >= Size)
			{
				return;
			}

			memcpy((data + n), (data + n + 1), (Size - n - 1));
		}

		//Deletes the old data, and makes a new vector
		void clear()
		{
			delete[] data;

			data = reinterpret_cast<T*>(new char[(sizeof(T) * 5)]);
			Size = 0;
			Capacity = 5;
		}

		//If the size is less than or equal to 0, returns true
		bool empty()
		{
			return (Size <= 0);
		}
		
		//Deletes the data pointer and sets it to null
		~Vector()
		{
			delete[] data;
			data = nullptr;
		}

	private:
		T* data;
		int Size;
		int Capacity;
	};
}